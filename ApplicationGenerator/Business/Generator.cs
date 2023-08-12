using System.Text;

namespace ApplicationGenerator.Business;

public class Generator
{
    public string CreateCommandHandlerTemplatePath = "";
    public static bool CreateCsFiles()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var splittedDirectory = currentDirectory.Split(".");
        if (splittedDirectory.Length < 1)
            return false;
        var rootProjectName = splittedDirectory[0] + splittedDirectory[1];
        var sb = new StringBuilder();
        sb.Append(rootProjectName);
        sb.Append(@"\Features\");
        if (!Path.Exists(sb.ToString()))
            Directory.CreateDirectory(sb.ToString());
        if (!Path.Exists(sb.ToString() + @"\Commands\"))
            Directory.CreateDirectory(sb.ToString());
        if (!Path.Exists(sb.ToString() + @"\Queries\"))
            Directory.CreateDirectory(sb.ToString());

        var entityNames = EntityLocator.GetEntityNames();
       
        for (int i = 0; i < entityNames.Length; i++)
        {
            var entityName = entityNames[i];
            var pathCommands = sb.ToString() + @"\Commands\" + entityName + @"Commands\";
            var pathQueries = sb.ToString() + @"\Queries\" + entityName + @"Queries\";
            if (!Path.Exists(pathCommands))
                Directory.CreateDirectory(pathCommands);
            if (!Path.Exists(pathQueries))
                Directory.CreateDirectory(pathQueries);

            for (int j = 0; i < Template.Commands.Length; i++)
            {
                var command = Template.Commands[j];
                var fileName = command + entityName + "Command";
                File.Create(pathCommands + fileName + ".cs");
                File.Create(pathCommands + fileName + "Handler.cs");
            }
            for (int j = 0; i < Template.Queries.Length; i++)
            {
                var query = Template.Queries[j];
                var fileName = query + entityName + "Query";
                File.Create(pathQueries + fileName + ".cs");
                File.Create(pathQueries + fileName + "Handler.cs");
            }

        }
        return true;
    }
}