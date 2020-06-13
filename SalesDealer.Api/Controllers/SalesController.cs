using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SalesDealer.Services;
using SalesDealer.Shared;

namespace SalesDealer.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly SalesService _salesService;

        public SalesController(SalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpGet()]
        public ActionResult<GenericResponse<string>> GenerateAllSales()
        {
            return Ok(new GenericResponse<string>()
            {
                Data = _salesService.GenerateSalesFile(),
                Success = true
            });
        }

        [HttpGet("{fileName}")]
        public ActionResult<GenericResponse<IList<SalesFH>>> GetSalesFromFile(string fileName)
        {
            return Ok(new GenericResponse<IList<SalesFH>>()
            {
                Data = _salesService.GetSalesFromFile(fileName),
                Success = true
            });
        }
    }
}
