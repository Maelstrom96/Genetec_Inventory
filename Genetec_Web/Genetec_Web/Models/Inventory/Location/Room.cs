using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genetec_Web.Models.Inventory.Location
{
    public class Room
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public Building Building { get; set; }
        public short Floor { get; set; }
    }
}