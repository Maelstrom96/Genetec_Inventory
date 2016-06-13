using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genetec_Web.Models.Item
{
    public class Model
    {
        public int ID { get; set; }
        public Type type { get; set; }
        public String Value { get; set; }
        public String Description { get; set; }

        public Model(int id, String value, String description, ref Type type_class)
        {
            ID = id;
            Value = value;
            Description = description;
            type = type_class;
        }

        public Model(String value, String description, ref Type type_class)
        {
            Value = value;
            Description = description;
            type = type_class;
        }
    }
}