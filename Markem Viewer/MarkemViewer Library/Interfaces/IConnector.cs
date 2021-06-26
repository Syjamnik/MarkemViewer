
namespace MarkemViewer_Library.Interfaces
{
    public interface IConnector
    {

        /// <summary>
        /// Client app is the one sending messages to a Server/listener.   
        /// Both listener and client can send messages back and forth once a   
        ///communication is established.  
        /// </summary>
        /// <param name="ipAddr"> ip address</param>
        /// <param name="portNr"> port number</param>
        /// <param name="command">markem command</param>
        /// <param name="WaitForResponse"> max time to await for response</param>
        /// <returns> response from markem instrument</returns>
        string startCommunication(string ipAddr, int portNr, string command, int WaitForResponse=3000);
    }
}
