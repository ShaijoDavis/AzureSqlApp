﻿using AzureSqlApp.Models;
using Microsoft.FeatureManagement;
using System.Data.SqlClient;

namespace AzureSqlApp.Services
{
    public class ProductService : IProductService
    {
        private static string db_source = "webappdemo500dbserver.database.windows.net";
        private static string db_user = "sqladmin";
        private static string db_password = "Sql@1212";
        private static string db_database = "webappdemo500db";

        private readonly IConfiguration _configuration;
        private readonly IFeatureManager _featureManager;
        public ProductService(IConfiguration configuration, IFeatureManager featureManager)
        {
            _configuration = configuration;
            _featureManager = featureManager;
        }


        public async Task<bool> IsBeta()
        {
            if (await _featureManager.IsEnabledAsync("beta"))
                return true;
            else
                return false;
        }

        private SqlConnection GetConnection()
        {
            // fetching data from Azure App configuration
            string SQLConnection = _configuration["SQLConnection"];

            string testDBValue = _configuration.GetConnectionString("TestDB");
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = db_source;
            builder.UserID = db_user;
            builder.Password = db_password;
            // builder.InitialCatalog = db_database;
            try
            {
                builder.InitialCatalog = testDBValue;
            }
            catch (Exception)
            {
                builder.InitialCatalog = db_database;
            }
            return new SqlConnection(builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            SqlConnection conn = GetConnection();
            List<Product> products = new List<Product>();
            string sqlQuery = "select ProductID, ProductName, Quantity from Products";
            conn.Open();
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };
                    products.Add(product);
                }
            }
            conn.Close();
            return products;
        }
    }
}
