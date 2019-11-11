namespace contact_book.core.Models
{
    public class Email : Entity
    {
        public string EmailName { get; set; }
        public Type EmailType { get; set; }
        public int ContactId { get; set; }

        public Email()
        {

        }
    }
}