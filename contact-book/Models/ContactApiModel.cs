using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using contact_book.core.Models;

namespace contact_book.Models
{
    public class ContactApiModel : EntityApiModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<AddressApiModel> Addresses { get; set; }
        public ICollection<PhoneNumberApiModel> PhoneNumbers { get; set; }
        public ICollection<EmailApiModel> Emails { get; set; }
        public string Company { get; set; }
        public string Notes { get; set; }
        public DateTime? BirthDate { get; set; }

        public ContactApiModel()
        {

        }
    }
}