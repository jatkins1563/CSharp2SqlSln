using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp2SqlLib
{
    public class VendorsController
    {
        private static Connection connection { get; set; }

        private Vendor FillVendorFromSqlRow(SqlDataReader reader)
        {
            var vendor = new Vendor()
            {
                Id = Convert.ToInt32(reader["Id"]),
                Code = Convert.ToString(reader["Code"]),
                Name = Convert.ToString(reader["Name"]),
                Address = Convert.ToString(reader["Address"]),
                City = Convert.ToString(reader["City"]),
                State = Convert.ToString(reader["State"]),
                Zip = Convert.ToString(reader["Zip"]),
                Phone = Convert.ToString(reader["Phone"]),
                Email = Convert.ToString(reader["Email"])
            };
            return vendor;
        }

        private int AddWithValue(Vendor vendor, SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@code", vendor.Code);
            cmd.Parameters.AddWithValue("@name", vendor.Name);
            cmd.Parameters.AddWithValue("@address", vendor.Address);
            cmd.Parameters.AddWithValue("@city", vendor.City);
            cmd.Parameters.AddWithValue("@state", vendor.State);
            cmd.Parameters.AddWithValue("@zip", vendor.Zip);
            cmd.Parameters.AddWithValue("@phone", vendor.Phone);
            cmd.Parameters.AddWithValue("@email", vendor.Email);
            return cmd.ExecuteNonQuery();
        }

        public List<Vendor> GetAll()
        {
            var sql = "SELECT * From Vendors;";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            var reader = cmd.ExecuteReader();
            var vendors = new List<Vendor>();
            while(reader.Read())
            {
                var vendor = FillVendorFromSqlRow(reader);
                vendors.Add(vendor);
            }
            reader.Close();
            return vendors;
        }

        public Vendor GetByPK(int id)
        {
            var sql = $"SELECT * From Vendors where Id = {id};";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            var reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                return null;
            }
            reader.Read();
            var vendor = FillVendorFromSqlRow(reader);
            reader.Close();
            return vendor;
        }

        public Vendor GetByCode(string code)
        {
            var sql = $"SELECT * From Vendors where Code = @code;";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            cmd.Parameters.AddWithValue("@code", code);
            var reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                reader.Close();
                return null;
            }
            reader.Read();
            var vendor = FillVendorFromSqlRow(reader);
            reader.Close();
            return vendor;
        }

        public bool Create(Vendor vendor)
        {
            var sql = $"INSERT into Vendors " +
                $" (Code, Name, Address, City," +
                $" State, Zip, Phone, Email) VALUES " +
                $"(@code, @name, @address, @city, " +
                $"@state, @zip, @phone, @email)";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            var rowsAffected = AddWithValue(vendor, cmd);

            return (rowsAffected == 1);
        }

        public bool Change(Vendor vendor)
        {
            var sql = $"UPDATE Vendors Set " +
                $"Code = @code, " +
                $"Name = @name, " +
                $"Address = @address, " +
                $"City = @city, " +
                $"State = @state, " +
                $"Zip = @zip " +
                $"Phone = @phone, " +
                $"Email = @email, " +
                $"Where Id = @id;";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            cmd.Parameters.AddWithValue("@id", vendor.Id);
            var rowsAffected = AddWithValue(vendor, cmd);

            return (rowsAffected == 1);
        }

        public bool Delete(Vendor vendor)
        {
            var sql = $"DELETE from Vendors " +
                $"Where Id = @id;";
            var sqlcmd = new SqlCommand(sql, connection.SqlConn);
            sqlcmd.Parameters.AddWithValue("@id", vendor.Id);
            var rowsAffected = sqlcmd.ExecuteNonQuery();

            return (rowsAffected == 1);
        }

        public VendorsController(Connection connection)
        {
            VendorsController.connection = connection;
        }
    }
}
