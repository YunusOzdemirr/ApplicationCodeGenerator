namespace ApplicationGenerator.Business;

public partial class Template
{
    public static string[] Commands = { "Create", "Update", "Delete" };
    public static string[] Queries = { "Get", "Search" };
    public string CreateCommandHandlerTemplate = File.ReadAllText(Directory.GetCurrentDirectory() + @"\CreateCommandHandlerTemplate");

}
