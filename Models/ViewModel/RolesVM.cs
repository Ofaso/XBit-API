using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XBitApi.Models.ViewModel
{
    public class RolesVM
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public List<ClaimsVM> Claims { get; set; }
    }
}
