namespace Amortization_Calculator_Api.Services.lease_contract
{
    public interface ILeaseConteactServicecs
    {
        public ContractType GetContractType(short RentalInterval);

        public void SetDefaultContractType();

        public string GetTemplateFolder(bool Begining);

        public string GetExcelFile(string _excelFilename , string FilePath , int _firstRowOfCopy , int StartCopyRow , int EndCopyRow , int GrossCopyRow , int no_of_line);



    }
}
