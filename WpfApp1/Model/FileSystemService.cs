using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace WpfApp1.Model
{
    internal class FileSystemService
    {
        static UCDrive UserControlDrive = new UCDrive();


        static public string CurrentDirectory = "";
        static public bool SelectDrive = false;

        static string[] deleteDirectories = new string[100];

        static public async Task GetFilesInDirectory()
        {
            if (SelectDrive == false)
            {
                foreach (DriveInfo drive in UserControlDrive.getDrive())
                    ((MainWindow)Application.Current.MainWindow).AddToWrap(drive.Name, "drive");
                SelectDrive = true;
                ((MainWindow)Application.Current.MainWindow).ParentDirectory.Visibility = Visibility.Hidden;
            }
            else
            {
                ((MainWindow)Application.Current.MainWindow).ParentDirectory.Visibility = Visibility.Visible;

                string[] directories = Directory.GetDirectories(CurrentDirectory);
                foreach (string directory in directories)
                {
                    string directoryName = Path.GetFileName(directory);
                    ((MainWindow)Application.Current.MainWindow).AddToWrap(directoryName, "folder");
                }

                string[] files = Directory.GetFiles(CurrentDirectory);
                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file);
                    ((MainWindow)Application.Current.MainWindow).AddToWrap(fileName, "file");
                }
            }
        }

        static public async Task GoToParentDirectory()
        {
            ((MainWindow)Application.Current.MainWindow).Wrap.Children.Clear();
            string parentDirectoryPath = Directory.GetParent(CurrentDirectory)?.FullName;

            if (parentDirectoryPath != null)
            {
                CurrentDirectory = parentDirectoryPath;

                await GetFilesInDirectory();
            }
            else
            {
                SelectDrive = false;

                await GetFilesInDirectory();
            }
        }

        static public async Task OpenDirectory(string Name)
        {
            SelectDrive = true;
            try
            {
                ((MainWindow)Application.Current.MainWindow).Wrap.Children.Clear();
                string folderName = Name;
                string currentPath = CurrentDirectory;
                string newFolderPath = Path.Combine(currentPath, folderName);
                CurrentDirectory = newFolderPath;
                ((MainWindow)Application.Current.MainWindow).GetFilesInDirectory();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                await GetFilesInDirectory();
            }
        }

        static public void OpenFile(string Name)
        {
            try
            {
                Process.Start("notepad.exe", CurrentDirectory + $@"\{Name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при открытии файла: {ex.Message}");
            }
        }

        static public async Task CreateFile(string Name, string Exaption)
        {
            File.Create($@"{CurrentDirectory}\{Name}{Exaption}");
            ((MainWindow)Application.Current.MainWindow).Wrap.Children.Clear();
            await GetFilesInDirectory();
        }

        static public async Task CreateFolder(string Name)
        {
            Directory.CreateDirectory($@"{CurrentDirectory}\{Name}");
            ((MainWindow)Application.Current.MainWindow).Wrap.Children.Clear();
            await GetFilesInDirectory();
        }

        static public void AddToDeleteList(string name)
        {
            for (int i = 0; i < deleteDirectories.Length; i++)
            {
                if (deleteDirectories[i] == null)
                {
                    deleteDirectories[i] = $@"{CurrentDirectory}\{name}";
                    break;
                }
            }
        }

        static public async Task DeleteFromList()
        {
            foreach (var file in deleteDirectories)
            {
                if (!string.IsNullOrEmpty(file))
                {
                    Directory.Delete(file);
                }
            }
            ((MainWindow)Application.Current.MainWindow).Wrap.Children.Clear();
            await GetFilesInDirectory();
        }

        static public async Task DownloadImage(string imageUrl, string savePath)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(imageUrl);

                if (response.IsSuccessStatusCode)
                {
                    byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();

                    using (FileStream fileStream = new FileStream(savePath, FileMode.Create))
                    {
                        await fileStream.WriteAsync(imageBytes, 0, imageBytes.Length);
                    }

                    MessageBox.Show("Image saved successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to download image from the provided URL.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
