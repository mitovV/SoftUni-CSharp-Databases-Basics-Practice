namespace AddMinion
{
    using System;
    using System.Data.SqlClient;

    public class Startup
    {
        private static string connectionString = @"Server=CODINGMANIA\SQLEXPRESS;Database=MinionsDB;Integrated Security=true;";

        public static void Main()
        {
            var connetion = new SqlConnection(connectionString);

            connetion.Open();

            try
            {
                using (connetion)
                {
                    var minionInfo = Console.ReadLine().Split("Minion: ")[1].Split(" ");
                    var villainName = Console.ReadLine().Split("Villain: ")[1];

                    var name = minionInfo[0];
                    var age = int.Parse(minionInfo[1]);
                    var townName = minionInfo[2];

                    var queryText = "SELECT Id FROM Towns WHERE Name = @townName";

                    using var townIdCmd = new SqlCommand(queryText, connetion);

                    townIdCmd.Parameters.AddWithValue("@townName", townName);

                    var townId = townIdCmd.ExecuteScalar();

                    if (townId is null)
                    {
                        queryText = "INSERT INTO Towns (Name) VALUES (@townName)";

                        using var insertTownCmd = new SqlCommand(queryText, connetion);

                        insertTownCmd.Parameters.AddWithValue("@townName", townName);

                        insertTownCmd.ExecuteNonQuery();

                        Console.WriteLine($"Town {townName} was added to the database.");

                        townId = townIdCmd.ExecuteScalar();
                    }

                    queryText = "SELECT Id FROM Villains WHERE Name = @Name";

                    using var villainCmd = new SqlCommand(queryText, connetion);

                    villainCmd.Parameters.AddWithValue("@Name", villainName);

                    var villainId = villainCmd.ExecuteScalar();

                    if (villainId is null)
                    {
                        queryText = "INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4)";

                        using var insertVillainCmd = new SqlCommand(queryText, connetion);

                        insertVillainCmd.Parameters.AddWithValue("@villainName", villainName);

                        insertVillainCmd.ExecuteNonQuery();

                        Console.WriteLine($"Villain {villainName} was added to the database.");

                        villainId = villainCmd.ExecuteScalar();
                    }

                    queryText = "INSERT INTO Minions (Name, Age, TownId) VALUES (@name, @age, @townId)";

                    using var insertMinionCmd = new SqlCommand(queryText, connetion);

                    insertMinionCmd.Parameters.AddWithValue("@name", name);
                    insertMinionCmd.Parameters.AddWithValue("@age", age);
                    insertMinionCmd.Parameters.AddWithValue("@townId", (int)townId);

                    insertMinionCmd.ExecuteNonQuery();

                    queryText = "SELECT Id FROM Minions WHERE Name = @Name";

                    using var minionIdCmd = new SqlCommand(queryText, connetion);

                    minionIdCmd.Parameters.AddWithValue("@Name", name);

                    var minionId = minionIdCmd.ExecuteScalar();

                    queryText = "INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@villainId, @minionId)";

                    using var insertIntoMinionsVillainsCmd = new SqlCommand(queryText, connetion);

                    insertIntoMinionsVillainsCmd.Parameters.AddWithValue("@villainId", (int)villainId);
                    insertIntoMinionsVillainsCmd.Parameters.AddWithValue("@minionId", (int)minionId);

                    insertIntoMinionsVillainsCmd.ExecuteNonQuery();

                    Console.WriteLine($"Successfully added {name} to be minion of {villainName}.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); ;
            }
        }
    }
}
