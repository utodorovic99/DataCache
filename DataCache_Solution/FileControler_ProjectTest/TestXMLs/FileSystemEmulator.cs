using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FileControler_ProjectTest.TestXMLs
{
    public class FileSystemEmulator
    {
        [ExcludeFromCodeCoverage]
        public static string GetResourceTextFile(string testFileName)
        {
            string testFileContent;
            Assembly assembly = Assembly.GetExecutingAssembly();
            string name = "FileControler_ProjectTest.TestXMLs." + testFileName;

            using (Stream stream = assembly.GetManifestResourceStream(name))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    testFileContent = reader.ReadToEnd();
                }
            }


            XmlDocument tmpDoc = new XmlDocument();
            tmpDoc.LoadXml(testFileContent);
            tmpDoc.Save(testFileName);
            return Directory.GetCurrentDirectory().ToString() + "\\" + testFileName;
        }
    }
}
