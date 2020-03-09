using P2PNET.TransportLayer;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CopyNinjaLib
{
    public class ClipboardP2P
    {
        private int portNumber;        
        private Timer timer;
        private TransportManager manager;
        private List<string> peers;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ClipboardP2P(int portNumber = 8080)
        {
            manager = new TransportManager(portNumber, true);

            timer = new Timer();
        }

        public async void Run()
        {
            manager.MsgReceived += (obj, args) =>
            {

            };

            manager.PeerChange += (obj, args) =>
            {
                foreach (var item in args.Peers)
                {
                    peers.Add(item.IpAddress);
                }
            };

            await manager.StartAsync();

            StartBroadcasting();
        }

        public async Task SendMessage(string message)
        {
            var messagebytes = Encoding.ASCII.GetBytes(message);

            await manager.SendToAllPeersAsyncTCP(messagebytes);
        }

        private void StartBroadcasting()
        {
            var bytes = Encoding.ASCII.GetBytes("ping");

            timer.Elapsed += (obj, args) =>
            {
                manager.SendToAllPeersAsyncUDP(bytes);
            };

            timer.Interval = 1000;
            timer.Start();
        }
    }
}
