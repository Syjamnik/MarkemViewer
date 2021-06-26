using MarkemViewer_Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace MarkemViewer_Library
{
    public class NgpclJsonFileCommandExtractor : INgpclJsonCommandsExtractor
    {
        
          protected readonly JsonSerializer serializer;
       
        /// <summary>
        ///  basic constructor
        /// </summary>
        /// <param name="serializer"> JsonSerializer object</param>
        public NgpclJsonFileCommandExtractor( JsonSerializer serializer)
        {

            this.serializer = serializer;

        }


        /// <summary>
        /// Reads Json command file and translate it to Model Class
        /// </summary>
        /// <param name="pathToJsonFile"> path to json file</param>
        /// <returns> list of available commands as object</returns>
        public NgpclCommandsModel GetListOfCommands(string pathToJsonFile)
        {

            using (StreamReader reader = new StreamReader(pathToJsonFile))
            {
                var textReader = new JsonTextReader(reader);
                return serializer.Deserialize<NgpclCommandsModel>(textReader);
            }

        }

    }
}
