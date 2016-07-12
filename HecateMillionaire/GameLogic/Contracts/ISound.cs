namespace HecateMillionaire.GameLogic.Contracts
{
    public interface ISound
    {
        void PlayStartSound();

        void PlayGameOverSound();

        void PlayWinSound();

        void PlayCorrectSound();

        void PlayWrongSound();
    }
}
