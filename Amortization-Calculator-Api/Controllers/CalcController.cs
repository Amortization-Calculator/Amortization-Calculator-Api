using Amortization_Calculator_Api.Dtos;
using Amortization_Calculator_Api.Services.lease_contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Amortization_Calculator_Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CalcController : ControllerBase
    {


        private readonly IWebHostEnvironment _hostingEnvironment;

        public CalcController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }









        [HttpPost]
        public async Task<IActionResult> CalcRental([FromBody] CalcDto calcDto)
        {

            Random r = new Random();
            var x = r.Next(0, 1000000);
            string sessionId = x.ToString("0000");
            var lcontract = new LeaseContract(sessionId);
            lcontract.AssetCost = calcDto.AssetCost;
            lcontract.AmountFinance = calcDto.AmountFinance;
            lcontract.IntrestRate = calcDto.IntrestRate;
            lcontract.EffectiveRate = (decimal)calcDto.efactiveintrest;
            lcontract.NoOfRental = calcDto.NoOfRental;
            lcontract.RentalInterval = calcDto.RentalInterval;
            lcontract.Rentaltype = 0;
            //= Equal
            //== Compare                                   
            lcontract.Begining = calcDto.SelectedRadio == "2" ? true : false;
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
 

            var result = new Result { rental = lcontract.rental };


            return Ok(result);

        }




    }
}
