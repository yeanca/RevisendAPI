using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RevisendAPI.Data;
using RevisendAPI.Data.BindingModel.Enums;
using RevisendAPI.Data.Models;

namespace RevisendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly RevisendAPIContext _context;
        public AddressController(RevisendAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<object> GetShipmentAddress()
        {
            try
            {
                var result = await _context.ReviAddresses.ToListAsync();
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Revisend Address Record here!", result));
            }
            catch (Exception Ex)
            {

                return await Task.FromResult(new ResponseModel(ResponseCode.Error, Ex.Message, "null"));
            }
        }

        [HttpGet("GetMyAddresses")]
        public async Task<object> GetMyShipmentAddress(int userId)
        {
            try
            {
                var result = await _context.UserAddresses.Where(x => x.UserId == userId).ToListAsync();
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Address Record here!", result));
            }
            catch (Exception Ex)
            {

                return await Task.FromResult(new ResponseModel(ResponseCode.Error, Ex.Message, "null"));
            }
        }

        [HttpPost("AddMyAddress")]
        public async Task<object> AddMyAddress(UserAddress address)
        {
            try
            {
                await _context.UserAddresses.AddAsync(address);
                await _context.SaveChangesAsync();
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Address Record here!", new UserAddress() { AddressId = address.AddressId, UserId = address.UserId, Country = address.Country, Address = address.Address }));
            }
            catch (Exception Ex)
            {

                return await Task.FromResult(new ResponseModel(ResponseCode.Error, Ex.Message, "null"));
            }
        }

        [HttpPost("EditMyAddress")]
        public async Task<object> ReasonForLateness(UserAddress address)
        {

            var obj = await _context.UserAddresses.SingleOrDefaultAsync(x => x.AddressId == address.AddressId);
            if (obj == null)
            {
                return BadRequest();
            }
            obj.Address = address.Address;
            obj.Country = address.Country;
            _context.Entry(obj).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExist(obj.AddressId))
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Not Found!", "null"));
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Reason Updated!", obj));

        }

        private bool AddressExist(int id)
        {
            return _context.UserAddresses.Any(e => e.AddressId == id);
        }
    }
}
