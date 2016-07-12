namespace HecateMillionaire
{
    public class GameConstants
    {
        public const string FileQuestions = @"..\..\questions.txt";
        public const string FilePlayerResults = @"..\..\RecordPlayerResults.txt";


        public const string FileGameOver = @"..\..\resources\gameover.txt";
        public const string FileChampion = @"..\..\resources\champion.txt";
        public const string FileHecateStart = @"..\..\resources\hecate.txt";

        public const string SoundWin = @"..\..\resources\win.wav";
        public const string SoundGameOver = @"..\..\resources\gameover.wav";
        public const string SoundCorrect = @"..\..\resources\correct.wav";
        public const string SoundWrong = @"..\..\resources\wrong.wav";
        public const string SoundStart = @"..\..\resources\start.wav";

        public const int QuestionScoreLevel1 = 100;
        public const int QuestionScoreLevel2 = 500;
        public const int QuestionScoreLevel3 = 1000;

        public const int TimerLevel1 = 30;
        public const int TimerLevel2 = 60;
        public const int TimerLevel3 = 90;

        public const int NumberOfQuestionPerLevel = 5;
        public const int NumberOfLevels = 3;
        public const int MaxWrongAnswers = 5;
        public const int MaxWrongAnswersForReduceScore = 3;

        public const int BonusChampionCoefficient = 2;
    }
}
