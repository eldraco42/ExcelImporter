using System.ComponentModel;

namespace ExcelImporter
{
    public class ExcelData
    {
        [Description("nummer")] public double Number { get; set; }
        [Description("voornaam")] public string FirstName { get; set; }
        [Description("tussenvoegsel")] public string MiddleName { get; set; }
        [Description("achternaam")] public string LastName { get; set; }
        [Description("geslacht")] public string Sex { get; set; }
    }
}
