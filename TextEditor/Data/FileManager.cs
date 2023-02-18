using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TextEditor.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TextEditor.Data
{
    public class FileManager
    {
        public static async Task<FileResult> GetFilePath()
        {
            try
            {
                var statusStorageRead = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

                if (statusStorageRead != PermissionStatus.Granted)
                    await Permissions.RequestAsync<Permissions.StorageRead>();

                FileResult result = await FilePicker.PickAsync();

                if (result != null)
                    return result;
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetTextFromFile(string filePath)
        {
            string content = File.ReadAllText(filePath, Encoding.UTF8);
            return content;
        }

        public bool CreateTextFileExisting(string fileName, string newText)
        {
            try
            {
                string folder = DependencyService.Get<IGetExternalDirectory>().GetEditedDirPath();
                string path = Path.Combine(folder, fileName);
                byte[] data = Encoding.UTF8.GetBytes(newText);
                MemoryStream ms = new MemoryStream(data);
                File.WriteAllBytes(path, ms.ToArray());
                ms.Close();
                ms.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateTextFile(string text)
        {
            try
            {
                string dir = DependencyService.Get<IGetExternalDirectory>().GetCreateDirPath();
                DateTime dt = DateTime.Now;
                string filename = string.Format("TextFile{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}{6:D3}.txt",
                                               dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);

                string filePath = Path.Combine(dir, filename);
                byte[] data = Encoding.UTF8.GetBytes(text);
                MemoryStream ms = new MemoryStream(data);
                File.WriteAllBytes(filePath, ms.ToArray());
                ms.Close();
                ms.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}