using Amortization_Calculator_Api.Dtos;
using Microsoft.Office.Interop.Excel;

namespace Amortization_Calculator_Api.Services.lease_contract
{
    public class LeaseContract
    {






        public Worksheet ContractExcelSheet { get; private set; }

        private Application ContractExcelApplication { get; set; }

        private Workbook ContractExcelWorkbook { get; set; }















        private void OpenExcelApplication(string xlFileName)
        {

            ContractExcelApplication = new Application();

            ContractExcelWorkbook = ContractExcelApplication.Workbooks.Open(xlFileName);

            ContractExcelSheet = (Worksheet)ContractExcelWorkbook.Worksheets[1];
        
        }

        private void CloseExcelApplication()
        {

            //Clear worksheet reference
            ContractExcelSheet = null;
            
            //Close excel workbook
            ContractExcelWorkbook.Save();
            ContractExcelWorkbook.Close(true);

            //Quit or exit the excel application
            ContractExcelApplication.Quit();
            ContractExcelApplication = null;

        }






    }
}
