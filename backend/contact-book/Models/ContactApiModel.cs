using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using contact_book.core.Models;
using Newtonsoft.Json;

namespace contact_book.Models
{
    public class ContactApiModel : EntityApiModel
    {
        [JsonProperty(Order = -4)]
        public string FirstName { get; set; }
        [JsonProperty(Order = -3)]
        public string LastName { get; set; }
        [JsonProperty(Order = 1)]
        public ICollection<AddressApiModel> Addresses { get; set; }
        [JsonProperty(Order = 2)]
        public ICollection<PhoneNumberApiModel> PhoneNumbers { get; set; }
        [JsonProperty(Order = 3)]
        public ICollection<EmailApiModel> Emails { get; set; }
        [JsonProperty(Order = -2)]
        public string Company { get; set; }
        [JsonProperty(Order = -1)]
        public string Notes { get; set; }
        [JsonProperty(Order = 0)]
        [DataType(DataType.Date)]
        public string BirthDate { get; set; }

        public ContactApiModel()
        {

        }
    }
}