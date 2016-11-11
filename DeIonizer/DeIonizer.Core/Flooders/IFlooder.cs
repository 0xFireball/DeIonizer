namespace DeIonizer.Core.Flooders
{
    public interface IFlooder
    {
        string DisplayName { get; }

        int Delay { get; set; }
        bool IsFlooding { get; set; }

        void Start();
        void Stop();
    }
}