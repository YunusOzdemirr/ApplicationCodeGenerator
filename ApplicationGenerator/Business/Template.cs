namespace ApplicationGenerator.Business;

public partial class Template
{
    public const string Path = @"C:\\Users\\yunus\\Documents\\GitHub\\beryque\\Business\\Beryque.Application";
    public static string[] Commands = { "Create", "Update", "Delete" };
    public static string[] Queries = { "Get", "Search" };

    public static string CreateCommandHandlerTemplate = File.ReadAllText(@"C:\\Users\\yunus\\Documents\\GitHub\\ApplicationCodeGenerator\\ApplicationGenerator\\Resources\\CreateCommandHandlerTemplate.txt");
    public static string CreateCommandTemplate = File.ReadAllText(@"C:\Users\yunus\Documents\GitHub\ApplicationCodeGenerator\ApplicationGenerator\Resources\CreateCommandTemplate.txt");
    public static string CreateQueryTemplate = File.ReadAllText(@"C:\Users\yunus\Documents\GitHub\ApplicationCodeGenerator\ApplicationGenerator\Resources\CreateQueryTemplate.txt");
    public struct Operations
    {
        public const string Command = "Command";
        public const string Commands = "Commands";
        public const string Query = "Query";
        public const string Queries = "Queries";
        public const string Handler = "Handler";
    }
}
