using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genetec_Web.Models
{
    public class Parameter
    {
        public int ID { get; set; }
        public String Value { get; set; }

        public Parameter(int id, String value)
        {
            ID = id;
            Value = value;
        }
    }
}