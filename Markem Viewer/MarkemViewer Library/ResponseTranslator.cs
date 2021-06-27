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
        public Dictionary<string, string> TranslateResponse(string ngpclResponse)
        {
            // check if there is failure  response and throw error 
            IsResponseFaulty(ngpclResponse);
            Dictionary<string, string> producedResponse = new Dictionary<string, string>();

            

            
            bool singleValueResponse = IsReturningSingleValue(ngpclResponse);

            // removing unused chars
            ngpclResponse = PrepareResponse(ngpclResponse);


            if (singleValueResponse)
            {
                producedResponse.Add("Response", ngpclResponse);


                return producedResponse;
            }
            else
            {
                string[] splittedResponse= ngpclResponse.Split('|');


                string key= "Response";
                string value="Value";
                for (int i = 0; i < splittedResponse.Length; i++)
                {
                    // even value -> response type  odd value -> response value
                    if(i%2== 0)
                    {
                        key = "Response " + splittedResponse[i];
                    }
                    else
                    {
                        value = splittedResponse[i];
                        producedResponse.Add(key, value);
                    }
                }

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
            if (counter == 2 || ngpclResponse== "{~UD1|}")
                return true;
            else
                throw new ArgumentException("there are less than 2 x '|' chars. This response is faulty");


        }


        /// <summary>
        /// checks for failure response from instrument {~UD1|}
        /// </summary>
        /// <param name="ngpclResponse"> return value from instrument</param>
        /// <returns>true if there is failure response</returns>
        protected bool IsResponseFaulty(string ngpclResponse)
        {
            if (ngpclResponse == "{~UD1|}")
                return true;
            else
                return false;
        }
   
        protected string PrepareResponse(string ngpclResponse)
        {
            if(ngpclResponse.Length>=7)
                ngpclResponse = ngpclResponse.Remove(0, 6);

            if(ngpclResponse.Contains("}"))
                ngpclResponse = ngpclResponse.Remove(ngpclResponse.LastIndexOf("}"));

            if(ngpclResponse.Contains("|"))
                ngpclResponse = ngpclResponse.Remove(ngpclResponse.LastIndexOf("|"));

            return ngpclResponse;
        }
    
    
    
    
    }
}
