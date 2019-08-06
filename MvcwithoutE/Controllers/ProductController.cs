using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using MvcwithoutE.Models;

namespace MvcwithoutE.Controllers
{
    public class ProductController : Controller
    {
        string connectionString = @"Data Source=SAFWAN-PC;Initial Catalog=Mvcproject;User ID=sa;Password=123";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM Mvcwef", sqlcon);
                sqlda.Fill(dtblProduct);
            }
                return View(dtblProduct);
        }

 

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProductModel());
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel productModel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "INSERT INTO Mvcwef VALUES(@ProductName,@Price,@Count)";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@ProductName", productModel.productName);
                cmd.Parameters.AddWithValue("@Price", productModel.price);
                cmd.Parameters.AddWithValue("@Count", productModel.Count);
                cmd.ExecuteNonQuery();
            }
                return RedirectToAction("Index");
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            ProductModel productModel = new ProductModel();
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "SELECT * FROM Mvcwef where productID=@ProductID";
                SqlDataAdapter sqlda = new SqlDataAdapter(query, sqlcon);
                sqlda.SelectCommand.Parameters.AddWithValue("@ProductID", id);
                sqlda.Fill(dtblProduct);
            }
            if (dtblProduct.Rows.Count == 1)
            {
                productModel.ProductID = Convert.ToInt32(dtblProduct.Rows[0][0].ToString());
                productModel.productName = dtblProduct.Rows[0][1].ToString();
                productModel.price = Convert.ToDecimal(dtblProduct.Rows[0][2].ToString());
                productModel.Count = Convert.ToInt32(dtblProduct.Rows[0][3].ToString());
                return View(productModel);
            }
            else
                return RedirectToAction("Index");
                
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductModel productModel)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "UPDATE Mvcwef SET ProductName=@ProductName,Price=@price,Count=@Count where Product=@Product";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@ProductID", productModel.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", productModel.productName);
                cmd.Parameters.AddWithValue("@Price", productModel.price);
                cmd.Parameters.AddWithValue("@Count", productModel.Count);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlcon = new SqlConnection(connectionString))
            {
                sqlcon.Open();
                string query = "Delete from Mvcwef where Product=@Product";
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                cmd.Parameters.AddWithValue("@ProductID", id);
               
            }
            return RedirectToAction("Index");
        }
    }
}
