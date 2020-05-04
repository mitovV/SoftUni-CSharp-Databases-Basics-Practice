namespace RemoveVillain
{
    using System;
    using System.Data.SqlClient;

    public class Startup
    {
        private static readonly string ConnectionString = @"Server=CODINGMANIA\SQLEXPRESS;Database=MinionsDB;Integrated Security=true;";
        private static readonly SqlConnection Connection = new SqlConnection(ConnectionString);
        private static SqlTransaction transaction;

        public static void Main()
        {
            var id = int.Parse(Console.ReadLine());

            Connection.Open();

            using (Connection)
            {
                transaction = Connection.BeginTransaction();

                try
                {
                    var command = new SqlCommand
                    {
                        Connection = Connection,
                        Transaction = transaction,
                        CommandText = "SELECT Name FROM Villains WHERE Id = @villainId"
                    };
                    command.Parameters.AddWithValue("@villainId", id);

                    var value = command.ExecuteScalar();

                    if (value is null)
                    {
                        throw new ArgumentNullException(null, "No such villain was found.");
                    }

                    var villainName = (string)value;

                    command.CommandText = @"DELETE 
                                              FROM MinionsVillains 
                                             WHERE VillainId = @villainId";

                    var minionsDeleted = command.ExecuteNonQuery();

                    command.CommandText = @"DELETE 
                                              FROM Villains
                                             WHERE Id = @villainId";

                    command.ExecuteNonQuery();

                    transaction.Commit();
                    Console.WriteLine($"{villainName} was deleted.");
                    Console.WriteLine($"{minionsDeleted} minions were released.");
                }
                catch (Exception e)
                {
                    try
                    {
                        Console.WriteLine(e.Message);
                        transaction.Rollback();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
