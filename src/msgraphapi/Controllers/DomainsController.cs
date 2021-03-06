﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using msgraphapi.MsGraph;

namespace msgraphapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DomainsController : ControllerBase
    {
        private readonly DomainsService _domainsService;

        public DomainsController(DomainsService domainsService)
        {
            _domainsService = domainsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 100)
        {
            var supportedDomains = await _domainsService.GetDomainsAsync(new Page(pageNumber, pageSize));
            return Ok(supportedDomains);
        }
    }
}
