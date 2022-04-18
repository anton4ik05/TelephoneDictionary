using Npgsql;
using TelephoneDictionary.Web.Models;

namespace TelephoneDictionary.Web.Repositories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        
        private string connectionString = "host=localhost;userid=postgres;password=123456;database=TelephoneDictionary";
        internal NpgsqlConnection Connection => new NpgsqlConnection(connectionString);

        /// <summary>
        /// Вывод всех сотрудников
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Employee> GetAll()
        {
            List<Employee> list = new List<Employee>();
            using (NpgsqlConnection dbConnection = Connection)
            {
                dbConnection.Open();
                using (var command = new NpgsqlCommand($"select * from employee", dbConnection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Patronymic = reader.GetString(3),
                            MobilePhone = reader.GetString(4),
                            Phone = reader.GetString(5),
                            DepartmentId = reader.GetInt32(6)
                        };
                        list.Add(employee);
                    }
                    reader.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// Сотрудник по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Employee GetEmployeeById(int id)
        {
            Employee employee = new();
            using (NpgsqlConnection dbConnection = Connection)
            {
                dbConnection.Open();
                using (var command =
                       new NpgsqlCommand(
                           $"SELECT * FROM employee WHERE id = {id}",
                           dbConnection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        employee = new Employee
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Patronymic = reader.GetString(3),
                            MobilePhone = reader.GetString(4),
                            Phone = reader.GetString(5),
                            DepartmentId = reader.GetInt32(6)
                        };
                    }
                    reader.Close();
                }
            }
            return employee;
        }

        /// <summary>
        /// Вывод всех сотрудников отдела
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Employee> GetAllByDepartmentId(int id)
        {
            List<Employee> list = new List<Employee>();
            using (NpgsqlConnection dbConnection = Connection)
            {
                dbConnection.Open();
                using (var command = new NpgsqlCommand($"select * from employee where departmentId = {id}", dbConnection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Patronymic = reader.GetString(3),
                            MobilePhone = reader.GetString(4),
                            Phone = reader.GetString(5),
                            DepartmentId = reader.GetInt32(6)
                        };
                        list.Add(employee);
                    }
                    reader.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// Поиск сотрудников
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Employee> SearchEmployees(string str)
        {
            List<Employee> list = new List<Employee>();
            using (NpgsqlConnection dbConnection = Connection)
            {
                dbConnection.Open();
                using (var command = new NpgsqlCommand($"select * from employee WHERE lower(FirstName) LIKE '%{str}%' OR lower(LastName) LIKE '%{str}%' OR lower(patronymic) LIKE '%{str}%' OR mobilephone LIKE '%{str}%' OR phone LIKE '%{str}%'", dbConnection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            Id = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Patronymic = reader.GetString(3),
                            MobilePhone = reader.GetString(4),
                            Phone = reader.GetString(5),
                            DepartmentId = reader.GetInt32(6)
                        };
                        list.Add(employee);
                    }
                    reader.Close();
                }
            }
            return list;
        }

        /// <summary>
        /// Создание сотрудника
        /// </summary>
        /// <param name="employee"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void CreateEmployee(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));
            using (NpgsqlConnection dbConnection = Connection)
            {
                dbConnection.Open();
                using (var command = new NpgsqlCommand($"insert into Employee (FirstName, LastName, Patronymic, MobilePhone, Phone, DepartmentId) values ('{employee.FirstName}', '{employee.LastName}', '{employee.Patronymic}','{employee.MobilePhone}','{employee.Phone}',{employee.DepartmentId})", dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Редактирование сотрудника
        /// </summary>
        /// <param name="employee"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void UpdateEmployee(Employee employee)
        {
            if (employee == null) throw new ArgumentNullException(nameof(employee));
            using (NpgsqlConnection dbConnection = Connection)
            {
                dbConnection.Open();
                using (var command = new NpgsqlCommand($"UPDATE Employee SET FirstName = '{employee.FirstName}', LastName = '{employee.LastName}', Patronymic = '{employee.Patronymic}', MobilePhone = '{employee.MobilePhone}', Phone = '{employee.Phone}', DepartmentId = {employee.DepartmentId}  WHERE id = {employee.Id}", dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="id"></param>
        public void RemoveEmployee(int id)
        {
            using (NpgsqlConnection dbConnection = Connection)
            {
                dbConnection.Open();
                using (var command = new NpgsqlCommand($"DELETE FROM employee WHERE id = {id}", dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
