using SportsClubApp.Data;
using Microsoft.Maui.Controls;
using SportsClubApp.Models;

namespace SportsClubApp
{
    public partial class MainPage : ContentPage
    {
        private readonly DatabaseService databaseService;

        public MainPage()
        {
            InitializeComponent();

            // Replace with your actual connection string
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SportsCLubClassesDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            databaseService = new DatabaseService(connectionString);

            // Display the content
            DisplayAspNetUsers();
        }

        private void OnConnectClicked(object sender, EventArgs e)
        {
            DisplayAspNetUsers();
        }

        private void DisplayAspNetUsers()
        {
            var users = databaseService.GetAspNetUsers();
            listView.ItemsSource = users;
        }

        // Removed the OnInsertUserClicked method

        // MainPage.xaml.cs
        private void OnViewUsersClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserPage());
        }
        private void OnViewLocationsClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LocationPage());
        }
        private void OnViewTrainersClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TrainerPage());
        }
    }

}
