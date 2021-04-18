namespace yolo
{
    public class Score
    {
        public int Value { get; private set; }

        public void addScoreForAchievement(float elapsedTime)
        {
            const int Points = 10;
            Value += Points;
        }
        
        public Score()
        {
            Value = 0;
        }
    }
}