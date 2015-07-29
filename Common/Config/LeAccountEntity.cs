using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Common.Config
{
    public class LeAccountEntity : IConfigEntity
    {
        public string BindWeiXinID { get; set; }
        public string InterfaceUrl { get; set; }
        public string Token { get; set; }
    }

    
}
