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
        public static List<int> User_GetAllIds()
        {
            List<int> data = new List<int>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string command = $"select id_user from [user]";
                using (SqlDataReader reader = new SqlCommand(command, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(reader.GetInt32(0));
                    }
                }
            }
            return data;
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
        public static int User_GetAccess_ByID(int id_user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string command = $"select role from [user] where id_user = '{id_user}'";
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
        public static string[] User_GetAllPublic_By_ID(int id_user)
        {
            string [] data = new string[5];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string command = $"select name_user, name, secondname, surename, role from [user] where id_user = {id_user}";
                using (SqlDataReader reader = new SqlCommand(command, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data[0] = reader.GetString(0);
                        try
                        {
                            data[1] = reader.GetString(1);
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            data[2] = reader.GetString(2);
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            data[3] = reader.GetString(3);
                        }
                        catch (Exception)
                        {
                        }
                        data[4] = reader.GetInt32(4).ToString();
                    }
                }
            }
            return data;
        }
        public static void User_FullUpdate(int id, string login, string name, string secondname, string surename, string role)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string command = $@"update [user] set name_user = '{login}' , name = '{name}', secondname = '{secondname}', surename = '{surename}', role = {role} where id_user = {id}";
                using (SqlCommand comm = new SqlCommand(command, connection))
                    comm.ExecuteNonQuery();
            }
        }
       
        public static List<int> Workshop_GetAllIds()
        {
            List<int> data = new List<int>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string command = $"select id_workshop from [Workshop]";
                using (SqlDataReader reader = new SqlCommand(command, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(reader.GetInt32(0));
                    }
                }
            }
            return data;
        }
        public static string[] Workshop_GetAllPublic_By_ID(int id_workshop)
        {
            string[] data = new string[2];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string command = $"select name_workshop, name_type from [workshop] inner join workshop_type on fk_typeof_workshop = id_type where id_workshop = {id_workshop}";
                using (SqlDataReader reader = new SqlCommand(command, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data[0] = reader.GetString(0);
                        try
                        {
                            data[1] = reader.GetString(1);
                        }
                        catch (Exception)
                        {
                        }       
                    }
                }
            }
            return data;
        }

        public static List<string> WBW_GetShops_ByWorkerID(int id_user)
        {
            List<string> data = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string command = $"select name_workshop from [workers_by_workshops] inner join workshop on fk_workshop = id_workshop where fk_worker = {id_user}";
                using (SqlDataReader reader = new SqlCommand(command, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(reader.GetString(0));
                    }
                }
            }
            return data;
        }
        public static List<string> WBW_GetWorkers_ByShopID(int id_workshop)
        {
            List<string> data = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string command = $"select id_user from [workers_by_workshops] inner join [user] on fk_worker = id_user where fk_workshop = {id_workshop}";
                using (SqlDataReader reader = new SqlCommand(command, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(reader.GetInt32(0).ToString());
                    }
                }
            }
            return data;
        }
        public static List<string> WBW_GetChecklist_ByWorkerID(int id_user)
        {
            List<string> data = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string command = $"select name_attendance_operation, datetime_attendance, name_workshop from [Attendance_log] inner join workshop on fk_workshop = id_workshop  inner join attendance_operation on fk_operation  = id_attendance_operation where fk_worker = {id_user} order by datetime_attendance desc";
                using (SqlDataReader reader = new SqlCommand(command, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(reader.GetDateTime(1).ToShortDateString() + " " + reader.GetDateTime(1).ToShortTimeString() + " - " + reader.GetString(0).Trim() + " - " + reader.GetString(2).Trim());
                    }
                }
            }
            return data;
        }
        public static List<string> WBF_GetFunctions_ByWorkerID(int id_user)
        {
            List<string> data = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string command = $"select name_function, salary from [Workers_By_Functions] inner join worker_function on fk_function  = id_function where fk_worker = {id_user}";
                using (SqlDataReader reader = new SqlCommand(command, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(reader.GetString(0).Trim() + " - " + reader.GetString(1).Trim() + "$");
                    }
                }
            }
            return data;
        }
    }
}
