using CityGameMobile.Helpers;
using CityGameMobile.Views;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CityGameMobile.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        public Command PageAppearingCommand { get; }
        public Command StartNewGameCommand { get; }

        public StartViewModel()
        {
            PageAppearingCommand = new Command(async () => await OnAppearingAsync());
            StartNewGameCommand = new Command(async () => await OnStartNewGame());
        }

        private async Task OnAppearingAsync()
        {
            await SecureStorage.SetAsync("currentLocalizationId", "0");
            await SecureStorage.SetAsync("currentScoresAmount", "0");
        }

        private async Task OnStartNewGame()
        {
            TimerSingleton.Instance.Timer.Start();
            await Shell.Current.GoToAsync($"{nameof(LocalizationPage)}");
        }
    }
}