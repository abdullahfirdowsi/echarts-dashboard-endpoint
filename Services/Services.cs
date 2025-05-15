// filepath: InternsApi/Services/ExcelService.cs
using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using InternsApi.Models;
using System.Linq;

namespace InternsApi.Services
{
    public class ExcelService
    {
        public List<Intern> GetInternsData(string filePath)
        {
            var interns = new List<Intern>();
            
            try
            {
                using (var workbook = new XLWorkbook(filePath))
                {
                    var worksheet = workbook.Worksheet(1); // Get the first worksheet
                    if (worksheet == null)
                    {
                        throw new Exception("Excel worksheet not found");
                    }
                    
                    var rows = worksheet.RowsUsed();
                    
                    foreach (var row in rows.Skip(1)) // Skip the header row
                    {
                        try
                        {
                            // Attempt to safely get values with proper error handling
                            var intern = new Intern
                            {
                                Id = GetIntValueFromCell(row.Cell(1), 0),
                                InternID = GetStringValueFromCell(row.Cell(2)),
                                Name = GetStringValueFromCell(row.Cell(3)),
                                Location = GetStringValueFromCell(row.Cell(4)),
                                ProgrammingLanguage = GetStringValueFromCell(row.Cell(5)),
                                OfficialEmailID = GetStringValueFromCell(row.Cell(6)),
                                CollegeName = GetStringValueFromCell(row.Cell(7)),
                                PrimarySkill = GetStringValueFromCell(row.Cell(8)),
                                SecondarySkill = GetStringValueFromCell(row.Cell(9)),
                                AreaOfInterest = GetStringValueFromCell(row.Cell(10)),
                                BU = GetStringValueFromCell(row.Cell(11))
                            };
                            
                            interns.Add(intern);
                        }
                        catch (Exception ex)
                        {
                            // Log error but continue processing other rows
                            Console.WriteLine($"Error processing row: {ex.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Rethrow with more context
                throw new Exception($"Error reading Excel file: {ex.Message}", ex);
            }
            
            return interns;
        }
        
        // Helper methods for safe cell access
        private string GetStringValueFromCell(IXLCell cell)
        {
            return cell?.GetString() ?? string.Empty;
        }
        
        private int GetIntValueFromCell(IXLCell cell, int defaultValue)
        {
            if (cell == null)
                return defaultValue;
                
            string value = cell.GetString();
            if (int.TryParse(value, out int result))
                return result;
                
            return defaultValue;
        }
    }
}
