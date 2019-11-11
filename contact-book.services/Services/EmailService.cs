using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using contact_book.core.Models;
using contact_book.data;

namespace contact_book.services.Services
{
    public class EmailService
    {
        public async Task<ICollection<Email>> GetEmailsByContactId(int id)
        {
            using (var context = new ContactBookDbContext())
            {
                return await context.Email
                    .Where(e => e.ContactId == id)
                    .Include(e => e.EmailType)
                    .ToListAsync();
            }
        }

        public async Task<Email> GetEmailById(int id)
        {
            using (var context = new ContactBookDbContext())
            {
                var result = await context.Email
                    .Where(e => e.Id == id)
                    .FirstOrDefaultAsync();
                return result;
            }
        }
        public async Task DeleteEmailById(int id)
        {
            using (var context = new ContactBookDbContext())
            {
                context.Email.Remove(context.Email.Single(e => e.Id == id));
                await context.SaveChangesAsync();
            }
        }
    }
}