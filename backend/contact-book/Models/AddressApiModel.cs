namespace contact_book.Models
{
    public class AddressApiModel : EntityApiModel
    {
        public string FullAddress { get; set; }
        public TypeApiModel AddressType { get; set; }

        public AddressApiModel()
        {

        }
    }
}