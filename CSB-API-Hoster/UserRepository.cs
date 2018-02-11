using Dapper;
using System;
using System.Collections.Generic;
using System.Data;

namespace CSB_API_Hoster
{
    public class UserRepository
    {
        public IEnumerable<UserVM> GetAllUsers()
        {
            string query = "SELECT * FROM `Users`";

            IDbConnection Conn = DataAccess.DbConnection;

            try
            {
                Conn.Open();
                return Conn.Query<UserVM>(query);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} -> Exception occured - {1}", "UserRepository.GetAllUsers()", ex.Message);
                return null;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public UserVM GetUserById(int userId)
        {
            string query = "SELECT * FROM `Users` WHERE `Id`=@Id";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", userId);

            IDbConnection Conn = DataAccess.DbConnection;

            try
            {
                Conn.Open();
                return Conn.QuerySingleOrDefault<UserVM>(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} -> Exception occured - {1}", "UserRepository.GetUserById()", ex.Message);
                return null;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }

        public UserVM AddNewUser(UserVM user)
        {
            user.Id = 0;
            DynamicParameters parameters = new DynamicParameters(user);

            string query = @"INSERT INTO `Users`(`FirstName`,`LastName`,`Gender`,`Mobile`) 
                            VALUES (@FirstName,@LastName,@Gender,@Mobile);
                            SELECT last_insert_rowid()";

            IDbConnection Conn = DataAccess.DbConnection;

            try
            {
                Conn.Open();
                user.Id = Conn.QueryFirstOrDefault<int>(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} -> Exception occured - {1}", "UserRepository.AddNewUser()", ex.Message);
                return null;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }

            return user;
        }

        public UserVM UpdateUser(UserVM user)
        {
            DynamicParameters parameters = new DynamicParameters(user);

            string query = @"UPDATE `Users` SET `FirstName`=@FirstName,`LastName`=@LastName,`Gender`=@Gender,`Mobile`=@Mobile 
                            WHERE `Id`=@Id";

            IDbConnection Conn = DataAccess.DbConnection;

            try
            {
                Conn.Open();
                Conn.Execute(query, parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} -> Exception occured - {1}", "UserRepository.UpdateUser()", ex.Message);
                return null;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }

            return user;
        }

        public bool DeleteUser(int userId)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", userId);

            string query = @"DELETE FROM `Users` WHERE `Id`=@Id";

            IDbConnection Conn = DataAccess.DbConnection;

            try
            {
                Conn.Open();
                return (Conn.Execute(query, parameters) > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} -> Exception occured - {1}", "UserRepository.DeleteUser()", ex.Message);
                return false;
            }
            finally
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
        }


    }
}
