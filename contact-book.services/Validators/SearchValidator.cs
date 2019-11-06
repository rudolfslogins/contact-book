using FluentValidation;

namespace contact_book.services.Validators
{
    public class SearchValidator : AbstractValidator<string>
    {
        public SearchValidator()
        {
            RuleFor(s => s).Matches(@"[A-Za-z0-9-+.@`]{3,20}");
        }
    }
}