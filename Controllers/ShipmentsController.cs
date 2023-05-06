using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevisendAPI.Data;

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
    }
}
