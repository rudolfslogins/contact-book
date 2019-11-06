using contact_book.core.Models;
using FluentValidation.Results;

namespace contact_book.services
{
    public class ServiceResult
    {
        public bool Succeeded { get; }
        public Contact Contact { get; }
        public ValidationResult ValidationResult { get; }

        public ServiceResult(bool succeeded, Contact contact)
        {
            Succeeded = succeeded;
            Contact = contact;
        }

        public ServiceResult(bool succeeded, ValidationResult result)
        {
            Succeeded = succeeded;
            ValidationResult = result;
        }
    }
}