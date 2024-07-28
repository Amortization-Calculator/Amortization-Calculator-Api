using Microsoft.Office.Interop.Excel;

namespace Amortization_Calculator_Api.Services.lease_contract
{
    public interface ILeaseConteactServicecs
    {

        public void OpenExcelApplication(string xlFileName , Worksheet ContractExcelSheet , Application ContractExcelApplication , Workbook ContractExcelWorkbook);


        public void CloseExcelApplication(Worksheet ContractExcelSheet, Application ContractExcelApplication, Workbook ContractExcelWorkbook);



    }
}
