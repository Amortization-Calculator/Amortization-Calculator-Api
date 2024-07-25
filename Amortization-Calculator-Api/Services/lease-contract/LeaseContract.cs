using Microsoft.Office.Interop.Excel;

namespace Amortization_Calculator_Api.Services.lease_contract
{
    public class LeaseContract: ILeaseConteactServicecs
    {
        public string SessionId { get; set; }

        private static object _lockObject;

        private int no_of_line;

        private int _firstRowOfCopy;

        private const string _endingFolderName = @"ExcelTemplates\Ending";

        private const string _beginingFolderName = @"ExcelTemplates\Begining";

        private short _no_of_rental;

        public double rental = 0;

        public double efactiveintrest;

        public double AssetCost { get; set; }
        public double AmountFinance { get; set; }

        public double Rentaltype { get; set; }

        public decimal IntrestRate { get; set; }

        public decimal EffectiveRate { get; set; }

        private int StartCopyRow { get; set; }

        private int EndCopyRow { get; set; }

        private int GrossCopyRow { get; set; }

        public int Customerno { get; set; }

        public int Contractno { get; set; }

        public Worksheet ContractExcelSheet { get; private set; }

        private Application ContractExcelApplication { get; set; }

        private Workbook ContractExcelWorkbook { get; set; }

        public double ResedialValue { get; set; }

        public double GressPriod { get; set; }

        public bool Begining { get; set; }

        public short RentalInterval { get; set; }

        public DateTime ContractDate { get; set; }

        public DateTime FirstDate { get; set; }

        public bool ActualDay { get; set; }

        public string FilePath { get; set; }

        public string SavePath { get; set; }

        public bool StartFromFristMonth { get; set; }
        public ContractType TypeOfContract { get; private set; }

        private string _excelFilename;

        private string _targetExcelFileName;

        public int _cellstart;

        public int _last_cell = 0;

    }
}
