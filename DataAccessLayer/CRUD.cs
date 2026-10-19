
using System;
using System.Collections.Generic;
using Models;
using Npgsql;

namespace DataAccessLayer
{
    public class CRUD
    {
        // READ ALL PRODUCTS
        public static List<ProductModel> GetAllData()
        {
            const string Query = @"
                SELECT ""PId"", ""ProductName"", ""Price""
                FROM ""Products""
                ORDER BY ""PId""";

            List<ProductModel> products = new List<ProductModel>();

            using (NpgsqlConnection connection = DBHelper.GetConnection())
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(Query, connection))
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductModel model = new ProductModel
                        {
                            PId = Convert.ToInt32(reader["PId"]),
                            ProductName = reader["ProductName"].ToString() ?? "",
                            Price = Convert.ToInt32(reader["Price"])
                        };

                        products.Add(model);
                    }
                }
            }

            return products;
        }

        // READ PRODUCT BY ID
        public static ProductModel? GetDataById(int id)
        {
            const string Query = @"
                SELECT ""PId"", ""ProductName"", ""Price""
                FROM ""Products""
                WHERE ""PId"" = @PId";

            using (NpgsqlConnection connection = DBHelper.GetConnection())
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@PId", id);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ProductModel
                            {
                                PId = Convert.ToInt32(reader["PId"]),
                                ProductName = reader["ProductName"].ToString() ?? "",
                                Price = Convert.ToInt32(reader["Price"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        // INSERT PRODUCT
        public static bool InsertData(ProductModel model)
        {
            const string Query = @"
                INSERT INTO ""Products"" (""ProductName"", ""Price"")
                VALUES (@ProductName, @Price)";

            using (NpgsqlConnection connection = DBHelper.GetConnection())
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@ProductName",
                        (object?)model.ProductName ?? DBNull.Value);

                    command.Parameters.AddWithValue("@Price", model.Price);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        // UPDATE PRODUCT
        public static bool UpdateData(ProductModel model)
        {
            const string Query = @"
                UPDATE ""Products""
                SET ""ProductName"" = @ProductName,
                    ""Price"" = @Price
                WHERE ""PId"" = @PId";

            using (NpgsqlConnection connection = DBHelper.GetConnection())
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@PId", model.PId);

                    command.Parameters.AddWithValue(
                        "@ProductName",
                        (object?)model.ProductName ?? DBNull.Value);

                    command.Parameters.AddWithValue("@Price", model.Price);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        // DELETE PRODUCT
        public static bool DeleteData(int id)
        {
            const string Query = @"
                DELETE FROM ""Products""
                WHERE ""PId"" = @PId";

            using (NpgsqlConnection connection = DBHelper.GetConnection())
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand(Query, connection))
                {
                    command.Parameters.AddWithValue("@PId", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
    }
}
