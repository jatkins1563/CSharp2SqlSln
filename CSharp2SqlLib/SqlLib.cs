using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace CSharp2SqlLib
{
    public class SqlLib
    {
        public SqlConnection sqlconn { get; set; }

        public List<User> GetAllUsers()
        {
            var sql = "SELECT * From Users;";
            var sqlcmd = new SqlCommand(sql, sqlconn);
            var sqldatareader = sqlcmd.ExecuteReader();

            var users = new List<User>();
            while (sqldatareader.Read())
            {
                var id = Convert.ToInt32(sqldatareader["Id"]);
                var username = Convert.ToString(sqldatareader["Username"]);
                var password = Convert.ToString(sqldatareader["Password"]);
                var firstname = Convert.ToString(sqldatareader["Firstname"]);
                var lastname = Convert.ToString(sqldatareader["Lastname"]);
                var phone = Convert.ToString(sqldatareader["Phone"]);
                var email = Convert.ToString(sqldatareader["Email"]);
                var isreviewer = Convert.ToBoolean(sqldatareader["IsReviewer"]);
                var isadmin = Convert.ToBoolean(sqldatareader["IsAdmin"]);
                var user = new User()
                {
                    Id = id,
                    Username = username,
                    Password = password,
                    Firstname = firstname,
                    Lastname = lastname,
                    Phone = phone,
                    Email = email,
                    IsReviewer = isreviewer,
                    IsAdmin = isadmin
                };
                users.Add(user);
            }
            sqldatareader.Close();
            return users;
        }
        public User GetByPK(int id)
        {
            var sql = $"SELECT * From Users where Id = {id};";
            var sqlcmd = new SqlCommand(sql, sqlconn);
            var sqldatareader = sqlcmd.ExecuteReader();
            if(!sqldatareader.HasRows)
            {
                sqldatareader.Close();
                return null;
            }
            sqldatareader.Read();
            var user = new User()
            {
                Id = Convert.ToInt32(sqldatareader["Id"]),
                Username = Convert.ToString(sqldatareader["Username"]),
                Password = Convert.ToString(sqldatareader["Password"]),
                Firstname = Convert.ToString(sqldatareader["Firstname"]),
                Lastname = Convert.ToString(sqldatareader["Lastname"]),
                Phone = Convert.ToString(sqldatareader["Phone"]),
                Email = Convert.ToString(sqldatareader["Email"]),
                IsReviewer = Convert.ToBoolean(sqldatareader["IsReviewer"]),
                IsAdmin = Convert.ToBoolean(sqldatareader["IsAdmin"]),
            };
            sqldatareader.Close();
            return user;
        }

        public void Connect()
        {
            var connStr = "server=localhost\\sqlexpress;" +
                            "database=PrsDb;" +
                            "trusted_connection=true;";
            sqlconn = new SqlConnection(connStr);
            sqlconn.Open();
            if (sqlconn.State != System.Data.ConnectionState.Open)
            {
                throw new Exception("Connection string is not correct.");
            }
            Console.WriteLine("Open connection successful!");
        }
        public void Disconnect()
        {
            if (sqlconn == null)
            {
                return;
            }
            sqlconn.Close();
            sqlconn = null;
        }
    }
}
