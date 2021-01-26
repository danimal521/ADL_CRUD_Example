using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Text;
using Azure;
using Azure.Storage.Files.DataLake;
using Azure.Storage.Files.DataLake.Models;
using Azure.Storage;
using System.IO;

namespace CRUDExample
{
    public class ADLCRUD
    {
        private const string ACCOUNTNAME                                = "ACC NAME";
        private const string ACCOUNTKEY                                 = "KEY";
        private const string CONTAINER                                  = "main"; //Container name
        private const string DFURL                                      = "https://" + ACCOUNTNAME + ".dfs.core.windows.net";

        public static void UploadFile(string FileName, string LocalFileName)
        {
            try
            {
                StorageSharedKeyCredential skc                          = new StorageSharedKeyCredential(ACCOUNTNAME, ACCOUNTKEY);

                DataLakeServiceClient dlsClient                         = new DataLakeServiceClient(new Uri(DFURL), skc);

                DataLakeFileSystemClient dlfsClient                     = dlsClient.GetFileSystemClient(CONTAINER);

                DataLakeFileClient dlfClient                            = dlfsClient.GetFileClient(FileName);
                dlfClient.Create();

                FileStream f                                            = File.OpenRead(LocalFileName);

                dlfClient.Append(f, 0);
                dlfClient.Flush(f.Length);
            }
            catch(Exception exError)
            {

            }
        }

        public static void ReadFile(string FileName, string LocalFileName)
        {
            try
            {
                StorageSharedKeyCredential skc                          = new StorageSharedKeyCredential(ACCOUNTNAME, ACCOUNTKEY);

                DataLakeServiceClient dlsClient                         = new DataLakeServiceClient(new Uri(DFURL), skc);

                DataLakeFileSystemClient dlfsClient                     = dlsClient.GetFileSystemClient(CONTAINER);

                DataLakeFileClient dlfClient                            = dlfsClient.GetFileClient(FileName);
                Response<FileDownloadInfo> fdli                         = dlfClient.Read();
                
                BinaryReader bReader                                     = new BinaryReader(fdli.Value.Content);

                using(FileStream fs                                     = File.OpenWrite(LocalFileName))
                {
                    int nBuffferSize                                    = 4096;

                    byte[] baBuffer                                     = new byte[nBuffferSize];

                    int nCount;

                    while ((nCount = bReader.Read(baBuffer, 0, baBuffer.Length)) != 0)
                    {
                        fs.Write(baBuffer, 0, nCount);
                    }

                    fs.Flush();
                    fs.Close();
                }

                /*


                FileStream f = File.OpenRead(LocalFileName);

                file.Append(f, 0);
                file.Flush(f.Length);*/
            }
            catch (Exception exError)
            {

            }
        }
    }
}
