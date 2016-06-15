using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genetec_Web.Models.Inventory.Location
{
    public class LocationManager
    {
        private Buildings buildings;
        public Buildings Buildings { get { return buildings; } }
        private Rooms rooms;
        public Rooms Rooms { get { return rooms; } }
        private Shelves shelves;
        public Shelves Shelves { get { return shelves; } }

        public LocationManager()
        {
            buildings = new Buildings();
            rooms = new Rooms(ref buildings);
            shelves = new Shelves(ref rooms);
        }
    }
}