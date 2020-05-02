namespace MinionNames
{
    using System;
    using System.Data.SqlClient;

    class Startup
    {
        private static readonly string connectionString = @"Server=CODINGMANIA\SQLEXPRESS;Database=MinionsDB;Integrated Security=true;";

        static void Main()
        {
            var connection = new SqlConnection(connectionString);

            connection.Open();

            using (connection)
            {
                var id = int.Parse(Console.ReadLine());

                var queryText = "SELECT Name FROM Villains WHERE Id = @Id";

                using var selectCmd = new SqlCommand(queryText, connection);
                selectCmd.Parameters.AddWithValue("@Id", id);

                var villain = selectCmd.ExecuteScalar();

                if (villain != null)
                {
                    Console.WriteLine($"Villain: {villain}");
                }
                else
                {
                    Console.WriteLine($"No villain with ID {id} exists in the database.");
                    return;
                }

                queryText = @"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                     m.Name, 
                                     m.Age
                                FROM MinionsVillains AS mv
                                JOIN Minions As m ON mv.MinionId = m.Id
                               WHERE mv.VillainId = @Id
                            ORDER BY m.Name";

                using var getMinionsCmd = new SqlCommand(queryText, connection);
                getMinionsCmd.Parameters.AddWithValue("@Id", id);

                using var reader = getMinionsCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    var counter = 1;

                    while (reader.Read())
                    {

                        Console.WriteLine($"{counter++}. {reader["Name"]} {reader["Age"]}");
                    }
                }
                else
                {
                    Console.WriteLine("(no minions)");
                }
            }
        }
    }
}
