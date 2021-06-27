using MarkemViewer_Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarkemViewer_Library
{
    public class ResponseTranslator : IResponseTranslator
    {

        /// <summary>
        /// translate command form NGPCL to list of response
        /// </summary>
        /// <param name="ngpclResponse"> return value from instrument</param>
        /// <returns>returns dictionary <(string)ValueName, (string)Value></returns>
        public Dictionary<string, string> translateResponse(string ngpclResponse)
        {
            Dictionary<string, string> producedResponse = new Dictionary<string, string>();

            // check if there is failure  response
            if (IsResponseInvalid(ngpclResponse))
            {
                producedResponse.Add("Response", "Failure");
                return producedResponse;
            }

            bool singleValueResponse = IsReturningSingleValue(ngpclResponse);

            ngpclResponse = PrepareResponse(ngpclResponse);
            if (singleValueResponse)
            {
                producedResponse.Add("Response", ngpclResponse);


                return producedResponse;
            }
            else
            {
                return producedResponse;
            }


        }

        /// <summary>checks if this is single response value</summary>
        /// <param name="ngpclResponse"> return value from instrument</param>
        ///<throws name="ArgumentException"> </throws> 
        /// <returns>true if response contains single value</returns>
        protected bool IsReturningSingleValue(string ngpclResponse)
        {
            char[] charResponse= ngpclResponse.ToCharArray();
            int counter=0;


            foreach (var letter in charResponse)
            {
                if (letter == '|')
                    counter++;
            }

            if (counter > 2)
                return false;
            if (counter == 2)
                return true;
            else
                throw new ArgumentException("there are less than 2 x '|' chars. This response is faulty");


        }


        /// <summary>
        /// checks for failure response from instrument {~UD1|}
        /// </summary>
        /// <param name="ngpclResponse"> return value from instrument</param>
        /// <returns>true if there is failure response</returns>
        protected bool IsResponseInvalid(string ngpclResponse)
        {
            if (ngpclResponse == "{~UD1|}")
                return true;
            else
                return false;
        }
   
        protected string PrepareResponse(string ngpclResponse)
        {
            ngpclResponse = ngpclResponse.Remove(0, 6);
            ngpclResponse = ngpclResponse.Remove(ngpclResponse.LastIndexOf("}")-1, 2);

            return ngpclResponse;
        }
    
    
    
    
    }
}
