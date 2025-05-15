using Microsoft.AspNetCore.Mvc;
using InternsApi.Services;
using InternsApi.Models;
using System.Collections.Generic;
using System.IO;
using System;
using Microsoft.Extensions.Logging;

namespace InternsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InternsController : ControllerBase
    {
        private readonly ExcelService _excelService;
        private readonly ILogger<InternsController> _logger;

        public InternsController(ILogger<InternsController>? logger = null)
        {
            _excelService = new ExcelService();
            _logger = logger ?? new LoggerFactory().CreateLogger<InternsController>();
        }

        [HttpGet]
        public ActionResult<List<Intern>> GetInterns()
        {
            try
            {
                _logger.LogInformation("Getting interns data from Excel file");
                
                // Try multiple potential file paths
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "InternsData.xlsx");
                
                if (!System.IO.File.Exists(filePath))
                {
                    _logger.LogWarning($"Excel file not found at primary path: {filePath}");
                    
                    // Try alternative locations
                    string altPath = Path.Combine(AppContext.BaseDirectory, "Data", "InternsData.xlsx");
                    if (System.IO.File.Exists(altPath))
                    {
                        _logger.LogInformation($"Found Excel file at alternative path: {altPath}");
                        filePath = altPath;
                    }
                    else 
                    {
                        _logger.LogError($"Excel file not found at alternative path either: {altPath}");
                        return Problem(
                            detail: $"Excel file not found. Tried: {filePath} and {altPath}. Current directory: {Directory.GetCurrentDirectory()}, Base directory: {AppContext.BaseDirectory}",
                            title: "File Not Found",
                            statusCode: 500
                        );
                    }
                }
                
                _logger.LogInformation($"Reading Excel file from: {filePath}");
                var interns = _excelService.GetInternsData(filePath);
                _logger.LogInformation($"Successfully read {interns.Count} interns from Excel file");
                
                return Ok(interns);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing interns data");
                return Problem(
                    detail: $"Error reading Excel file: {ex.Message}. Stack trace: {ex.StackTrace}",
                    title: "Internal Server Error",
                    statusCode: 500
                );
            }
        }
    }
}