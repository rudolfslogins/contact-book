using System;
using System.Collections.Generic;
using contact_book.services.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace contact_book.Tests.ServicesTests
{
    [TestClass]
    public class SearchValidatorTest
    {
        private readonly SearchValidator _validator = new SearchValidator();
        [TestMethod]
        public void SearchLengthTest()
        {
            var searchStrings = new List<string>
            {
                 "ab", "a", "abcdefghijklmnoprstuv"
            };
            const string expResult = "'Search string' is not in the correct format.";
            foreach (var str in searchStrings)
                Assert.AreEqual(expResult, _validator.Validate(str).ToString());
            
        }

        [TestMethod]
        public void SearchCharTest()
        {
            var searchStrings = new List<string>
            {
                "kaR:.", "///", "\\\\", "|||", "***", "^^^", "###"
            };
            const string expResult = "'Search string' is not in the correct format.";
            foreach (var str in searchStrings)
                Assert.AreEqual(expResult, _validator.Validate(str).ToString());

        }
    }
}
