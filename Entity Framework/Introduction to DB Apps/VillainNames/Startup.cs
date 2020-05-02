namespace VillainNames
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
                try
                {
                    var queryText = @"SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  
                                        FROM Villains AS v
                                        JOIN MinionsVillains AS mv ON v.Id = mv.VillainId
                                    GROUP BY v.Id, v.Name
                                      HAVING COUNT(mv.VillainId) > 3
                                    ORDER BY COUNT(mv.VillainId)";

                    using var selectCmd = new SqlCommand(queryText, connection);

                    var reader = selectCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]} - {reader["MinionsCount"]}");
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }
        }
    }
}
