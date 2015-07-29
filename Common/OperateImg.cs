using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Drawing;

namespace Common
{
    /// <summary>
    /// string静态扩展类
    /// </summary>
    public static class OperateImg
    {
        #region 生成缩略图函数
        /// <summary>
        /// 生成缩略图函数 
        /// </summary>
        /// <param name="fromFileStream">源图文件流</param>
        /// <param name="fileSaveUrl">缩略图存放地址</param>
        /// <param name="templateWidth">模版宽</param>
        /// <param name="templateHeight">模版高</param>
        public static void MakeSmallImg(System.IO.Stream fromFileStream, string fileSaveUrl, System.Double templateWidth, System.Double templateHeight)
        {
            //从文件取得图片对象，并使用流中嵌入的颜色管理信息 
            System.Drawing.Image myImage = System.Drawing.Image.FromStream(fromFileStream, true);
            //缩略图宽、高 
            System.Double newWidth = myImage.Width, newHeight = myImage.Height;
            //宽大于模版的横图 
            if (myImage.Width > myImage.Height || myImage.Width == myImage.Height)
            {
                if (myImage.Width > templateWidth)
                {
                    //宽按模版，高按比例缩放 
                    newWidth = templateWidth;
                    newHeight = myImage.Height * (newWidth / myImage.Width);
                }
            }
            //高大于模版的竖图 
            else
            {
                if (myImage.Height > templateHeight)
                {
                    //高按模版，宽按比例缩放 
                    newHeight = templateHeight;
                    newWidth = myImage.Width * (newHeight / myImage.Height);
                }
            }
            //取得图片大小 
            System.Drawing.Size mySize = new Size((int)newWidth, (int)newHeight);
            //新建一个bmp图片 
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(mySize.Width, mySize.Height);
            //新建一个画板 
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //设置高质量插值法 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //清空一下画布 
            g.Clear(Color.White);
            //在指定位置画图 
            g.DrawImage(myImage, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
            new System.Drawing.Rectangle(0, 0, myImage.Width, myImage.Height),
            System.Drawing.GraphicsUnit.Pixel);
            ///文字水印 
            //System.Drawing.Graphics G=System.Drawing.Graphics.FromImage(bitmap); 
            //System.Drawing.Font f=new Font("宋体",10); 
            //System.Drawing.Brush b=new SolidBrush(Color.Black); 
            //G.DrawString("myohmine",f,b,10,10); 
            //G.Dispose(); 
            ///图片水印 
            //System.Drawing.Image copyImage = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath("pic/1.gif")); 
            //Graphics a = Graphics.FromImage(bitmap); 
            //a.DrawImage(copyImage, new Rectangle(bitmap.Width-copyImage.Width,bitmap.Height-copyImage.Height,copyImage.Width, copyImage.Height),0,0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel); 
            //copyImage.Dispose(); 
            //a.Dispose(); 
            //copyImage.Dispose(); 
            //保存缩略图 
            bitmap.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
            g.Dispose();
            myImage.Dispose();
            bitmap.Dispose();
        }
        #endregion
    }
}
