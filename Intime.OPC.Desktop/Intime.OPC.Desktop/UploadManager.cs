using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;

namespace Intime.OPC.Desktop
{
    public static class UploadManager
    {
        private static readonly string basePath = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string tempPath = basePath + "temp";

        internal static void Copy()
        {
            if (Directory.Exists(tempPath))
            {
                string[] files = Directory.GetFiles(tempPath);
                foreach (string file in files)
                {
                    string name = Path.GetFileName(file);
                    File.Copy(file, basePath + name, true);
                }
                Directory.Delete(tempPath, true);
            }
        }

        internal static void UpLoad()
        {
            Version server = getServerVersion();
            Version client = getCurrentVersion();
            if (client < server)
            {
                try
                {
                    var task = new Task(DownloadFiles);

                    task.Start();
                }
                catch (Exception ex)
                {
                }
            }
        }

        private static void DownloadFiles()
        {
            try
            {
                string url = ConfigurationManager.AppSettings["updateAddress"];
                var myWebClient = new WebClient();
                if (!Directory.Exists(tempPath))
                {
                    Directory.CreateDirectory(tempPath);
                }
                string tempFile = tempPath + "//bin.zip";
                myWebClient.DownloadFile(url + "//bin.zip", tempFile);
                UnZip(tempFile, tempPath);
                File.Delete(tempFile);
            }
            catch (Exception ex)
            {
            }
        }

        private static Version getServerVersion()
        {
            return new Version(1, 0, 0, 1);
        }

        private static Version getCurrentVersion()
        {
            string version = ConfigurationManager.AppSettings["version"];
            return new Version(version);
        }

        private static void UnZip(string zipfilepath, string unzippath)
        {
            var s = new ZipInputStream(File.OpenRead(zipfilepath));

            ZipEntry theEntry;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                string directoryName = "";
                if (!theEntry.IsDirectory)
                    directoryName = unzippath + Path.DirectorySeparatorChar;
                string fileName = Path.GetFileName(theEntry.Name);

//                MessageBox.Show(unzippath + "--BB--" + fileName);
                //生成解压目录
                Directory.CreateDirectory(directoryName);

                if (fileName != String.Empty)
                {
                    //如果文件的压缩后大小为0那么说明这个文件是空的,因此不需要进行读出写入
                    if (theEntry.CompressedSize == 0)
                        break;
                    //解压文件到指定的目录
                    // directoryName = Path.GetDirectoryName(unzippath + theEntry.Name);

                    //建立下面的目录和子目录
                    Directory.CreateDirectory(directoryName);
                    //                   MessageBox.Show(directoryName +"--BBBBBB--" +theEntry.Name);
                    FileStream streamWriter = File.Create(directoryName + theEntry.Name);

                    int size = 4096;
                    var data = new byte[4096];
                    while (true)
                    {
                        size = s.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            streamWriter.Write(data, 0, size);
                        }
                        else
                        {
                            break;
                        }
                    }
                    streamWriter.Close();
                }
            }
            s.Close();
        }
    }
}