using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genetec_Web.Models.Item
{
    public class Item
    {
        List<Parameter> Parameters { get; set; }

        public Item()
        {
            Parameters = new List<Parameter>();
        }
    }
}