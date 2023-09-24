using System.Text;

namespace ApplicationGenerator.Business;

public class Generator
{
    public void CreateControllersFiles()
    {
        var currentDirectory = Template.PathAPI;
        var splittedDirectory = currentDirectory.Split(".");
        if (splittedDirectory.Length < 1)
            return;
        var rootProjectName = splittedDirectory[0] + "." + splittedDirectory[1];
        var sb = new StringBuilder();
        sb.Append(rootProjectName);
        sb.Append(Template.Line + "Controllers" + Template.Line);
        if (!Path.Exists(sb.ToString()))
            Directory.CreateDirectory(sb.ToString());

        var entityNames = EntityLocator.GetEntityNames();
        var template = Template.ControllerTemplate;
        for (int i = 0; i < entityNames.Length; i++)
        {
            var entityName = entityNames[i];
            for (int j = 0; j < Template.Commands.Length; j++)
            {
                var fileName = entityName + "sController";
                var commandPath = sb + fileName + ".cs";
                var fileStream = File.Create(commandPath);
                Replace(fileStream, entityName, template);
                fileStream.Dispose();
            }
        }
    }

    public bool CreateRequestsCsFiles()
    {
        //var currentDirectory = Directory.GetCurrentDirectory();
        var currentDirectory = Template.PathAPI;
        var splittedDirectory = currentDirectory.Split(".");
        //  var splittedRootName = splittedDirectory[0].Split('\\');
        if (splittedDirectory.Length < 1)
            return false;
        var rootProjectName = splittedDirectory[0] + "." + splittedDirectory[1];
        var sb = new StringBuilder();
        sb.Append(rootProjectName);
        sb.Append(Template.Line + "ViewModels" + Template.Line + "Requests" + Template.Line);
        if (!Path.Exists(sb.ToString()))
            Directory.CreateDirectory(sb.ToString());
        if (!Path.Exists(sb.ToString() + Template.Line + @"Requests" + Template.Line))
            Directory.CreateDirectory(sb.ToString());

        var entityNames = EntityLocator.GetEntityNames();
        var template = Template.ViewModelRequestTemplate;
        for (int i = 0; i < entityNames.Length; i++)
        {
            var entityName = entityNames[i];
            var pathCommands = sb.ToString() + Template.Line + @"Commands" + Template.Line + entityName + @"Commands" +
                               Template.Line;
            var pathQueries = sb.ToString() + Template.Line + @"Queries" + Template.Line + entityName + @"Queries" +
                              Template.Line;

            if (!Path.Exists(pathCommands))
                Directory.CreateDirectory(pathCommands);
            if (!Path.Exists(pathQueries))
                Directory.CreateDirectory(pathQueries);

            for (int j = 0; j < Template.Commands.Length; j++)
            {
                var command = Template.Commands[j];
                var fileName = command + entityName + Template.Types.Request;
                var commandPath = pathCommands + fileName + ".cs";
                var fileStream = File.Create(commandPath);
                SetFields(fileStream, entityName, "Request", command, template);
                fileStream.Dispose();
            }

            for (int j = 0; j < Template.Queries.Length; j++)
            {
                var query = Template.Queries[j];
                var fileName = query + entityName + Template.Types.Request;
                var queryPath = pathQueries + fileName + ".cs";
                var fileStream = File.Create(queryPath);
                SetFields(fileStream, entityName, "Request", query, template);
                fileStream.Dispose();
            }
        }

        return true;
    }

