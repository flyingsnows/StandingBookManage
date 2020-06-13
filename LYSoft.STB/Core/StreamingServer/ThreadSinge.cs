using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace StreamingServer
{
    public class ThreadSinge
    {
        public CancellationToken token { get; set; }
        public Socket clinet { get; set; }
    }
}
