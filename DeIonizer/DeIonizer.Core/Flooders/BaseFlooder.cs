namespace DeIonizer.Core.Flooders
{
    public abstract class BaseFlooder : IFlooder
    {
        public abstract string DisplayName { get; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}