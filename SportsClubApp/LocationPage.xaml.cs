using SportsClubApp.Data;
using Microsoft.Maui.Controls;
using SportsClubApp.Models;

namespace SportsClubApp
{

public partial class LocationPage : ContentPage
{
    private readonly DatabaseService databaseService;

    public LocationPage()
    {
        InitializeComponent();
        // Replace with your actual connection string
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SportsCLubClassesDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        databaseService = new DatabaseService(connectionString);

        DisplayLocations();
    }

    private void DisplayLocations()
    {
        var locations = databaseService.GetLocations();
        locationsListView.ItemsSource = locations;
        totalLocationsLabel.Text = $"Total Locations: {locations.Count}";
        }

    private async void OnAddLocationClicked(object sender, EventArgs e)
    {
        try
        {
            string name = await DisplayPromptAsync("Add Location", "Enter location name:", "Add", "Cancel");
            string address = await DisplayPromptAsync("Add Location", "Enter location address:", "Add", "Cancel");

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(address))
            {
                // Insert the location into the database
                databaseService.InsertLocation(name, address);

                // Refresh the displayed locations
                DisplayLocations();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding location: {ex.Message}");
        }
    }
        private async void OnUpdateLocationClicked(object sender, EventArgs e)
        {
            try
            {
                // Get the selected location from the ListView
                var selectedLocation = (LocationModel)locationsListView.SelectedItem;

                if (selectedLocation != null)
                {
                    // Prompt the user for new information
                    string newLocationName = await DisplayPromptAsync("Update Location", "Enter new location name:", "Update", "Cancel");
                    string newLocationAddress = await DisplayPromptAsync("Update Location", "Enter new location address:", "Update", "Cancel");

                    if (!string.IsNullOrEmpty(newLocationName) && !string.IsNullOrEmpty(newLocationAddress))
                    {
                        // Update the location in the database
                        databaseService.UpdateLocation(selectedLocation.Name, selectedLocation.Address, newLocationName, newLocationAddress);

                        // Refresh the displayed locations
                        DisplayLocations();
                    }
                }
                else
                {
                    Console.WriteLine("Please select a location to update.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating location: {ex.Message}");
            }
        }

        private async void OnDeleteLocationClicked(object sender, EventArgs e)
        {
            try
            {
                // Get the selected location from the ListView
                var selectedLocation = (LocationModel)locationsListView.SelectedItem;

                if (selectedLocation != null)
                {
                    // Confirm with the user before deletion
                    bool deleteConfirmed = await DisplayAlert("Delete Location", $"Are you sure you want to delete {selectedLocation.Name}?", "Delete", "Cancel");

                    if (deleteConfirmed)
                    {
                        // Delete the location from the database
                        databaseService.DeleteLocation(selectedLocation.Name, selectedLocation.Address);

                        // Refresh the displayed locations
                        DisplayLocations();
                    }
                }
                else
                {
                    Console.WriteLine("Please select a location to delete.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting location: {ex.Message}");
            }
        }
    }
}