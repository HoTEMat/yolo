namespace yolo
{
    public class Score
    {
        public int Value { get; private set; }

        public void addScoreForAchievement(float elapsedTime)
        {
            const int maxPoints = 100;
            Value += (int) (maxPoints / (elapsedTime / 10));
        }
        
        public Score()
        {
            Value = 0;
        }
    }
}