using CityGameMobile.Config;
using CityGameMobile.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CityGameMobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            SetStartPage();

            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));

            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        }

        private void SetStartPage()
        {
            CurrentItem = Settings.LoginStatus switch
            {
                AccountStatus.LoggedIn => GamePage,
                _ => LoginPage,
            };
        }

        private void OnMenuItemClicked(object sender, EventArgs e)
        {
            SecureStorage.Remove("username");
            SecureStorage.Remove("userId");
            SecureStorage.Remove("userScore");

            Settings.LoginStatus = AccountStatus.LoggedOut;
            //await Current.GoToAsync($"//{nameof(LoginPage)}");
            (Application.Current as App).MainPage = new AppShell();
        }
    }
}