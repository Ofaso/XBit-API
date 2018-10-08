using System;
using System.Collections.Generic;

namespace XBitApi.Models
{
    public class Miner
    {
        public Guid Id { get; set; }
        public Guid MinerTypeId { get; set; }
        public Guid CoinAlgorithmId { get; set; }
        public Guid MiningFarmId { get; set; }
        public Guid ShelfId { get; set; }

        public virtual MinerType MinerType { get; set; }
        public virtual CoinAlgorithm CoinAlgorithm { get; set; }
        public virtual MiningFarm MiningFarm { get; set; }
        public virtual Shelf Shelf { get; set; }
    }
}
