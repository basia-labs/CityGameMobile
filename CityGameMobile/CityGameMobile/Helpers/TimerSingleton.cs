using System.Timers;

namespace CityGameMobile.Helpers
{
    public class TimerSingleton
    {
        private static TimerSingleton instance;
        private static readonly object locker = new object();

        public static TimerSingleton Instance
        {
            get
            {
                lock (locker)
                {
                    instance ??= new TimerSingleton();

                    return instance;
                }
            }
        }

        private int elapsedSeconds;
        public Timer Timer { get; }
        public string ElapsedTime { get; private set; }

        private TimerSingleton()
        {
            Timer = new Timer(1000);
            Timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            elapsedSeconds++;
            int hours = elapsedSeconds / 3600;
            int minutes = (elapsedSeconds % 3600) / 60;
            int seconds = elapsedSeconds % 60;
            ElapsedTime = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }

        public void ResetTimer()
        {
            elapsedSeconds = 0;
        }
    }
}