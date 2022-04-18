using System.Reflection.Metadata.Ecma335;
using Npgsql;
using TelephoneDictionary.Web.Models;

namespace TelephoneDictionary.Web.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {

        private string connectionString = "host=localhost;userid=postgres;password=123456;database=TelephoneDictionary";
        internal NpgsqlConnection Connection => new NpgsqlConnection(connectionString);

        /// <summary>
        /// Вывод всех отделов
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Department> GetAll()
        {
            List<Department> list = new List<Department>();
            using (NpgsqlConnection dbConnection = Connection)
            {
                dbConnection.Open();
                using (var command = new NpgsqlCommand($"select * from department ORDER BY path", dbConnection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Department department = new Department
                        {
                            Id = reader.GetInt32(0), 
                            Name = reader.GetString(2), 
                            Path = reader.GetFieldValue<int[]>(1)
                        };
                        list.Add(department);
                    }
                    reader.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// Вывод дочерних отделов
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Department> GetAllChildDepartments(int id)
        {
            List<Department> list = new List<Department>();
            using (NpgsqlConnection dbConnection = Connection)
            {
                dbConnection.Open();
                using (var command = new NpgsqlCommand($"SELECT * FROM department WHERE path && ARRAY[{id}] ORDER BY array_length(path, 1);", dbConnection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Department department = new Department
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(2),
                            Path = reader.GetFieldValue<int[]>(1)
                        }; 
                        list.Add(department);
                    }
                    reader.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// Вывод определенного отдела
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Department GetDepartmentById(int id)
        {
            Department department = new();
            using (NpgsqlConnection dbConnection = Connection)
            {
                dbConnection.Open();
                using (var command =
                       new NpgsqlCommand(
                           $"SELECT * FROM department WHERE id = {id}",
                           dbConnection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        department = new Department
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(2),
                            Path = reader.GetFieldValue<int[]>(1)
                        };
                    }

                    reader.Close();
                }
            }
            return department;
        }

        /// <summary>
        /// Создание отдела/под-отдела
        /// </summary>
        /// <param name="department"></param>
        /// <param name="id"></param>
        public void CreateDepartment(Department department, int id)
        {
            if (department == null) throw new ArgumentNullException(nameof(department));
            using (NpgsqlConnection dbConnection = Connection)
            {
                dbConnection.Open();
                using (var command = new NpgsqlCommand($"INSERT INTO department (name, path) values('{department.Name}',(SELECT path FROM department WHERE id = {id}) || (select currval('department_id_seq')::integer)) ; ", dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Изменение названия отдела 
        /// </summary>
        /// <param name="department"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void UpdateDepartment(Department department)
        {
            if(department == null) throw new ArgumentNullException(nameof(department));
            using (NpgsqlConnection dbConnection = Connection)
            {
                dbConnection.Open();
                using (var command = new NpgsqlCommand($"UPDATE Department SET name = '{department.Name}' WHERE id = {department.Id}", dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }

        }

        /// <summary>
        /// Удаление отдела вместе с под-отделами
        /// </summary>
        /// <param name="id"></param>
        public void RemoveDepartment(int id)
        {
            using (NpgsqlConnection dbConnection = Connection)
            {
                dbConnection.Open();
                using (var command = new NpgsqlCommand($"DELETE FROM department WHERE path && ARRAY[{id}]", dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
