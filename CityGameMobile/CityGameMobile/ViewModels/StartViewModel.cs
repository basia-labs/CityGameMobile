using CityGameMobile.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CityGameMobile.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        public Command StartNewGameCommand { get; }

        public StartViewModel()
        {
            StartNewGameCommand = new Command(async () => await OnStartNewGame());
        }

        private async Task OnStartNewGame()
        {
            await Shell.Current.GoToAsync($"{nameof(GamePage)}");
        }
    }
}