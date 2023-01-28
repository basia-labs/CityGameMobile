using CityGameMobile.Helpers;
using CityGameMobile.Models;
using CityGameMobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CityGameMobile.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        private long currentUserId;
        private int elapsedSeconds;
        private long currentLocalizationId;
        private long currentQuestionId;
        private Answer correctAnswer;

        private string currentQuestion;
        private int currentScore;
        private string answerA;
        private string answerB;
        private string answerC;
        private string answerD;
        private string elapsedTime;

        public string CurrentQuestion { get => currentQuestion; set => SetValue(ref currentQuestion, value); }
        public int CurrentScore { get => currentScore; set => SetValue(ref currentScore, value); }
        public string AnswerA { get => answerA; set => SetValue(ref answerA, value); }
        public string AnswerB { get => answerB; set => SetValue(ref answerB, value); }
        public string AnswerC { get => answerC; set => SetValue(ref answerC, value); }
        public string AnswerD { get => answerD; set => SetValue(ref answerD, value); }
        public string ElapsedTime { get => elapsedTime; set => SetValue(ref elapsedTime, value); }

        private IEnumerable<Localization> localizations = Enumerable.Empty<Localization>();
        private IEnumerable<Question> questions = Enumerable.Empty<Question>();

        public Command PageAppearingCommand { get; }
        public Command MarkAnswerACommand { get; }
        public Command MarkAnswerBCommand { get; }
        public Command MarkAnswerCCommand { get; }
        public Command MarkAnswerDCommand { get; }

        private readonly LocalizationService localizationService = new LocalizationService();
        private readonly QuestionService questionService = new QuestionService();
        private readonly UserService userService = new UserService();

        public GameViewModel()
        {
            ElapsedTime = "00:00:00";

            PageAppearingCommand = new Command(async () => await OnAppearingAsync());
            MarkAnswerACommand = new Command(async () => await OnMarkAnswerAAsync());
            MarkAnswerBCommand = new Command(async () => await OnMarkAnswerBAsync());
            MarkAnswerCCommand = new Command(async () => await OnMarkAnswerCAsync());
            MarkAnswerDCommand = new Command(async () => await OnMarkAnswerDAsync());
        }

        private async Task OnAppearingAsync()
        {
            IsBusy = true;
            currentUserId = long.Parse(await SecureStorage.GetAsync("userId"));
            await OnLoadLocalizationsAsync();
            await LoadQuestionsForLocalizationAsync();
            LoadQuestionDetails();
            IsBusy = false;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                elapsedSeconds++;
                int hours = elapsedSeconds / 3600;
                int minutes = (elapsedSeconds % 3600) / 60;
                int seconds = elapsedSeconds % 60;
                ElapsedTime = $"{hours:D2}:{minutes:D2}:{seconds:D2}";

                return true;
            });
        }

        private async Task OnLoadLocalizationsAsync()
        {
            localizations = await localizationService.GetLocalizationsAsync();
            currentLocalizationId = localizations.FirstOrDefault().Id;
        }

        private async Task LoadQuestionsForLocalizationAsync()
        {
            questions = await questionService.GetQuestionAsync(currentLocalizationId);
            currentQuestionId = questions.FirstOrDefault().Id;
        }

        private void LoadQuestionDetails()
        {
            var question = questions.FirstOrDefault(q => q.Id == currentQuestionId);
            CurrentQuestion = question.Content;
            CurrentScore = question.Score;
            AnswerA = question.AnswerA;
            AnswerB = question.AnswerB;
            AnswerC = question.AnswerC;
            AnswerD = question.AnswerD;
            correctAnswer = question.CorrectAnswer;
        }

        private async Task OnMarkAnswerAAsync()
        {
            await CheckAnswerAsync(Answer.A);
            NextQuestion();
        }

        private async Task OnMarkAnswerBAsync()
        {
            await CheckAnswerAsync(Answer.B);
            NextQuestion();
        }

        private async Task OnMarkAnswerCAsync()
        {
            await CheckAnswerAsync(Answer.C);
            NextQuestion();
        }

        private async Task OnMarkAnswerDAsync()
        {
            await CheckAnswerAsync(Answer.D);
            NextQuestion();
        }

        public async Task CheckAnswerAsync(Answer answer)
        {
            if (correctAnswer == answer)
            {
                ToastHelper.MakeLongToast("Poprawna odpowiedź!");
                await userService.IncreaseScoreAsync(currentUserId, CurrentScore);
            }
            else
            {
                ToastHelper.MakeLongToast("Błędna odpowiedź!");
            }
        }

        private void NextQuestion()
        {
            currentQuestionId = localizations.FirstOrDefault(l => l.Id > currentQuestionId).Id;
            LoadQuestionDetails();
        }
    }
}