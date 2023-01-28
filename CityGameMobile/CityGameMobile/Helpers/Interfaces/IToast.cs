namespace CityGameMobile.Helpers.Interfaces
{
    public interface IToast
    {
        void MakeLongToast(string message);
        void MakeShortToast(string message);
    }
}