namespace eCommerce_RESTful_API.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public JsonResult Get()
        {
            Dictionary<string, int> keyValuePairs = new Dictionary<string, int>()
            {
                { "Rado", 16 },
            };
            return new JsonResult(Ok(keyValuePairs.ToList()));
        }
    }
}
