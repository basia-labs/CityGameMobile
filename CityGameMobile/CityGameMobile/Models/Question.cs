namespace CityGameMobile.Models
{
    public class Question
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
        public Answer CorrectAnswer { get; set; }
        public int Score { get; set; }

        public long LocalizationId { get; set; }
        public Localization Localization { get; set; }
    }
}