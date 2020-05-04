namespace PrintAllMinionNames
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    public class Startup
    {
        private static readonly string connectionString = @"Server=CODINGMANIA\SQLEXPRESS;Database=MinionsDB;Integrated Security=true;";

        public static void Main()
        {
            var connection = new SqlConnection(connectionString);

            connection.Open();

            var names = new List<string>();

            using (connection)
            {
                var queryText = "SELECT Name FROM Minions";

                using var selectNamesCmd = new SqlCommand(queryText, connection);

                var reader = selectNamesCmd.ExecuteReader();

                while (reader.Read())
                {
                    names.Add((string)reader["Name"]);
                }
            }

            while (names.Any())
            {
                Console.WriteLine(names[0]);
                names.RemoveAt(0);

                if (names.Any())
                {
                    Console.WriteLine(names.Last());
                    names.RemoveAt(names.Count() - 1);
                }
            }
        }
    }
}
