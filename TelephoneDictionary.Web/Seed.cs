using Npgsql;

namespace TelephoneDictionary.Web
{
    public class Seed
    {
        private static string connectionString = "host=localhost;userid=postgres;password=123456;database=TelephoneDictionary";

        public static void SeedData()
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new NpgsqlCommand("CREATE TABLE if not exists Department (Id SERIAL PRIMARY KEY, Path INTEGER[], Name CHARACTER VARYING(30))", connection))
                {
                    command.ExecuteNonQuery();
                }

                using (var command = new NpgsqlCommand(
                           "CREATE TABLE if not exists Employee(Id SERIAL PRIMARY KEY, FirstName CHARACTER VARYING(30), LastName CHARACTER VARYING(30), Patronymic CHARACTER VARYING(30), MobilePhone CHARACTER VARYING(30) UNIQUE, Phone CHARACTER VARYING(30) UNIQUE,DepartmentId INTEGER,FOREIGN KEY(DepartmentId) REFERENCES Department(Id) ON DELETE CASCADE);",
                           connection))
                {
                    command.ExecuteNonQuery();
                }


            }
        }
            
        

    }
}
