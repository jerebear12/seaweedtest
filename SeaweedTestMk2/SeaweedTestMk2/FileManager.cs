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
        int chunkSize = 10000 * 1024;
        bool isPaused = false;
        FilerUtils filer;
        CancellationTokenSource source = new CancellationTokenSource();
        CancellationToken pause;
        public FileManager(string JWT, string filer_ip_address, string storage_folder_name)
        {
            InitializeComponent();
            //ilImages.ImageSize = new Size(width:64, height:64);
            //lvImages.LargeImageList = ilImages;
            lvImages.Scrollable = true;
            token = JWT;
            filer_ip = filer_ip_address;
            folder_name = storage_folder_name;
            // Instantiate FilerUtils to use DownloadAll()
            filer = new FilerUtils(filer_ip, folder_name, token);
            pause = source.Token;
            rbFile.Checked = true;
        }

        public delegate FilerPostResp AsyncCaller(string filePath, Progress<int> progress, long offset);

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
                        txtFileInfo.Text = ofdFile.FileName;
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
        
        public async Task<Task> CallDownload()
        {
            Action download = async () =>
            {
                await filer.DownloadAll().GetAwaiter().GetResult();
                //ShowAllImages();
            };
            return Task.FromResult(Task.Run(download));
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
                // Create image list
                var files = Directory.GetFiles("files");
                ImageList ilImages = new ImageList();
                int index = 0;
                foreach (var f in files)
                {
                    // Split filename into multiple list items
                    string[] splitFullPath = f.Split(@"\");
                    // Get the file name from split path
                    string fileName = splitFullPath[splitFullPath.Length - 1];

                    // Get more file data
                    FileInfo fileData = new FileInfo(f);
                    long fileLength = fileData.Length;
                    
                    // Convert to mbs to save space
                    int totalMB;
                    double mb = 1024 * 1024;
                    // Convert to mbs to avoid Int64 type in prgress bar
                    totalMB = (int)((double)fileLength / mb);

                    string dateCreated = fileData.CreationTime.ToString("MM/dd/yyyy");
                    
                    // Init ListViewItem with fileName as text and index as image index
                    ListViewItem newLVI = new ListViewItem();
                    // Add text, image name, to item
                    newLVI.Text = "Name: " + fileName + " Size in MBs: " + totalMB.ToString() + " Date Created: " + dateCreated;
                    newLVI.Name = fileName;

                    // Add key to reference image to display in the item
                    if (ImageExtensions.Contains(Path.GetExtension(f).ToUpperInvariant()))
                    {
                        newLVI.ImageKey = "image";
                        ilImages.Images.Add(Bitmap.FromFile(f));
                        newLVI.ImageIndex = index;
                    }
                    else
                    {
                        newLVI.ImageKey = "other";
                        newLVI.ImageIndex = -1;
                    }
                    // Add to ListView
                    Action addToListView = () =>
                    {
                        lvImages.Items.Add(newLVI);
                        lvImages.Refresh();
                    };
                    this.BeginInvoke(addToListView);
                    index++;
                }
            }
            else
            {
                // Create folder
                Directory.CreateDirectory("files");
                // Try download
                Task<Task> task = CallDownload();
                task.Wait();
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
            FilerGetMetadata metadata = filer.GetFileMetadata(fileName);
            long fileOffset = 0;
            long progressBarMax = 0;
            FileInfo file = new FileInfo(filePath);
            progressBarMax = file.Length;
            // If null no file exists yet
            if (metadata != null)
            {
                // Make sure there is a filesize stored to avoid errors
                if (metadata.FileSize != null)
                {
                    // If file stored on server is smaller than the one on the device
                    if (metadata.FileSize > fileOffset)
                    {
                        // Set file offset to size stored on server
                        fileOffset = (long)metadata.FileSize;
                        // Set offset of ProgressBar to remaining bytes
                        progressBarMax = file.Length - fileOffset;
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
            if (string.IsNullOrEmpty(folder_name))
            {
                AlertUser("Add a Folder in Settings", "error");
                ClearProgressBar();
                return resp;
            }
            SeaweedUploader seaweed = new SeaweedUploader(filer_ip, folder_name, token, pause, 10);
            InitProgressBar(progressBarMax);
            AsyncCaller caller = seaweed.HandleFile;
            // Wait for file to be uploaded while allowing UI thread to update
            resp = await Task.Run(() => caller(filePath, progress, fileOffset));
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

        private void ClearProgressBar()
        {
            pbUpload.Value = 0;
            pbUpload.Maximum = 100;
            pbUpload.Visible = false;
            pbUpload.Update();
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
            btnLogOut.BringToFront();
        }

        private async void btnViewFiles_Click(object sender, EventArgs e)
        {
            pbDisplayImage.ImageLocation = null;
            pnlViewFiles.BringToFront();
            btnLogOut.BringToFront();
            _ = (Task<Task>)await CallDownload();
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
                        pbDisplayImage.ImageLocation = "files\\" + lvItem.Name;
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
                        p.StartInfo = new ProcessStartInfo("files\\" + lvItem.Name)
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
            if (isPaused == false)
            {
                isPaused = true;
                btnPause.Text = "UnPause";
                btnPause.BackColor = System.Drawing.ColorTranslator.FromHtml("#78BC98");
                source.Cancel();
                AlertUser("Paused", "error");
            }
            else
            {
                isPaused = false;
                AlertUser("UnPaused", "success");
                btnPause.Text = "Pause";
                btnPause.BackColor = System.Drawing.ColorTranslator.FromHtml("#CD5C5C");
                source.Dispose(); // Clean up old token source....
                source = new CancellationTokenSource(); // "Reset" the cancellation token source...
                pause = source.Token;
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            // Remove auto log in
            if (File.Exists("refresh.txt"))
            {
                File.Delete("refresh.txt");
            }
            formLogin newLoginForm = new formLogin();
            newLoginForm.Show();
            this.Hide();
            //this.Close();
        }
    }
}
