namespace Amortization_Calculator_Api.Services.background
{
    public class DeleteServices
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public DeleteServices(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }



        public void DeleteFilesOlderThan24Hours()
    {
        // Get all files in the specified directory

        string dirName = Path.Combine(_hostingEnvironment.ContentRootPath, "Excel");

        string[] files = Directory.GetFiles(dirName);

        foreach (string file in files)
        {
            FileInfo fi = new FileInfo(file);
            // Check if the file's creation time or last access time is older than 24 hours
            if (fi.CreationTime < DateTime.Now.AddHours(-24) || fi.LastAccessTime < DateTime.Now.AddHours(-24))
            {
                try
                {
                    // Delete the file
                    fi.Delete();
                    Console.WriteLine($"Deleted file: {file}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting file {file}: {ex.Message}");
                }
            }
        }
    }
    }
}
