using SportsClubApp.Data;
using Microsoft.Maui.Controls;
using SportsClubApp.Models;
using System;
using System.Collections.Generic;

namespace SportsClubApp
{
    public partial class TrainerPage : ContentPage
    {
        private readonly DatabaseService databaseService;

        public TrainerPage()
        {
            InitializeComponent();
            // Înlocuiește cu șirul de conexiune actual
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SportsCLubClassesDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            databaseService = new DatabaseService(connectionString);

            DisplayTrainers();
        }

        private void DisplayTrainers()
        {
            var trainers = databaseService.GetTrainers();
            trainersListView.ItemsSource = trainers;
            totalTrainersLabel.Text = $"Total Trainers: {trainers.Count}";
        }

        private async void OnAddTrainerClicked(object sender, EventArgs e)
        {
            try
            {
                string firstName = await DisplayPromptAsync("Add Trainer", "Enter trainer's first name:", "Add", "Cancel");
                string lastName = await DisplayPromptAsync("Add Trainer", "Enter trainer's last name:", "Add", "Cancel");
                string details = await DisplayPromptAsync("Add Trainer", "Enter trainer's details:", "Add", "Cancel");
                string phoneNumber = await DisplayPromptAsync("Add Trainer", "Enter trainer's phone number:", "Add", "Cancel");
                string email = await DisplayPromptAsync("Add Trainer", "Enter trainer's email:", "Add", "Cancel");

                if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) &&
                    !string.IsNullOrEmpty(details) && !string.IsNullOrEmpty(phoneNumber) && !string.IsNullOrEmpty(email))
                {
                    // Inserează antrenorul în baza de date
                    databaseService.InsertTrainer(new TrainerModel
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Details = details,
                        PhoneNumber = phoneNumber,
                        Email = email
                    });

                    // Reîmprospătează lista de antrenori afișată
                    DisplayTrainers();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding trainer: {ex.Message}");
            }
        }
        private async void OnUpdateTrainerClicked(object sender, EventArgs e)
        {
            try
            {
                // Get the selected trainer from the ListView
                var selectedTrainer = (TrainerModel)trainersListView.SelectedItem;

                if (selectedTrainer != null)
                {
                    // Prompt the user for new details
                    string newFirstName = await DisplayPromptAsync("Update Trainer", "Enter new first name:", "Update", "Cancel", null, -1, Keyboard.Default);
                    string newLastName = await DisplayPromptAsync("Update Trainer", "Enter new last name:", "Update", "Cancel", null, -1, Keyboard.Default);
                    string newDetails = await DisplayPromptAsync("Update Trainer", "Enter new details:", "Update", "Cancel", null, -1, Keyboard.Default);
                    string newPhoneNumber = await DisplayPromptAsync("Update Trainer", "Enter new phone number:", "Update", "Cancel", null, -1, Keyboard.Telephone);
                    string newEmail = await DisplayPromptAsync("Update Trainer", "Enter new email:", "Update", "Cancel", null, -1, Keyboard.Email);

                    if (!string.IsNullOrEmpty(newFirstName) && !string.IsNullOrEmpty(newLastName))
                    {
                        // Update the trainer in the database
                        databaseService.UpdateTrainer(
                            selectedTrainer.FirstName,
                            selectedTrainer.LastName,
                            newFirstName,
                            newLastName,
                            newDetails,
                            newPhoneNumber,
                            newEmail
                        );

                        // Refresh the displayed trainers
                        DisplayTrainers();
                    }
                }
                else
                {
                    Console.WriteLine("Please select a trainer to update.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating trainer: {ex.Message}");
            }
        }
        private async void OnDeleteTrainerClicked(object sender, EventArgs e)
        {
            try
            {
                // Get the selected trainer from the ListView
                var selectedTrainer = (TrainerModel)trainersListView.SelectedItem;

                if (selectedTrainer != null)
                {
                    // Confirm with the user before deletion
                    bool deleteConfirmed = await DisplayAlert("Delete Trainer", $"Are you sure you want to delete {selectedTrainer.FirstName} {selectedTrainer.LastName}?", "Delete", "Cancel");

                    if (deleteConfirmed)
                    {
                        // Delete the trainer from the database
                        databaseService.DeleteTrainer(selectedTrainer.FirstName, selectedTrainer.LastName);

                        // Refresh the displayed trainers
                        DisplayTrainers();
                    }
                }
                else
                {
                    Console.WriteLine("Please select a trainer to delete.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting trainer: {ex.Message}");
            }
        }

        // Adaugă metode pentru actualizare și ștergere dacă este nevoie
    }
}
