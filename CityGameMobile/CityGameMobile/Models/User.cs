namespace CityGameMobile.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Identificator { get; set; }
        public int Score { get; set; }
        public bool MyAccount { get; set; }
    }
}