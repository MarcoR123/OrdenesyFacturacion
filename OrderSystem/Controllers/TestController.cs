using Microsoft.AspNetCore.Mvc;
using OrderSystem.Data;

namespace OrderSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly OrdersDbContext _context;

        public TestController(OrdersDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                _context.Database.CanConnect();
                return Ok("Connection successful.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Connection failed: {ex.Message}");
            }
        }
    }
}
