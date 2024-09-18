using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Core.config;

public class Config
{
    public int Min { get; set; }
    public int Max { get; set; }
    public int Tries { get; set; }

    public static Config ParseConfig(string path)
    {
        Config config = new Config();

        using StreamReader sr = new StreamReader(path);
        string text = sr.ReadToEnd();

        var parser = new Parser(new StringReader(text));
        var deserializer =
            new DeserializerBuilder()
                .WithNamingConvention(new UnderscoredNamingConvention())
                .Build();

        parser.Consume<StreamStart>();
        parser.Consume<DocumentStart>();
        parser.Consume<MappingStart>();

        while (parser.TryConsume<Scalar>(out var key))
        {
            if (key.Value == "min")
            {
                config.Min = int.Parse(parser.Consume<Scalar>().Value);
            }
            else if (key.Value == "max")
            {
                config.Max = int.Parse(parser.Consume<Scalar>().Value);
            }
            else if (key.Value == "tries")
            {
                config.Tries = int.Parse(parser.Consume<Scalar>().Value);
            }
            else
            {
                throw new Exception($"Unknown key: {key.Value}");
            }
        }

        return config;
    }
}
