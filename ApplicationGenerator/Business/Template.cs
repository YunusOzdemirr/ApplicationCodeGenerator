namespace ApplicationGenerator.Business;

public partial class Template
{
    public const string Path = @"C:\\Users\\yunus\\Documents\\GitHub\\beryque\\Business\\Beryque.Application";
    public static string[] Commands = { "Create", "Update", "Delete" };
    public static string[] Queries = { "Get", "Search" };

    public static string HandlerTemplate = File.ReadAllText(@"C:\\Users\\yunus\\Documents\\GitHub\\ApplicationCodeGenerator\\ApplicationGenerator\\Resources\\HandlerTemplate.txt");
    public static string RequestTemplate = File.ReadAllText(@"C:\Users\yunus\Documents\GitHub\ApplicationCodeGenerator\ApplicationGenerator\Resources\RequestTemplate.txt");
    public static string RequestTemplates = File.ReadAllText(@"C:\Users\yunus\Documents\GitHub\ApplicationCodeGenerator\ApplicationGenerator\Resources\RequestTemplates.txt");

    public struct Types
    {
        public const string Command = "Command";
        public const string Commands = "Commands";
        public const string Query = "Query";
        public const string Queries = "Queries";
        public const string Handler = "Handler";
    }
    public struct Cruds
    {
        public const string Create = "Create";
        public const string Update = "Update";
        public const string Archive = "Archive";
        public const string Get = "Get";
        public const string Search = "Search";
    }
    public struct Methods
    {
        public const string AddAsync = "AddAsync";
        public const string Update = "UpdateAsync";
        public const string ArchiveByIdAsync = "ArchiveByIdAsync";
        public const string GetByIdAsync = "GetByIdAsync";
        public const string SearchAsync = "SearchAsync";
    }
}
