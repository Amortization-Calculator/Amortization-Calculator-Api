using System.ComponentModel.DataAnnotations;

namespace Amortization_Calculator_Api.Dtos
{
    public class CalcDto
    {
        private short _no_of_rental;

        public double rental = 0;

        public double efactiveintrest;
        public bool startFromFristMonth { get; set; }
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


        public bool ActualDay { get; set; }
        
        //public bool StartFromFristMonth { get; set; }



    }
}
