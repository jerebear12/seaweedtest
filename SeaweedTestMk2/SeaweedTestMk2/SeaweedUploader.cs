using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaweedTestMk2
{
    internal class SeaweedUploader
    {
        public SeaweedUploader(string FilerIpAddress, string FolderName, string JWToken, CancellationToken CancellationToken, int chunkSizeInMbs)
        {
            filer_ip = FilerIpAddress;
            folder_name = FolderName;
            token = JWToken;
            cancellationToken = CancellationToken;
            maxMB = chunkSizeInMbs;
        }

        private string filer_ip { get; set; }
        private string folder_name { get; set; }
        private string token { get; set; }
        private CancellationToken cancellationToken { get; set; }
        private int maxMB { get; set; }

        private int ProgressBarTick(int amount)
        {
            int amountMb;
            double mb = 1024 * 1024;
            // Convert to mbs to avoid Int64 type in prgress bar
            amountMb = (int)((double)amount / mb);
            return amountMb;
        }


        // TODO
        // Instead of AsyncAlert function:
        // raise FileNotRead
        // raise FileUploadFailed
        public FilerPostResp HandleFile(string filePath, IProgress<int> progress, long fileOffset)
        {
            // Response variable
            FilerPostResp response = new FilerPostResp();
            // Task alert for async alerts
            Task alert;
            // Split path by directory
            string[] directorySplit = filePath.Split(@"\");
            // Get file name from path
            string fileName = directorySplit[directorySplit.Length - 1];
            // Create new file info
            FileInfo fileInfo = new FileInfo(filePath);
            // If file is too small to be chunked
            if (fileInfo.Length < ((maxMB * 2) * (1024 * 1024))) // Converts from mbs to byte size and multiplies by 2 
            {
                // Upload it
                response = UploadFile(filePath, fileName);
                return response;  // Upload is done
            }
            // Prep for chunked upload
            // Create chunk size for file split
            int chunkSize = 1024 * 1024 * maxMB; 
            // Not sure how useful this is
            int BUFFER_SIZE = 1024 * 1024 * maxMB;  // 4mb in byte form
            byte[] buffer = new byte[BUFFER_SIZE];
            // Offset
            long offset = fileOffset;  // Current bytes read - location to read from
            // Number of chunk bring read
            int chunkNum = 1;
            using (Stream input = File.OpenRead(filePath))
            {
                // If file is fully uploaded already
                if (offset == input.Length)
                {
                    response.name = "uploaded";
                    return response;
                }
                long bytesRemaining = (long)input.Length - offset;  // offset is subtracted to account for partial uploads
                input.Position = input.Length - bytesRemaining;
                int readError = 0;
                // Old buffer is used to see if the new or current buffer is filled with new bytes each loop
                byte[] oldBuffer = new byte[BUFFER_SIZE];
                // Make sure cancelation has not been requested by pause button
                while (cancellationToken.IsCancellationRequested != true)
                {
                    if (bytesRemaining > 0)  // Will read until file is completely read
                    {
                        int size = (int)Math.Min(bytesRemaining, (long)chunkSize);
                        buffer = new byte[size];
                        long bytesRead;
                        try
                        {
                            bytesRead = input.Read(buffer, 0, size);  // Store bytes in buffer, start at offset of index, read full chunksize worth of bytes minus what is already read
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                            bytesRead = 0;
                            readError++;
                            if (readError >= 3)
                            {
                                Debug.WriteLine("Error reading from stream");
                                //Task f = AsyncAlert("Error reading file, try again", "error");
                                break;
                            }
                        }
                        if (bytesRead == 0)  // Error - Read() returns 0 if nothing is read
                        {
                            break;
                        }
                        offset += bytesRead;
                        bytesRemaining -= bytesRead;
                    }
                    // If offset is not 0, there is no error, upload chunk
                    if (offset != 0) // Our previous chunk may have been the last one
                    {
                        if (buffer == oldBuffer)
                        {
                            // No new data was read from stream
                            continue;
                        }
                        // Set old buffer to current buffer for next iteration
                        oldBuffer = buffer;
                        int retryCount = 0;
                        while (retryCount < 6)  // 5 retries
                        {
                            response = UploadChunk(buffer, fileName);
                            if (response.name == null)
                            {
                                Debug.WriteLine("Could not upload file");
                                break;
                            }
                            if (response.name != null)
                            {
                                if (progress != null)
                                {
                                    int amount = ProgressBarTick(buffer.Length);
                                    progress.Report(amount);
                                }
                                break;  // Exit if response is good
                            }
                            else
                            {
                                //alert = AsyncAlert("Failed, retry: " + retryCount, "error");
                                retryCount++;
                            }
                        }
                        if (response.name == null)  // If retries fail
                        {
                            Debug.WriteLine("Chunk Upload Failed");
                            Debug.WriteLine("Chunk: ", chunkNum + 1);
                            // Alert user
                            //alert = AsyncAlert("Upload Failed, please check your network connection and try again.", "error");
                            break;  // Exit loop to stop uploading chunks
                        }
                        chunkNum++;
                    }
                    // When whole file is read, exit
                    if (bytesRemaining == 0)
                    {
                        //Success
                        break;
                    }
                }
                if (cancellationToken.IsCancellationRequested == true)
                {
                    response.name = "paused";
                }
                return response;
            }
        }

        public FilerPostResp UploadFile(string filePath, string fileName)
        {
            // Split filepath by directory
            string[] directorySplit = filePath.Split(Path.DirectorySeparatorChar);
            // Prep FilerPostResp
            FilerPostResp responseObj = null;
            try
            {
                // POST file to filer
                // Can split folders by username here
                // Folder name is not required to be populated by the user as of now
                var client = new RestClient(filer_ip + "/" + folder_name + "?maxMB=" + maxMB.ToString());  // Filer IP 
                var request = new RestRequest();
                // Prep headers for filer
                request.AddHeader("Content-Type", "multipart/form-data");
                // Send authorization
                request.AddHeader("Authorization", "Bearer " + token);
                // Set name of file to last item in list which == file name
                // Current implementation
                request.AddFile(directorySplit[directorySplit.Length - 1], filePath);
                // Get response from server
                var response = client.Post(request);
                // Response to string
                var content = response.Content;
                if (content != null)
                {
                    JObject jResponse = JObject.Parse(content);
                    // If json response contained a name key from filer, the POST was good
                    if (jResponse.ContainsKey("name"))
                    {
                        responseObj = JsonConvert.DeserializeObject<FilerPostResp>(content);
                        //AlertUser("Upload Successful", "success");
                    }
                    else
                    {
                        // Else #1
                        // Verify Content
                        Debug.WriteLine("Response Content from else #1:");
                        Debug.WriteLine(content);
                    }
                }
                else
                {
                    // Else #2
                    // Verify Content
                    Debug.WriteLine("Response Content from else #2:");
                    Debug.WriteLine(content);
                }
            }
            catch (IOException io)
            {
                Debug.WriteLine("IOException: " + io.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Execption: " + ex.ToString());
            }
            return responseObj;
        }

        public Func<Stream> GetStream(byte[] data)
        {
            Stream openStream() => new MemoryStream(data);
            return openStream;
        }

        public FilerPostResp UploadChunk(byte[] data, string fileName)
        {
            // Prep FilerPostResp
            FilerPostResp responseObj = null;
            try
            {
                // POST file to filer
                // Can split folders by username here
                var client = new RestClient(filer_ip + "/" + folder_name + "?op=append&maxMB=" + maxMB.ToString());  // tag "op"(operation) = append
                var request = new RestRequest();
                // Prep headers for filer
                request.AddHeader("Content-Type", "multipart/form-data");
                // Send authorization
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddFile(name: fileName, getFile: GetStream(data), fileName: fileName);
                // Get response from server
                var response = client.Post(request);
                // Response to string
                var content = response.Content;
                if (content != null)
                {
                    JObject jResponse = JObject.Parse(content);
                    // If json response contained a name key from filer, the POST was good
                    if (jResponse.ContainsKey("name"))
                    {
                        responseObj = JsonConvert.DeserializeObject<FilerPostResp>(content);
                    }
                    else
                    {
                        // Else #1
                        // Verify Content
                        Debug.WriteLine("Response Content from else #1:");
                        Debug.WriteLine(content);
                        // responseObj will return null
                    }
                }
                else
                {
                    // Else #2
                    // Verify Content
                    Debug.WriteLine("Response Content from else #2:");
                    Debug.WriteLine(content);
                    // responseObj will return null
                }
            }
            catch (IOException io)
            {
                Debug.WriteLine("IOException: " + io.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Execption: " + ex.ToString());
            }
            return responseObj;
        }
    }
}
