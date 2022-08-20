using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using static System.Windows.Forms.ListView;
using System.Net.Http.Headers;

namespace SeaweedTestMk2
{
    public partial class FileManager : Form
    {
        string token;
        string filer_ip;
        string folder_name;
        string filePath = "";
        List<FileNameData> uploadedFiles = new List<FileNameData>();
        int chunkSize = 10000 * 1024;
        bool isPaused = false;
        public FileManager(string JWT, string filer_ip_address, string storage_folder_name)
        {
            InitializeComponent();
            //ilImages.ImageSize = new Size(width:64, height:64);
            //lvImages.LargeImageList = ilImages;
            token = JWT;
            filer_ip = filer_ip_address;
            folder_name = storage_folder_name;
        }

        public delegate FilerPostResp AsyncCaller(string filePath, Progress<int> progress);

        private async void btnChoose_Click(object sender, EventArgs e)
        {
            // File
            if (rbFile.Checked)
            {
                if (ofdFile.ShowDialog() == DialogResult.OK)
                {
                    //Debug.WriteLine(ofdFile.FileName);
                    if (File.Exists(ofdFile.FileName))
                    {
                        if (isPaused == true)
                        {
                            AlertUser("Uploads are Paused", "error");
                            return;
                        }
                        FilerPostResp resp = await CallHandleFile(ofdFile.FileName);
                        if (resp != null)
                        {

                        }
                    }
                    else
                    {
                        AlertUser("Failed to find file", "error");
                        return;
                    }
                }
            }
            // Folder
            if (rbFolder.Checked)
            {
                if (fbdFolder.ShowDialog() == DialogResult.OK)
                {
                    if (Directory.Exists(fbdFolder.SelectedPath))
                    {
                        string folderName = fbdFolder.SelectedPath;
                        //txtFileInfo.Text = folderName;
                        // Get Folder info
                        DirectoryInfo dirInfo = new DirectoryInfo(folderName);
                        DeepUpload(dirInfo, token);
                        //txtFileInfo.Text = "";
                        // Alert User
                        AlertUser("Upload Successful", "success");
                    }
                    else
                    {
                        AlertUser("Failed to find directory", "error");
                    }
                }
            }
        }

        private void AlertUser(string msg, string type)
        {
            if (type == "error")
            {
                pnlError.BringToFront();
                lblError.Text = "";
                lblError.Text = msg;
            }
            if (type == "success")
            {
                pnlSuccess.BringToFront();
                lblSuccess.Text = "";
                lblSuccess.Text = msg;
            }
        }

        private async Task AsyncAlert(string msg, string type)
        {
            Action alert = () =>
            {
                AlertUser(msg, type);
            };
            Action invoke = () =>
            {
                this.BeginInvoke(alert);
            };
            await Task.FromResult(Task.Run(invoke));
        }

