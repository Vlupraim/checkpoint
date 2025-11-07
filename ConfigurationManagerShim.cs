using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace System.Configuration
{
    /// <summary>
    /// Shim liviano que emula ConfigurationManager.ConnectionStrings sin
    /// referenciar System.Configuration. Lee el App.config del ejecutable actual.
    /// </summary>
    public static class ConfigurationManager
    {
        private static ConnectionStringSettingsCollection _connectionStrings;

        public static ConnectionStringSettingsCollection ConnectionStrings
        {
            get
            {
                if (_connectionStrings == null)
                    _connectionStrings = LoadConnectionStrings();
                return _connectionStrings;
            }
        }

        private static ConnectionStringSettingsCollection LoadConnectionStrings()
        {
            var result = new ConnectionStringSettingsCollection();
            try
            {
                var configPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                if (string.IsNullOrWhiteSpace(configPath) || !File.Exists(configPath))
                    return result;

                var doc = new XmlDocument();
                doc.Load(configPath);

                var nodes = doc.SelectNodes("/configuration/connectionStrings/add");
                if (nodes != null)
                {
                    foreach (XmlNode node in nodes)
                    {
                        var name = node.Attributes?["name"]?.Value;
                        var cs = node.Attributes?["connectionString"]?.Value;
                        var provider = node.Attributes?["providerName"]?.Value;

                        if (!string.IsNullOrEmpty(name))
                            result.Add(new ConnectionStringSettings(name, cs ?? string.Empty, provider ?? string.Empty));
                    }
                }
            }
            catch
            {
                // Silencio: devuelve colección vacía si hay problema leyendo el .config
            }
            return result;
        }
    }

    public class ConnectionStringSettings
    {
        public string Name { get; }
        public string ConnectionString { get; }
        public string ProviderName { get; }

        public ConnectionStringSettings(string name, string connectionString, string providerName)
        {
            Name = name;
            ConnectionString = connectionString;
            ProviderName = providerName;
        }
    }

    public class ConnectionStringSettingsCollection
    {
        private readonly Dictionary<string, ConnectionStringSettings> _dict =
            new Dictionary<string, ConnectionStringSettings>(StringComparer.OrdinalIgnoreCase);

        public void Add(ConnectionStringSettings s)
        {
            if (s == null || string.IsNullOrEmpty(s.Name)) return;
            _dict[s.Name] = s;
        }

        public ConnectionStringSettings this[string name]
        {
            get
            {
                if (string.IsNullOrEmpty(name)) return null;
                return _dict.TryGetValue(name, out var v) ? v : null;
            }
        }
    }
}
