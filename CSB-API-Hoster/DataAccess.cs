using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;
using Dapper;

namespace CSB_API_Hoster
{
    public static class DataAccess
    {
        private readonly static string _dbFileName = "data.dat";
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        
        public static bool InitializeDatabase()
        {
            if (IsThisFirstTime)
            {
                Console.WriteLine("First time usage detected.");

                #region Create Databse

                SQLiteConnection.CreateFile(_dbFileName);
                Console.WriteLine("Database Created.");

                #endregion

                using (IDbConnection Conn = DbConnection)
                {
                    Conn.Open();

                    try
                    {
                        string query = @"CREATE TABLE `Users` (
                                        `Id`	INTEGER PRIMARY KEY AUTOINCREMENT,
                                        `FirstName`	TEXT NOT NULL,
                                        `LastName`	TEXT NOT NULL,
                                        `Gender`	TEXT NOT NULL,
                                        `Mobile`	TEXT NOT NULL )";

                        Conn.Execute(query);

                        new UserRepository().AddNewUser(new UserVM() { FirstName = "Chathuranga", LastName = "Basnayake", Gender = "M", Mobile = "077xxxxxxx" });

                        Console.WriteLine("Users Table created and seeded.");
                    }
                    catch (Exception ex)
                    {
                        Conn.Close();
                        Console.WriteLine("Creating Users table failed.\nPlease restart application...");
                        Console.WriteLine(ex.ToString());
                        File.Delete(_dbFileName);
                        return false;
                    }
                    finally
                    {
                        if (Conn.State == ConnectionState.Open)
                            Conn.Close();
                    }
                }

                return true;
            }
            else
            {
                return true;
            }
        }
        public static IDbConnection DbConnection
        {
            get
            {
                if (IsThisFirstTime)
                {
                    if (!InitializeDatabase())
                        return null;
                }

                return new SQLiteConnection(ConnectionString);
            }
        }
        
        public static bool IsThisFirstTime
        {
            get
            {
                return !File.Exists(_dbFileName); ;
            }
        }
    }
}
