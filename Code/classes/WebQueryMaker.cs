using System.Data;
using System.Net;


namespace task2.Code
{   
    /*Класс, содержащий функционал для получения данных с сайта*/
    public class WebQueryMaker//название временное
    {
        /* Принимает url в параметрах и выполняет запрос по сформированному адресу, в случае, если по url удалось считать xml таблицу,
           она сохраняется в датасете и возврщается методом. Если при получении возникла ошибка, метод вернет null,
           т.е. ПРИ ВЫЗОВЕ МЕТОДА НЕОБХОДИМА ПРОВЕРКА РЕЗУЛЬТАТА НА NULL    */
        public DataSet GetRssDataset(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new System.ArgumentException("Url cant be null or empty ", nameof(url));
            }

            DataSet dataSet = new DataSet();
            WebResponse webResponse;
            try
            {
                webResponse = System.Net.WebRequest.Create(url).GetResponse();
                dataSet.ReadXml(webResponse.GetResponseStream());
            }
            catch
            {
                return null;// i know it's so bad :(
            }
            return dataSet;
        }

    }
}