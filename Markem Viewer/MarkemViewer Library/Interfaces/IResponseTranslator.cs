using System;
using System.Collections.Generic;
using System.Text;

namespace MarkemViewer_Library.Interfaces
{
    /// <summary>
    /// allows you to get all information returned from instrument
    /// </summary>
    interface IResponseTranslator
    {

        /// <summary>
        /// translate command form NGPCL to list of response
        /// </summary>
        /// <param name="ngpclResponse"> return value from instrument</param>
        /// <returns>returns dictionary <(string)ValueName, (string)Value></returns>
        Dictionary<string, string> translateResponse(string ngpclResponse);
        
    }
}
