using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using contact_book.core.Models;
using contact_book.data;

namespace contact_book.services.Services
{
    public class AddressService
    {
        public async Task<ICollection<Address>> GetAddressesByContactId(int id)
        {
            using (var context = new ContactBookDbContext())
            {
                return await context.Contact
                    .Where(c => c.Id == id)
                    .SelectMany(c => c.Addresses)
                    .Include(a => a.AddressType)
                    .ToListAsync();
            }
        }
        public async Task<Address> GetAddressById(int id)
        {
            using (var context = new ContactBookDbContext())
            {
                var result = await context.Address
                    .Where(a => a.Id == id)
                    .FirstOrDefaultAsync();
                return result;
            }
        }
        public async Task DeleteAddressById(int id)
        {
            using (var context = new ContactBookDbContext())
            {
                context.Address.Remove(context.Address.Single(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }
    }
}