using CityGameMobile.Helpers;
using CityGameMobile.Services;
using CityGameMobile.Views;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CityGameMobile.ViewModels
{
    public class FinishViewModel : BaseViewModel
    {
        private string totalScores;
        private string elapsedTime;

        public string TotalScores { get => totalScores; set => SetValue(ref totalScores, value); }
        public string ElapsedTime { get => elapsedTime; set => SetValue(ref elapsedTime, value); }

        public Command PageAppearingCommand { get; }
        public Command GoToRankingCommand { get; }

        private readonly UserService userService = new UserService();

        public FinishViewModel()
        {
            PageAppearingCommand = new Command(async () => await OnAppearingAsync());
            GoToRankingCommand = new Command(async () => await OnGoToRankingAsync());
        }

        private async Task OnAppearingAsync()
        {
            var currentScore = int.Parse(await SecureStorage.GetAsync("currentScoresAmount"));
            TotalScores = currentScore.ToString();
            ElapsedTime = TimerSingleton.Instance.ElapsedTime;

            var currentUserId = long.Parse(await SecureStorage.GetAsync("userId"));
            await userService.SetScoreAsync(currentUserId, currentScore);
            await userService.SetTimeAsync(currentUserId, ElapsedTime);

            TimerSingleton.Instance.Timer.Stop();
            TimerSingleton.Instance.ResetTimer();
        }

        private async Task OnGoToRankingAsync()
        {
            await Shell.Current.GoToAsync($"//{nameof(RankingPage)}");
        }
    }
}