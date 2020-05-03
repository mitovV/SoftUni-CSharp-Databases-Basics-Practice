namespace IncreaseAgeStored
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class Startup
    {
        private static readonly string connectionString = @"Server=CODINGMANIA\SQLEXPRESS;Database=MinionsDB;Integrated Security=true;";

        public static void Main()
        {
            var connection = new SqlConnection(connectionString);

            connection.Open();

            using (connection)
            {
                var id = int.Parse(Console.ReadLine());
                var queryText = "usp_GetOlder";

                using var cmd = new SqlCommand(queryText, connection);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();

                queryText = "SELECT Name, Age FROM Minions WHERE Id = @Id";

                using var selectCmd = new SqlCommand(queryText, connection);

                selectCmd.Parameters.AddWithValue("@Id", id);

                using var reader = selectCmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Name"]} – {reader["Age"]} years old");
                }
            }
        }
    }
}
