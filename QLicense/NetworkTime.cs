using System;
using System.Net;
using System.Net.Sockets;

namespace License
{
    public class NetworkTime
    {
        private const int RequestTimeout = 3000;
        private const int TimesForEachServer = 5;
        private const byte OffTime = 40;

        private uint _lastSrv;

        //NIST Servers
        private static readonly string[] Srvs = { "pl.pool.ntp.org", "europe.pool.ntp.org", "pool.ntp.org", "time.nist.gov", "time.windows.com" };

        private IPAddress GetServer()
        {
            _lastSrv = (uint)((_lastSrv + 1) % Srvs.Length);

            IPAddress[] address = Dns.GetHostEntry(Srvs[_lastSrv]).AddressList;

            if (address == null || address.Length == 0)
                return IPAddress.None;

            return address[0];
        }

        public DateTime GetDateTime(bool utc = false)
        {
            //Examine all servers until we find a server that responds
            for (int st = 0; st < Srvs.Length * TimesForEachServer; st++)
            {
                try
                {
                    IPAddress ip = GetServer();
                    IPEndPoint ipEndP = new IPEndPoint(ip, 123);

                    Socket sk = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
                    {
                        ReceiveTimeout = RequestTimeout
                    };

                    sk.Connect(ipEndP);

                    byte[] data = new byte[48];

                    data[0] = 0x23;

                    for (int i = 1; i < 48; i++)
                        data[i] = 0;

                    sk.Send(data);

                    sk.Receive(data);

                    byte[] integerPart = new byte[4];

                    integerPart[0] = data[OffTime + 3];
                    integerPart[1] = data[OffTime + 2];
                    integerPart[2] = data[OffTime + 1];
                    integerPart[3] = data[OffTime + 0];

                    byte[] fractPart = new byte[4];

                    fractPart[0] = data[OffTime + 7];
                    fractPart[1] = data[OffTime + 6];
                    fractPart[2] = data[OffTime + 5];
                    fractPart[3] = data[OffTime + 4];

                    long ms = (long)((ulong)BitConverter.ToUInt32(integerPart, 0) * 1000 + (ulong)BitConverter.ToUInt32(fractPart, 0) * 1000 / 0x100000000L);

                    sk.Close();

                    /* DateTime*/
                    DateTime date = new DateTime(1900, 1, 1);
                    date += TimeSpan.FromTicks(ms * TimeSpan.TicksPerMillisecond);

                    return utc ? date : date.ToLocalTime();

                }
                catch (Exception)
                {
                    // następny serwer
                }
            }

            return DateTime.Now;
        }
    }
}
