using System.IO.Compression;

namespace BackupWizard
{
    public partial class BackupWizard : Form
    {
        private DateTime _now = DateTime.Now;
        private string _dateStamp;
        private string _timestamp;

        public BackupWizard()
        {
            InitializeComponent();
            _dateStamp = _now.ToString("dd.MM.yyyy");
            _timestamp = _now.ToString("HH.mm.ss");
        }

        private TextBox? GetTextBox(string name)
        {
            return this.Controls.Find(name, true).FirstOrDefault() as TextBox;
        }


        private string ReadSourcePathFromFile()
        {
            string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string dataFilePath = Path.Combine(exeDirectory, "datasource.dat");

            if (File.Exists(dataFilePath))
            {
                try
                {
                    string fullPathFromFile = File.ReadAllText(dataFilePath).Trim();

                    if (!string.IsNullOrEmpty(fullPathFromFile))
                    {
                        string trimmedPath = fullPathFromFile.TrimEnd(Path.DirectorySeparatorChar);
                        string parentPath = Path.GetDirectoryName(trimmedPath);

                        if (Directory.Exists(parentPath))
                        {
                            return parentPath;
                        }
                        else
                        {
                            MessageBox.Show($"The calculated backup source directory does not exist or is inaccessible:\n{parentPath}",
                                            "Invalid Source Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not read or access 'datasource': {ex.Message}",
                                    "File Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return string.Empty;
        }


        private void BackupWizard_Load(object sender, EventArgs e)
        {
            TextBox? locTextBox = GetTextBox("locTextBox");
            if (locTextBox != null)
            {
                locTextBox.Text = "C:\\BackupWizard";
            }

            TextBox? nameTextBox = GetTextBox("nameTextBox");
            if (nameTextBox != null)
            {
                nameTextBox.Text = $"BackupWizard_{_dateStamp}-{_timestamp}";

            }

            string preloadedSourcePath = ReadSourcePathFromFile();
            TextBox? sourceTextBox = GetTextBox("sourceTextBox");

            if (sourceTextBox != null && !string.IsNullOrEmpty(preloadedSourcePath))
            {
                sourceTextBox.Text = preloadedSourcePath;
            }
        }

        private void LogSkippedFile(string zipFileLocation, string fileName, string errorMessage)
        {
            try
            {
                string logFilePath = zipFileLocation.Replace(".zip", "_Skipped_Files_Log.txt");

                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - SKIPPED: {fileName}\n";
                logEntry += $"    Reason: {errorMessage}\n";

                File.AppendAllText(logFilePath, logEntry);
            }
            catch
            {
            }
        }

        private void DirectoryCopy(string sourceDir, string destinationDir, string fullZipFileLocation)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDir);

            if (!Directory.Exists(destinationDir))
            {
                Directory.CreateDirectory(destinationDir);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destinationDir, file.Name);

                const int MaxRetries = 20;
                const int DelayMs = 200;
                bool success = false;
                Exception lastException = null;

                for (int attempt = 1; attempt <= MaxRetries; attempt++)
                {
                    try
                    {
                        file.CopyTo(tempPath, true);
                        success = true;
                        break;
                    }
                    catch (IOException ex) when (ex.Message.Contains("being used by another process") ||
                                                 ex.Message.Contains("access to the path"))
                    {
                        lastException = ex;
                        if (attempt < MaxRetries)
                        {
                            System.Threading.Thread.Sleep(DelayMs);
                        }
                    }
                    catch (Exception ex)
                    {
                        lastException = ex;
                        break;
                    }
                }

                if (!success)
                {
                    string reason = (lastException != null) ? lastException.Message : "Unknown Error.";
                    LogSkippedFile(fullZipFileLocation, file.FullName, $"Copy Failed after {MaxRetries} attempts: {reason}");
                }
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo subdir in dirs)
            {
                string tempPath = Path.Combine(destinationDir, subdir.Name);
                DirectoryCopy(subdir.FullName, tempPath, fullZipFileLocation);
            }
        }

        private void DirectoryDelete(string targetDir)
        {
            try
            {
                if (Directory.Exists(targetDir))
                {
                    Directory.Delete(targetDir, true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Could not delete temporary directory {targetDir}. Error: {ex.Message}");
            }
        }

        private void CreateZipWithProgress(
            string sourceFolder,
            string destinationZipFile,
            IProgress<ZipProgress> progressReporter)
        {
            string[] files = Directory.GetFiles(sourceFolder, "*.*", SearchOption.AllDirectories);
            int totalFiles = files.Length;


            string rootFolderName = new DirectoryInfo(sourceFolder).Name;

            if (!sourceFolder.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                sourceFolder += Path.DirectorySeparatorChar;
            }

            using (FileStream zipStream = new FileStream(destinationZipFile, FileMode.Create))
            using (ZipArchive zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                for (int i = 0; i < totalFiles; i++)
                {
                    string sourceFile = files[i];

                    string relativePath = sourceFile.Substring(sourceFolder.Length);
                    string entryName = Path.Combine(rootFolderName, relativePath).Replace(Path.DirectorySeparatorChar, '/');

                    if (progressReporter != null)
                    {
                        progressReporter.Report(new ZipProgress
                        {
                            Percentage = (int)(((double)(i + 1) / totalFiles) * 100),
                            CurrentFile = Path.GetFileName(sourceFile)
                        });
                    }

                    try
                    {
                        zipArchive.CreateEntryFromFile(sourceFile, entryName);
                    }
                    catch (Exception ex)
                    {
                        LogSkippedFile(destinationZipFile, sourceFile, $"ZIP Failed: {ex.Message}");
                    }
                }
            }
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            TextBox? locTextBox = GetTextBox("locTextBox");

            if (locTextBox != null && Directory.Exists(locTextBox.Text))
            {
                fbd.SelectedPath = locTextBox.Text;
            }

            if (fbd.ShowDialog() == DialogResult.OK && locTextBox != null)
            {
                locTextBox.Text = fbd.SelectedPath;
            }
        }

        private void sourceBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            TextBox? sourceTextBox = GetTextBox("sourceTextBox");

            if (sourceTextBox != null && Directory.Exists(sourceTextBox.Text))
            {
                fbd.SelectedPath = sourceTextBox.Text;
            }

            if (fbd.ShowDialog() == DialogResult.OK && sourceTextBox != null)
            {
                sourceTextBox.Text = fbd.SelectedPath;
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to exit the backup wizard?",
                "Confirm Cancellation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {

                Application.Exit();
            }
        }

        private async void saveBtn_Click(object sender, EventArgs e)
        {
            TextBox? locTextBox = GetTextBox("locTextBox");
            TextBox? nameTextBox = GetTextBox("nameTextBox");
            TextBox? sourceTextBox = GetTextBox("sourceTextBox");

            if (locTextBox == null || nameTextBox == null || sourceTextBox == null) return;

            string backupPath = locTextBox.Text.Trim();
            string backupName = nameTextBox.Text.Trim();
            string sourcePath = sourceTextBox.Text.Trim();

            string trimmedSourcePath = sourcePath.TrimEnd(Path.DirectorySeparatorChar);


            if (string.IsNullOrEmpty(trimmedSourcePath) || string.IsNullOrEmpty(backupPath) || string.IsNullOrEmpty(backupName))
            {
                if (string.IsNullOrEmpty(trimmedSourcePath)) { MessageBox.Show("Please choose which folder to backup.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning); sourceTextBox.Focus(); }
                else if (string.IsNullOrEmpty(backupPath)) { MessageBox.Show("Please choose a location to save your backup files.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning); locTextBox.Focus(); }
                else { MessageBox.Show("Please type a name for the backup.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning); nameTextBox.Focus(); }
                return;
            }

            try
            {
                if (!Directory.Exists(backupPath)) { Directory.CreateDirectory(backupPath); }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not create the destination directory:\n{ex.Message}", "Directory Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Directory.Exists(trimmedSourcePath))
            {
                MessageBox.Show("The specified source folder does not exist or is inaccessible.", "Invalid Source", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            string fullZipFileLocation = Path.Combine(backupPath, backupName + ".zip");

            string sourceFolderName = Path.GetFileName(trimmedSourcePath);
            string tempFolderName = sourceFolderName + "-" + _dateStamp + "-" + _timestamp + "-bak";

            string sourceDirParent = Path.GetDirectoryName(trimmedSourcePath) ?? Path.GetPathRoot(sourcePath);

            string tempCopyPath = Path.Combine(sourceDirParent, tempFolderName);


            string logFilePath = fullZipFileLocation.Replace(".zip", "_Skipped_Files_Log.txt");
            if (File.Exists(logFilePath))
            {
                File.Delete(logFilePath);
            }


            DialogResult result = MessageBox.Show(
                $"Your backup will be saved to:\n{fullZipFileLocation}\n\nDo you want to proceed?",
                "Confirm Backup Destination",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                saveBtn.Enabled = false;
                cancelBtn.Enabled = false;

                using (LoadingScreen progressDialog = new LoadingScreen())
                {
                    var progressHandler = new Progress<ZipProgress>(progress =>
                    {
                        if (progressDialog.IsHandleCreated)
                        {
                            progressDialog.progressBar.Value = progress.Percentage;
                            progressDialog.label1.Text = $"Zipping: {progress.CurrentFile} ({progress.Percentage}%)";
                        }
                    });

                    progressDialog.Show();

                    try
                    {
                        await Task.Run(() =>
                        {
                            DirectoryDelete(tempCopyPath);

                            DirectoryCopy(trimmedSourcePath, tempCopyPath, fullZipFileLocation);

                            if (File.Exists(fullZipFileLocation))
                            {
                                File.Delete(fullZipFileLocation);
                            }

                            CreateZipWithProgress(tempCopyPath, fullZipFileLocation, progressHandler);

                        });

                        progressDialog.Close();

                        DirectoryDelete(tempCopyPath);

                        if (File.Exists(logFilePath))
                        {
                            MessageBox.Show($"Backup completed with WARNINGS. One or more files were skipped during the copy process (likely locked by VFP).\n\nDetails saved in: {Path.GetFileName(logFilePath)}\n\nZIP file location:\n{fullZipFileLocation}",
                                            "Success with Warnings", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show($"Backup successfully created as a ZIP file:\n{fullZipFileLocation}",
                                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        DialogResult result1 = MessageBox.Show(
                            $"Exit Application?",
                            " ",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);

                        if (result1 == DialogResult.Yes)
                        {
                            Application.Exit();
                        }
                    }
                    catch (Exception ex)
                    {
                        DirectoryDelete(tempCopyPath);
                        progressDialog.Close();
                        MessageBox.Show($"A critical error occurred during the backup process:\n{ex.Message}",
                                        "Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        saveBtn.Enabled = true;
                        cancelBtn.Enabled = true;
                        
                    }
                }
            }
        }
    }
}