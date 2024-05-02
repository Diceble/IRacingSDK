﻿using System.Text;

namespace IRacingSDK;

/// <summary>
/// Handles All the YAML Parsing
/// </summary>
internal class YAMLParser
{
    internal static string FixYaml(string yaml)
    {
        string _yaml;
        using (StringReader reader = new(yaml))
        {
            StringBuilder builder = new();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Count(c => c == ':') > 1)
                {
                    var chars = line.ToCharArray();
                    bool foundFirst = false;
                    for (int i = 0; i < chars.Length; i++)
                    {
                        var c = chars[i];
                        if (c == ':')
                        {
                            if (!foundFirst)
                            {
                                foundFirst = true;
                                continue;
                            }
                            chars[i] = '-';
                        }
                    }
                    line = new string(chars);
                }
                builder.AppendLine(line);
            }
            _yaml = builder.ToString();
        }

        return _yaml;
    }
}
