using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingCheck
{
    class SiteListReader
    {
        public static List<string> generateSiteList(string site_list)
        {
            List<string> siteList = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader("Resources/site_configs/" + site_list))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        siteList.Add(line);
                    }
                   
                }
                return siteList;
            }
            catch
            {
                Console.Write("Failure to read file, set to default.");
                siteList.Add("www.google.ca");
                siteList.Add("www.youtube.com");
                siteList.Add("www.facebook.com");
                return siteList;
            }
        }
        //TODO: Implement
        /*(public static List<string> addToList(string url)
        {
            return;
        }*/
    }
}

