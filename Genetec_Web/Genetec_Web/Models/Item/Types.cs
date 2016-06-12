using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Genetec_Web.Models.Item
{
    public class Types
    {
        public Parameters parameters = new Parameters();
        List<Type> types = new List<Type>();

        public Types()
        {
            Import();
        }

        public void Import()
        {
            MySqlConnection conn = Database.GetConnection();
            DataTable dt = new DataTable();
            dt.Clear();
            MySqlCommand cmd = new MySqlCommand("Types_List", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlDataAdapter da = new MySqlDataAdapter();

            try
            {
                conn.Open();

                da.SelectCommand = cmd;
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    int id = int.Parse(row["ID"].ToString());
                    String name = row["Name"].ToString();
                    Boolean bulkQty = int.Parse(row["BulkQty"].ToString()) == 1;
                    Boolean canRent = int.Parse(row["CanRent"].ToString()) == 1;

                    types.Add(new Type(this, name, bulkQty, canRent, id));
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conn.Close();
            }
        }

        public void Reset()
        {
            types.Clear();
            Import();

            //Maybe? 
            parameters.Reset();
        }
    }
}