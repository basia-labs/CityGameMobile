using CityGameMobile.Config;
using CityGameMobile.Models;
using CityGameMobile.Services;
using CityGameMobile.Views;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CityGameMobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string identificator = "";

        public string Identificator { get => identificator; set => SetValue(ref identificator, value); }

        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }

        private readonly UserService userService;   

        public LoginViewModel()
        {
            userService = new UserService();

            LoginCommand = new Command(async () => await OnLoginAsync(), CanLogin);
            PropertyChanged += (_, __) => LoginCommand.ChangeCanExecute();

            RegisterCommand = new Command(async () => await OnRegisterAsync(), CanRegister);
            PropertyChanged += (_, __) => RegisterCommand.ChangeCanExecute();
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(Identificator)
                && !IsBusy;
        }

        private bool CanRegister()
        {
            return !IsBusy;
        }

        private async Task OnLoginAsync()
        {
            IsBusy = true;
            var user = await userService.LoginAsync(Identificator);
            IsBusy = false;

            switch (user.Id > 0)
            {
                case true:
                    await LoggedInCorrectlyAsync(user);
                    break;
                case false:
                    await InvalidUsernameOrPasswordAsync();
                    break;
            }
        }

        private async Task OnRegisterAsync()
        {
            if (!IsBusy)
            {
                await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
            }
        }

        private async Task LoggedInCorrectlyAsync(User user)
        {
            await SecureStorage.SetAsync("username", user.Username);
            await SecureStorage.SetAsync("userId", $"{user.Id}");
            await SecureStorage.SetAsync("userScore", $"{user.BestScore}");

            Settings.LoginStatus = AccountStatus.LoggedIn;
            await Shell.Current.GoToAsync($"//{nameof(StartPage)}");
        }

        private async Task InvalidUsernameOrPasswordAsync()
        {
            Settings.LoginStatus = AccountStatus.LoggedOut;
            await Application.Current.MainPage.DisplayAlert("Uwaga!", "Niepoprawny identyfikator", "Ok");
        }
    }
}