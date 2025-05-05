using Microsoft.AspNetCore.Mvc;
using InternsApi.Services;
using InternsApi.Models;
using System.Collections.Generic;

namespace InternsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InternsController : ControllerBase
    {
        private readonly ExcelService _excelService;

        public InternsController()
        {
            _excelService = new ExcelService();
        }

        [HttpGet]
        public ActionResult<List<Intern>> GetInterns()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "InternsData.xlsx");
            var interns = _excelService.GetInternsData(filePath);
            return Ok(interns);
        }
    }
}