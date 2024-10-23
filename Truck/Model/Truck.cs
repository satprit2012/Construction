using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Truck.Model
{
    public class Truck
    {
        /// <summary>
        /// Id of the Truck
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// alphanureric code of the truck
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Name of the truck
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// optional description of the truck
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// status of the truck
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// status id of the truck
        /// </summary>
        public int StatusId { get; set; }
    }
}