namespace fdm_gamify2
{
    public class Player
    {
        private string _nickname;
        private int _score;
        
        public Player(string nickname, int score)
        {
            _nickname = nickname;
            _score = score;
        }

        public string GetNickname()
        {
            return _nickname;
        }

        public void SetNickname(string nickname)
        {
            _nickname = nickname;
        }

        public int GetScore()
        {
            return _score;
        }

        public void SetScore(int score)
        {
            _score = score;
        }
    }
}