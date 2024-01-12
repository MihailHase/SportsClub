using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SportsClubApp.Models;

namespace SportsClubApp.Data
{
    public class DatabaseService
    {
        private readonly string connectionString;

        public DatabaseService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public List<AspNetUserModel> GetAspNetUsers()
        {
            List<AspNetUserModel> users = new List<AspNetUserModel>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT UserName, Email FROM dbo.AspNetUsers", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new AspNetUserModel
                            {
                                UserName = reader["UserName"].ToString(),
                                Email = reader["Email"].ToString()
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }
        public AspNetUserModel GetUserByEmail(string email)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                string query = "SELECT UserName, Email, PasswordHash FROM dbo.AspNetUsers WHERE Email = @Email";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new AspNetUserModel
                            {
                                UserName = reader["UserName"].ToString(),
                                Email = reader["Email"].ToString(),
                                PasswordHash = reader["PasswordHash"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

        public void InsertUser(string email)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                // Assuming your AspNetUsers table has only the Email column
                string query = "INSERT INTO dbo.AspNetUsers (Email) VALUES (@Email)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateUser(string oldEmail, string newEmail)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                string query = "UPDATE dbo.AspNetUsers SET Email = @NewEmail WHERE Email = @OldEmail";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewEmail", newEmail);
                    command.Parameters.AddWithValue("@OldEmail", oldEmail);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUser(string email)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                string query = "DELETE FROM dbo.AspNetUsers WHERE Email = @Email";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    command.ExecuteNonQuery();
                }
            }
        }


        public List<LocationModel> GetLocations()
        {
            List<LocationModel> locations = new List<LocationModel>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT Name, Address FROM dbo.Location", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var location = new LocationModel
                            {
                                Name = reader["Name"].ToString(),
                                Address = reader["Address"].ToString()
                            };

                            locations.Add(location);
                        }
                    }
                }
            }

            return locations;
        }
        public void InsertLocation(string name, string address)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                string query = "INSERT INTO dbo.Location (Name, Address) VALUES (@Name, @Address)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Address", address);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateLocation(string oldName, string oldAddress, string newName, string newAddress)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                string query = "UPDATE dbo.Location SET Name = @NewName, Address = @NewAddress WHERE Name = @OldName AND Address = @OldAddress";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OldName", oldName);
                    command.Parameters.AddWithValue("@OldAddress", oldAddress);
                    command.Parameters.AddWithValue("@NewName", newName);
                    command.Parameters.AddWithValue("@NewAddress", newAddress);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void DeleteLocation(string name, string address)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                string query = "DELETE FROM dbo.Location WHERE Name = @Name AND Address = @Address";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Address", address);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<TrainerModel> GetTrainers()
        {
            List<TrainerModel> trainers = new List<TrainerModel>();

            using (var connection = GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT FirstName, LastName, Details, PhoneNumber, Email FROM dbo.Trainer", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var trainer = new TrainerModel
                            {
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Details = reader["Details"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                Email = reader["Email"].ToString()
                            };

                            trainers.Add(trainer);
                        }
                    }
                }
            }
            return trainers;
        }
        public void InsertTrainer(TrainerModel trainer)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                string query = "INSERT INTO dbo.Trainer (FirstName, LastName, Details, PhoneNumber, Email) " +
                               "VALUES (@FirstName, @LastName, @Details, @PhoneNumber, @Email)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", trainer.FirstName);
                    command.Parameters.AddWithValue("@LastName", trainer.LastName);
                    command.Parameters.AddWithValue("@Details", trainer.Details);
                    command.Parameters.AddWithValue("@PhoneNumber", trainer.PhoneNumber);
                    command.Parameters.AddWithValue("@Email", trainer.Email);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateTrainer(string oldFirstName, string oldLastName, string newFirstName, string newLastName, string newDetails, string newPhoneNumber, string newEmail)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                string query = "UPDATE dbo.Trainer SET FirstName = @NewFirstName, LastName = @NewLastName, Details = @NewDetails, PhoneNumber = @NewPhoneNumber, Email = @NewEmail " +
                               "WHERE FirstName = @OldFirstName AND LastName = @OldLastName";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OldFirstName", oldFirstName);
                    command.Parameters.AddWithValue("@OldLastName", oldLastName);
                    command.Parameters.AddWithValue("@NewFirstName", newFirstName);
                    command.Parameters.AddWithValue("@NewLastName", newLastName);
                    command.Parameters.AddWithValue("@NewDetails", newDetails);
                    command.Parameters.AddWithValue("@NewPhoneNumber", newPhoneNumber);
                    command.Parameters.AddWithValue("@NewEmail", newEmail);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTrainer(string firstName, string lastName)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                string query = "DELETE FROM dbo.Trainer WHERE FirstName = @FirstName AND LastName = @LastName";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);

                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
