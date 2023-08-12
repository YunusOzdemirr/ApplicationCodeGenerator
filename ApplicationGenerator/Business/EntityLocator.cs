using System.Text;

namespace ApplicationGenerator.Business;

public class EntityLocator
{
    public static string GetEntityPath()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var splittedDirectory = currentDirectory.Split(".");
        if (splittedDirectory.Length < 1)
            return null;
        var rootProjectName = splittedDirectory[0];
        var sb = new StringBuilder();
        sb.Append(rootProjectName);
        sb.Append(".Domain");
        sb.Append(@"\Entities\");
        return sb.ToString();
    }

    public static string[] GetEntityNames()
    {
        var domainEntitiesRoot = GetEntityPath();
        string[] fileNames = Directory.GetFiles(domainEntitiesRoot);
        return fileNames;
    }

    public static Dictionary<string, string> GetEntityProperties(string entityName)
    {
        var domainEntitiesRoot = GetEntityPath();
        var entityClassText = File.ReadAllLines(domainEntitiesRoot + entityName);
        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        for (int i = 0; i < entityClassText.Length; i++)
        {
            var line = entityClassText[i];
            if (!line.Contains("Public"))
                continue;
            var propertyLines = line.Split(" ");
            var variableType = propertyLines[1];
            var variableName = propertyLines[2];
            keyValuePairs.Add(variableType, variableName);
        }
        return keyValuePairs;
    }
}
