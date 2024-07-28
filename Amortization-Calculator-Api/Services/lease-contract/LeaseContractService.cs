using Microsoft.Office.Interop.Excel;

namespace Amortization_Calculator_Api.Services.lease_contract
{
    public class LeaseContractService : ILeaseConteactServicecs
    {


        public void OpenExcelApplication(string xlFileName, Worksheet ContractExcelSheet, Application ContractExcelApplication, Workbook ContractExcelWorkbook)
        {
            ContractExcelApplication = new Application();

            ContractExcelWorkbook = ContractExcelApplication.Workbooks.Open(xlFileName);

            ContractExcelSheet = (Worksheet)ContractExcelWorkbook.Worksheets[1];
        }


        public void CloseExcelApplication(Worksheet ContractExcelSheet, Application ContractExcelApplication, Workbook ContractExcelWorkbook)
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

        
        
        public ContractType GetContractType(short RentalInterval)
        {
            if (RentalInterval == 12)
            {
                return ContractType.Monthly;
            }
            else if (RentalInterval == 4)
            {
                return ContractType.Quarter;
            }

            else if (RentalInterval == 3)
            {
                return ContractType.Quarter1;
            }

            else if (RentalInterval == 2)
            {
                return ContractType.SemiAnnual;
            }

            else if (RentalInterval == 6)
            {
                return ContractType.Monthly1;
            }

            else if (RentalInterval == 1)
            {
                return ContractType.Annual;
            }
            else
            {
                return ContractType.None;
            }
        }
    }
}
