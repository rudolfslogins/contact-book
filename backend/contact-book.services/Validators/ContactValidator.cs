
using contact_book.core.Models;
using FluentValidation;

namespace contact_book.services.Validators
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(contact => contact.FirstName)
                .NotEmpty()
                .Matches(@"^[A-Z][a-z'`-]+$");
            RuleFor(contact => contact.LastName)
                    .NotEmpty()
                    .Matches(@"^[A-Z][a-z'`-]+$");
            RuleForEach(contact => contact.Emails)
                .ChildRules(email =>
                {
                    email.RuleFor(e => e.EmailName)
                        .EmailAddress();
                });
            RuleForEach(contact => contact.PhoneNumbers)
                .ChildRules(phoneNumber =>
                {
                    phoneNumber.RuleFor(n => n.Prefix)
                        .Matches(@"^[+][0-9]{3}$");
                    phoneNumber.RuleFor(p => p.Number)
                            .Matches(@"^[0-9]{8}$");
                });
        }
    }
}