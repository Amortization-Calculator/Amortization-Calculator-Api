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

      
        
        public string GetTemplateFolder(bool Begining, int _cellstart)
        {
            const string _endingFolderName = @"ExcelTemplates\Ending";

            const string _beginingFolderName = @"ExcelTemplates\Begining";

            if (Begining)
            {
                _cellstart = 4;
                return _beginingFolderName;

            }
            else
            {
                _cellstart = 5;
                return _endingFolderName;

            }

        }

        public string GetExcelFile()
        {
            _excelFilename = Path.Combine(FilePath, GetTemplateFolder());
            TypeOfContract = GetContractType();
            string fileToCopy;

            switch (TypeOfContract)
            {
                case ContractType.None:
                    fileToCopy = "G1.xls";
                    StartCopyRow = 0;
                    EndCopyRow = 0;
                    GrossCopyRow = 0;
                    break;
                case ContractType.Monthly:
                    fileToCopy = StartFromFristMonth ? "G1f1.xls" : "G1.xls";
                    _firstRowOfCopy = 17;
                    StartCopyRow = 16;
                    EndCopyRow = 16;
                    GrossCopyRow = 0;
                    no_of_line = 0;
                    break;
                case ContractType.Monthly1:
                    fileToCopy = StartFromFristMonth ? "G6f1.xls" : "G6.xls";
                    _firstRowOfCopy = 17;
                    StartCopyRow = 15;
                    EndCopyRow = 16;
                    GrossCopyRow = 1;
                    no_of_line = 2;
                    break;
                case ContractType.Quarter:
                    fileToCopy = StartFromFristMonth ? "G3f1.xls" : "G3.xls";
                    _firstRowOfCopy = 17;
                    StartCopyRow = 14;
                    EndCopyRow = 16;
                    GrossCopyRow = 2;
                    no_of_line = 3;
                    break;
                case ContractType.Quarter1:
                    fileToCopy = StartFromFristMonth ? "G4f1.xls" : "G4.xls";
                    _firstRowOfCopy = 17;
                    StartCopyRow = 13;
                    EndCopyRow = 16;
                    GrossCopyRow = 3;
                    no_of_line = 4;
                    break;
                case ContractType.SemiAnnual:
                    fileToCopy = StartFromFristMonth ? "G2f1.xls" : "G2.xls";
                    _firstRowOfCopy = 17;
                    StartCopyRow = 11;
                    EndCopyRow = 16;
                    GrossCopyRow = 5;
                    no_of_line = 6;
                    break;
                case ContractType.Annual:
                    fileToCopy = StartFromFristMonth ? "G12f1.xls" : "G12.xls";
                    _firstRowOfCopy = 17;
                    StartCopyRow = 17;
                    EndCopyRow = 28;
                    GrossCopyRow = 11;
                    no_of_line = 12;
                    break;
                default:
                    throw new Exception("Invalid contract type.");
            }
    }
}
