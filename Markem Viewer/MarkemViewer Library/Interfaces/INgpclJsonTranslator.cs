using System;
using System.Collections.Generic;
using System.Text;

namespace MarkemViewer_Library.Interfaces
{
    public interface INgpclJsonCommandsExtractor
    {


        /// <summary>
        /// Reads Json command file and translate it to Model Class
        /// </summary>
        /// <param name="pathToJsonFile"> path to json file</param>
        /// <returns> list of available commands as object</returns>
        NgpclCommandsModel GetListOfCommands(string pathToJsonFile);
    }
}
