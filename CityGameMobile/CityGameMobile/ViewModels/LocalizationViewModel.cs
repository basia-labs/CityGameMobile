using CityGameMobile.Helpers;
using CityGameMobile.Models;
using CityGameMobile.Services;
using CityGameMobile.Views;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CityGameMobile.ViewModels
{
    public class LocalizationViewModel : BaseViewModel
    {
        private double latitude;
        private double longitude;
        private string nextDestinationName;
        private string userCurrentLocalization;
        private double distance;
        private string elapsedTime;

        public string NextDestinationName { get => nextDestinationName; set => SetValue(ref nextDestinationName, value); }
        public string UserCurrentLocalization { get => userCurrentLocalization; set => SetValue(ref userCurrentLocalization, value); }
        public double Distance { get => distance; set => SetValue(ref distance, value); }
        public string ElapsedTime { get => elapsedTime; set => SetValue(ref elapsedTime, value); }

        public Command PageAppearingCommand { get; }
        public Command RefreshLocalizationCommand { get; }

        private readonly LocalizationService localizationService = new LocalizationService();

        public LocalizationViewModel()
        {
            ElapsedTime = "00:00:00";

            PageAppearingCommand = new Command(async () => await OnAppearingAsync());
            RefreshLocalizationCommand = new Command(async () => await OnRefreshLocalizationAsync());
        }

        private async Task OnAppearingAsync()
        {
            IsBusy = true;

            if (!TimerSingleton.Instance.Timer.Enabled)
            {
                return;
            }

            TimerSingleton.Instance.Timer.Elapsed += OnTimedElapsed;
            await LoadLocalizationAsync();
            await OnRefreshLocalizationAsync();
            IsBusy = false;
        }

        private async Task LoadLocalizationAsync()
        {
            var localizations = await localizationService.GetLocalizationsAsync();
            var currentLocalizationId = long.Parse(await SecureStorage.GetAsync("currentLocalizationId"));

            var localization = currentLocalizationId == 0
                ? localizations.FirstOrDefault()
                : localizations.FirstOrDefault(l => l.Id > currentLocalizationId);

            await SecureStorage.SetAsync("currentLocalizationId", localization.Id.ToString());

            NextDestinationName = localization.PlaceName;
            latitude = localization.Latitude;
            longitude = localization.Longitude;
        }

        private async Task OnRefreshLocalizationAsync()
        {
            IsBusy = true;

            try
            {
                await GetUserGeolocationAsync();
                IsBusy = false;
                
                if (Distance < 50)
                {
                    ToastHelper.MakeLongToast("Brawo za dotarcie na miejsce!");
                    await Shell.Current.GoToAsync($"{nameof(GamePage)}");
                }
            }
            catch (FeatureNotSupportedException)
            {
                ToastHelper.MakeLongToast("Na Twoim urządzeniu ta opcja nie działa");
            }
            catch (FeatureNotEnabledException)
            {
                ToastHelper.MakeLongToast("Upewnij się, czy masz włączoną lokalizację na swoim urządzeniu");
            }
            catch (PermissionException)
            {
                ToastHelper.MakeLongToast("Aby pobrać lokalizację musisz wyrazić zgodę na przydzielenie uprawnień aplikacji!");
            }
            catch (Exception)
            {
                ToastHelper.MakeLongToast("Błąd! Spróbuj później!");
            }
        }

        private async Task GetUserGeolocationAsync()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            var location = await Geolocation.GetLocationAsync(request);
            var addresses = await Geocoding.GetPlacemarksAsync(location);
            var address = addresses.FirstOrDefault();

            UserCurrentLocalization = $"{address.Thoroughfare} {address.SubThoroughfare}, {address.PostalCode} {address.Locality}";
            Distance = Math.Round(location.CalculateDistance(latitude, longitude, DistanceUnits.Kilometers) * 1000);
        }

        private void OnTimedElapsed(object source, ElapsedEventArgs e)
        {
            ElapsedTime = TimerSingleton.Instance.ElapsedTime;
        }
    }
}