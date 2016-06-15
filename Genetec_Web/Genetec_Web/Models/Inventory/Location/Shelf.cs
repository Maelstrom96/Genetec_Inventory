using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genetec_Web.Models.Inventory.Location
{
    public class Shelf
    {
        public int ID { get; set; }
        public Room Room;
        public String Tag { get; set; }
        public String Description { get; set; }

        public Shelf(int id, ref Room room, String tag, String description)
        {
            ID = id;
            Room = room;
            Tag = tag;
            Description = description;
        }

        public Shelf(ref Room room, String tag, String description) : this(0, ref room, tag, description) { }
    }
}