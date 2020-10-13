using System.Data;
using System.Net;


namespace task2.Code
{
    public class WebQueryMaker
    {
        //может вернуть null, если возникли проблемы с жопой
        public DataSet GetRssDataset(string url)
        {

            WebResponse webResponse = System.Net.WebRequest.Create(url).GetResponse();

            DataSet ds = new DataSet();
            try
            {
                ds.ReadXml(webResponse.GetResponseStream());
            }
            catch
            {
                return null;
            }

            return ds;
        }

    }
}