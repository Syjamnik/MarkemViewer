using MarkemViewer_Library.Interfaces;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MarkemViewer_Library
{

    /// <summary>
    ///  This is singleton use .Instance() method
    /// </summary>
    public class TcpIpConnector: IConnector
    {
        protected TcpIpConnector()
        {

        }

        /// <summary>
        /// singleton hidden instance
        /// </summary>
        protected static TcpIpConnector instance;
       
        /// <summary>
        /// singleton instance
        /// </summary>
        public static TcpIpConnector Instance { get{

                if (instance == null)
                {
                    instance = new TcpIpConnector();
                    return instance;
                }
                else
                {
                    return instance;
                }

            } protected set {
                instance = value;
            }
        }



        /// <summary>
        /// Client app is the one sending messages to a Server/listener.   
        /// Both listener and client can send messages back and forth once a   
        /// communication is established.  
        /// </summary>
        /// <param name="ipAddr"> ip address</param>
        /// <param name="portNr"> port number</param>
        /// <param name="command">markem command</param>
        /// <param name="WaitForResponse"> max time to await for response in miliseconds</param>
        /// <returns> response from markem instrument</returns>
        public string startCommunication(string ipAddr,int portNr, string command, int WaitForResponse = 3000)
            {
            byte[] bytes = new byte[1024];
            Socket sender=null;
            try
            {
                // Connect with a device
                IPAddress ipAddress = IPAddress.Parse(ipAddr);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, portNr);

                // Create a TCP/IP socket.    
                sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);
                sender.ReceiveTimeout = WaitForResponse+300;


                // Connect to Remote EndPoint  
                sender.Connect(remoteEP);


                // Encode the data string into a byte array.    
                byte[] msg = Encoding.ASCII.GetBytes(command);

                // Send the data through the socket.    
                int bytesSent = sender.Send(msg);

                // Receive the response from the remote device.
                string receivedMessage = "";

                DateTime start = DateTime.Now;


                // we give Markem instrument time for response
                while (sender.Available <= 0 && (DateTime.Now- start).TotalMilliseconds< WaitForResponse)
                {
                    Thread.Sleep(10);

                }

                // 
                int bytesRec = sender.Receive(bytes);
                receivedMessage = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                // Release the socket.    

                return receivedMessage;


            }
            catch (TimeoutException exc)
            {
                throw exc;
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
        }
    }
}
