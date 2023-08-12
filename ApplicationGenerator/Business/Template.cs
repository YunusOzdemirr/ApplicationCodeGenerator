namespace ApplicationGenerator.Business;

public partial class Template
{
    public static string[] Commands = { "Create", "Update", "Delete" };
    public static string[] Queries = { "Get", "Search" };
    public string CreateCommandHandlerTemplate = File.ReadAllText(Directory.GetCurrentDirectory() + @"\CreateCommandHandlerTemplate");
    public string CreateCommandTemplate = File.ReadAllText(Directory.GetCurrentDirectory() + @"\CreateCommandTemplate");
    public struct Operations
    {
        public const string Command = "Command";
        public const string Commands = "Commands";
        public const string Query = "Query";
        public const string Queries = "Queries";
        public const string Handler = "Handler";
    }
}
