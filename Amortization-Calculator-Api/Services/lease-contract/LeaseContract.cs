using Amortization_Calculator_Api.Dtos;
using Microsoft.Office.Interop.Excel;

namespace Amortization_Calculator_Api.Services.lease_contract
{
    public class LeaseContract
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

        public short NoOfRental
        {
            get { return _no_of_rental; }
            set
            {
                if (value > 200)
                {
                    _no_of_rental = 200;
                }
                else
                {
                    _no_of_rental = value;
                }
            }
        }

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

        public List<ExcelRow> ExcelSheet { get; set; }
        public LeaseContract(string sessionId)
        {
            SessionId = sessionId;
            SetDefaultContractType();
            if (_lockObject == null) _lockObject = new object();
        }
        public LeaseContract(double assetCost, double amountFinace, decimal interestRate
            , decimal effectiveRate, short noOfRental, double resedialValue, double gressPriod
            , bool begining, short rentalInterval, DateTime contractDate, DateTime firstDate,
            bool actualDay, bool startFromFristMonth, int rentaltype, int customerno, int contractno, string savePath, string filePath)
        {
            AssetCost = assetCost;
            AmountFinance = amountFinace;
            IntrestRate = interestRate;
            EffectiveRate = effectiveRate;
            NoOfRental = noOfRental;
            ResedialValue = resedialValue;
            GressPriod = gressPriod;
            Begining = begining;
            RentalInterval = rentalInterval;
            ContractDate = contractDate;
            FirstDate = firstDate;
            ActualDay = actualDay;
            StartFromFristMonth = startFromFristMonth;
            SetDefaultContractType();
            Rentaltype = rentaltype;
            Customerno = customerno;
            Contractno = contractno;
            FilePath = filePath;
            SavePath = savePath;
            // no_of_line = NoOfRental * 12 / (RentalInterval);
            //
            _firstRowOfCopy = 0;


        }


        private void SetDefaultContractType()
        {
            TypeOfContract = ContractType.None;
        }
        private ContractType GetContractType()
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

        private string GetTemplateFolder()
        {

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


        private string GetExcelFile()
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

            _excelFilename = Path.Combine(_excelFilename, fileToCopy);
            _targetExcelFileName = Path.Combine(SavePath, $"{SessionId}.xls");

            if (!File.Exists(_excelFilename))
            {
                throw new FileNotFoundException($"The file {_excelFilename} does not exist.");
            }

            File.Copy(_excelFilename, _targetExcelFileName, true);
            return _targetExcelFileName;

        }


        private void OpenExcelApplication(string xlFileName)
        {

            //Instanciate an excel application
            ContractExcelApplication = new Application();
            //Open excel workbook from excel filename
            //ContractExcelWorkbook = ContractExcelApplication.Workbooks.Open(xlFileName, null, null, null, null, null, true, null, null, null, false, false);
            ContractExcelWorkbook = ContractExcelApplication.Workbooks.Open(xlFileName);
            //Get the first worksheet in the excel workbook
            //Worksheet xlWorksheet = xlWorkbook.Worksheets[1];
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
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(ContractExcelApplication);
            //System.Runtime.InteropServices.Marshal.ReleaseComObject(ContractExcelApplication);
        }


        public void Calculate()
        {
            try
            {
                #region MyRegion
                lock (_lockObject)
                {
                    string xlFileName = GetExcelFile();


                    OpenExcelApplication(xlFileName);



                    if (Begining)
                    {
                        no_of_line = (NoOfRental * 12 / (RentalInterval)) - no_of_line;

                    }
                    else
                    {
                        no_of_line = (NoOfRental * 12 / (RentalInterval));
                    }






                    //ContractExcelApplication.Visible = true;
                    //string startEndCellRange = string.Format("a{0}:a{1}",StartCopyRow, EndCopyRow);
                    string startEndCellRange = "a" + StartCopyRow + ":o" + EndCopyRow;
                    ContractExcelSheet.Range[startEndCellRange].Select();
                    ContractExcelSheet.Range[startEndCellRange].Copy();
                    int x = 0;
                    for (int i = _firstRowOfCopy; i < no_of_line + _cellstart; ++i)
                    {
                        startEndCellRange = "a" + i + ":o" + (i + GrossCopyRow);
                        ContractExcelSheet.Range[startEndCellRange].Select();
                        ContractExcelSheet.Paste();
                        i = i + GrossCopyRow;

                        _last_cell = i;
                    }



                    set_data();
                    set_date();

                    if (Rentaltype == 1)
                    {

                        ContractExcelApplication.Visible = true;
                        return;
                    }




                    for (int i = _cellstart; i < no_of_line + _cellstart; ++i)
                    {
                        startEndCellRange = "c" + i;
                        var cell = ContractExcelSheet.Range[startEndCellRange].Value;
                        if (int.Parse(cell.ToString()) > 0)
                        {
                            _cellstart = i;
                            break;
                        }

                    }

                    //ContractExcelApplication.Visible = true;

                    string startcelRental = "c" + _cellstart;
                    string last_cel_bo = "F" + _last_cell;
                    if (ResedialValue > 0)
                    {
                        //bool x = ContractExcelSheet.Range[last_cel_bo].GoalSeek(ResedialValue, ContractExcelSheet.Range[startcelRental]);
                        ContractExcelSheet.Range[last_cel_bo].GoalSeek(ResedialValue, ContractExcelSheet.Range[startcelRental]);
                        ContractExcelSheet.Range["C2"].Value = "Residual Value";
                        ContractExcelSheet.Range["D2"].Value = ResedialValue;
                        string formula_c = (string)ContractExcelSheet.Range["c" + _last_cell].Formula;
                        formula_c = formula_c + "+D2";
                        ContractExcelSheet.Range["c" + _last_cell].Formula = formula_c;
                    }
                    else
                    {
                        //bool x = ContractExcelSheet.Range[last_cel_bo].GoalSeek(0, ContractExcelSheet.Range[startcelRental]);
                        ContractExcelSheet.Range[last_cel_bo].GoalSeek(0, ContractExcelSheet.Range[startcelRental]);
                    }

                    rental = (double)ContractExcelSheet.Range["C" + _cellstart].Value;


                    //ContractExcelApplication.Visible = true;

                    ////////
                    //Your Code Here    e.g Assign 'Asset Cost' value, 'Interest Rate'.... etc.
                    ////////
                    //int ii = 0;
                    //int zz = 5;
                    //int yy = zz / ii;
                    ReadExcel();

                    CloseExcelApplication();
                }
                #endregion

            }
            catch (Exception e)
            {
                List<string> errorDescription = new List<string>();

                errorDescription.Add(Environment.NewLine);
                errorDescription.Add(HandleStringValue(e.StackTrace));
                errorDescription.Add(Environment.NewLine);
                errorDescription.Add(HandleStringValue(e.Message));
                errorDescription.Add(Environment.NewLine);
                errorDescription.Add(HandleStringValue(e.HResult.ToString()));
                errorDescription.Add(Environment.NewLine);
                errorDescription.Add(HandleStringValue(e.HelpLink));

                string errorData = string.Join("", errorDescription.ToArray());
                string path = _excelFilename.Substring(0, _excelFilename.Length - 4) + ".txt";
                System.IO.File.WriteAllText(path, errorData);
            }

        }






