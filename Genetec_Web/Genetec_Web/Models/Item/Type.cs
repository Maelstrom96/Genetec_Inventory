using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Genetec_Web.Models.Item
{
    public class Type
    {
        private Types types;
        public Parameters parameters;
        public int ID { get; }
        public String Name { get; set; }
        public Boolean BulkQuantity;
        public Boolean CanRent;

        public Type(Types types_class, String name, Boolean bulkQuantity, Boolean canRent, int id = 0)
        {
            types = types_class;
            ID = id;
            Name = name;
            BulkQuantity = bulkQuantity;
            CanRent = canRent;
            parameters = new Parameters(this);
        }

        public class Parameters
        {
            Type type;
            List<Parameter> parameters = new List<Parameter>();

            public Parameters(Type type_class)
            {
                type = type_class;
                Import();
            }

            public void Import()
            {
                MySqlConnection conn = Database.GetConnection();
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataTable dt = new DataTable();
                dt.Clear();

                try
                {
                    MySqlCommand cmd = new MySqlCommand("Type_Parameters_Get", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("?tid", type.ID);
                    cmd.Parameters["?tid"].Direction = ParameterDirection.Input;

                    conn.Open();
                    da.SelectCommand = cmd;
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                        parameters.Add(type.types.parameters.parameters.Find(x => x.ID == int.Parse(row["Parameter_ID"].ToString())));
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
                parameters.Clear();
                Import();
            }

            /// <summary>
            /// Add a parameter to a Item type
            /// </summary>
            /// <param name="param"> Need fully populated parameter (ID and Value) </param>
            public void Add(Parameter param)
            {
                MySqlConnection conn = Database.GetConnection();

                try
                {
                    MySqlCommand cmd = new MySqlCommand("Type_Parameter_Add", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("?pid", param.ID);
                    cmd.Parameters["?pid"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("?tid", type.ID);
                    cmd.Parameters["?tid"].Direction = ParameterDirection.Input;

                    conn.Open();
                    Int64 retval = (Int64)cmd.ExecuteNonQuery();

                    // If No error in DB
                    if (retval == 1) parameters.Add(param);
                }
                catch (Exception e)
                {

                }
                finally
                {
                    conn.Close();
                }
            }

            public void Delete(Parameter param)
            {
                MySqlConnection conn = Database.GetConnection();

                try
                {
                    MySqlCommand cmd = new MySqlCommand("Type_Parameter_Add", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("?pid", param.ID);
                    cmd.Parameters["?pid"].Direction = ParameterDirection.Input;
                    cmd.Parameters.AddWithValue("?tid", type.ID);
                    cmd.Parameters["?tid"].Direction = ParameterDirection.Input;

                    conn.Open();
                    Int64 retval = (Int64)cmd.ExecuteNonQuery();

                    // If No error in DB
                    if (retval == 1) parameters.Add(param);
                }
                catch (Exception e)
                {

                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}