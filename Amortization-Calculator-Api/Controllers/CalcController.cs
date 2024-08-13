using Amortization_Calculator_Api.Dtos;
using Amortization_Calculator_Api.Models;
using Amortization_Calculator_Api.Services.lease_contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Office.Interop.Excel;


namespace Amortization_Calculator_Api.Controllers
{

    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CalcController : ControllerBase
    {


        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public CalcController(IWebHostEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager)
        {
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }



        [HttpPost]
        public async Task<IActionResult> CalcRental([FromBody] CalcDto calcDto)
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);
            
            if (user == null) {
                return Unauthorized();
            }





            Random r = new Random();
            var x = r.Next(0, 1000000);

            string sessionId = x.ToString("0000");
            var lcontract = new LeaseContract(sessionId);
            lcontract.AssetCost = calcDto.AssetCost;
            lcontract.AmountFinance = calcDto.AmountFinance;
            lcontract.IntrestRate = calcDto.IntrestRate;
            lcontract.EffectiveRate = calcDto.EffectiveRate;
            lcontract.NoOfRental = calcDto.NoOfRental;
            lcontract.RentalInterval = calcDto.RentalInterval;
            lcontract.Rentaltype = 0;                          
            lcontract.Begining = calcDto.Begining;
            lcontract.GressPriod = calcDto.GressPriod;
            lcontract.ResedialValue = calcDto.ResedialValue;
            lcontract.ActualDay = true;
            lcontract.ContractDate = DateTime.Now;
            lcontract.FirstDate = DateTime.Now.AddMonths(1);
            lcontract.StartFromFristMonth = calcDto.startFromFristMonth;
            lcontract.Customerno = 1;
            lcontract.Contractno = 1;
            lcontract.FilePath = Path.Combine(_hostingEnvironment.ContentRootPath, "");
            lcontract.SavePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Excel");
            lcontract.Calculate();

            user.usageLease += 1;
            await _userManager.UpdateAsync(user);


            var result = new Result { rental = lcontract.rental , excelFile = sessionId+".xls"};


            return Ok(result);

        }







        [HttpGet("{fileName}")]
        public async Task<IActionResult> GetExcelFile(string fileName)
        {
            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Excel", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            Application excelApp = new Application();
            Workbook workbook = excelApp.Workbooks.Open(filePath);

            var tempFilePath = Path.Combine(Path.GetTempPath(), fileName);

            workbook.SaveAs(tempFilePath);
            workbook.Close();
            excelApp.Quit();

            var fileBytes = await System.IO.File.ReadAllBytesAsync(tempFilePath);
            System.IO.File.Delete(tempFilePath);

            var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

           
            var result = File(fileBytes, mimeType, fileName);

            Response.OnCompleted(() =>
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                return Task.CompletedTask;
            });

            return result;
        }









        [HttpGet("pdf/{fileName}")]
        public async Task<IActionResult> GetPdfFile(string fileName)
        {
            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Excel", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            Application excelApp = new Application();

            Workbook workbook = excelApp.Workbooks.Open(filePath);

            var tempPdfFilePath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(fileName) + ".pdf");

            // Save the workbook as a PDF
            workbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, tempPdfFilePath);

            workbook.Close();
            excelApp.Quit();

            var fileBytes = System.IO.File.ReadAllBytes(tempPdfFilePath);
            System.IO.File.Delete(tempPdfFilePath);

            var mimeType = "application/pdf";
            var pdfFileName = Path.GetFileNameWithoutExtension(fileName) + ".pdf";
            return File(fileBytes, mimeType, pdfFileName);
        }


    }
}
