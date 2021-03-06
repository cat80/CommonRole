﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Definitions
{
    public class RenderMessagePager
    {
        public int PageIndex
        { get; set; }
        public int PageSize
        {
            get;
            set;
        }
        public int RecordCount
        { get; set; }

        public string HrefFormat
        { get; set; }

        public int PageCount
        {
            get
            {
                if (RecordCount % PageSize == 0)
                {
                    return RecordCount / PageSize;
                }
                return RecordCount / PageSize + 1;
            }
        }
        private int PrevPageIndex
        {
            get
            {
                if (PageIndex == 1)
                {
                    return PageIndex;
                }
                return PageIndex - 1;
            }
        }

        private int NextPageIndex
        {
            get
            {
                if (PageIndex == PageCount)
                {
                    return PageIndex;
                }
                return PageIndex + 1;
            }
        }
        /// <summary>
        /// 最大显示的页码数
        /// </summary>
        public int MaxShowPage
        {
            get;
            set;
        }

        public RenderMessagePager(int pageIndex, int pageSize, int recordCount, int maxShowPage, string hrefFormat)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            if (pageSize < 1)
            {
                pageSize = 10;
            }

            this.RecordCount = recordCount;
            this.HrefFormat = hrefFormat;
            this.PageSize = pageSize;
            if (pageIndex > PageCount || pageIndex < 1)
            {
                pageIndex = 1;
            }
            this.PageIndex = pageIndex;

            if (maxShowPage < 1)
            {
                MaxShowPage = 4;
            }
            else
            {
                MaxShowPage = maxShowPage;
            }
            CurrentStyle = "current";
        }

        public string CurrentStyle
        {
            get;
            set;
        }
        public RenderMessagePager(int pageIndex, int pageSize, int recordCount, string hrefFormat)
            : this(pageIndex, pageSize, recordCount, 4, hrefFormat)
        {

        }

        public string MessagePage()
        {
            if (RecordCount == 0)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();

            Match match = Regex.Match(HrefFormat, @"[\?|\&]page=(\d+)[\&]?", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                HrefFormat = Regex.Replace(HrefFormat, @"page=(\d+)", "page={0}");
            }
            else
            {
                if (HrefFormat.IndexOf("?") == -1)
                {
                    HrefFormat += "?page={0}";
                }
                else
                {
                    HrefFormat += "&page={0}";
                }
            }
            sb.AppendFormat("<a class=\"left\" {2} href=\"" + HrefFormat + "\">{1}</a>", PrevPageIndex, "上一页", PageIndex == 1 ? "disabled=\"disabled\"" : "");
            sb.AppendFormat(PageIndex + "/" + PageCount);
            sb.AppendFormat("<a {2} class=\"right\" href=\"" + HrefFormat + "\">{1}</a>", NextPageIndex, "下一页", PageCount == PageIndex ? "disabled=\"disabled\"" : "");
            //'dsds?ab='+this.value+'asda'
            return sb.ToString();
        }


        public string RenderForAjax()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("<a class=\"prev\" {2} href=\"javascript:void(0);\" onclick=\"" + HrefFormat + "\">{1}</a>", PrevPageIndex, "上一页", PageIndex == 1 ? "disabled=\"disabled\"" : "");

            sb.AppendFormat("<a {1} href=\"javascript:void(0);\" onclick=\"" + HrefFormat + "\">{0}</a>", 1, PageIndex == 1 ? "class=\"" + CurrentStyle + "\"" : "");


            if (PageIndex > MaxShowPage)
            {
                sb.AppendFormat("<a href=\"javascript:void(0);\" onclick=\"" + HrefFormat + "\">...</a>", PageIndex - MaxShowPage);
            }

            for (int i = PageIndex - 3; i < PageCount && i <= PageIndex; i++)
            {
                if (i > 1)
                {
                    sb.AppendFormat("<a {1} href=\"javascript:void(0);\" onclick=\"" + HrefFormat + "\">{0}</a>", i, i == PageIndex ? "class=\"" + CurrentStyle + "\"" : "");
                }
            }
            //if (PageIndex > 4)
            //{
            //    sb.AppendFormat("<a  class=\"current\" href=\"" + HrefFormat + "\">{0}</a>", PageIndex);
            //}

            for (int i = PageIndex + 1; i < PageCount && i < PageIndex + MaxShowPage; i++)
            {
                sb.AppendFormat("<a href=\"javascript:void(0);\" onclick=\"" + HrefFormat + "\">{0}</a>", i);
            }

            if (PageIndex + MaxShowPage < PageCount)
            {
                sb.AppendFormat("<a href=\"javascript:void(0);\" onclick=\"" + HrefFormat + "\">...</a>", PageIndex + MaxShowPage);
            }
            if (PageCount > 1)
            {
                sb.AppendFormat("<a {1} href=\"javascript:void(0);\" onclick=\"" + HrefFormat + "\">{0}</a>", PageCount, PageCount == PageIndex ? "class=\"" + CurrentStyle + "\"" : "");
            }
            sb.AppendFormat("<a {2} class=\"prev\" href=\"javascript:void(0);\" onclick=\"" + HrefFormat + "\">{1}</a>", NextPageIndex, "下一页", PageCount == PageIndex ? "disabled=\"disabled\"" : "");

            return sb.ToString();
        }
    }
}
