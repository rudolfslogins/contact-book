using System;
using System.Collections.Generic;
using contact_book.core.Models;
using contact_book.services.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Type = contact_book.core.Models.Type;

namespace contact_book.Tests.ServicesTests
{
    [TestClass]
    public class ContactValidatorTest
    {
        private readonly ContactValidator _validator = new ContactValidator();
        [TestMethod]
        public void NameNotEmptyTest()
        {
            var contact = new Contact
            {
                FirstName = null,
                LastName = null
            };
            Assert.AreEqual("'First Name' must not be empty. 'Last Name' must not be empty.", _validator.Validate(contact).ToString(" "));
        }

        [TestMethod]
        public void NameAllowedCharsTest()
        {
            var contacts = new List<Contact>
            {
                new Contact { FirstName = "aldis", LastName = "abele"},
                new Contact { FirstName = "aLdis", LastName = "abelE"},
                new Contact { FirstName = "Aldis1", LastName = "Abel3"},
                new Contact { FirstName = "Aldis_", LastName = "Abele."},
                new Contact { FirstName = "A", LastName = "A"}
            };
            const string expResult = "'First Name' is not in the correct format. 'Last Name' is not in the correct format.";
            foreach(var contact in contacts)
                Assert.AreEqual(expResult, _validator.Validate(contact).ToString(" "));

        }

        [TestMethod]
        public void EmailValidationTest()
        {
            var contacts = new List<Contact>
            {
                new Contact { FirstName = "Aldis", LastName = "Abele", Emails = new List<Email>{ new Email { EmailName = "aldisabele.lv", EmailType = new Type {TypeName = "Home"} } }},
                new Contact { FirstName = "Aldis", LastName = "Abele", Emails = new List<Email>{ new Email { EmailName = "aldis@abelelv", EmailType = new Type {TypeName = "Home"} } }},
                new Contact { FirstName = "Aldis", LastName = "Abele", Emails = new List<Email>{ new Email { EmailName = "aldis@abele_lv", EmailType = new Type {TypeName = "Home"} } }}

            };
            const string expResult = "'Email Name' is not a valid email address.";
            foreach(var contact in contacts)
                Assert.AreEqual(expResult, _validator.Validate(contact).ToString());
        }

        [TestMethod]
        public void PhoneValidationTest()
        {
            var contacts = new List<Contact>
            {
                new Contact { FirstName = "Aldis", LastName = "Abele", PhoneNumbers = new List<PhoneNumber>{ new PhoneNumber { Prefix = "371", Number = "2445566", PhoneNumberType = new Type {TypeName = "Home"} } }},
                new Contact { FirstName = "Aldis", LastName = "Abele", PhoneNumbers = new List<PhoneNumber>{ new PhoneNumber { Prefix = "+37", Number = "a2445566", PhoneNumberType = new Type {TypeName = "Home"} } }},
                new Contact { FirstName = "Aldis", LastName = "Abele", PhoneNumbers = new List<PhoneNumber>{ new PhoneNumber { Prefix = "+3712", Number = "322445566", PhoneNumberType = new Type {TypeName = "Home"} } }}

            };
            const string expResult = "'Prefix' is not in the correct format. 'Number' is not in the correct format.";
            foreach (var contact in contacts)
                Assert.AreEqual(expResult, _validator.Validate(contact).ToString(" "));
        }
    }
}
