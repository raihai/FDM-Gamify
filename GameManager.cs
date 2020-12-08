using System.ComponentModel;

namespace fdm_gamify2
{
    public class GameManager : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public AstraModel Astra { get; set; }
        public bool IsRunning;

        public GameManager()
        {
            Astra = new AstraModel();
        }

        public async void MainLoop()
        {
            IsRunning = true;
            while (IsRunning)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Astra)));

                if (Astra.DistFromGround <= 0)
                {
                    GameOver();
                }
            }
        }

        public void StartGame()
        {
            if (!IsRunning)
            {
                Astra = new AstraModel();
                MainLoop();
            }
            
        }

        public void GameOver()
        {
            IsRunning = false;
        }
    }
}