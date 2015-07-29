using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net;

namespace Common
{
    /// <summary>
    /// string静态扩展类
    /// </summary>
    public static class StringEx
    {
        #region 把路径中的分隔符\转换成/
        /// <summary>
        /// 把路径中的分隔符\转换成/
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string PathSplitConvert(this string str)
        {
            return str.Replace("\\", "/");
        }
        #endregion

        #region 变为State为属性的json格式
        /// <summary>
        /// 变为State为属性的json格式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string JsonError(string s)
        {
            return "{\"State\": \"" + s + "\"}";
        }
        #endregion

        #region 百分比
        /// <summary>
        /// 百分比
        /// </summary>
        /// <param name="MaxValue"></param>
        /// <param name="MinValue"></param>
        /// <param name="IsNotRadix"></param>
        /// <returns></returns>
        public static string GetPercentage(double MaxValue, double MinValue, bool? IsNotRadix)
        {
            if (MinValue == 0 || MinValue == 0)
                return "0%";
            if (IsNotRadix == null || IsNotRadix == false) return (MinValue / MaxValue).ToString("P");
            else return (MinValue / MaxValue).ToString("0%");
        }
        #endregion

        #region MD5 16位加密 加密后密码为小写
        /// <summary>
        /// MD5 16位加密 加密后密码为小写
        /// </summary>
        /// <param name="MaxValue"></param>
        /// <param name="MinValue"></param>
        /// <param name="IsNotRadix"></param>
        /// <returns></returns>
        public static string GetMd5(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(strText));
            return System.Text.Encoding.Default.GetString(result);
        }
        #endregion

        #region 删除最后一位字符
        /// <summary>
        /// 删除最后一位字符
        /// </summary>
        public static string LastRemoveOne(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            else return s.Substring(0, s.Length - 1);
        }
        #endregion


        #region 路径返回图片
        /// <summary>
        /// 路径返回图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ToImg(string imgurl,string saveurl)
        {
            Image image = null;
            Stream stream = null;
            try
            {
                WebRequest webreq = WebRequest.Create(imgurl);
                WebResponse webres = webreq.GetResponse();
                stream = webres.GetResponseStream();
                image = System.Drawing.Image.FromStream(stream);
            }
            catch
            {
                image = new Bitmap(800, 600);   
                Graphics g1 = Graphics.FromImage(image);  
                g1.FillRectangle(Brushes.White, new Rectangle(0, 0, 800, 600));  
            }
            image.Save(saveurl, System.Drawing.Imaging.ImageFormat.Png);
        }
        #endregion

        #region 将图片数据转换为Base64字符串
        /// <summary>
        /// 将图片数据转换为Base64字符串
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static string ToBase64(Image img)
        {
            BinaryFormatter binFormatter = new BinaryFormatter();
            MemoryStream memStream = new MemoryStream();
            binFormatter.Serialize(memStream, img);
            memStream.Position = 0;
            byte[] bytes = memStream.GetBuffer();
            return Convert.ToBase64String(bytes);
        }
        #endregion
    }
}
