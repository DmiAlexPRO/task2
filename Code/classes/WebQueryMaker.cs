using System.Data;
using System.Net;


namespace task2.Code
{   
    /*Класс, содержащий функционал для получения данных с сайта*/
    public class WebQueryMaker
    {
        /*Принимает url в параметрах и выполняет запрос по сформированному адресу, в случае, если по url удалось считать xml таблицу,
         она сохраняется в датасете и возврщается методом. Если при получении возникла ошибка, метод вернет null.*/
        public DataSet GetRssDataset(string url)
        { 
            DataSet ds = new DataSet();
            WebResponse webResponse;
            try
            {
                webResponse = System.Net.WebRequest.Create(url).GetResponse();
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