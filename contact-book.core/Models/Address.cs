namespace contact_book.core.Models
{
    public class Address : Entity
    {
        public string FullAddress { get; set; }
        public Type AddressType { get; set; }
    }
}