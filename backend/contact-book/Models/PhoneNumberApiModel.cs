namespace contact_book.Models
{
    public class PhoneNumberApiModel : EntityApiModel

    {
        public string Prefix { get; set; }
        public string Number { get; set; }
        public TypeApiModel PhoneNumberType { get; set; }

        public PhoneNumberApiModel()
        {

        }
    }
}