// See https://aka.ms/new-console-template for more information
public class ModuleDescription
{
    public string Name { get; set; } = string.Empty;

    public IEnumerable<string> Pins { get; set; } = new List<string>();

    public IEnumerable<GateDescription> SubModules {get; set; } = new List<GateDescription>();

    public IEnumerable<ConnectionDescription> Connections {get; set; } = new List<ConnectionDescription>();
}
