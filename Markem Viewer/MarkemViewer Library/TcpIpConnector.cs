using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
        /// <returns> response from markem instrument</returns>
        public string startCommunication(string ipAddr,int portNr, string command)
            {
                byte[] bytes = new byte[1024];

                try
                {
                    // Connect with a device
                    IPAddress ipAddress = IPAddress.Parse(ipAddr);
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, portNr);

                    // Create a TCP/IP socket.    
                    Socket sender = new Socket(ipAddress.AddressFamily,
                        SocketType.Stream, ProtocolType.Tcp);

 

                    // Connect to Remote EndPoint  
                    sender.Connect(remoteEP);


                    // Encode the data string into a byte array.    
                    byte[] msg = Encoding.ASCII.GetBytes(command);

                    // Send the data through the socket.    
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.    
                    int bytesRec = sender.Receive(bytes);
                    string receivedMessage = Encoding.ASCII.GetString(bytes, 0, bytesRec);



                    // Release the socket.    
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                    return receivedMessage;


                }
                catch (Exception exc)
                {
                throw exc;
                }
            }
    }
}
