using Android.Widget;
using CityGameMobile.Droid.Helpers;
using CityGameMobile.Helpers.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(Toaster))]
namespace CityGameMobile.Droid.Helpers
{
    public class Toaster : IToast
    {
        public void MakeLongToast(string message)
        {
            Toast.MakeText(Platform.AppContext, message, ToastLength.Long).Show();
        }

        public void MakeShortToast(string message)
        {
            Toast.MakeText(Platform.AppContext, message, ToastLength.Short).Show();
        }
    }
}