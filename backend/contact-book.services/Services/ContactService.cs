using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using contact_book.core.Models;
using contact_book.data;
using contact_book.services.Interfaces;
using contact_book.services.Validators;

namespace contact_book.services.Services
{
    public class ContactService : IService
    {
        public async Task<ICollection<Contact>> GetAllContacts()
        {
            using (var context = new ContactBookDbContext())
            {
                return await context.Contact
                    .Include(c => c.PhoneNumbers.Select(pn => pn.PhoneNumberType))
                    .Include(c => c.Addresses.Select(a => a.AddressType))
                    .Include(c => c.Emails.Select(e => e.EmailType))
                    .ToListAsync();
            }
        }

        public async Task<Contact> GetContactById(int id)
        {
            using (var context = new ContactBookDbContext())
            {
                return await context.Contact
                    .Where(c => c.Id == id)
                    .Include(c => c.PhoneNumbers.Select(pn => pn.PhoneNumberType))
                    .Include(c => c.Addresses.Select(a => a.AddressType))
                    .Include(c => c.Emails.Select(e => e.EmailType))
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
                var result = await context.Contact
                    .Where(s =>
                        s.FirstName.ToLower().Contains(search) ||
                        s.LastName.ToLower().Contains(search) ||
                        s.Company.ToLower().Contains(search) ||
                        s.PhoneNumbers.Any(p => p.Prefix.Contains(search) ||
                                                p.Number.Contains(search)) ||
                        s.Emails.Any(e => e.EmailName.ToLower().Contains(search)))
                    .Include(c => c.PhoneNumbers.Select(pn => pn.PhoneNumberType))
                    .Include(c => c.Addresses.Select(a => a.AddressType))
                    .Include(c => c.Emails.Select(e => e.EmailType))
                    .ToListAsync();

                return new ServiceResult(true, result);
            }
        }

        public async Task<ICollection<Type>> GetAllTypes()
        {
            using (var context = new ContactBookDbContext())
            {
                return await context.Type.ToListAsync();
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
                await CheckContactTypes(contact, context);
                context.Contact.Add(contact);
                await context.SaveChangesAsync();
                return new ServiceResult(true, contact);
            }
        }

        public async Task<ServiceResult> UpdateContact(int id, Contact newContactDetails)
        {
            var validator = new ContactValidator();
            var validationResult = validator.Validate(newContactDetails);
            if (!validationResult.IsValid)
                return new ServiceResult(false, validationResult);
            await DeleteEntities(id, newContactDetails);
            using (var context = new ContactBookDbContext())
            {
                var contactToUpdate = await context.Contact
                    .Where(c => c.Id == id)
                    .Include(c => c.PhoneNumbers.Select(pn => pn.PhoneNumberType))
                    .Include(c => c.Addresses.Select(a => a.AddressType))
                    .Include(c => c.Emails.Select(e => e.EmailType))
                    .FirstOrDefaultAsync();

                if (contactToUpdate == default)
                    return new ServiceResult(false, "Contact not found!");

                
                await CheckContactTypes(newContactDetails, context);

                contactToUpdate.FirstName = newContactDetails.FirstName;
                contactToUpdate.LastName = newContactDetails.LastName;
                contactToUpdate.BirthDate = newContactDetails.BirthDate;
                contactToUpdate.Company = newContactDetails.Company;
                contactToUpdate.Notes = newContactDetails.Notes;

                if (newContactDetails.PhoneNumbers != null)

                    foreach (var phoneNumber in newContactDetails.PhoneNumbers)
                    {
                        var phoneToUpdate = await context.PhoneNumber
                            .Where(pn => pn.Id == phoneNumber.Id)
                            .FirstOrDefaultAsync();
                        if (phoneToUpdate == default)
                            contactToUpdate.PhoneNumbers.Add(phoneNumber);
                        else
                        {
                            phoneToUpdate.Prefix = phoneNumber.Prefix;
                            phoneToUpdate.Number = phoneNumber.Number;
                            phoneToUpdate.PhoneNumberType = phoneNumber.PhoneNumberType;
                        }
                    }

                if(newContactDetails.Emails != null)
                    foreach (var email in newContactDetails.Emails)
                    {
                        var emailToUpdate = await context.Email
                            .Where(e => e.Id == email.Id)
                            .FirstOrDefaultAsync();
                        if (emailToUpdate == default)
                            contactToUpdate.Emails.Add(email);
                        else
                        {
                            emailToUpdate.EmailName = email.EmailName;
                            emailToUpdate.EmailType = email.EmailType;
                        }
                    }

                if (newContactDetails.Addresses != null)
                    foreach (var address in newContactDetails.Addresses)
                    {
                        var addressToUpdate = await context.Address
                            .Where(a => a.Id == address.Id)
                            .FirstOrDefaultAsync();
                        if (addressToUpdate == default)
                            contactToUpdate.Addresses.Add(address);
                        else
                        {
                            addressToUpdate.FullAddress = address.FullAddress;
                            addressToUpdate.AddressType = address.AddressType;
                        }
                    }

                await context.SaveChangesAsync();
                return new ServiceResult(true, contactToUpdate);
            }
        }

        public async Task DeleteEntities(int id, Contact contact)
        {
            var addressService = new AddressService();
            var phoneService = new PhoneNumberService();
            var emailService = new EmailService();

            var addresses = await addressService.GetAddressesByContactId(id);
            foreach (var address in addresses)
            {
                if (contact.Addresses.FirstOrDefault(a => a.Id == address.Id) == default)
                    await addressService.DeleteAddressById(address.Id);
            }

            var phoneNumbers = await phoneService.GetPhoneNumbersByContactId(id);
            foreach (var phone in phoneNumbers)
            {
                if (contact.PhoneNumbers.FirstOrDefault(ph => ph.Id == phone.Id) == default)
                    await phoneService.DeletePhoneNumberById(phone.Id);
            }

            var emails = await emailService.GetEmailsByContactId(id);
            foreach (var email in emails)
            {
                if (contact.Emails.FirstOrDefault(e => e.Id == email.Id) == default)
                    await emailService.DeleteEmailById(email.Id);
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

                if(contact?.Addresses != null)
                    foreach (var address in contact.Addresses.ToList())
                    {
                        context.Address.Remove(address);
                    }
                if(contact?.Emails != null)
                    foreach (var email in contact.Emails.ToList())
                    {
                        context.Email.Remove(email);
                    }
                if(contact?.PhoneNumbers != null)
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
                
                context.SeedMethod();
            }
        }

        public async Task CheckContactTypes(Contact contact, ContactBookDbContext context)
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
        }
    }
}