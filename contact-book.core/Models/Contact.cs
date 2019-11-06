using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace contact_book.core.Models
{
    public class Contact : Entity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
        public ICollection<Email> Emails { get; set; }
        public string Company { get; set; }
        public string Notes { get; set; }
        public DateTime? BirthDate { get; set; }

        public Contact()
        {

        }
    }
}