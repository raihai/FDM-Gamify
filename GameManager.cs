using System;
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
                Console.Write(Astra.DistFromGround);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Astra)));
                Astra.DistFromGround = Astra.DistFromGround - 1;

                if (Astra.DistFromGround <= 0)
                {

                    GameOver();
                }
            }
        }

        public String StartGame()
        {
            Console.Write("test123");
            if (!IsRunning)
            {
                Console.Write("test123");
                Astra = new AstraModel();
                MainLoop();
            }
            Console.Write("test123");
            return "";
        }

        public void GameOver()
        {
            IsRunning = false;
        }

        public void StateHasChanged()
        {
            
        }
    }
}