using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarkemViewer_Library
{
    public class NgpclCommandModel
    {
        /// <summary>
        /// NGOCL (raw command) made by Markem
        /// </summary>
        [JsonProperty("NGPCL")]
        public string NGPCL { get; protected set; }

        /// <summary>
        /// simplified name used in command
        /// </summary>
        [JsonProperty("Identifier")]
        public string Identifier { get; protected set; }

        /// <summary>
        ///  Clue about returned type
        /// </summary>
        [JsonProperty("Units")]
        public string Units { get; protected set; }

        /// <summary>
        /// Command name
        /// </summary>
        [JsonProperty("CommandName")]
        public string CommandName { get; protected set; }
        /// <summary>
        /// Command description
        /// </summary>
        [JsonProperty("Description")]
        public string Description { get; protected set; }

        /// <summary>
        /// Place where you can save output from command as string
        /// </summary>
        public string Output { get;  set; }


        /// <summary>
        ///  basic constructor
        /// </summary>
        /// <param name="CommandName">command name</param>
        /// <param name="NGPCL">NGOCL (raw command) made by Markem</param>
        /// <param name="Identifier">simplified name used in command</param>
        /// <param name="Units">Clue about returned type</param>
        /// <param name="Description">Command description</param>
        public NgpclCommandModel( string NGPCL,
            string Identifier, string Units,string CommandName, string Description)
        {
            this.NGPCL = NGPCL;
            this.Identifier = Identifier;
            this.Units = Units;
            this.CommandName = CommandName;
            this.Description = Description;
        }

    }
}
