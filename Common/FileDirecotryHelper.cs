#region 文件描述

// 描述：文件或目录的辅助类
// 作者：cat80
// 时间：2012-06-13 11:40

#endregion

#region 类修改记录 : 每次修改一组描述

// 修改描述：
// 修 改 人：
// 修改时间：

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace Common
{
    public class FileDirecotryHelper
    {
        /// <summary>
        /// 创建目录（当父级目录不存在时，会递归创建父目录)
        /// </summary>
        /// <param name="path">创建目录</param>
        public static void CreateDirectory(string path)
        {
            try
            {

                DirectoryInfo direct = new DirectoryInfo(path);
                if (direct.Exists)
                {
                    return;
                }
                if (!direct.Parent.Exists)
                {
                    CreateDirectory(direct.Parent.FullName);
                }
                direct.Create();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #region  public static string GetDirectoryJson(string directory)/// 得到目录的JSON文当格式
        /// <summary>
        /// 得到目录的JSON文当格式
        /// </summary>
        /// <param name="directory"></param>
        public static string GetDirectoryJson(string directory)
        {
            if (!Directory.Exists(directory))
            {
                return "[]";
            }
            return JSON.GetJson(GetDirectoryTreeObject(new DirectoryInfo(directory), ""));
        }

        private static object GetDirectoryTreeObject(FileSystemInfo filesytem, string parentDir)
        {

            if (filesytem is DirectoryInfo)
            {
                List<object> list = new List<object>();
                DirectoryInfo dir = filesytem as DirectoryInfo;
                foreach (var item in dir.GetDirectories())
                {
                    string childParentDir = parentDir + "/" + item.Name;
                    if (string.IsNullOrEmpty(parentDir))
                    {
                        childParentDir = item.Name;
                    }
                    list.Add(new
                    {
                        text = item.Name,
                        isexpand = "false",
                        dirname = childParentDir,
                        children = GetDirectoryTreeObject(item, childParentDir)
                    });
                }
                foreach (var item in dir.GetFiles())
                {
                    list.Add(GetDirectoryTreeObject(item, parentDir));
                }
                return list;
            }
            else if (filesytem is FileInfo)
            {
                return new
                {
                    text = filesytem.Name,
                    isexpand = "false",
                    dirname = parentDir
                };
            }
            else
            {
                return new { };
            }
        }


        #endregion

        /// <summary>
        /// 获取文件夹下面的所有文件列表
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static void GetDirFiles(string dir, List<string> filelist)
        {
            if (Directory.Exists(dir))
            {
                foreach (var item in Directory.GetFiles(dir))
                {
                    filelist.Add(item);
                }
                foreach (var item in Directory.GetDirectories(dir))
                {
                    GetDirFiles(item, filelist);
                }
            }
        }

        /// <summary>
        /// 获取某个文件夹的所有文件
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static List<string> GetDirFiles(string dir)
        {
            List<string> list = new List<string>();
            if (Directory.Exists(dir))
            {
                foreach (var item in Directory.GetFiles(dir))
                {
                    list.Add(item);
                }
                foreach (var item in Directory.GetDirectories(dir))
                {
                    list.AddRange(GetDirFiles(item));
                }
            }
            return list;
        }
        /// <summary>
        /// 强力删除文件目录
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteFileSystem(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                dir.Attributes = FileAttributes.Normal;
                foreach (var item in dir.GetFileSystemInfos())
                {
                    DeleteFileSystem(item.FullName);
                }
                dir.Delete();
            }
            if (File.Exists(path))
            {
                FileInfo fileInfo = new FileInfo(path);
                fileInfo.IsReadOnly = false;
                fileInfo.Delete();
            }
        }

        /// <summary> 
        /// 复制文件夹（及文件夹下所有子文件夹和文件） 
        /// </summary> 
        /// <param name="sourcePath">待复制的文件夹路径</param> 
        /// <param name="destinationPath">目标路径</param> 
        public static void CopyDirectory(String sourcePath, String destinationPath)
        {
            DirectoryInfo info = new DirectoryInfo(sourcePath);
            //   Directory.CreateDirectory(destinationPath);
            if (!Directory.Exists(destinationPath))
            {
                CreateDirectory(destinationPath);
            }
            foreach (FileSystemInfo fsi in info.GetFileSystemInfos())
            {
                String destName = Path.Combine(destinationPath, fsi.Name);

                if (fsi is System.IO.FileInfo)          //如果是文件，复制文件 
                {
                    File.Copy(fsi.FullName, destName, true);
                    FileInfo file = new FileInfo(destName);
                    file.IsReadOnly = false;
                }
                else                                    //如果是文件夹，新建文件夹，递归 
                {
                    // Directory.CreateDirectory(destName);
                    CopyDirectory(fsi.FullName, destName);
                }
            }
        }

        public static string GetFileSystemInfoMD5(FileSystemInfo fileSystem, int prevWidth = 0)
        {
            StringBuilder sb = new StringBuilder();

            string prevString = "".PadLeft(prevWidth, ' ');
            if (fileSystem is DirectoryInfo)
            {
                DirectoryInfo dir = fileSystem as DirectoryInfo;
                //foreach (FileSystemInfo item in dir.GetFileSystemInfos())
                //{
                //    if (item is DirectoryInfo)
                //    {
                //        sb.Append(prevString + item.Name + Environment.NewLine);
                //    }
                //    GetFileSystemInfoMD5(item, prevWidth + 3);
                //}
                foreach (var item in dir.GetDirectories())
                {
                    sb.Append(prevString + item.Name + Environment.NewLine);
                    sb.Append(GetFileSystemInfoMD5(item, prevWidth + 2));
                }

                foreach (var item in dir.GetFiles())
                {
                    sb.AppendFormat("{0}{1} {2}{3}", prevString, item.Name.PadRight(30), Utils.FileMD5(item.FullName), Environment.NewLine);

                }

            }
            else if (fileSystem is FileInfo)
            {

                sb.AppendFormat("{0}{1} {2}{3}", prevString, fileSystem.Name.PadRight(30), Utils.FileMD5(fileSystem.FullName), Environment.NewLine);
            }
            return sb.ToString();
        }

        public static XElement GetFileSystemInfoMD5Xml(FileSystemInfo fileSystem)
        {
            if (fileSystem == null)
            {
                throw new ArgumentException("fileSystem Not Allow Null");
            }

            if (fileSystem is DirectoryInfo)
            {
                DirectoryInfo dir = fileSystem as DirectoryInfo;
                XElement chlidDire = new XElement("Directory");
                chlidDire.SetAttributeValue("Name", fileSystem.Name);

                foreach (var item in dir.GetDirectories())
                {
                    chlidDire.Add(GetFileSystemInfoMD5Xml(item));
                }
                foreach (var item in dir.GetFiles())
                {
                    chlidDire.Add(GetFileSystemInfoMD5Xml(item));
                }
                return chlidDire;
            }
            else
            {
                XElement chlid = new XElement("File");
                chlid.SetAttributeValue("Name", fileSystem.Name);
                chlid.SetAttributeValue("MD5", Utils.FileMD5(fileSystem.FullName));
                return chlid;
            }
        }
    }
}
