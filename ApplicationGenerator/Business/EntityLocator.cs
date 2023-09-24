using System.Linq;
using System.Text;

namespace ApplicationGenerator.Business;

public class EntityLocator
{
    public static string GetEntityPath()
    {
        //var currentDirectory = Directory.GetCurrentDirectory();
        var currentDirectory = Template.PathApplication;
        var splittedDirectory = currentDirectory.Split(".");
        if (splittedDirectory.Length < 1)
            return null;
        var rootProjectName = splittedDirectory[0];
        var sb = new StringBuilder();
        sb.Append(rootProjectName);
        sb.Append(".Domain");
        sb.Append(@"\\Entities\\");
        return sb.ToString();
    }

    public static string[] GetEntityNames()
    {
        var domainEntitiesRoot = GetEntityPath();
        string[] fileNames = Directory.GetFiles(domainEntitiesRoot);
        for (int i = 0; i < fileNames.Length; i++)
        {
            var fileName = fileNames[i];
            var splittedNames = fileName.Split(Template.Line);
            var entityNameWithCs = splittedNames[splittedNames.Length - 1];
            string entityName = entityNameWithCs.Substring(0, entityNameWithCs.Length - 3);
            fileNames[i] = entityName;
        }
        return fileNames;
    }

    public static Dictionary<string, string> GetEntityProperties(string entityName)
    {
        var domainEntitiesRoot = GetEntityPath();
        var entityClassText = File.ReadAllLines(domainEntitiesRoot + entityName + ".cs");
        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        for (int i = 0; i < entityClassText.Length; i++)
        {
            var line = entityClassText[i];
            if (!line.Contains("public") || line.Contains("class"))
                continue;
            var propertyLines = line.Split(" ");
            var key = "";
            for (int j = propertyLines.Length - 1; j >= 1; j--)
            {
                var property = propertyLines[j];
                if (property.Contains("{") || property.Contains("}") || property.Contains("get") || property.Contains("set") || property.Contains("void")) continue;
                if (string.IsNullOrEmpty(property)) continue;
                if (property == "public") continue;
                if (!string.IsNullOrEmpty(key))
                {
                    keyValuePairs[key] = property;
                    key = string.Empty;
                    continue;
                };
                key = property;
                keyValuePairs.TryAdd(property, string.Empty);
            }
        }
        return keyValuePairs;
    }
}







