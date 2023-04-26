using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyMetal.Code
{
    static class SQLHandler
    {
        public static string connectionString = $@"Data Source=DESKTOP-1GFFQSL; Initial Catalog=HeavyMetal; Integrated Security=True";

        public static void User_Insert(string name_user, string password_user)
        {
            if (User_GetID_ByName(name_user) == 0 && name_user != "")
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string command = $@"Insert into [user] (name_user, password_user) values ('{name_user}', '{password_user}')";
                    using (SqlCommand comm = new SqlCommand(command, connection))
                        comm.ExecuteNonQuery();
                }
        }
        public static int User_GetID_ByName(string name_user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string command = $"select id_user from [user] where name_user = '{name_user}'";
                using (SqlDataReader reader = new SqlCommand(command, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }
            }
            return 0;
        }
        public static int User_GetID_ByName(string name_user, string password_user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string command = $"select id_user from [user] where name_user = '{name_user}' and password_user = '{password_user.Trim()}'";
                using (SqlDataReader reader = new SqlCommand(command, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }
            }
            return 0;
        }    
        public static int User_GetAccess_ByID(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string command = $"select role from [user] where id_user = '{id}'";
                using (SqlDataReader reader = new SqlCommand(command, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                }
            }
            return 0;
        }
    }
}
