using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using contact_book.core.Models;
using contact_book.data;
using contact_book.services.Validators;
using MoreLinq;

namespace contact_book.services
{
    public class ContactService
    {
        public async Task<ICollection<Contact>> GetAllContacts()
        {
            using (var context = new ContactBookDbContext())
            {
                return await context.Contact
                    .Include(c => c.PhoneNumbers)
                    .Include(c => c.Addresses)
                    .Include(c => c.Emails)
                    .ToListAsync();
            }
        }

        public async Task<Contact> GetContactById(int id)
        {
            using (var context = new ContactBookDbContext())
            {
                return await context.Contact
                    .Where(c => c.Id == id)
                    .Include(c => c.PhoneNumbers)
                    .Include(c => c.Addresses)
                    .Include(c => c.Emails)
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<ServiceResult> SearchContacts(string search)
        {
            var validator = new SearchValidator();
            var validationResult = validator.Validate(search);
            if(!validationResult.IsValid)
                return new ServiceResult(false, validationResult);
            using (var context = new ContactBookDbContext())
            {
                var result = await context.Contact.Where(s =>
                        s.FirstName.ToLower().Contains(search) ||
                        s.LastName.ToLower().Contains(search) ||
                        s.Company.ToLower().Contains(search) ||
                        s.PhoneNumbers.Any(p => p.Prefix.Contains(search) ||
                                                p.Number.Contains(search)) ||
                        s.Emails.Any(e => e.EmailName.ToLower().Contains(search)))
                    .Include(s => s.PhoneNumbers)
                    .Include(s => s.Addresses)
                    .Include(s => s.Emails) 
                    .ToListAsync();

                return new ServiceResult(true, result);
            }
        }

        public async Task<ServiceResult> AddContact(Contact contact)
        {
            var validator = new ContactValidator();
            var validationResult = validator.Validate(contact);
            if(!validationResult.IsValid)
                return new ServiceResult(false, validationResult);

            using (var context = new ContactBookDbContext())
            {
                if (contact.Addresses != null)
                {
                    foreach (var address in contact.Addresses)
                    {
                        var addressType = context.Type
                            .FirstOrDefault(t => t.TypeName == address.AddressType.TypeName);
                        if (addressType != default)
                            address.AddressType = addressType;
                    }
                }

                if (contact.Emails != null)
                {
                    foreach (var email in contact.Emails)
                    {
                        var emailType = context.Type
                            .FirstOrDefault(t => t.TypeName == email.EmailType.TypeName);
                        if (emailType != default)
                            email.EmailType = emailType;
                    }
                }

                if (contact.PhoneNumbers != null)
                {
                    foreach (var number in contact.PhoneNumbers)
                    {
                        var numberType = context.Type
                            .FirstOrDefault(t => t.TypeName == number.PhoneNumberType.TypeName);
                        if (numberType != default)
                            number.PhoneNumberType = numberType;
                    }
                }

                context.Contact.Add(contact);
                await context.SaveChangesAsync();
                return new ServiceResult(true, contact);
            }
        }
        public async Task DeleteContactById(int id)
        {
            using (var context = new ContactBookDbContext())
            {
                var contact = await context.Contact
                    .Where(c => c.Id == id)
                    .Include(c => c.PhoneNumbers)
                    .Include(c => c.Addresses)
                    .Include(c => c.Emails)
                    .FirstOrDefaultAsync();

                if(contact.Addresses != null)
                    foreach (var address in contact.Addresses.ToList())
                    {
                        context.Address.Remove(address);
                    }
                if(contact.Emails != null)
                    foreach (var email in contact.Emails.ToList())
                    {
                        context.Email.Remove(email);
                    }
                if(contact.PhoneNumbers != null)
                    foreach (var phoneNumber in contact.PhoneNumbers.ToList())
                    {
                        context.PhoneNumber.Remove(phoneNumber);
                    }

                context.Contact.Remove(context.Contact.Single(c => c.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAllContacts()
        {
            using (var context = new ContactBookDbContext())
            {
                context.Address.RemoveRange(context.Address);
                context.Email.RemoveRange(context.Email);
                context.PhoneNumber.RemoveRange(context.PhoneNumber);
                context.Type.RemoveRange(context.Type);
                context.Contact.RemoveRange(context.Contact);
                await context.SaveChangesAsync();
                await context.Database.ExecuteSqlCommandAsync("DBCC CHECKIDENT ('Contacts.dbo.Contact', RESEED, 0)");
                await context.Database.ExecuteSqlCommandAsync("DBCC CHECKIDENT ('Contacts.dbo.Address', RESEED, 0)");
                await context.Database.ExecuteSqlCommandAsync("DBCC CHECKIDENT ('Contacts.dbo.Email', RESEED, 0)");
                await context.Database.ExecuteSqlCommandAsync("DBCC CHECKIDENT ('Contacts.dbo.PhoneNumber', RESEED, 0)");
                await context.Database.ExecuteSqlCommandAsync("DBCC CHECKIDENT ('Contacts.dbo.Type', RESEED, 0)");
            }
        }
    }
}