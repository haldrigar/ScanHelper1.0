using System.IO;
using IniParser;
using IniParser.Model;

namespace ScanHelper.Functions
{
    public static class IniParser
    {
        public static void SaveIni(string section, string key, string value)
        {
            if (!File.Exists("configuration.ini")) File.Create("configuration.ini").Dispose();

            FileIniDataParser parser = new FileIniDataParser();
            IniData iniFile = parser.ReadFile("configuration.ini");
            iniFile[section][key] = value;

            parser.WriteFile("configuration.ini", iniFile);
        }

        public static string ReadIni(string section, string key)
        {
            if (!File.Exists("configuration.ini")) File.Create("configuration.ini").Dispose();

            FileIniDataParser parser = new FileIniDataParser();
            IniData iniFile = parser.ReadFile("configuration.ini");
            return iniFile[section][key];
        }
    }
}
