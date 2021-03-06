﻿using System;
using System.Collections.Generic;

namespace XBitApi.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string Place { get; set; }
        public string Zip { get; set; }
        public Guid CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}
