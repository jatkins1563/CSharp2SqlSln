using CSharp2SqlLib;
using System;

namespace CSharp2Sql
{
    class Program
    {
        static void Main(string[] args)
        {

            var sqlconn = new Connection("localhost\\sqlexpress", "PrsDb");

            var productsController = new ProductsController(sqlconn);
            //var success = productsController.Create(newProduct, "DEM");
            
            var vendorsController = new VendorsController(sqlconn);
            var vendors = vendorsController.GetAll();

            var products = productsController.GetAll();
            var product = productsController.GetByPK(5);
            
            //var sqllib = new SqlLib();
            //sqllib.Connect();

            //var user = sqllib.GetByPK(9);
            //var deleteSuccess = sqllib.Delete(user);

            //var user = sqllib.GetByPK(7);
            //user.Phone = "513-555-1212";
            //var success = sqllib.Change(user);



            //var newUser = new User()
            //{
            //    Id = 0,
            //    Username = "XYY",
            //    Password = "XYZ",
            //    Firstname = "XYZ",
            //    Lastname = "XYZ",
            //    Phone = "XYZ",
            //    Email = "XYZ",
            //    IsReviewer = true,
            //    IsAdmin = true
            //};
            //var createSuccess = sqllib.Create(newUser);

            //var users = sqllib.GetAllUsers();
            //var nulluser = sqllib.GetByPK(0);

            //sqllib.Disconnect();
        }
    }
}
