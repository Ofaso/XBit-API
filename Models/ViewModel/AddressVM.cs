using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XBitApi.Models.ViewModel
{
    public class AddressVM
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string Place { get; set; }
        public string Zip { get; set; }
        [Required]
        public string CountryName { get; set; }
    }
}
