using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace CSharp2SqlLib
{
    public class SqlLib
    {
        public SqlConnection sqlconn { get; set; }

        private User FillUserFromSqlRow(SqlDataReader reader)
        {
            var user = new User()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Username = Convert.ToString(reader["Username"]),
                Password = Convert.ToString(reader["Password"]),
                Firstname = Convert.ToString(reader["Firstname"]),
                Lastname = Convert.ToString(reader["Lastname"]),
                Phone = Convert.ToString(reader["Phone"]),
                Email = Convert.ToString(reader["Email"]),
                IsReviewer = Convert.ToBoolean(reader["IsReviewer"]),
                IsAdmin = Convert.ToBoolean(reader["IsAdmin"]),
            };
            return user;
        }

        public bool Change(User user)
        {
            var sql = $"UPDATE Users Set " +
                $"Username = @username, " +
                $"Password = @password, " +
                $"Firstname = @firstname, " +
                $"Lastname = @lastname, " +
                $"Phone = @phone, " +
                $"Email = @email, " +
                $"IsReviewer = @isreviewer, " +
                $"IsAdmin = @isadmin " +
                $"Where Id = @id;";
            var sqlcmd = new SqlCommand(sql, sqlconn);
            sqlcmd.Parameters.AddWithValue("@id", user.Id);
            sqlcmd.Parameters.AddWithValue("@username", user.Username);
            sqlcmd.Parameters.AddWithValue("@password", user.Password);
            sqlcmd.Parameters.AddWithValue("@firstname", user.Firstname);
            sqlcmd.Parameters.AddWithValue("@lastname", user.Lastname);
            sqlcmd.Parameters.AddWithValue("@phone", user.Phone);
            sqlcmd.Parameters.AddWithValue("@email", user.Email);
            sqlcmd.Parameters.AddWithValue("@isreviewer", user.IsReviewer);
            sqlcmd.Parameters.AddWithValue("@isadmin", user.IsAdmin);
            var rowsAffected = sqlcmd.ExecuteNonQuery();

            return (rowsAffected == 1);
        }

        public bool Delete(User user)
        {
            var sql = $"DELETE from Users " +
                $"Where Id = @id;";
            var sqlcmd = new SqlCommand(sql, sqlconn);
            sqlcmd.Parameters.AddWithValue("@id", user.Id);
            var rowsAffected = sqlcmd.ExecuteNonQuery();

            return (rowsAffected == 1);
        }

        public bool CreateMultiple(List<User> users)
        {
            var success = true;
            foreach(var user in users)
            {
                success = success && Create(user);
            }
            return success;
        }

        public bool Create(User user)
        {
            var sql = $"INSERT into Users " +
                $" (Username, Password, Firstname, Lastname," +
                $" Phone, Email, IsReviewer, IsAdmin) VALUES " +
                $"(@username, @password, @firstname, @lastname, " +
                $"@phone, @email, @isreviewer, @isadmin)";
            var sqlcmd = new SqlCommand(sql, sqlconn);
            sqlcmd.Parameters.AddWithValue("@username", user.Username);
            sqlcmd.Parameters.AddWithValue("@password", user.Password);
            sqlcmd.Parameters.AddWithValue("@firstname", user.Firstname);
            sqlcmd.Parameters.AddWithValue("@lastname", user.Lastname);
            sqlcmd.Parameters.AddWithValue("@phone", user.Phone);
            sqlcmd.Parameters.AddWithValue("@email", user.Email);
            sqlcmd.Parameters.AddWithValue("@isreviewer", user.IsReviewer);
            sqlcmd.Parameters.AddWithValue("@isadmin", user.IsAdmin);
            var rowsAffected = sqlcmd.ExecuteNonQuery();

            return (rowsAffected == 1);
        }

        public List<User> GetAllUsers()
        {
            var sql = "SELECT * From Users;";
            var sqlcmd = new SqlCommand(sql, sqlconn);
            var reader = sqlcmd.ExecuteReader();

            var users = new List<User>();
            while (reader.Read())
            {
                var user = FillUserFromSqlRow(reader);
                users.Add(user);
            }
            reader.Close();
            return users;
        }
        public User GetByPK(int id)
        {
            var sql = $"SELECT * From Users where Id = {id};";
            var sqlcmd = new SqlCommand(sql, sqlconn);
            var reader = sqlcmd.ExecuteReader();
            if(!reader.HasRows)
            {
                reader.Close();
                return null;
            }
            reader.Read();
            var user = FillUserFromSqlRow(reader);
            reader.Close();
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
