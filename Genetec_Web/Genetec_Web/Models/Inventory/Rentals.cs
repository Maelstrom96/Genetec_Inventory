using Genetec_Web.Models.Active_Directory;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Genetec_Web.Models.Inventory
{
    public class Rentals
    {

        public void Add()
        {

        }

        public static List<Rental> GetList()
        {
            List<Rental> rentals = new List<Rental>();

            MySqlConnection conn = Database.GetConnection();
            DataTable dt = new DataTable();
            dt.Clear();
            MySqlCommand cmd = new MySqlCommand("Rentals_List", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter da = new MySqlDataAdapter();

            try
            {
                conn.Open();

                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    Rental rental = new Rental();
                    rental.ID= int.Parse(row["ID"].ToString());
                    rental.Item = int.Parse(row["Item"].ToString());
                    rental.RentingUser = ADUsers.cacheADUser.Find(x => x.GUID.ToString() == row["RentingUser"].ToString());
                    rental.CreatorUser = ADUsers.cacheADUser.Find(x => x.GUID.ToString() == row["CreatorUser"].ToString());
                    String comment = row["Comment"].ToString();
                    if (row["StartDate"].ToString() != String.Empty) rental.StartDate = DateTime.Parse(row["StartDate"].ToString());
                    if (row["EndDate"].ToString() != String.Empty) rental.EndDate = DateTime.Parse(row["EndDate"].ToString());
                    if (row["PickUpDate"].ToString() != String.Empty) rental.PickUpDate = DateTime.Parse(row["PickUpDate"].ToString());
                    if (row["ReturnDate"].ToString() != String.Empty) rental.ReturnDate = DateTime.Parse(row["ReturnDate"].ToString());
                    if (row["LastAction"].ToString() != String.Empty) rental.LastAction = DateTime.Parse(row["LastAction"].ToString());
                    if (row["CreationDate"].ToString() != String.Empty) rental.CreationDate = DateTime.Parse(row["CreationDate"].ToString());

                    rentals.Add(rental);
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
            return rentals;
        }
    }
}