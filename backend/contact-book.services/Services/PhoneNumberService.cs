using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using contact_book.core.Models;
using contact_book.data;

namespace contact_book.services.Services
{
    public class PhoneNumberService
    {
        public async Task<ICollection<PhoneNumber>> GetPhoneNumbersByContactId(int id)
        {
            using (var context = new ContactBookDbContext())
            {
                return await context.PhoneNumber
                    .Where(pn => pn.ContactId == id)
                    .Include(pn => pn.PhoneNumberType)
                    .ToListAsync();
            }
        }
        public async Task<PhoneNumber> GetPhoneNumberById(int id)
        {
            using (var context = new ContactBookDbContext())
            {
                var result = await context.PhoneNumber
                    .Where(e => e.Id == id)
                    .FirstOrDefaultAsync();
                return result;
            }
        }
        public async Task DeletePhoneNumberById(int id)
        {
            using (var context = new ContactBookDbContext())
            {
                context.PhoneNumber.Remove(context.PhoneNumber.Single(e => e.Id == id));
                await context.SaveChangesAsync();
            }
        }
    }
}