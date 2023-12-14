using System.Collections.Generic;

namespace Emm.SqlStringsGenerator;

public class SqlClassQueries
{
    public string Namespace { get; set; } = "Emm.SqlStringsGenerator";
    public string ClassModifier { get; set; } = "internal";
    public string ClassName { get; set; } = "";
    public string Modifiers { get; set; } = "public const string";
    public Dictionary<string, string> Queries { get; set; } = new();
    
}