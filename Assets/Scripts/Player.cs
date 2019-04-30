[System.Serializable]
public class Player 
{
    private int _hp;
    public int Health
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }
    private int _score;
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
        }
    }

    private int _highScore;

    public int HighScore
    {
        get
        {
            return _highScore;
        }
        set
        {
            _highScore = value;
        }
    }

    public void Reset()
    {
        Health = 0;
        Score = 0;
        HighScore = 0;
    }
}
