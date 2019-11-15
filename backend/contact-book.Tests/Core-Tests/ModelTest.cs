using System;
using System.Collections.Generic;
using contact_book.core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Type = contact_book.core.Models.Type;

namespace contact_book.Tests
{
    [TestClass]
    public class ModelTest
    {
        [TestMethod]
        public void CreateContact()
        {
            var testContact = new Contact
            {
                FirstName = "Aldis",
                LastName = "Sene",
                PhoneNumbers = new List<PhoneNumber>()
                {
                    new PhoneNumber()
                    {
                        Prefix = "+371",
                        Number = "26889922",
                        PhoneNumberType = new Type()
                        {
                            TypeName = "Home"
                        }
                    },
                    new PhoneNumber()
                    {
                        Prefix = "+371",
                        Number = "29442233",
                        PhoneNumberType = new Type()
                        {
                            TypeName = "Work"
                        }
                    }
                }
            };
            Assert.IsNotNull(testContact);
            Assert.AreEqual(2, testContact.PhoneNumbers.Count);
        }

    }
}
