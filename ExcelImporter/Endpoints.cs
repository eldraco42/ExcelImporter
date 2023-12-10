using OfficeOpenXml;

namespace ExcelImporter
{
    public static class Endpoints
    {
        public static WebApplication MapEndpoints(this WebApplication app)
        {
            app.MapPost("/readexcelfile", ReadExcelFile);
            return app;
        }

        /// <summary>
        /// Reads an excel file and imports the first table it finds into a list of pre-defined data objects.
        /// </summary>
        /// <param name="excelFile">The excel file to import.</param>
        /// <returns>A list of the imported data objects in JSON format.</returns>
        internal static IResult ReadExcelFile(ILogger<Program> logger, IFormFile excelFile)
        {
            logger.LogInformation($"Reading excel file with name \"{excelFile.FileName}\".");

            // Read the excel file and take the first sheet
            using var stream = excelFile.OpenReadStream();
            using var excelPackage = new ExcelPackage(stream);
            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];

            try
            {
                // Read the first table found into a list of ExcelData
                List<ExcelData> dataObjects = worksheet.Tables[0].ToCollection<ExcelData>();

                logger.LogInformation($"Importing excel file, found {dataObjects.Count} rows to import.");
                return Results.Ok(dataObjects);
            }
            catch (Exception e)
            {
                return Results.Problem($"Cannot import excel file. Make sure there is at least one table of data.\nDetails: {e.GetBaseException()?.Message}");
            }
        }
    }
}
