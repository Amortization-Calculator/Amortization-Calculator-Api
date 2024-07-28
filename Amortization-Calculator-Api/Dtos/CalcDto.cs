using System.ComponentModel.DataAnnotations;

namespace Amortization_Calculator_Api.Dtos
{
    public class CalcDto
    {
        private int no_of_line;
        private int _firstRowOfCopy;
        private const string _endingFolderName = @"ExcelTemplates\Ending";
        private const string _beginingFolderName = @"ExcelTemplates\Begining";
        private short _no_of_rental;
        public double rental = 0;
        public double efactiveintrest;
        public bool startFromFristMonth { get; set; }
        public enum ContractType { None, Monthly, Quarter, SemiAnnual, Annual, Monthly1, Quarter1 };
        public int ContractTypeInt { get; set; }
        public int RasedalAmount { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]

        public double AssetCost { get; set; }

        public string SelectedRadio { get; set; }
        
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]

        public double AmountFinance { get; set; }

        public double Rentaltype { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]

        public decimal IntrestRate { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]

        //this is Proparty
        public decimal EffectiveRate { get; set; }
        private int StartCopyRow { get; set; }
        private int EndCopyRow { get; set; }
        private int GrossCopyRow { get; set; }
        public int Customerno { get; set; }
        public int Contractno { get; set; }

        /// <summary>
        /// Value of NoOfRental could not be greater than 200
        /// </summary>       
        public short NoOfRental
        {
            //this is the encabsulation practice
            get
            {
                return _no_of_rental;
            }
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
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]

        public double ResedialValue { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]

        public double GressPriod { get; set; }

        public bool Begining { get; set; }

        public short RentalInterval { get; set; }

        public DateTime ContractDate { get; set; }

        public DateTime FirstDate { get; set; }

        public bool ActualDay { get; set; }
        //public bool StartFromFristMonth { get; set; }
        public int _cellstart;
        public int _last_cell = 0;


    }
}