        private string HandleStringValue(string value)
        {
            if (value == null) return "";
            return value;
        }
        public void GoalSeekByChangingIntrest(double newrent)
        {
            OpenExcelApplication(_excelFilename);

            string startcelRental = "c" + _cellstart;
            string last_cel_bo = "F" + _last_cell;

            ContractExcelSheet.Range[startcelRental].Value = newrent;
            ContractExcelSheet.Range[last_cel_bo].GoalSeek(0, ContractExcelSheet.Range["e1"]);
            //ContractExcelApplication.Visible = true;
            efactiveintrest = (double)ContractExcelSheet.Range["e1"].Value;
            ReadExcel();
            CloseExcelApplication();
        }


        private void set_date()
        {
            if (Begining)
            {
                no_of_line = no_of_line + 1;

            }
            else
            {
                no_of_line = no_of_line;
            }
            DateTime ContractDatecalc = ContractDate;
            for (int i = 4; i < no_of_line + _cellstart; ++i)
            {
                string startEndCellRange = "a" + i;
                ContractExcelSheet.Range[startEndCellRange].Value = ContractDatecalc.Date;
                ContractDatecalc = ContractDatecalc.Date.AddMonths(1);
                if (ActualDay == true && i >= 5)
                {
                    int x = i - 1;
                    string formula_a = "=A" + i + "-A" + x;
                    ContractExcelSheet.Range["n" + i].Formula = formula_a;
                }

            }
        }

        private void set_data()
        {
            ContractExcelSheet.Range["A1"].Value = Customerno + ' ' + Contractno;
            //ContractExcelSheet.Range["b1"].Value =
            ContractExcelSheet.Range["E1"].Value = IntrestRate;
            ContractExcelSheet.Range["B4"].Value = AssetCost;

            if (Begining)
            {
                ContractExcelSheet.Range["B2"].Value = AssetCost - AmountFinance;
            }

            else
            {

                ContractExcelSheet.Range["c4"].Value = AssetCost - AmountFinance;

            }
        }

        public void ReadExcel()
        {
            /////////
            if (ExcelSheet == null)
            {
                ExcelSheet = new List<ExcelRow>();
            }
            // ContractExcelApplication.Visible = true;
            ExcelSheet.Clear();
            for (int i = 5; i < no_of_line + _cellstart; ++i)
            {
                ExcelRow e = new ExcelRow();

                if (ContractExcelSheet.Range["f" + i].Value == null)
                {
                    break;
                }

                e.rentalDate = (DateTime)ContractExcelSheet.Range["a" + i].Value;
                // e.Seqrental  = int.Parse (ContractExcelSheet.Range["b" + i].ToString ());
                e.Rental = (double)ContractExcelSheet.Range["c" + i].Value;
                e.capital = (double)ContractExcelSheet.Range["d" + i].Value;
                e.Intrest = (double)ContractExcelSheet.Range["e" + i].Value;
                e.openBalnce = (double)ContractExcelSheet.Range["f" + i].Value + (double)ContractExcelSheet.Range["d" + i].Value;
                e.closeBalnce = (double)ContractExcelSheet.Range["f" + i].Value;
                ExcelSheet.Add(e);

            }
        }


        public struct ExcelRow
        {
            public ExcelRow(DateTime _rental_date, double _rental, double _intrest, int _Seqrental, double _capital, double _openBalnce, double _closeBalnce)
            {
                rentalDate = _rental_date;
                Seqrental = _Seqrental;
                Rental = _rental;
                Intrest = _intrest;
                capital = _capital;
                closeBalnce = _closeBalnce;
                openBalnce = _openBalnce;
            }

            public DateTime rentalDate { get; set; }
            public int Seqrental { get; set; }
            public double Rental { get; set; }
            public double Intrest { get; set; }
            public double openBalnce { get; set; }
            public double capital { get; set; }
            public double closeBalnce { get; set; }
            //public DateTime Date {get; set;}
        }



    }
}
