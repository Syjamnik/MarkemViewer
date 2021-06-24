using MarkemViewer_Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace MarkemViewer_Library
{
    class NgpclJsonFileCommandExtractor : INgpclJsonCommandsExtractor
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
        public List<NgpclCommandModel> GetListOfCommands(string pathToJsonFile)
        {
            
            using (StreamReader reader= new StreamReader(pathToJsonFile))
            {
                return serializer.Deserialize<List<NgpclCommandModel>>(new JsonTextReader(reader));
            }
           
        }



    }
}
