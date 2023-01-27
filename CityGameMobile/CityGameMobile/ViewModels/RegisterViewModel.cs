using CityGameMobile.Services;
using CityGameMobile.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CityGameMobile.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private string username;
        public string Username { get => username; set => SetValue(ref username, value); }

        public Command CancelCommand { get; }
        public Command RegisterCommand { get; }

        private readonly UserService userService;

        public RegisterViewModel()
        {
            userService = new UserService();

            RegisterCommand = new Command(async () => await OnRegisterAsync(), CanRegister);
            PropertyChanged += (_, __) => RegisterCommand.ChangeCanExecute();

            CancelCommand = new Command(async () => await OnCancelAsync(), CanCancel);
            PropertyChanged += (_, __) => CancelCommand.ChangeCanExecute();
        }

        public bool CanRegister()
        {
            return !string.IsNullOrEmpty(Username)
                && !IsBusy;
        }

        public bool CanCancel()
        {
            return !IsBusy;
        }

        private async Task OnRegisterAsync()
        {
            IsBusy = true;
            var user = await userService.RegisterUserAsync(Username);
            IsBusy = false;

            if (user.Id > 0)
            {
                await Application.Current.MainPage.DisplayAlert("Sukces!", $"Twój unikalny identyfikator do gry to {user.Identificator}", "Ok");
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Uwaga!", "Rejestracja nie powiodła się. Spróbuj ponownie później", "Ok");
            }
        }

        private async Task OnCancelAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}