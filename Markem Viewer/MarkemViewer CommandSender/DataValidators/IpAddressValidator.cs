using System;
using System.Linq;

namespace MarkemViewer_CommandSender
{
    public static class IpAddressValidator
    {
        public static bool ValidateIpAddress(this string ipAddress)
        {
            string[] splittedAddress = ipAddress.Split('.');


            if (splittedAddress.Length != 4)
                return false;

            for (int i = 0; i < splittedAddress.Length; i++)
            {
                if (!splittedAddress[i].All(x => char.IsNumber(x) && x>=0 && x<=255) || string.IsNullOrWhiteSpace(splittedAddress[i]))
                    return false;

                if (i == 3 && (Convert.ToInt32(splittedAddress[i]) < 1 || Convert.ToInt32(splittedAddress[i]) > 254))
                    return false;

            }

            return true;
        }
    }
}
