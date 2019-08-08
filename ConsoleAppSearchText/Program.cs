using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace ConsoleAppSearchText
{
    class Program
    {
        static void Main(string[] args)
        {
            string strContains = ConfigurationSettings.AppSettings["keySearchTextValue"].ToString(); 
            string strFolderScan = ConfigurationSettings.AppSettings["keyFileRouteScan"].ToString();
            string strExtension = ConfigurationSettings.AppSettings["keyFileExtension"].ToString();

            DirectoryInfo dir = new DirectoryInfo(strFolderScan);
            IEnumerable<FileInfo> fileList = dir.GetFiles("*.*", SearchOption.AllDirectories);
            
            IEnumerable<FileInfo> fileQuery =
            from file in fileList
            where file.Extension == strExtension//".config"
            orderby file.Name
            select file;                

            Console.WriteLine("INI::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::");
            foreach (FileInfo fi in fileQuery)
            {
                int counter = 0;
                string[] lines = File.ReadAllLines(fi.FullName);
                Console.WriteLine("LOCATION " + fi.FullName);

                foreach (string line in lines)
                {
                    counter++;
                    if (line.Contains(strContains))//server999
                        Console.WriteLine("\t" + "LINE " + counter + " :: " + line);
                }
            }            
            Console.WriteLine("FIN::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::");
            Console.ReadKey();
        }
    }
}