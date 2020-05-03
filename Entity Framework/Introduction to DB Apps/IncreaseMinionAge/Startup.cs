namespace IncreaseMinionAge
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;

    public class Startup
    {
        private static readonly string connectionString = @"Server=CODINGMANIA\SQLEXPRESS;Database=MinionsDB;Integrated Security=true;";

        public static void Main()
        {
            var connection = new SqlConnection(connectionString);

            connection.Open();

            using (connection)
            {
                var minionIDs = Console.ReadLine()
                                       .Split(" ")
                                       .Select(int.Parse)
                                       .ToArray();


                foreach (var id in minionIDs)
                {
                    var query = @"UPDATE Minions
                                         SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1
                                       WHERE Id = @Id";

                    using var cmd = new SqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.ExecuteNonQuery();
                }

                var queryText = "SELECT Name, Age FROM Minions";

                using var minionsCmd = new SqlCommand(queryText, connection);

                var reader = minionsCmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Name"]} {reader["Age"]}");
                }
            }
        }
    }
}
