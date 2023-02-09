using GlovobolieApp.Exceptions;
using GlovobolieApp.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GlovobolieApp.Services.UserService
{
    public class AuthService : DataServiceBase, IAuthService
    {
        public Task<bool> CheckCredentialsAsync(string email, string password)
        {
            string query = $"SELECT * FROM `users` HAVING email = '{email}' AND _password = '{password}' LIMIT 1;";
            return Task.Run<bool>(() => CheckCredentials(query));
        }

        public Task<bool> CheckEmailAsync(string email)
        {
            string query = $"SELECT * FROM `users` HAVING email = '{email}' LIMIT 1;";
            return Task.Run<bool>(() => CheckEmail(query));
        }

        public Task<User> GetUserDataAsync(string email)
        {
            string query = $"SELECT * FROM `users` HAVING email = '{email}' LIMIT 1";
            return Task.Run<User>(() => GetUserData(query));
        }

        public async Task LoginAsync(string email, string password)
        {
            if (!(await CheckCredentialsAsync(email, password)))
            {
                throw new ValidationException("Credentials do not match!");
            }
            var data = await GetUserDataAsync(email);
            var personalData = new PersonalData(data.FirstName, data.LastName, data.Address, data.PhoneNumber, data.Country, data.City);
            DependencyService.Get<SessionService>().UpdateSession(data.Id, data.Email, personalData);
        }

        public async Task SignUpAsync(string email, string password, User data)
        {
            if (await CheckEmailAsync(email))
            {
                throw new ValidationException("Email already exists!");
            }
            string query = "INSERT INTO `users` " +
                "(`first_name`, `last_name`, `address`, `country`, `city`," +
                " `phone_number`, `email`, `_password`) " +
                $"VALUES ('{data.FirstName}','{data.LastName}','{data.Address}','{data.Country}'," +
                $"'{data.City}','{data.PhoneNumber}','{email}','{password}')";
            await Task.Run(() => this.SignUp(query));
        }
        public async Task LogoutAsync()
        {
            DependencyService.Get<SessionService>().DisposeSession();
            await Shell.Current.GoToAsync("//Login");
        }
        private void SignUp(string query)
        {
            try
            {
                connection.Open();
                var command = new MySqlCommand(query, connection);
                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected < 1) throw new Exception("Failed to register user!");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }

        private User GetUserData(string query)
        {
            try
            {
                connection.Open();
                var command = new MySqlCommand(query, connection);
                var reader = command.ExecuteReader();
                if (!reader.Read()) throw new Exception("Couldn't find user!");

                return new User {
                    Id = reader.GetInt32("id"),
                    Email = reader.GetString("email"),
                    FirstName = reader.GetString("first_name"),
                    LastName = reader.GetString("last_name"),
                    Address = reader.GetString("address"),
                    PhoneNumber = reader.GetString("phone_number"),
                    Country = reader.GetString("country"),
                    City = reader.GetString("city")
                    };
            } catch (Exception e) { 
                Debug.WriteLine(e.Message);
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }
        private bool CheckEmail(string query)
        {
            bool emailExists = false;
            try
            {
                connection.Open();
                var command = new MySqlCommand(query, connection);
                var reader = command.ExecuteScalar();
                emailExists = (reader != null);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw e;
            }
            finally
            {
                connection.Close();
            }
            return emailExists;
        }
        private bool CheckCredentials(string query)
        {
            bool hasUser = false;
            try
            {
                connection.Open();
                var command = new MySqlCommand(query, connection);
                var reader = command.ExecuteScalar();
                if (reader != null) hasUser = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw e;
            }
            finally
            {
                connection.Close();
            }
            return hasUser;
        }
    }
}
