using SportsClubApp.Data;
using Microsoft.Maui.Controls;
using SportsClubApp.Models;

namespace SportsClubApp
{
    public partial class UserPage : ContentPage
    {
        private readonly DatabaseService databaseService;

        public UserPage()
        {
            InitializeComponent();

            // Replace with your actual connection string
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SportsCLubClassesDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            databaseService = new DatabaseService(connectionString);

            DisplayAspNetUsers();
        }

        private void DisplayAspNetUsers()
        {
            var users = databaseService.GetAspNetUsers();
            usersListView.ItemsSource = users;
            totalUsersLabel.Text = $"Total Users: {users.Count}";
        }

        private async void OnUpdateUserClicked(object sender, EventArgs e)
        {
            try
            {
                // Get the selected user from the ListView
                var selectedUser = (AspNetUserModel)usersListView.SelectedItem;

                if (selectedUser != null)
                {
                    // Prompt the user for a new email
                    string newEmail = await DisplayPromptAsync("Update Email", "Enter new email:", "Update", "Cancel", null, -1, Keyboard.Email);

                    if (!string.IsNullOrEmpty(newEmail))
                    {
                        // Update the user in the database
                        databaseService.UpdateUser(selectedUser.Email, newEmail);

                        // Refresh the displayed users
                        DisplayAspNetUsers();
                    }
                }
                else
                {
                    Console.WriteLine("Please select a user to update.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user: {ex.Message}");
            }
        }

        private async void OnDeleteUserClicked(object sender, EventArgs e)
        {
            try
            {
                // Get the selected user from the ListView
                var selectedUser = (AspNetUserModel)usersListView.SelectedItem;

                if (selectedUser != null)
                {
                    // Confirm with the user before deletion
                    bool deleteConfirmed = await DisplayAlert("Delete User", $"Are you sure you want to delete {selectedUser.UserName}?", "Delete", "Cancel");

                    if (deleteConfirmed)
                    {
                        // Delete the user from the database
                        databaseService.DeleteUser(selectedUser.Email);

                        // Refresh the displayed users
                        DisplayAspNetUsers();
                    }
                }
                else
                {
                    Console.WriteLine("Please select a user to delete.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user: {ex.Message}");
            }
        }
    }

}

