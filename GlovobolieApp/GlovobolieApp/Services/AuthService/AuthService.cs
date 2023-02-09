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

        public Task<PersonalData> GetPersonalDataAsync(string email)
        {
            string query = $"SELECT first_name, last_name, address, country, city, phone_number, email FROM `users` HAVING email = '{email}' LIMIT 1";
            return Task.Run<PersonalData>(() => GetPersonalData(query));
        }

        public async Task LoginAsync(string email, string password)
        {
            if (!(await CheckCredentialsAsync(email, password)))
            {
                throw new ValidationException("Credentials do not match!");
            }
            var data = await GetPersonalDataAsync(email);

            DependencyService.Get<SessionService>().UpdateSession(1, email, data);
        }

        public async Task SignUpAsync(string email, string password, PersonalData data)
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

        private PersonalData GetPersonalData(string query)
        {
            try
            {
                connection.Open();
                var command = new MySqlCommand(query, connection);
                var reader = command.ExecuteReader();
                if (!reader.Read()) throw new Exception("Couldn't find user!");

                return new PersonalData(
                    reader.GetString("first_name"),
                    reader.GetString("last_name"),
                    reader.GetString("address"),
                    reader.GetString("phone_number"),
                    reader.GetString("country"),
                    reader.GetString("city")
                    );
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
            bool userExists = false;
            try
            {
                connection.Open();
                var command = new MySqlCommand(query, connection);
                var reader = command.ExecuteScalar();
                userExists = (reader != null);
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
            return userExists;
        }
    }
}
