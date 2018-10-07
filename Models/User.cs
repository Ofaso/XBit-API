using System;
using System.Collections.Generic;

namespace XBitApi.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid UserInformationId { get; set; }
        public Guid AddressId { get; set; }
        public string FarmMail { get; set; }
        public string Password { get; set; }

        public virtual UserInformation UserInformation { get; set; }
        public virtual Address Address { get; set; }
    }
}
