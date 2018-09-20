﻿using System;
using System.Collections.Generic;

namespace XBitApi.Models
{
    public class MiningFarm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid AdminCustomerId { get; set; }

        public User Customer { get; set; }
    }
}
