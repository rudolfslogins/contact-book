using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using contact_book.core.Models;
using contact_book.services.Services;

namespace contact_book.Controllers
{
    public class PhoneNumberController : BaseController
    {
        private readonly PhoneNumberService _phoneNumberService;
        public PhoneNumberController()
        {
            _phoneNumberService = new PhoneNumberService();
        }
        //GET api/contacts/{id}/phoneNumbers
        [HttpGet]
        [Route("api/contacts/{id}/phoneNumbers")]
        public async Task<IHttpActionResult> GetPhoneNumbersByContactId(int id)
        {
            var result = await _phoneNumberService.GetPhoneNumbersByContactId(id);
            return Ok(ConvertToApiPhoneList(result));
        }
        //DELETE api/
        [HttpDelete]
        [Route("api/phoneNumber/{id}")]
        public async Task<IHttpActionResult> DeletePhoneNumberById(int id)
        {
            var phoneNumber = await _phoneNumberService.GetPhoneNumberById(id);
            if (phoneNumber == null)
                return NotFound();

            await _phoneNumberService.DeletePhoneNumberById(id);
            return Ok();
        }
    }
}
