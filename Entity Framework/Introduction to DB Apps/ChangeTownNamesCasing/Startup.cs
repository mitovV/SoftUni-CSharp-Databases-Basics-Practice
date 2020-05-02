namespace ChangeTownNamesCasing
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    class Startup
    {
        private static string connectionString = @"Server=CODINGMANIA\SQLEXPRESS;Database=MinionsDB;Integrated Security=true;";

        static void Main()
        {
            var connection = new SqlConnection(connectionString);

            connection.Open();

            using (connection)
            {
                var coutryName = Console.ReadLine();

                var queryText = @"UPDATE Towns
                                     SET Name = UPPER(Name)
                                   WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @countryName)";

                using var cmd = new SqlCommand(queryText, connection);

                cmd.Parameters.AddWithValue("@countryName", coutryName);

                var count = cmd.ExecuteNonQuery();

                if (count == 0)
                {
                    Console.WriteLine("No town names were affected.");
                }
                else
                {
                    Console.WriteLine($"{count} town names were affected");

                    queryText = @"SELECT t.Name 
                                 FROM Towns as t
                                 JOIN Countries AS c 
                                   ON c.Id = t.CountryCode
                                WHERE c.Name = @countryName";

                    using var townsCmd = new SqlCommand(queryText, connection);

                    townsCmd.Parameters.AddWithValue("@countryName", coutryName);

                    using var reader = townsCmd.ExecuteReader();

                    var towns = new List<string>();

                    while (reader.Read())
                    {
                        towns.Add((string)reader["Name"]);
                    }

                    Console.WriteLine($"[{string.Join(", ", towns)}]");
                }
            }
        }
    }
}
