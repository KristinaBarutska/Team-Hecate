namespace HecateMillionaire.GameLogic.Contracts
{
    public interface ISound
    {
        void playStartSound();
        void playGameOverSound();
        void playWinSound();
        void playCorrectSound();
        void playWrongSound();
    }
}
