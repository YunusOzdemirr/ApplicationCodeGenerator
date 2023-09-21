namespace ApplicationGenerator.Business;

public partial class Template
{
    public const string PathApplication = @"C:\\Users\\yunus\\Documents\\GitHub\\beryque\\backend\\Business\\Beryque.Application";
    public const string PathAPI= @"C:\\Users\\yunus\\Documents\\GitHub\\beryque\\backend\\Services\\Beryque.API";
    public const string PathOfApplication = @"C:\\Users\\yunus\\Documents\\GitHub\\ApplicationCodeGenerator\\ApplicationGenerator\\Resources\\";
    //public const char Line = '/';
    public const string Line = @"\";
    //public const string Path = @"/Users/yunus/Documents/GitHub/beryque/Business/Beryque.Application";
    //public const string PathOfApplication = @"/Users/yunus/Documents/GitHub/ApplicationCodeGenerator/ApplicationGenerator/Resources/";
    public static string[] Commands = { "Create", "Update", "Delete" };
    public static string[] Queries = { "Get", "Search" };

    public static string HandlerTemplate = File.ReadAllText(PathOfApplication + "HandlerTemplate.txt");
    public static string ViewModelRequestTemplate = File.ReadAllText(PathOfApplication + "ViewModelRequestTemplate.txt");
    public static string RequestTemplate = File.ReadAllText(PathOfApplication + "RequestTemplate.txt");
    public static string RequestTemplates = File.ReadAllText(PathOfApplication + "RequestTemplates.txt");

    public struct Types
    {
        public const string Command = "Command";
        public const string Request = "Request";
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
