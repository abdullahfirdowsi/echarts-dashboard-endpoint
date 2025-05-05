// filepath: InternsApi/Services/ExcelService.cs
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using InternsApi.Models;

namespace InternsApi.Services
{
    public class ExcelService
    {
        public List<Intern> GetInternsData(string filePath)
        {
            var interns = new List<Intern>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1); // Get the first worksheet
                var rows = worksheet.RowsUsed();

                foreach (var row in rows.Skip(1)) // Skip the header row
                {
                    interns.Add(new Intern
                    {
                        SNo = int.Parse(row.Cell(1).GetValue<string>()),
                        InternID = row.Cell(2).GetValue<string>(),
                        Name = row.Cell(3).GetValue<string>(),
                        Location = row.Cell(4).GetValue<string>(),
                        ProgrammingLanguage = row.Cell(5).GetValue<string>(),
                        OfficialEmailID = row.Cell(6).GetValue<string>(),
                        CollegeName = row.Cell(7).GetValue<string>(),
                        PrimarySkill = row.Cell(8).GetValue<string>(),
                        SecondarySkill = row.Cell(9).GetValue<string>(),
                        AreaOfInterest = row.Cell(10).GetValue<string>(),
                        BU = row.Cell(11).GetValue<string>()
                    });
                }
            }

            return interns;
        }
    }
}

//using System.Collections.Generic;
//using System.IO;
//using OfficeOpenXml;
//using InternsApi.Models;

//namespace InternsApi.Services
//{
//    public class ExcelService
//    {
//        public List<Intern> GetInternsData(string filePath)
//        {
//            var interns = new List<Intern>();

//            // Set the license context for EPPlus
//            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

//            using (var package = new ExcelPackage(new FileInfo(filePath)))
//            {
//                var worksheet = package.Workbook.Worksheets[0];
//                var rowCount = worksheet.Dimension.Rows;

//                for (int row = 2; row <= rowCount; row++) // Start from row 2 to skip headers
//                {
//                    interns.Add(new Intern
//                    {
//                        SNo = int.Parse(worksheet.Cells[row, 1].Text),
//                        InternID = worksheet.Cells[row, 2].Text,
//                        Name = worksheet.Cells[row, 3].Text,
//                        Location = worksheet.Cells[row, 4].Text,
//                        ProgrammingLanguage = worksheet.Cells[row, 5].Text,
//                        OfficialEmailID = worksheet.Cells[row, 6].Text,
//                        CollegeName = worksheet.Cells[row, 7].Text,
//                        PrimarySkill = worksheet.Cells[row, 8].Text,
//                        SecondarySkill = worksheet.Cells[row, 9].Text,
//                        AreaOfInterest = worksheet.Cells[row, 10].Text,
//                        BU = worksheet.Cells[row, 11].Text
//                    });
//                }
//            }

//            return interns;
//        }
//    }
//}