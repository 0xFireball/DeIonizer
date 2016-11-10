using System.Collections.Generic;

namespace DeIonizer.Core.Flooders
{
    public class FlooderLoaderService
    {
        public static IEnumerable<IFlooder> BuiltinFlooders => new[] {
            new UdpFlooder()
        };
    }
}