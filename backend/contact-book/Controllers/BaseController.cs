using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using contact_book.core.Models;
using contact_book.Models;
using MoreLinq.Extensions;
using Type = contact_book.core.Models.Type;

namespace contact_book.Controllers
{
    public class BaseController : ApiController
    {
        public Type ConvertToType(TypeApiModel typeModel)
        {
            return new Type
            {
                Id = typeModel.Id,
                TypeName = typeModel.TypeName
            };
        }

        public TypeApiModel ConvertToApiType(Type type)
        {
            return new TypeApiModel
            {
                Id = type.Id,
                TypeName = type.TypeName
            };
        }

        public ICollection<Address> ConvertToAddressList(ICollection<AddressApiModel> addressModels)
        {
            return addressModels.Select(addressModel => new Address
            {
                Id = addressModel.Id, 
                FullAddress = addressModel.FullAddress, 
                AddressType = ConvertToType(addressModel.AddressType)
            }).ToList();
        }

        public ICollection<AddressApiModel> ConvertToApiAddressList(ICollection<Address> addresses)
        {

            return addresses.Select(address => new AddressApiModel
            {
                Id = address.Id,
                FullAddress = address.FullAddress,
                AddressType = ConvertToApiType(address.AddressType)
            }).ToList();
        }

        public ICollection<Email> ConvertToEmailList(ICollection<EmailApiModel> emailModels)
        {
            return emailModels.Select(emailModel => new Email
            {
                Id = emailModel.Id,
                EmailName = emailModel.EmailName,
                EmailType = ConvertToType(emailModel.EmailType)
            }).ToList();
        }

        public ICollection<EmailApiModel> ConvertToApiEmailList(ICollection<Email> emails)
        {
            return emails.Select(email => new EmailApiModel
            {
                Id = email.Id,
                EmailName = email.EmailName,
                EmailType = ConvertToApiType(email.EmailType)
            }).ToList();
        }

        public ICollection<PhoneNumber> ConvertToPhoneList(ICollection<PhoneNumberApiModel> phoneModels)
        {
            return phoneModels.Select(phoneModel => new PhoneNumber
            {
                Id = phoneModel.Id,
                Prefix = phoneModel.Prefix,
                Number = phoneModel.Number,
                PhoneNumberType = ConvertToType(phoneModel.PhoneNumberType)
            }).ToList();
        }

        public ICollection<PhoneNumberApiModel> ConvertToApiPhoneList(ICollection<PhoneNumber> phones)
        {
            return phones.Select(phone => new PhoneNumberApiModel
            {
                Id = phone.Id,
                Prefix = phone.Prefix,
                Number = phone.Number,
                PhoneNumberType = ConvertToApiType(phone.PhoneNumberType)
            }).ToList();
        }

        public Contact ConvertToContact(ContactApiModel contactModel)
        {
            return new Contact
            {
                Id = contactModel.Id,
                FirstName = contactModel.FirstName,
                LastName = contactModel.LastName,
                Company = contactModel.Company,
                Notes = contactModel.Notes,
                BirthDate = contactModel.BirthDate,
                Addresses = contactModel.Addresses != null ? ConvertToAddressList(contactModel.Addresses) : null,
                PhoneNumbers = contactModel.PhoneNumbers != null ? ConvertToPhoneList(contactModel.PhoneNumbers) : null,
                Emails = contactModel.Emails != null ? ConvertToEmailList(contactModel.Emails) : null
            };
        }

        public ContactApiModel ConvertToApiContact(Contact contact)
        {
            return new ContactApiModel
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Company = contact.Company,
                Notes = contact.Notes,
                BirthDate = contact.BirthDate,
                Addresses = contact.Addresses != null ? ConvertToApiAddressList(contact.Addresses) : null,
                PhoneNumbers = contact.PhoneNumbers != null ? ConvertToApiPhoneList(contact.PhoneNumbers) : null,
                Emails = contact.Emails != null ? ConvertToApiEmailList(contact.Emails) : null
            };
        }
    }
}
