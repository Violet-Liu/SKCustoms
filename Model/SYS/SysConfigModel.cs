using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public partial class SysConfigModel
    {
        public string _webname { get; set; } = "";
        public string _webcompany { get; set; } = "";
        public string _weburl { get; set; } = "";
        public string _webtel { get; set; } = "";
        public string _webmail { get; set; } = "";
        public string _webcrod { get; set; } = "";
        public string _webtitle { get; set; } = "";
        public string _webkeyword { get; set; } = "";
        public string _webdescription { get; set; } = "";
        public string _webcopyright { get; set; } = "";
        public string _webpath { get; set; } = "";
        public string _webmanagepath { get; set; } = "";
        public int _webstatus { get; set; } = 1;
        public string _webclosereason { get; set; } = "";
        public string _webcountcode { get; set; } = "";



        //===============IM JOB LOG Ex Config===============
        public int _webimstatus { get; set; } = 1;
        public int _refreshcurrentwin { get; set; } = 10000;
        public int _refreshrecentcontact { get; set; } = 30000;
        public int _refreshnewmessage { get; set; } = 20000;

        public int _logstatus { get; set; } = 1;
        public int _exceptionstatus { get; set; } = 1;
        public int _globalexceptionstatus { get; set; } = 1;
        public string _globalexceptionurl { get; set; } = "/SysException/Error";
        public int _issinglelogin = 0;

        public int _taskstatus { get; set; } = 1;
        //===============IM JOB LOG Ex Config===============
        public string _emailstmp = "";
        public int _emailport { get; set; } = 25;
        public string _emailfrom { get; set; } = "";
        public string _emailusername { get; set; } = "";
        public string _emailpassword { get; set; } = "";
        public string _emailnickname { get; set; } = "";

        public string _attachpath { get; set; } = "";
        public string _attachextension { get; set; } = "";
        public int _attachsave { get; set; } = 1;
        public int _attachfilesize { get; set; } = 0;
        public int _attachimgsize { get; set; } = 0;
        public int _attachimgmaxheight { get; set; } = 0;
        public int _attachimgmaxwidth { get; set; } = 0;
        public int _thumbnailheight { get; set; } = 0;
        public int _thumbnailwidth { get; set; } = 0;
        public int _watermarktype { get; set; } = 0;
        public int _watermarkposition { get; set; } = 9;
        public int _watermarkimgquality { get; set; } = 80;
        public string _watermarkpic { get; set; } = "";
        public int _watermarktransparency { get; set; } = 10;
        public string _watermarktext { get; set; } = "";
        public string _watermarkfont { get; set; } = "";
        public int _watermarkfontsize { get; set; } = 12;

        //===============当前模板配置信息===============
        public string _templateskin { get; set; } = "blue";
        //==============系统安装时配置信息==============
        public string _sysdatabaseprefix { get; set; } = "Sys_";
        public string _sysencryptstring { get; set; } = "App";
    }
}
