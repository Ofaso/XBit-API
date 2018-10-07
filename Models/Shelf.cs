using System;
using System.Collections.Generic;

namespace XBitApi.Models
{
    public class Shelf
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid LocationId { get; set; }

        public virtual Location Location { get; set; }
    }
}
