using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Genetec_Web.Models.Inventory.Location
{
    public class Shelves : List<Shelf>
    {
        private Rooms Rooms;

        public Shelves(ref Rooms rooms)
        {
            Rooms = rooms;

            this.AddRange(GetShelves());
        }

        public List<Shelf> GetShelves()
        {
            List<Shelf> shelves = new List<Shelf>();

            MySqlConnection conn = Database.GetConnection();
            DataTable dt = new DataTable();
            dt.Clear();
            MySqlCommand cmd = new MySqlCommand("Shelves_List", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter da = new MySqlDataAdapter();

            try
            {
                conn.Open();

                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    int room_id = int.Parse(row["Room"].ToString());
                    Room room = (from r in Rooms where r.ID == room_id select r).First();
                    shelves.Add(new Shelf(int.Parse(row["ID"].ToString()), ref room, row["Tag"].ToString(), row["Description"].ToString()));
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
            return shelves;
        }
    }
}