using System;
using Xamarin.Forms;
using TextEditor.Services;
using Java.IO;
using System.Collections.Generic;

[assembly:Dependency(typeof(TextEditor.Droid.GetExternalDirectory))]
namespace TextEditor.Droid
{
    public class GetExternalDirectory : IGetExternalDirectory
    {
        [Obsolete]
        public string GetCreateDirPath()
        {
            File rootDir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
            File appDir = new File(rootDir, "Text Files");
            appDir.Mkdirs();
            File createDir = new File(appDir, "Created");
            createDir.Mkdirs();

            return createDir.AbsolutePath;
        }

        [Obsolete]
        public string GetEditedDirPath()
        {
            File rootDir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
            File appDir = new File(rootDir, "Text Files");
            appDir.Mkdirs();
            File createDir = new File(appDir, "Edited");
            createDir.Mkdirs();

            return createDir.AbsolutePath;
        }

        public List<string> GetFiles(string path)
        {
            try
            {
                File directory = new File(path);
                File[] files = directory.ListFiles();
                List<string> paths = new List<string>();

                foreach (var file in files)
                {
                    paths.Add(file.AbsolutePath);
                }

                return paths;
            }
            catch (IOException)
            {
                return null;
            }
        }
    }
}