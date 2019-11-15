namespace contact_book.core.Models
{
    public class PhoneNumber : Entity
    {
        public string Prefix { get; set; }
        public string Number { get; set; }
        public Type PhoneNumberType { get; set; }

        public PhoneNumber()
        {

        }
    }
}