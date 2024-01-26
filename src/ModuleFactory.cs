using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using System.Reflection;

namespace Gates
{
    public class ModuleFactory
    {
        private Dictionary<string, ModuleDescription> _moduleTypes;
        private IDeserializer _deserializer;

        public ModuleFactory()
        {
            _moduleTypes = new Dictionary<string, ModuleDescription>();
            _deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
        }

        private Module Load(string moduleType, string moduleName)
        {
            if (!_moduleTypes.TryGetValue(moduleType, out var moduleDescription))
            {
                TextReader text_reader = File.OpenText($@"Modules\{moduleType}.yaml");
                moduleDescription = _deserializer.Deserialize<ModuleDescription>(text_reader);
                _moduleTypes.Add(moduleType, moduleDescription);
            }

            var module = new Module(moduleType, moduleName);

            foreach (var pin in moduleDescription.Pins)
            {
                module.Pins.Add(pin, new Pin());
            }

            foreach (var subModule in moduleDescription.SubModules)
            {
                var m = Create(subModule.Type, subModule.Name);


                var type = m.GetType();


				foreach (var property in subModule.Properties)
                {
                    var p = type.GetProperty(property.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    var pt = p.PropertyType;
                    var value = Convert.ChangeType(property.Value, pt);
					p?.SetValue(m, value);

                }



				module.Modules.Add(m.Name, m);
            }

            foreach (var connection in moduleDescription.Connections)
            {
                var fromPin = GetPin(connection.From, module);
                var toPin = GetPin(connection.To, module);

                fromPin.ConnectTo(toPin);
            }

            return module;
        }

        private Pin GetPin(string pinDescription, Module module)
        {
            Pin pin;

            if (pinDescription.Contains("."))
            {
                var parts = pinDescription.Split(".");
                var moduleName = parts[0];
                var pinName = parts[1];
                pin = module.Modules[moduleName].Pins[pinName];
            }
            else
            {
                pin = module.Pins[pinDescription];
            }
            return pin;
        }

        public Module Create(string moduleType, string name)
        {
            var module = moduleType.ToLower() switch
            {
                "not" => new Not(name),
                "and" => new And(name),
                "or" => new Or(name),
                _ => Load(moduleType, name),
            };

            module.Initialise();

            return module;
        }


    }
}
