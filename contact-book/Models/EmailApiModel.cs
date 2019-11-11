using contact_book.core.Models;

namespace contact_book.Models
{
    public class EmailApiModel : EntityApiModel
    {
        public string EmailName { get; set; }
        public TypeApiModel EmailType { get; set; }

        public EmailApiModel()
        {

        }
    }
}