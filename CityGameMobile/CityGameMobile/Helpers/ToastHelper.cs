using CityGameMobile.Helpers.Interfaces;
using Xamarin.Forms;

namespace CityGameMobile.Helpers
{
    public class ToastHelper
    {
        public static void MakeLongToast(string message)
        {
            DependencyService.Get<IToast>()?.MakeLongToast(message);
        }

        public static void MakeShortToast(string message)
        {
            DependencyService.Get<IToast>()?.MakeShortToast(message);
        }
    }
}