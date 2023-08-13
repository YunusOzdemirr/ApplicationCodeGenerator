using System.Text;

namespace ApplicationGenerator.Business;

public class Generator
{
    public bool CreateFeatureCsFiles()
    {
        //var currentDirectory = Directory.GetCurrentDirectory();
        var currentDirectory = Template.Path;
        var splittedDirectory = currentDirectory.Split(".");
        //  var splittedRootName = splittedDirectory[0].Split('\\');
        if (splittedDirectory.Length < 1)
            return false;
        var rootProjectName = splittedDirectory[0] + "." + splittedDirectory[1];
        var sb = new StringBuilder();
        sb.Append(rootProjectName);
        sb.Append(@"/Features/");
        if (!Path.Exists(sb.ToString()))
            Directory.CreateDirectory(sb.ToString());
        if (!Path.Exists(sb.ToString() + @"/Commands/"))
            Directory.CreateDirectory(sb.ToString());
        if (!Path.Exists(sb.ToString() + @"/Queries/"))
            Directory.CreateDirectory(sb.ToString());

        var entityNames = EntityLocator.GetEntityNames();
        for (int i = 0; i < entityNames.Length; i++)
        {
            var entityName = entityNames[i];
            var pathCommands = sb.ToString() + @"/Commands/" + entityName + @"Commands/";
            var pathQueries = sb.ToString() + @"/Queries/" + entityName + @"Queries/";
            if (!Path.Exists(pathCommands))
                Directory.CreateDirectory(pathCommands);
            if (!Path.Exists(pathQueries))
                Directory.CreateDirectory(pathQueries);

            for (int j = 0; j < Template.Commands.Length; j++)
            {
                var command = Template.Commands[j];
                var fileName = command + entityName + Template.Types.Command;
                var commandPath = pathCommands + fileName + ".cs";
                var fileStream = File.Create(commandPath);
                SetFields(fileStream, entityName, "Command", command);
                fileStream.Dispose();
                //var fileStreamHandler = File.Create(pathCommands + fileName + "Handler.cs");
                //SetHandler(fileStreamHandler, entityName);
                //fileStreamHandler.Dispose();
            }

            for (int j = 0; j < Template.Queries.Length; j++)
            {
                var query = Template.Queries[j];
                var fileName = query + entityName + Template.Types.Query;
                var queryPath = pathQueries + fileName + ".cs";
                var fileStream = File.Create(queryPath);
                SetFields(fileStream, entityName, "Quer", query);
                fileStream.Dispose();
                var fileStreamHandler = File.Create(pathQueries + fileName + "Handler.cs");
                SetHandler(fileStreamHandler, entityName, query, "Query");
                fileStreamHandler.Dispose();
            }
        }

        return true;
    }

    private void SetFields(FileStream fileStream, string entityName, string type, string operation)
    {
        using (StreamReader reader = new StreamReader(fileStream))
        {
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                string[] lines = Template.RequestTemplate.Split("\n");
                if (type == "Quer")
                    lines = Template.RequestTemplates.Split("\n");
                var properties = EntityLocator.GetEntityProperties(entityName);
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];

                    if (line.Contains("RootNameSpace") || line.Contains("EntityName"))
                    {
                        var currentDirectory = Template.Path;
                        var splittedDirectory = currentDirectory.Split(".");
                        if (splittedDirectory.Length < 1)
                            break;
                        var splittedRootName = splittedDirectory[0].Split('/');
                        var rootName = splittedRootName[splittedRootName.Length - 1];
                        line = line.Replace("{RootNameSpace}", rootName);
                        line = line.Replace("{EntityName}", entityName);
                        line = line.Replace("{Crud}", operation);
                        line = line.Replace("{Type}", type);
                        writer.WriteLine(line);
                        continue;
                    }

                    if (!line.Contains("*"))
                    {
                        writer.WriteLine(line);
                        continue;
                    }

                    StringBuilder sb = new StringBuilder();
                    foreach (var property in properties)
                    {
                        sb.Append('\n');
                        sb.Append("    public " + property.Value + " " + property.Key + " { get; set; }");
                    }

                    line = sb.ToString();
                    writer.WriteLine(line);
                }
            }
        }
    }

    private void SetHandler(FileStream fileStream, string entityName, string crud, string type)
    {
        using (StreamReader reader = new StreamReader(fileStream))
        {
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                var lines = Template.HandlerTemplate.Split("\n");
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];

                    var currentDirectory = Template.Path;
                    var splittedDirectory = currentDirectory.Split(".");
                    if (splittedDirectory.Length < 1)
                        break;
                    var splittedRootName = splittedDirectory[0].Split('/');
                    var rootName = splittedRootName[splittedRootName.Length - 1];
                    line = line.Replace("{RootNameSpace}", rootName);
                    line = line.Replace("{EntityName}", entityName);
                    line = line.Replace("{Crud}", crud);
                    line = line.Replace("{Type}", type);
                    switch (crud)
                    {
                        case "Create":
                            line = line.Replace("{MethodName}", "AddAsync");
                            break;
                        case "Update":
                            line = line.Replace("{MethodName}", "UpdateAsync");
                            break;
                        case "Get":
                            line = line.Replace("{MethodName}", "GetByIdAsync");
                            break;
                        case "Delete":
                            line = line.Replace("{MethodName}", "ArchiveByIdAsync");
                            break;
                        case "Search":
                            line = line.Replace("{MethodName}", "GetAllAsync");
                            break;
                    }

                    writer.WriteLine(line);
                }
            }
        }
    }
}