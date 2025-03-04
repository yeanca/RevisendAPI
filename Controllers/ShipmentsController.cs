using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RevisendAPI.Data;
using RevisendAPI.Data.BindingModel.Enums;
using RevisendAPI.Data.Entities;
using RevisendAPI.Data.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RevisendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentsController : ControllerBase
    {
        private readonly RevisendAPIContext _context;
        public ShipmentsController(RevisendAPIContext context)
        {
            _context = context;
        }


        //Users get their shipment status from a time range
        [HttpGet]
        public async Task<object> GetMyShipments(int userId)
        {
            try
            {
                var result = await _context.Shipments.Select(a=>new UserShipmentDTO()
                                    {
                                        ShipmentNo = a.ShipmentNo,
                                        ByUser = a.ByUser,
                                        SourceCountry = a.SourceCountry,
                                        SourceStore = a.SourceStore,
                                        CreatedAt = a.CreatedAt,
                                        Weight = a.Weight,
                                        DateReceived = a.DateReceived,
                                        DateDelivered = a.DateDelivered,
                                        DateEta = a.DateEta,
                                        StatusId=a.Status,
                                        Status = a.ShipmentStatus.Status
                                    }).Where(x => x.ByUser == userId).OrderBy(x => x.CreatedAt).ToListAsync();

                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Shipment Record here!", result));
            }
            catch (Exception Ex)
            {

                return await Task.FromResult(new ResponseModel(ResponseCode.Error, Ex.Message, "null"));
            }
        }

        [HttpGet("MyShipments")]
        public async Task<object> GetMyShipmentStatus(int userId, int statusId)
        {
            try
            {
                var result = await _context.Shipments.Select(a => new UserShipmentDTO()
                {
                    ShipmentNo = a.ShipmentNo,
                    ByUser = a.ByUser,
                    SourceCountry = a.SourceCountry,
                    SourceStore = a.SourceStore,
                    CreatedAt = a.CreatedAt,
                    Weight = a.Weight,
                    DateReceived = a.DateReceived,
                    DateDelivered = a.DateDelivered,
                    DateEta = a.DateEta,
                    StatusId = a.Status,
                    Status = a.ShipmentStatus.Status
                }).Where(x => x.ByUser == userId && x.StatusId == statusId).OrderBy(x => x.CreatedAt).ToListAsync();

                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Shipment Record here!", result));
            }
            catch (Exception Ex)
            {

                return await Task.FromResult(new ResponseModel(ResponseCode.Error, Ex.Message, "null"));
            }
        }

        [HttpGet("TotalShipments")]
        public async Task<object> GetTotalShipments(int userId)
        {
            try
            {
                var result=await _context.Shipments.Where(x=>x.ByUser== userId).ToListAsync();
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Total", result.Count));
            }
            catch (Exception Ex)
            {

                return await Task.FromResult(new ResponseModel(ResponseCode.Error, Ex.Message, "null"));
            }
        }

    }
}
