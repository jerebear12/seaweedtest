using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SeaweedTestMk2
{
    internal class FilerUtils
    {
        public FilerUtils(string FilerIpAddress, string FolderName, string JWToken)
        {
            filer_ip = FilerIpAddress;
            folder_name = FolderName;
            token = JWToken;
        }

        private string filer_ip { get; set; }
        private string folder_name { get; set; }
        private string token { get; set; }

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

        private FilerGetResp? GetFolderInfoFromFiler()
        {
            // POST file to filer
            // Can split folders by username here
            var client = new RestClient(filer_ip + "/" + folder_name);  // Filer IP
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
        
        public async Task<Task> DownloadAll()
        {
            Action downloadAll = () =>
            {
                if (!Directory.Exists("files"))
                {
                    Directory.CreateDirectory("files");
                }
                FilerGetResp folderFromFiler = GetFolderInfoFromFiler();
                List<string> filesToDownload = GetFilesToDownload(folderFromFiler);
                foreach (string file in filesToDownload)
                {
                    using (var client = new HttpClient())
                    {
                        // Testing adding headers to request to filer
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        try
                        {
                            string url = filer_ip + "/" + folder_name + "/" + file;
                            using (var s = client.GetStreamAsync(url))
                            {
                                using (var fs = new FileStream(path: "files/" + file, FileMode.Create))
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
            };
            return await Task.FromResult(Task.Run(downloadAll));
        }
        
        private List<string> ListSavedFiles()
        {
            List<string> savedFiles = new List<string>(Directory.GetFiles("files", "*ProfileHandler.cs", SearchOption.TopDirectoryOnly));
            return savedFiles;
        }

        private List<string> GetFilesToDownload(FilerGetResp filerDirectoryData)
        {
            // Return value
            List<string> FilesToDownload = new List<string>();
            // Folder data from filer must be populated
            if (filerDirectoryData == null)
            {
                return FilesToDownload;
            }
            // Entries must be found in the folder
            if (filerDirectoryData.Entries == null)
            {
                return FilesToDownload;
            }
            // Currently stored files
            List<string> savedFiles = ListSavedFiles();
            // Init list to hold all files that are uploaded
            List<string> uploadedFiles = new List<string>();
            foreach (Entry up in filerDirectoryData.Entries)
            {
                // Split file address by directory
                string[] splitFullPath = up.FullPath.Split(@"/");
                // Get the file name from split path
                string fileName = splitFullPath[splitFullPath.Length - 1];
                // Add file path to list if it doesn't exist on the client
                uploadedFiles.Add(fileName);
            }
            foreach (string file in uploadedFiles)
            {
                if (!File.Exists(folder_name + file))
                {
                    FilesToDownload.Add(file);
                }
            }
            return FilesToDownload;
        }
        /*
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
        */
        /*
        private List<FileNameData> CreateNeededFileList()
        {
            List<FileNameData> fileNameDatas = new List<FileNameData>();

            FilerGetResp filerMetaData = GetFolderInfoFromFiler();
            if (filerMetaData != null)
            {
                List<FileNameData> files = CreateFileLists(filerMetaData);
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
                            foreach (string fileId in files)
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

        // Make the entry loop recursive in the future
        private List<FileNameData> CreateFileLists(FilerGetResp responseInfo)
        {
            //List<string> fileNames = new List<string>();
            //List<string> currentFiles = ListSavedFiles();
            List<FileNameData> uploadedFiles = new List<FileNameData>();
            //var allExceptions = fileNames.Except(currentFiles);
            // Loop through entries
            if (responseInfo.Entries == null)
            {
                return uploadedFiles;
            }
            foreach (Entry entry in responseInfo.Entries)
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
                    //fileNames.Add(chunk.file_id);
                    // Add currentFile obj to list for later use
                    uploadedFiles.Add(currentFile);
                }
            }
            // Return file names that aren't already downloaded - the exceptions
            //Debug.WriteLine(fileNames);
            //Debug.WriteLine(allExceptions);
            return uploadedFiles;
        }
        */
    }
}
