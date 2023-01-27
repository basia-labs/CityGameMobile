using System;
using Xamarin.Essentials;

namespace CityGameMobile.Config
{
    public static class Settings
    {
        private static readonly string loginStatus = AccountStatus.LoggedOut.ToString();
        private static readonly string baseApiUrl = "https://city-game-api-app.azurewebsites.net/api/";

        public static AccountStatus LoginStatus
        {
            get => (AccountStatus)Enum.Parse(typeof(AccountStatus), Preferences.Get(nameof(LoginStatus), loginStatus));
            set => Preferences.Set(nameof(LoginStatus), value.ToString());
        }

        public static string BaseApiUrl { get => baseApiUrl; }
    }
}