using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace StreamingServer
{
    /// <summary>
    /// Provides a streaming server that can be used to stream any images source
    /// to any client.
    /// </summary>
    public class ImageStreamingServer : IDisposable
    {
        private List<Socket> _Clients;
        private Thread _Thread;

        //public ImageStreamingServer() : this(Screen.Snapshots(600, 450, true))
        //{

        //}

        public ImageStreamingServer(IEnumerable<byte[]> imagesSource)
        {

            _Clients = new List<Socket>();
            _Thread = null;

            this.ImagesSource = imagesSource;
            this.Interval = 50;

        }


        /// <summary>
        /// Gets or sets the source of images that will be streamed to the 
        /// any connected client.
        /// </summary>
        public IEnumerable<byte[]> ImagesSource { get; set; }

        /// <summary>
        /// Gets or sets the interval in milliseconds (or the delay time) between 
        /// the each image and the other of the stream (the default is . 
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// Gets a collection of client sockets.
        /// </summary>
        public IEnumerable<Socket> Clients { get { return _Clients; } }

        /// <summary>
        /// Returns the status of the server. True means the server is currently 
        /// running and ready to serve any client requests.
        /// </summary>
        public bool IsRunning { get { return (_Thread != null && _Thread.IsAlive); } }

        private CancellationTokenSource ctsToken = new CancellationTokenSource();
        private Socket Server = null;

        /// <summary>
        /// Starts the server to accepts any new connections on the specified port.
        /// </summary>
        /// <param name="port"></param>
        public void Start(int port)
        {

            lock (this)
            {
                _Thread = new Thread(new ParameterizedThreadStart(ServerThread));
                _Thread.IsBackground = true;
                _Thread.Start(port);
            }

        }

        /// <summary>
        /// Starts the server to accepts any new connections on the default port (8080).
        /// </summary>
        public void Start()
        {
            this.Start(8080);
        }

        public void Stop()
        {

            if (this.IsRunning)
            {
                try
                {
                    ctsToken.Cancel();
                    Server.Close();
                    _Thread.Join();
                    _Thread.Abort();
                }
                finally
                {

                    lock (_Clients)
                    {

                        foreach (var s in _Clients)
                        {
                            try
                            {
                                s.Close();
                            }
                            catch { }
                        }
                        _Clients.Clear();

                    }

                    _Thread = null;
                }
            }
        }

        /// <summary>
        /// This the main thread of the server that serves all the new 
        /// connections from clients.
        /// </summary>
        /// <param name="state"></param>
        private void ServerThread(object state)
        {

            try
            {
                Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                Server.Bind(new IPEndPoint(IPAddress.Any, (int)state));
                Server.Listen(10);

                //LogHelper.PrintLog(string.Format("Server started on port {0}.", state));

                foreach (Socket client in Server.IncommingConnectoins())
                {
                    ThreadSinge single = new ThreadSinge();
                    single.clinet = client;
                    single.token = ctsToken.Token;
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(ClientThread), client);
                    ThreadPool.QueueUserWorkItem(o => ClientThread(single));
                }

            }
            catch { }

            //this.Stop();
        }

        /// <summary>
        /// Each client connection will be served by this thread.
        /// </summary>
        /// <param name="client"></param>
        private void ClientThread(object client)
        {
            ThreadSinge single = (ThreadSinge)client;
            Socket socket = single.clinet;

            System.Diagnostics.Debug.WriteLine(string.Format("New client from {0}", socket.RemoteEndPoint.ToString()));

            lock (_Clients)
                _Clients.Add(socket);

            try
            {
                using (MjpegWriter wr = new MjpegWriter(new NetworkStream(socket, true), "--------HHHHHOOOO"))
                {

                    // Writes the response header to the client.
                    wr.WriteHeader();

                    // Streams the images from the source to the client.
                    foreach (var imgStream in Streams(this.ImagesSource))
                    {
                        if (!single.token.IsCancellationRequested)
                        {
                            if (this.Interval > 0)
                                Thread.Sleep(this.Interval);

                            wr.Write(imgStream);
                        }
                        else
                        {
                            break;
                        }
                    }

                }
            }
            catch { }
            finally
            {
                lock (_Clients)
                    _Clients.Remove(socket);
            }
        }

        private IEnumerable<MemoryStream> Streams(IEnumerable<byte[]> source)
        {
            foreach (var img in source)
            {

                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(img, 0, img.Length);
                    //ms.SetLength(0);
                    //img.Save(ms, jpsEncodeer, eps);
                    yield return ms;
                }
            }
            yield break;
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Stop();
        }

        #endregion
    }

    static class SocketExtensions
    {

        public static IEnumerable<Socket> IncommingConnectoins(this Socket server)
        {
            while (true)
            {

                yield return server.Accept();
            }
        }

    }



}
