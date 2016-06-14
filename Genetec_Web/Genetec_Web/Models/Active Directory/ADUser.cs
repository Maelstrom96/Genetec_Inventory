using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genetec_Web.Models.Active_Directory
{
    public class ADUser
    {
        public Guid GUID { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
    }
}