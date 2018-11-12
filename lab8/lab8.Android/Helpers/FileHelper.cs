using System;
using System.IO;
using lab8.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace lab8.Helpers
{
    class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}