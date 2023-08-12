using System.Security.Cryptography;
using System.Text;

namespace ApplicationGenerator.Business;

public class Generator
{
    public static bool CreateFeatureCsFiles()
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
                var fileName = command + entityName + Template.Operations.Command;
                var commandPath = pathCommands + fileName + ".cs";
                File.Create(commandPath);
                SetFields(commandPath, entityName);
                File.Create(pathCommands + fileName + "Handler.cs");
            }
            for (int j = 0; i < Template.Queries.Length; i++)
            {
                var query = Template.Queries[j];
                var fileName = query + entityName + Template.Operations.Query;
                var queryPath = pathQueries + fileName + ".cs";
                File.Create(queryPath);
                SetFields(queryPath, entityName);
                File.Create(pathQueries + fileName + "Handler.cs");
            }

        }
        return true;
    }

    public static void SetFields(string path, string entityName)
    {
        var commandContent = File.ReadAllLines(path)!;
        var properties = EntityLocator.GetEntityProperties(entityName);
        for (int i = 0; i < commandContent.Length; i++)
        {
            string line = commandContent[i]!;
            if (!line.Contains("*"))
                continue;
            StringBuilder sb = new StringBuilder();
            sb.Append(line);
            foreach (var property in properties)
            {
                sb.Append('\n');
                sb.Append("public " + property.Key + " " + property.Value + " { get; set; }");
            }
        }
    }

}