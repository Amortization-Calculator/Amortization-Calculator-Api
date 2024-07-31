using System.ComponentModel.DataAnnotations;

namespace Amortization_Calculator_Api.Dtos
{
    public class CalcDto
    {
        private short _no_of_rental;

        public double rental = 0;

        public double efactiveintrest;
        public bool startFromFristMonth { get; set; }

        
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]
        public required double AssetCost { get; set; }


        
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]
        public required double AmountFinance { get; set; }

        
       
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]
        public required decimal IntrestRate { get; set; }
        

       
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]
        public required decimal EffectiveRate { get; set; }

     
    
        
        public short NoOfRental
        {
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
       
        

        
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]

        public required double ResedialValue { get; set; }





        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid doubleNumber")]
        public required double GressPriod { get; set; }

        
        
        public required bool Begining { get; set; }

        
        
        public required  short RentalInterval { get; set; }

        
    }
}
