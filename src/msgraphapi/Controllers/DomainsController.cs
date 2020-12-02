using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using msgraphapi.MsGraph;

namespace msgraphapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DomainsController : ControllerBase
    {
        private readonly MsGraphClient _msGraphClient;

        public DomainsController(MsGraphClient msGraphClient)
        {
            _msGraphClient = msGraphClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var supportedDomains = await _msGraphClient.GetSupportedDomains();
            return Ok(supportedDomains);
        }
    }
}
