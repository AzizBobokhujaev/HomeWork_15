using System;
using System.Data.SqlClient;

namespace ConsoleApp8
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string сonnectionString = @"Data Source=WIN-HFC12JL6G7P\SQLEXPRESS;Initial Catalog=Person;Integrated Security=True";
            SqlConnection sqlConnection = new SqlConnection(сonnectionString);
            sqlConnection.Open();

            if ((sqlConnection.State == System.Data.ConnectionState.Open))
            {
                Console.WriteLine("БД готова к работе.");
            }
            Console.WriteLine("Please select command:\n1. Insert(Добавление)\n2. Select All(Выбрать все)\n3. Select by Id(Выбор по Id)\n4. Update(Обновить каждый столбец по Id)\n5. Delete(Удаление по Id)");
            int a = Convert.ToInt32(Console.ReadLine());
            switch (a)
            {
                case 1: 
                    {
                        Console.Write("LastName: "); string lastName = Console.ReadLine();
                        Console.Write("FirstName: "); string firstName = Console.ReadLine();
                        Console.Write("MiddleName: "); string middleName = Console.ReadLine();
                        Console.Write("BirthDate: "); string birthDate =(Console.ReadLine());
                        
                        InsertUsers(sqlConnection, new Person {LastName=lastName,FirstName=firstName,MiddleName=middleName,BirthDate=birthDate} );
                        
                    }
                    break;
                case 2:
                    {
                        SelectAllUsers(sqlConnection);
                    }
                    break;
                case 3:
                    {
                        Console.Write("Enter ID: ");int id = Convert.ToInt32(Console.ReadLine());
                        SelectByIdUsers(sqlConnection, id);
                    }
                    break;
                case 4:
                    {
                        Console.Write("Id:"); var id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("LastName: "); string lastName = Console.ReadLine();
                        Console.Write("FirstName: "); string firstName = Console.ReadLine();
                        Console.Write("MiddleName: "); string middleName = Console.ReadLine();
                        Console.Write("BirthDate: "); string birthDate = (Console.ReadLine());

                        UpdateUsersById(sqlConnection,id, new Person { LastName = lastName, FirstName = firstName, MiddleName = middleName, BirthDate = birthDate });

                    }
                    break;
                case 5:
                    {
                        Console.Write("ID: "); var id = Convert.ToInt32(Console.ReadLine());
                        DeleteUserBuId(sqlConnection, id);
                    }
                    break;
                default:
                    break;
            }

            sqlConnection.Close();

        }

        private static void DeleteUserBuId(SqlConnection sqlConnection, int id)
        {
            var sqlQuery = $"DELETE PERSON WHERE PERSON.ID = {id}";
            var sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Person deleted succesfuly");
            }
            else
            {
                Console.WriteLine($"Peson with this Id: {id} not found!!!");
            }
        }

        private static void UpdateUsersById(SqlConnection sqlConnection, int id, Person person)
        {
            var sqlQuery = $"UPDATE PERSON SET LASTNAME = '{person.LastName}',FIRSTNAME = '{person.FirstName}',MIDDLENAME = '{person.MiddleName}', BIRTHDATE = {person.BirthDate} WHERE PERSON.ID ={id}";
            var sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var result = sqlCommand.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Update completed succesfuly");
            }
            else
            {
                Console.WriteLine($"Person with this Id: {id} not found!!!");
            }
        }

        private static void SelectByIdUsers(SqlConnection sqlConnection, int id)
        {
            var sqlQuery = $"SELECT PERSON.ID, PERSON.LASTNAME,PERSON.FIRSTNAME,PERSON.MIDDLENAME, PERSON.BIRTHDATE FROM PERSON WHERE PERSON.ID = {id}";
            var sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var sqlReader = sqlCommand.ExecuteReader();
            while (sqlReader.Read())
            {
                Console.WriteLine($"Id:{sqlReader.GetValue(0)}, LastName {sqlReader.GetValue(1)}, FirstName: {sqlReader.GetValue(2)}, MidlleName: {sqlReader.GetValue(3)}, BirthDate: {sqlReader.GetValue(4)}");
            }
            sqlReader.Close();
        }

        private static void SelectAllUsers(SqlConnection sqlConnection)
        {
            var sqlQuery = "SELECT PERSON.ID, PERSON.LASTNAME,PERSON.FIRSTNAME,PERSON.MIDDLENAME, PERSON.BIRTHDATE FROM PERSON";
            var sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var sqlReader = sqlCommand.ExecuteReader();
            while (sqlReader.Read())
            {
                Console.WriteLine($"Id:{sqlReader.GetValue(0)}, LastName {sqlReader.GetValue(1)}, FirstName: {sqlReader.GetValue(2)}, MidlleName: {sqlReader.GetValue(3)}, BirthDate: {sqlReader.GetValue(4)}");
            }
            sqlReader.Close();
        }

        private static void InsertUsers(SqlConnection sqlConnection, Person person)
        {
            var sqlQuery = $"INSERT INTO PERSON (LastName,FirstName,MiddleName, BirthDate) VALUES ('{person.LastName}','{person.FirstName}','{person.MiddleName}','{person.BirthDate}')";
            var sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = sqlQuery;
            var result = sqlCommand.ExecuteNonQuery();
            if (result>0)
            {
                Console.WriteLine("Person added succesfuly");
            }
        }
    }
    public class Person
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string BirthDate { get; set; }
        public Person() { }
        public Person(string lastName, string firstName, string middleName, string birthDate)
        {
            lastName = LastName;
            firstName = FirstName;
            middleName = MiddleName;
            birthDate = BirthDate;
        }
    }
}
