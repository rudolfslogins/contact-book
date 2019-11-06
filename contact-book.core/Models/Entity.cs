using contact_book.core.Interfaces;

namespace contact_book.core.Models
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}