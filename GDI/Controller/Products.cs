using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GDI.Controller
{
   // [Route("api/[controller]")]
    [ApiController]
    public class Products : ControllerBase
    {
        [HttpGet]
        [Route("api/test12")]
        public string GetString()
        {
            return "Hello API";
        }
    }
}
