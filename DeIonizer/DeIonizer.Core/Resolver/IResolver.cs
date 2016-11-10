namespace DeIonizer.Core.Resolver
{
    public interface IResolver
    {
        bool CanResolve(string target);
        string Resolve(string target);
    }
}