    public bool CreateFeatureCsFiles()
    {
        //var currentDirectory = Directory.GetCurrentDirectory();
        var currentDirectory = Template.PathApplication;
        var splittedDirectory = currentDirectory.Split(".");
        //  var splittedRootName = splittedDirectory[0].Split('\\');
        if (splittedDirectory.Length < 1)
            return false;
        var rootProjectName = splittedDirectory[0] + "." + splittedDirectory[1];
        var sb = new StringBuilder();
        sb.Append(rootProjectName);
        sb.Append(Template.Line + @"Features" + Template.Line);
        if (!Path.Exists(sb.ToString()))
            Directory.CreateDirectory(sb.ToString());
        if (!Path.Exists(sb.ToString() + Template.Line + @"Commands" + Template.Line))
            Directory.CreateDirectory(sb.ToString());
        if (!Path.Exists(sb.ToString() + Template.Line + @"Queries" + Template.Line))
            Directory.CreateDirectory(sb.ToString());

        var entityNames = EntityLocator.GetEntityNames();
        var template = Template.RequestTemplate;
        for (int i = 0; i < entityNames.Length; i++)
        {
            var entityName = entityNames[i];
            var pathCommands = sb.ToString() + Template.Line + @"Commands" + Template.Line + entityName + @"Commands" +
                               Template.Line;
            var pathQueries = sb.ToString() + Template.Line + @"Queries" + Template.Line + entityName + @"Queries" +
                              Template.Line;
            if (!Path.Exists(pathCommands))
                Directory.CreateDirectory(pathCommands);
            else
                continue;
            if (!Path.Exists(pathQueries))
                Directory.CreateDirectory(pathQueries);
            else
                continue;

            for (int j = 0; j < Template.Commands.Length; j++)
            {
                var command = Template.Commands[j];
                var fileName = command + entityName + Template.Types.Command;
                var commandPath = pathCommands + fileName + ".cs";
                var fileStream = File.Create(commandPath);
                SetFields(fileStream, entityName, "Command", command, template);
                fileStream.Dispose();
                var fileStreamHandler = File.Create(pathCommands + fileName + "Handler.cs");
                SetHandler(fileStreamHandler, entityName,command,"Command");
                fileStreamHandler.Dispose();
            }

            for (int j = 0; j < Template.Queries.Length; j++)
            {
                var query = Template.Queries[j];
                var fileName = query + entityName + Template.Types.Query;
                var queryPath = pathQueries + fileName + ".cs";
                var fileStream = File.Create(queryPath);
                SetFields(fileStream, entityName, "Quer", query, template);
                fileStream.Dispose();
                var fileStreamHandler = File.Create(pathQueries + fileName + "Handler.cs");
                SetHandler(fileStreamHandler, entityName, query, "Query");
                fileStreamHandler.Dispose();
            }
        }

        return true;
    }

    private void Replace(FileStream fileStream, string entityName, string templatePath)
    {
        using (StreamReader reader = new StreamReader(fileStream))
        {
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                string[] lines = templatePath.Split("\n");
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];

                    if (!line.Contains("EntityName"))
                    {
                        writer.WriteLine(line);
                        continue;
                    }

                    line = line.Replace("{EntityName}", entityName);
                    writer.WriteLine(line);
                }
            }
        }
    }

    private void SetFields(FileStream fileStream, string entityName, string type, string operation, string templatePath)
    {
        using (StreamReader reader = new StreamReader(fileStream))
        {
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                string[] lines = templatePath.Split("\n");
                if (type == "Quer")
                    lines = Template.RequestTemplates.Split("\n");
                var properties = EntityLocator.GetEntityProperties(entityName);
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];

                    if (line.Contains("RootNameSpace") || line.Contains("EntityName"))
                    {
                        var currentDirectory = Template.PathApplication;
                        var splittedDirectory = currentDirectory.Split(".");
                        if (splittedDirectory.Length < 1)
                            break;
                        var splittedRootName = splittedDirectory[0].Split(Template.Line);
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
                    if (operation == "Search")
                    {
                        sb.Append('\n');
                        sb.Append("    public " + "bool " +"IsActive"+ " { get; set; }");
                    }
                    if (operation == "Get")
                    {
                        sb.Append('\n');
                        sb.Append("    public " + "int " + "IsActive" + " { get; set; }");
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

                    var currentDirectory = Template.PathApplication;
                    var splittedDirectory = currentDirectory.Split(".");
                    if (splittedDirectory.Length < 1)
                        break;
                    var splittedRootName = splittedDirectory[0].Split(Template.Line);
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