using Newtonsoft.Json;
using ProcessJasonData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProcessJasonData.Common
{
    public class DataController
    {
        private string json_data = string.Empty;

        /// <summary>
        /// Download and process jasondata from given url
        /// </summary>
        /// <param name="path">jason URL</param>
        /// <returns>array of jason objects if valid path is passed else null</returns>
        public JasonData[] DownloadAndProcessdata(string path)
        {
            JasonData[] data = null;
            if (!string.IsNullOrEmpty(path))
            {
                using (var webClient = new WebClient())
                {
                    try
                    {
                        json_data = webClient.DownloadString(path);
                        //json_data = webClient.DownloadString(@"D:\JasonData.txt");
                        data = JsonConvert.DeserializeObject<JasonData[]>(json_data);
                    }
                    catch ( WebException ex)
                    {
                        // code here for exception handling
                    }
                    catch (NotSupportedException ex)
                    {
                        // code here for exception handling
                    }
                }
                
            }
            return data;
        }
    }
}