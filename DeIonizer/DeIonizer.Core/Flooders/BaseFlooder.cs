using System;
using System.Net;

namespace DeIonizer.Core.Flooders
{
    public abstract class BaseFlooder : IFlooder
    {
        public abstract string DisplayName { get; }

        public virtual string Message { get; set; }

        public virtual int Port { get; set; } = 80;

        public virtual int Delay { get; set; } = 10;

        public virtual int Threads { get; set; } = 4;

        public virtual bool IsFlooding { get; set; } = false;

        public virtual IPAddress Target { get; set; }

        public virtual AttackStatus State { get; set; } = AttackStatus.Idle;

        public virtual int Requested { get; set; }

        public virtual int Failed { get; set; }

        public virtual void Start()
        {
            IsFlooding = true;
        }

        public virtual void Stop()
        {
            IsFlooding = false;
        }

        public virtual void Reset()
        {
            Requested = 0;
            Failed = 0;
        }

        public abstract event EventHandler FloodError;

        public override string ToString()
        {
            return DisplayName;
        }
    }
}