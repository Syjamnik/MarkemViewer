using System;
using System.Collections.Generic;
using System.Text;

namespace MarkemViewer_CommandSender
{
    static class IpAddressValidator
    {
        public static bool ValidateIpAddress(string ipAddress)
        {
            string[] splittedIP= ipAddress.Split('.');

            if (splittedIP.Length != 4)
                return false;

            for (int i = 0; i < splittedIP.Length; i++)
            {
                int IpPartAsNumber = 0;
                
                try
                {
                    IpPartAsNumber = Int32.Parse(splittedIP[i]);
                }
                catch (FormatException ex)
                {
                    return false;
                }
                if(IpPartAsNumber<0 || IpPartAsNumber > 254)
                {
                    return false;
                }

            }


            return true;
        }
    }
}
