using System;
using System.Data;
using System.Data.SqlClient;

namespace ADO.net_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=91.201.72.213;Initial Catalog=RGMyDb;User ID=study;Password=study;";

            string productCount = "SELECT count(*) FROM Product";
            string newCategory = "INSERT INTO Category (Name) VALUES ('Обувь')";
            string newProduct = "INSERT INTO Product (Name, CategoryId) VALUES ('Теплые ботинки', 3)";
            string updateProduct = "UPDATE Product SET Name = 'Красная кепка' WHERE Name = 'Кепка'";
            string deleteProduct = "DELETE FROM Product WHERE Name='Штаны теплые'";
            string read = "SELECT Product.Id, Product.Name, Category.Name FROM Product INNER JOIN Category ON Category.Id = Product.CategoryId";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var getProductCount = new SqlCommand(productCount, connection))
                {
                    var count = Convert.ToByte(getProductCount.ExecuteScalar());
                    Console.WriteLine(count);
                }

                using (var addNewCategory = new SqlCommand(newCategory, connection))
                {
                    addNewCategory.ExecuteNonQuery();
                }

                using (var addNewProduct = new SqlCommand(newProduct, connection))
                {
                    addNewProduct.ExecuteNonQuery();
                }

                using (var update = new SqlCommand(updateProduct, connection))
                {
                    update.ExecuteNonQuery();
                }

                using (var command = new SqlCommand(deleteProduct, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (var commandRead = new SqlCommand(read, connection))
                {
                    var reader = commandRead.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("id: " + reader.GetInt32(0) + "; Name: " + reader.GetString(1) + "; Category: " + reader.GetString(2));
                    }
                    reader.Close();
                }

                using (var commandAdapter = new SqlCommand(read, connection))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(commandAdapter);

                    var ds = new DataSet();

                    adapter.Fill(ds);

                    foreach (DataTable table in ds.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            Console.Write("id = ");
                            foreach (DataColumn column in table.Columns)
                            {
                                Console.Write(row[column] + " ");
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
    }
}
