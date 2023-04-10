using DocumentFormat.OpenXml.CustomProperties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PictureCat
{
    public static class DirectoryParser
    {
        public const string PATH_TO_FOLDER = "..\\..\\..\\..\\PictureCat\\";

        public static List<string>? ParseFolder(string folderName)
        {
            string mypath = PATH_TO_FOLDER + folderName + "\\";
            return Directory
                .GetFiles(mypath, "*", SearchOption.AllDirectories)
                .ToList();
        }

        public static async Task AddToFolderAsync(string folderName, string[] fileNames)
        {
            await Task.Run(() =>
            {
                string path = PATH_TO_FOLDER + folderName;
                string fileName = null!;
                string destFile = null!;
                foreach (string file in fileNames)
                {
                    string extension = Path.GetExtension(file).ToLower();
                    if ( extension != ".png" 
                        && extension != ".jpg" 
                        && extension != ".jpeg"
                        && extension != ".jfif")
                    {
                        continue;
                    }
                    fileName = Path.GetFileName(file);
                    destFile = Path.Combine(path, fileName);
                    File.Copy(file, destFile, true);
                }
            });
        }

        public static async Task AddToFolderImageByteArrayAsync(string folderName, byte[] array)
        {
            string path = Path.Combine(PATH_TO_FOLDER, folderName);
            await File.WriteAllBytesAsync(path, array, CancellationToken.None);
        }

        public static void AddToFolderImageByteArrayRangeSync(string folderName, List<byte[]> byteArrays, List<string> fileNames)
        {
            string path = Path.Combine(PATH_TO_FOLDER, folderName);
            for (int i = 0; i < byteArrays.Count; i++)
            {
                File.WriteAllBytes(Path.Combine(path, fileNames[i]), byteArrays[i]);
            }
        }

        public static void AddToFolderSync(string folderName, string[] fileNames)
        {
            string path = PATH_TO_FOLDER + folderName;
            string fileName = null!;
            string destFile = null!;
            foreach (string file in fileNames)
            {
                string extension = Path.GetExtension(file).ToLower();
                if (extension != ".png"
                    && extension != ".jpg"
                    && extension != ".jpeg"
                    && extension != ".jfif")
                {
                    continue;
                }
                fileName = Path.GetFileName(file);
                destFile = Path.Combine(path, fileName);
                File.Copy(file, destFile, true);
            }
        }

        public static void RemoveFromFolder(string FolderWithFileName)
        {
            File.Delete(FolderWithFileName);
        }

        public static void RemoveFileFromFolder(string folderName, string fileName)
        {
            RemoveFromFolder(string.Concat(PATH_TO_FOLDER, folderName, fileName));
        }

        public static void RemoveFromFolderRange(string folderName, string[] fileName)
        {
            string currentFolder = string.Concat(PATH_TO_FOLDER, folderName);
            foreach (var item in fileName)
            {
                RemoveFromFolder(Path.Combine(currentFolder, Path.GetFileName(item)));
            }
        }
    }
}
