namespace DeIonizer.Core.Flooders
{
    public abstract class BaseFlooder : IFlooder
    {
        public abstract string DisplayName { get; }

        public virtual int Delay { get; set; } = 10;

        public virtual bool IsFlooding { get; set; } = false;

        public abstract void Start();

        public abstract void Stop();

        public override string ToString()
        {
            return DisplayName;
        }
    }
}