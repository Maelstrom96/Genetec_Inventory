using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genetec_Web.Models
{
    public class Building
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String City { get; set; }

        public Building(String name, String city)
        {
            Name = name;
            City = city;
        }

        public Building(int id, String name, String city)
        {
            ID = id;
            Name = name;
            City = city;
        }
    }
}