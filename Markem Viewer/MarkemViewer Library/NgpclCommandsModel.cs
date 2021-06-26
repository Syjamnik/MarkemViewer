using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarkemViewer_Library
{
    public class NgpclCommandsModel
    {
        /// <summary>
        /// contains list of NgpclCommandModel that represents GPCL commands
        /// </summary>
        [JsonProperty("Commands")]
        public List<NgpclCommandModel> Commands{ get; protected set; }

        public NgpclCommandsModel(List<NgpclCommandModel> Commands)
        {
            this.Commands = Commands;
        }
    }
}
