using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using contact_book.services.Services;

namespace contact_book.Controllers
{
    public class AddressController : BaseController
    {
        private readonly AddressService _addressService;
        public AddressController()
        {
            _addressService = new AddressService();
        }
        //GET api/contacts/{id}/addresses
        [HttpGet]
        [Route("api/contacts/{id}/addresses")]
        public async Task<IHttpActionResult> GetAddressesByContactId(int id)
        {
            var result = await _addressService.GetAddressesByContactId(id);
            return Ok(ConvertToApiAddressList(result));
        }
        //DELETE api/
        [HttpDelete]
        [Route("api/address/{id}")]
        public async Task<IHttpActionResult> DeleteAddressById(int id)
        {
            var address = await _addressService.GetAddressById(id);
            if (address == null)
                return NotFound();

            await _addressService.DeleteAddressById(id);
            return Ok();
        }
    }
}
