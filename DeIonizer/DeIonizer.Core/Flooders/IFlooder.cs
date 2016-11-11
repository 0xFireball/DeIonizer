using System;
using System.Net;

namespace DeIonizer.Core.Flooders
{
    public interface IFlooder
    {
        string DisplayName { get; }

        IPAddress Target { get; set; }
        string Message { get; set; }
        int Delay { get; set; }
        int Threads { get; set; }
        int Port { get; set; }
        bool IsFlooding { get; set; }

        //Statistics
        AttackStatus State { get; set; }
        int Requested { get; set; }
        int Failed { get; set; }

        void Start();

        void Stop();

        event EventHandler FloodError;
    }
}