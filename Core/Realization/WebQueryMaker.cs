using NLog;
using System;
using System.Data;
using System.Net;


namespace task2.Code
{
    /*The class that contains functionality for getting data from a site*/
    public class WebQueryMaker
    {
        /* It accepts url in parameters and executes a request at the generated address,
        if the xml table was read by url, it is saved in the dataset and returned by the method.
        If an error occurs while getting, the method will return null,
        i.e. WHEN CALLING THE METHOD YOU NEED TO CHECK THE RESULT FOR NULL   */
        public DataSet GetRssDataset(string url)
        {
            Logger logger = LogManager.GetCurrentClassLogger();

            if (string.IsNullOrEmpty(url))
            {
                //logger.Debug($"Url is null of empty - {string.IsNullOrEmpty(url)}");
                throw new System.ArgumentException("Url cant be null or empty ", nameof(url));
            }

            DataSet dataSet = new DataSet();
            WebResponse webResponse;

            try
            {
                webResponse = System.Net.WebRequest.Create(url).GetResponse();
                dataSet.ReadXml(webResponse.GetResponseStream());//тут трабла TODO (System.Xml.XmlException, System.Data.DuplicateNameException)
            }
            catch(Exception ex)
            {
                logger.Debug($"Problem getting a stream response:  {ex}");
                return null;
            }
            return dataSet;
        }

    }
}