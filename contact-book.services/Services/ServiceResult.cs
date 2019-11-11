using System.Collections.Generic;
using contact_book.core.Models;
using FluentValidation.Results;

namespace contact_book.services.Services
{
    public class ServiceResult
    {
        public bool Succeeded { get; }
        public Contact Contact { get; }
        public ICollection<Contact> ContactCollection { get; }
        public ValidationResult ValidationResult { get; }
        public string ErrorMessage { get; }

        public ServiceResult(bool succeeded, Contact contact)
        {
            Succeeded = succeeded;
            Contact = contact;
        }

        public ServiceResult(bool succeeded, ICollection<Contact> contacts)
        {
            Succeeded = succeeded;
            ContactCollection = contacts;
        }

        public ServiceResult(bool succeeded, ValidationResult result)
        {
            Succeeded = succeeded;
            ValidationResult = result;
        }

        public ServiceResult(bool succeeded, string errMessage)
        {
            Succeeded = succeeded;
            ErrorMessage = errMessage;
        }
    }
}