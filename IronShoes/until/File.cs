//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace IronShoes.until
{
    class File
    {
        /// <summary>
        /// 上传文件方法
        /// </summary>
        /// <param name="filePath">本地文件所在路径（包括文件）</param>
        /// <param name="serverPath">文件存储服务器路径（包括文件）</param>
        public void UploadFile(string filePath, string serverPath)
        {
            //创建WebClient实例
            WebClient webClient = new WebClient();
            webClient.Credentials = CredentialCache.DefaultCredentials;
            //要上传的文件
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] postArray = br.ReadBytes((int)fs.Length);
            Stream postStream = webClient.OpenWrite(serverPath, "PUT");
            try
            {
                if (postStream.CanWrite)
                {
                    postStream.Write(postArray, 0, postArray.Length);
                    postStream.Close();
                    fs.Dispose();
                }
                else
                {
                    postStream.Close();
                    fs.Dispose();
                }

            }
            catch (Exception ex)
            {
                postStream.Close();
                fs.Dispose();
                throw ex;
            }
            finally
            {
                postStream.Close();
                fs.Dispose();
            }
        }

        /// <summary>
        /// 下载文件方法
        /// </summary>
        /// <param name="serverPath">被下载的文件地址（服务器地址包括文件）</param>
        /// <param name="filePath">另存放的路径（本地需要存储文件的文件夹地址）</param>
        public void Download(string serverPath, string filePath)
        {
            WebClient client = new WebClient();
            string fileName = serverPath.Substring(serverPath.LastIndexOf("/") + 1); ;//被下载的文件名
            string path = filePath + fileName;//另存为地址
            try
            {
                WebRequest myre = WebRequest.Create(serverPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            try
            {
                client.DownloadFile(serverPath, fileName);
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader r = new BinaryReader(fs);
                byte[] mbyte = r.ReadBytes((int)fs.Length);
                FileStream fstr = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                fstr.Write(mbyte, 0, (int)fs.Length);
                fstr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}