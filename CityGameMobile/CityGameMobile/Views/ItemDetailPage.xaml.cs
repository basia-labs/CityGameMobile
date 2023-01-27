using CityGameMobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CityGameMobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}