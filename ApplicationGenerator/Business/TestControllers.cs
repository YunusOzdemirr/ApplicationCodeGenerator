using System.Text;

namespace ApplicationGenerator.Business;

public class TestControllers
{
    public async Task Run()
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

        string[] methods = new[] { "GetByIdAsync" };
        var entityNames = EntityLocator.GetEntityNames();
        for (int i = 0; i < entityNames.Length; i++)
        {
            var entityName = entityNames[i];
            var apiUrl = "api/" + entityName + "sController/";
        }
    }
}