using CityGameMobile.Models;
using CityGameMobile.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CityGameMobile.ViewModels
{
    public class RankingViewModel : BaseViewModel
    {
        private bool isRefreshing;

        public bool IsRefreshing { get => isRefreshing; set => SetValue(ref isRefreshing, value); }

        public ObservableCollection<User> Users { get; }

        public Command PageAppearingCommand { get; }
        public Command LoadUsersCommand { get; }

        private readonly UserService userService = new UserService();

        public RankingViewModel()
        {
            Users = new ObservableCollection<User>();

            PageAppearingCommand = new Command(async () => await OnAppearingAsync());
            LoadUsersCommand = new Command(async () => await OnLoadUsersAsync());
        }

        private async Task OnAppearingAsync()
        {
            await OnLoadUsersAsync();
        }

        private async Task OnLoadUsersAsync()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;
            IsRefreshing = true;

            Users.Clear();

            var currentUserId = long.Parse(await SecureStorage.GetAsync("userId"));
            var users = await userService.GetUsersAsync();
            users.ToList().ForEach(u =>
            {
                if (u.Id == currentUserId)
                {
                    u.MyAccount = true;
                }

                Users.Add(u);
            });

            IsRefreshing = false;
            IsBusy = false;
        }
    }
}