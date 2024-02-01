using IniParser;
using IniParser.Model;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading;

namespace INIGestor 
{
    public class IniManager
    {
        public string Path { get; private set; }
        private readonly FileIniDataParser _fileINIParser;
        private readonly IniData _iniData;

        public IniManager(string path)
        {
            this.Path = path;
            _fileINIParser = new FileIniDataParser();
            //case insensitive
            _fileINIParser.Parser.Configuration.CaseInsensitive = true;
            //comment char
            _fileINIParser.Parser.Configuration.CommentString = ";";
            _iniData = new IniData();

            //Si no existe el fichero, lo crea
            if (File.Exists(path))
            {
                _iniData = _fileINIParser.ReadFile(path);
            }
            else
            {
                //crete the file
                File.Create(path);
            }
        }
        //get the number of Keys in a section
        public int GetNumKeys(string section)
        {
            if (!_iniData.Sections.ContainsSection(section))
            {
                return 0;
            }
            return _iniData[section].Count;
        }
        //find keys in a section
        public List<string> GetKeysInSection(string section)
        {
            List<string> retList = new List<string>();
            if (!_iniData.Sections.ContainsSection(section))
            {
                return retList;
            }
            foreach (KeyData key in _iniData[section])
            {
                retList.Add(key.KeyName);
            }
            return retList;
        }
        // tries to get the string, if it fails it will create the field with default value and return it
        public string GetString(string section, string key, string defaultString, ref List<string> refErrorList)
        {
            bool success = false;
            if (!_iniData.Sections.ContainsSection(section) || !_iniData[section].ContainsKey(key))
            {
                SetValue(section, key, defaultString);
                refErrorList.Add("The key " + key + " in section " + section + " was not found, it was created with the default value: " + defaultString);
                return defaultString;
            }
            success = true;
            string retString = _iniData[section][key];
            return retString;
        }
        //If the section or the key doesn't exist, it will return an empty string
        public string GetString(string section, string key, ref List<string> refErrorList)
        {
            if (!_iniData.Sections.ContainsSection(section) || !_iniData[section].ContainsKey(key))
            {
                refErrorList.Add("The key " + key + " in section " + section + " was not found");
                return "";
            }
            return _iniData[section][key];
        }

        static public bool GetCabeceraInputProperties(string section, string key, string entradaProperties, out int globalINPos, out int inputIsNegated, out int msFilterRisingEdge, out int msFilterFallingEdge, out string inputName, ref List<string> refErrorList)
        {
            try
            {
                string[] _entradaPropertiesArray = entradaProperties.Split(',');
                globalINPos = Int32.Parse(_entradaPropertiesArray[0]);
                inputIsNegated = Int32.Parse(_entradaPropertiesArray[1]);
                msFilterRisingEdge = Int32.Parse(_entradaPropertiesArray[2]);
                msFilterFallingEdge = Int32.Parse(_entradaPropertiesArray[3]);
                inputName = _entradaPropertiesArray[4];
            }
            catch (Exception ex)
            {
                globalINPos = 0;
                inputIsNegated = 0;
                msFilterRisingEdge = 0;
                msFilterFallingEdge = 0;
                inputName = "Error";
                refErrorList.Add("The key " + key + " in section " + section + " is not correct. Error message : " + ex.Message);
            }
            return true;
        }

        static public bool GetCabeceraOutputProperties(string section, string key, string entradaProperties, out int globalINPos, out int outputIsNegated, out string inputName, ref List<string> refErrorList)
        {
            try
            {
                string[] _entradaPropertiesArray = entradaProperties.Split(',');
                globalINPos = Int32.Parse(_entradaPropertiesArray[0]);
                outputIsNegated = Int32.Parse(_entradaPropertiesArray[1]);
                inputName = _entradaPropertiesArray[2];
            }
            catch (Exception ex)
            {
                globalINPos = 0;
                outputIsNegated = 0;
                inputName = "Error";
                refErrorList.Add("The key " + key + " in section " + section + " is not correct. Error message : " + ex.Message);
            }
            return true;
        }

        // tries to get the int, if it fails it will create the field with default value and return it
        public int GetInt(string section, string key, int defaultInt, ref List<string> refErrorList)
        {
            int retInt = defaultInt;
            if (!_iniData.Sections.ContainsSection(section) || !_iniData[section].ContainsKey(key))
            {
                SetValue(section, key, defaultInt.ToString());
                refErrorList.Add("The key " + key + " in section " + section + " was not found, it was created with the default value: " + defaultInt);
                return retInt;
            }
            try
            {
                retInt = int.Parse(_iniData[section][key]);
            }
            catch (Exception ex)
            {
                retInt = defaultInt;
                refErrorList.Add("The key " + key + " in section " + section + " threw an error: " + ex.Message);
            }
            return retInt;
        }

        public int GetInt(string section, string key, ref List<string> refErrorList)
        {
            int retInt = -99;
            if (!_iniData.Sections.ContainsSection(section) || !_iniData[section].ContainsKey(key))
            {
                refErrorList.Add("The key " + key + " in section " + section + " was not found");
                return retInt;
            }
            try
            {
                retInt = int.Parse(_iniData[section][key]);
            }
            catch (Exception ex)
            {
                refErrorList.Add("The key " + key + " in section " + section + " threw an error: " + ex.Message);
                retInt = -99;
            }
            return retInt;
        }

        public List<int> GetCommaSeparatedInts(string section, string key, ref List<string> refErrorList)
        {
            List<int> retList = new List<int>();
            if (!_iniData.Sections.ContainsSection(section) || !_iniData[section].ContainsKey(key))
            {
                refErrorList.Add("The key " + key + " in section " + section + " was not found");
                return retList;
            }
            try
            {
                string[] _entradaPropertiesArray = _iniData[section][key].Split(',');
                foreach (string s in _entradaPropertiesArray)
                {
                    if (s == "")
                    {
                        continue;
                    }
                    int i = int.Parse(s);
                    if (i == -1)
                    {
                        continue;
                    }
                    retList.Add(i);
                }
            }
            catch (Exception ex)
            {
                refErrorList.Add("The key " + key + " in section " + section + " threw an error: " + ex.Message);
            }
            return retList;
        }

        //If the section or the key doesn't exist, it will be created
        public void SetValue(string section, string key, string value)
        {
            _iniData[section][key] = value;
            //parse the data to the file
            _fileINIParser.WriteFile(Path, _iniData);
        }

        //Add a comment to a section, if the section doesn't exist do nothing
        public void AddComment(string section, string comment)
        {
            if (!_iniData.Sections.ContainsSection(section))
            {
                return;
            }
            _iniData.Sections.GetSectionData(section).Comments.Add(comment);
            _fileINIParser.WriteFile(Path, _iniData);
        }
        //Add a comment to a key, if the section doesn't exist do nothing
        public void AddComment(string section, string key, string comment)
        {
            if (!_iniData.Sections.ContainsSection(section))
            {
                return;
            }
            if (!_iniData[section].ContainsKey(key))
            {
                return;
            }
            _iniData.Sections.GetSectionData(section).Keys.GetKeyData(key).Comments.Add(comment);
            _fileINIParser.WriteFile(Path, _iniData);
        }
    }
}
