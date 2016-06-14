using Genetec_Web.Models.Active_Directory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Genetec_Web.Models.Inventory
{
    public class Rental
    {
        public int ID { get; set; }
        public int Item { get; set; }
        public String Comment { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? PickUpDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public ADUser RentingUser { get; set; }
        public ADUser CreatorUser { get; set; }
        public Boolean Canceled { get; set; }
        public DateTime? LastAction { get; set; }
        public DateTime? CreationDate { get; set; }

        public Rental()
        {
            StartDate = null;
            EndDate = null;
            PickUpDate = null;
            ReturnDate = null;
            LastAction = null;
            CreationDate = null;
        }
    }
}