        public async void DeepUpload(DirectoryInfo dirInfo, string token)
        {
            // Loop through all folders and call function again to recursively upload all files
            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
                DeepUpload(dir, token);
            }
            // Loop through files
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                FilerPostResp resp = await CallHandleFile(file.FullName);
            }
        }

        private static List<string> ListSavedFiles()
        {
            List<string> savedFiles = new List<string>(Directory.GetFiles("files", "*ProfileHandler.cs", SearchOption.TopDirectoryOnly));
            return savedFiles;
        }

        private FilerGetResp? GetFolderInfoFromFiler()
        {
            // POST file to filer
            // Can split folders by username here
            var client = new RestClient(filer_ip + "/test_user");  // Filer IP
            var request = new RestRequest();
            // Prep headers for filer
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + token);
            // Get response from server
            // Init response obj in outer scope
            RestResponse response;
            try
            {
                // Send GET request
                response = client.Get(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                // Abort func if error
                return null;
            }
            // Response to string
            var content = response.Content;
            // Resonse to JSON
            if (content != null)
            {
                // From string to obj representing json
                FilerGetResp responseInfo = JsonConvert.DeserializeObject<FilerGetResp>(content);
                return responseInfo;
            }
            else
            {
                Debug.WriteLine("No response content found");
                return null;
            }
        }

        private IEnumerable<string> CreateFileLists(FilerGetResp responseInfo)
        {
            List<string> fileNames = new List<string>();
            List<string> currentFiles = ListSavedFiles();
            var allExceptions = fileNames.Except(currentFiles);
            // Loop through entries
            if (responseInfo.Entries == null)
            {
                return allExceptions;
            }
            foreach(Entry entry in responseInfo.Entries)
            {
                // Create new FileNameData obj
                FileNameData currentFile = new FileNameData();
                // Store filename in obj
                // Split file path by / and store in list
                string[] splitFullPath = entry.FullPath.Split(@"/");
                // Get the file name from split path
                currentFile.FileName = splitFullPath[splitFullPath.Length - 1];
                // Loop through chunks in each entry
                foreach (Chunk chunk in entry.chunks)
                {
                    // Store file_id in FileNameData obj
                    currentFile.FileId = chunk.file_id;
                    // For each chunk add file_id to fileNames list
                    fileNames.Add(chunk.file_id);
                    // Add currentFile obj to list for later use
                    uploadedFiles.Add(currentFile);
                }
            }
            // Return file names that aren't already downloaded - the exceptions
            Debug.WriteLine(fileNames);
            Debug.WriteLine(allExceptions);
            return allExceptions;
        }

        private List<FileNameData> CreateNeededFileList()
        {
            List<FileNameData> fileNameDatas = new List<FileNameData>();

            FilerGetResp filerMetaData = GetFolderInfoFromFiler();
            if (filerMetaData != null)
            {
                IEnumerable<string> fileIds = CreateFileLists(filerMetaData);
                // Compare uploaded files with GetFileNames
                // For each obj containing fileid and filename in list
                try  // uploadedFiles may break because of modifications during access
                {
                    foreach (FileNameData file in uploadedFiles)
                    {
                        // Skip adding file if it exists/is downloaded
                        if (!File.Exists("files\\" + file.FileName))
                        {
                            // Loop through fileid in list of file ids
                            foreach (string fileId in fileIds)
                            {
                                // If fileId matches the current FileID in the FileNameData obj
                                if (file.FileId == fileId)
                                {
                                    // Add obj to list
                                    // This file is now marked as needing to be downloaded
                                    fileNameDatas.Add(file);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            else
            {
                Debug.WriteLine("No filerMetaData found");
            }
            return fileNameDatas;
        }

        public async Task<Task> CallDownload()
        {
            Action download = async () =>
            {
                await DownloadAll().GetAwaiter().GetResult();
                //ShowAllImages();
            };
            return Task.FromResult(Task.Run(download));
        }

        // Changed volume impl to filer
        private async Task<Task> DownloadAll()
        {
            return Task.FromResult(Task.Run(() =>
            {
                if (!Directory.Exists("files"))
                {
                    Directory.CreateDirectory("files");
                }
                FilerGetResp filerMetaData = GetFolderInfoFromFiler();
                if (filerMetaData != null)
                {
                    // List of files
                    List<FileNameData> fileDataObjs = CreateNeededFileList();
                    if (fileDataObjs.Count > 0)
                    {
                        // Loop through file objects
                        foreach (FileNameData file in fileDataObjs)
                        {
                            // Abort if file is already downloaded
                            // Mildly redundant
                            // DownloadAll() already has this check
                            if (!File.Exists("files\\" + file.FileName))
                            {
                                using (var client = new HttpClient())
                                {
                                    // Testing adding headers to request to filer
                                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                                    try
                                    {
                                        string url;
                                        // If extension is .json, file is a manifest file
                                        if (Path.GetExtension(file.FileName) == ".json")
                                        {
                                            // Add resolveManifest parameter to get all file chunks
                                            url = filer_ip + "/" + folder_name + "/" + file.FileName + "?resolveManifest=true";
                                        }
                                        else
                                        {
                                            // Use default
                                            url = filer_ip + "/" + folder_name + "/" + file.FileName;
                                        }
                                        using (var s = client.GetStreamAsync(url))  // Filer IP 
                                        {
                                            using (var fs = new FileStream(path: "files/" + file.FileName, FileMode.Create))
                                            {
                                                s.Result.CopyTo(fs);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Debug.WriteLine("Error downloading file: ");
                                        Debug.WriteLine(ex.Message);
                                    }
                                }
                            }
                            else
                            {
                                Debug.WriteLine("File downloaded in DownloadAll if statement");
                            }
                        }
                    }
                    else
                    {
                        // Display info
                        // Not technically an error
                        Debug.WriteLine("No fileIds from GetFileNames");
                    }
                }
                else
                {
                    // Display error
                    Debug.WriteLine("No filerMetaData from GetFileData");
                }
            }));
        }

        private void ShowAllImages()
        {
            // For async calls
            Action clearListView = () =>
            {
                lvImages.Items.Clear();
            };
            this.BeginInvoke(clearListView);
            List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG" };
            if (Directory.Exists("files"))
            {
                var files = Directory.GetFiles("files");
                foreach (var f in files)
                {
                    // Init ListViewItem
                    ListViewItem newLVI = new ListViewItem();
                    // Split filename into multiple list items
                    string[] splitFullPath = f.Split(@"\");
                    // Get the file name from split path
                    string fileName = splitFullPath[splitFullPath.Length - 1];
                    // Add text, image name, to item
                    newLVI.Text = fileName;
                    // Add key to reference image to display in the item
                    if (ImageExtensions.Contains(Path.GetExtension(f).ToUpperInvariant()))
                    {
                        
                        newLVI.ImageKey = "image";
                    }
                    else
                    {
                        newLVI.ImageKey = "other";
                    }
                    // Add to ListView
                    Action addToListView = () =>
                    {
                        lvImages.Items.Add(newLVI);
                    };
                    this.BeginInvoke(addToListView);
                }
            }
            else
            {
                // Create folder
                Directory.CreateDirectory("files");
                // Try download
                CallDownload();
                // Try this function again
                //ShowAllImages();
                btnViewFiles.PerformClick();
            }
        }
        public async Task<FilerPostResp> CallHandleFile(string filePath)
        {
            // Init return variable
            FilerPostResp resp = null;
            // Init progress variable to store prgress data and handle callbacks
            var progress = new Progress<int>(percent =>
            {
                pbUpload.Value = pbUpload.Value + percent;
            });
            // Split filepath by directory
            string[] directorySplit = filePath.Split(@"\");
            // Get file name from path
            string fileName = directorySplit[directorySplit.Length - 1];
            // Get file info from Filer
            FilerGetMetadata metadata = GetFileMetadata(fileName);
            long fileOffset = 0;
            FileInfo file = new FileInfo(filePath);
            fileOffset = file.Length;
            // If null no file exists yet
            if (metadata != null)
            {
                // Make sure there is a filesize stored to avoid errors
                if (metadata.FileSize != null)
                {
                    // If file stored on server is smaller than the one on the device
                    if (metadata.FileSize > fileOffset)
                    {
                        // Set offset of ProgressBar to server size
                        fileOffset = (long)metadata.FileSize;
                        AlertUser("Resuming upload", "success");
                    }
                    // Else file is fully uploaded 
                    else
                    {
                        AlertUser("File is Already Uploaded", "error");
                        ClearProgressBar();
                        return resp;
                    }
                }
            }
            InitProgressBar(fileOffset);
            AsyncCaller caller = HandleFile;
            // Wait for file to be uploaded while allowing UI thread to update
            resp = await Task.Run(() => caller(filePath, progress));
            // Handle response from method
            if (resp.name == null)
            {
                AlertUser("Failed to upload file", "error");
                Debug.WriteLine("Failed to upload file:");
                Debug.WriteLine(resp);
            }
            if (resp.name == "paused")
            {
                AlertUser("Uploads are Paused", "error");
                ClearProgressBar();
                return resp;
            }
            if (resp.name == "uploaded")
            {
                AlertUser("File is Already Uploaded", "error");
                ClearProgressBar();
                return resp;
            }
            if (resp.name != null)
            {
                txtFileInfo.Text = "";
                AlertUser("Upload Successful", "success");
            }
            // Clear progress bar no matter what
            ClearProgressBar();
            return resp;
        }
        private void InitProgressBar(long total)
        {
            int totalMB;
            double mb = 1024 * 1024;
            // Convert to mbs to avoid Int64 type in prgress bar
            totalMB = (int)((double)total / mb);
            pbUpload.Maximum = totalMB + 1;
            pbUpload.Visible = true;
            pbUpload.Update();
        }

        private int ProgressBarTick(int amount)
        {
            int amountMb;
            double mb = 1024 * 1024;
            // Convert to mbs to avoid Int64 type in prgress bar
            amountMb = (int)((double)amount / mb);
            return amountMb;
        }

        private void ClearProgressBar()
        {
            pbUpload.Value = 0;
            pbUpload.Maximum = 100;
            pbUpload.Visible = false;
            pbUpload.Update();
        }

        public FilerPostResp HandleFile(string filePath, IProgress<int> progress)
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
            if (fileInfo.Length < 20000000) // 20mb
            {
                // Upload it
                response = UploadFile(filePath, fileName);
                return response;  // Upload is done
            }
            // Prep for chunked upload
            // Create chunk size for file split
            int chunkSize = 1024 * 1024 * 10;  // 10mb
            const int BUFFER_SIZE = 1024 * 1024 * 10;  // 10mb in byte form
            byte[] buffer = new byte[BUFFER_SIZE];
            // Offset
            long offset = 0;  // Current bytes read - location to read from
            // Number of chunk bring read
            int chunkNum = 1;
            // See if file exists
            FilerGetMetadata metadata = GetFileMetadata(fileName);
            if (metadata != null)
            {
                if (metadata.FileSize > 0)
                {
                    offset += (long)metadata.FileSize;
                }
            }
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
                byte[] oldBuffer = new byte[BUFFER_SIZE];
                // Make sure cancelation has not been requested by pause button
                while (isPaused != true)
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
                                Task f = AsyncAlert("Error sending file, try again", "error");
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
                            if (response.name != null)
                            {
                                Task g = AsyncAlert("Successful chunk: " + chunkNum, "success");
                                if (progress != null)
                                {
                                    int amount = ProgressBarTick(buffer.Length);
                                    progress.Report(amount);
                                }
                                break;  // Exit if response is good
                            }
                            else
                            {
                                alert = AsyncAlert("Failed, retry: " + retryCount, "error");
                                retryCount++;
                            }
                        }
                        if (response.name == null)  // If retries fail
                        {
                            Debug.WriteLine("Chunk Upload Failed");
                            Debug.WriteLine("Chunk: ", chunkNum + 1);
                            // Alert user
                            alert = AsyncAlert("Upload Failed, please check your network connection and try again.", "error");
                            break;  // Exit loop to stop uploading chunks
                        }
                        chunkNum++;
                    }
                    // When whole file is read, exit
                    if (bytesRemaining == 0)
                    {
                        //alert = AsyncAlert("Full chunk uploaded successfully", "success");
                        break;
                    }
                }
                if (isPaused == true)
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
                var client = new RestClient(filer_ip + "/" + folder_name + "?maxMB=10");  // Filer IP 
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
                        AlertUser("Upload Successful", "success");
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
                var client = new RestClient(filer_ip + "/" + folder_name + "?op=append&maxMB=10");  // tag "op"(operation) = append
                var request = new RestRequest();
                // Prep headers for filer
                request.AddHeader("Content-Type", "multipart/form-data");
                // Send authorization
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddFile(name:fileName, getFile: GetStream(data), fileName:fileName);
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

        public FilerGetMetadata GetFileMetadata(string fileName)
        {
            
            // Link to file requesting metadata
            var client = new RestClient(filer_ip + "/" + folder_name + "/" + fileName + "?metadata=true&pretty=yes");  // Filer IP
            var request = new RestRequest();
            // Prep headers for filer
            request.AddHeader("Authorization", "Bearer " + token);
            // Get response from server
            // Init response obj in outer scope
            RestResponse response;
            try
            {
                // Send GET request
                response = client.Get(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                // Abort func if error
                return null;
            }
            // Response to string
            var content = response.Content;
            // Resonse to JSON
            if (content == null)
            {
                Debug.WriteLine("No response content found");
                return null;
                
            }
            // From string to obj representing json
            FilerGetMetadata responseInfo = JsonConvert.DeserializeObject<FilerGetMetadata>(content);
            return responseInfo;
        }

        // For old HandleFile with chunk concat
        private List<ChunkInfo> ParseMetadata(FilerGetMetadata metaData, int index)
        {
            // Same chunkSize as HandleFile()
            // Offset will change to be a floor of past full chunks
            // If index is 1 then 1 full chunk has been written
            // If one full chunk has been written, then the offset should start at 10.24mb - the size of a full chunk
            int offset = index * chunkSize;
            List<ChunkInfo> chunks = new List<ChunkInfo>();
            foreach (Chunk chunk in metaData.chunks)
            {
                ChunkInfo chunkInfo = new ChunkInfo();
                chunkInfo.fid = chunk.file_id;
                // Starts at 0, then gets changed to increment chunk sizes
                chunkInfo.offset = offset;
                chunkInfo.size = chunk.size;
                // Add chunk to list
                chunks.Add(chunkInfo);
                // Start next chunks offset where this chunk ends
                offset = offset + (int)chunk.size;
            }
            return chunks;
        }

        private void pnlUploadFiles_Click(object sender, EventArgs e)
        {
            pnlError.SendToBack();
            lblError.Text = "";
            pnlSuccess.SendToBack();
            lblSuccess.Text = "";
        }

        private void btnUploadFiles_Click(object sender, EventArgs e)
        {
            pnlUploadFiles.BringToFront();
        }

        private async void btnViewFiles_Click(object sender, EventArgs e)
        {
            pbDisplayImage.ImageLocation = null;
            pnlViewFiles.BringToFront();
            await CallDownload().Result;
            ShowAllImages();
            lvImages.Refresh();
        }

        private void lvImages_Click(object sender, EventArgs e)
        {
            // Navigate to the list view item 
            foreach (ListViewItem lvItem in lvImages.Items)
            {
                // Find selected ListViewItem
                // Multiselect is turned off in designer
                if (lvItem.Selected)
                {
                    if (lvItem.ImageKey == "image")
                    {
                        //Debug.WriteLine(lvItem.Text);
                        pnlPicture.BringToFront();
                        pbDisplayImage.ImageLocation = "files\\" + lvItem.Text;
                        Size size = new Size(320, 220);
                        pbDisplayImage.Size = size;
                        try
                        {
                            pbDisplayImage.Load();
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                        }
                        pbDisplayImage.Refresh();
                    }
                    else
                    {
                        //Process.Start("files\\" + lvItem.Text);
                        var p = new Process();
                        p.StartInfo = new ProcessStartInfo("files\\" + lvItem.Text)
                        {
                            UseShellExecute = true
                        };
                        try
                        {
                            p.Start();
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Could not open file with default application:");
                            Debug.WriteLine(ex.Message);
                        }
                        
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            pnlPicture.SendToBack();
            pbDisplayImage.ImageLocation = null;
            pbDisplayImage.Refresh();
            //pbDisplayImage.Dispose();  // Breaks images
        }

        private void FileManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Close everything
            Application.Exit();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                AlertUser("Paused", "success");
            }
            else
            {
                AlertUser("UnPaused", "success");
            }
        }
    }
}
