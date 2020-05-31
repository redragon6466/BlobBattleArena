
using Mono.Data.SqliteClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Data
{
    public class DataService
    {
        public string TestDatabase()
        {
            try
            {
                string cs = "Data Source=:memory:";
                string stm = "SELECT SQLITE_VERSION()";

                Debug.Log(" hey");
                using (var con = new SqliteConnection(cs))
                {
                    Debug.Log(" listen");
                    con.Open();

                    using (var cmd = new SqliteCommand(stm, con))
                    {
                        string version = cmd.ExecuteScalar().ToString();

                        Debug.Log($"SQLite version: {version}");
                    }
                }
                   
            }
            catch (Exception e)
            {
                return e.Message + ", " + e.InnerException.Message;
            }
            return "";
        }
    }
}
