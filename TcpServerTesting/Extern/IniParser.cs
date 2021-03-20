/* 
 * Orginal StackOverflow Question: https://stackoverflow.com/questions/217902/reading-writing-an-ini-file
 * Copyright JB Stepan. 2020. All Rights Reserved.
 * Licensed under the MIT License
*/

using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace JB.JBIni
{
    class IniFile
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        string Path;
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>
        /// Initialize the INI Functionality 
        /// </summary>
        /// <param name="IniPath"></param>
        public IniFile(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName;
        }

        /// <summary>
        /// Reads the contents of the given Key
        /// </summary>
        /// <param name="Key">Key you want to read from</param>
        /// <param name="Section">Section you want to read from</param>
        /// <returns></returns>
        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }

        public bool ReadBool(string Key, string Section = null)
        {
            string key = Read(Key, Section);
            bool KeyToBool = bool.Parse(key);
            return KeyToBool;
        }

        public int ReadInt(string Key, string Section = null)
        {
            string key = Read(Key, Section);
            int KeyToInt = int.Parse(key);
            return KeyToInt;
        }
    }
}