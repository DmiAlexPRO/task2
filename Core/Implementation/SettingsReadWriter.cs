using task2.Models;

namespace task2.Code.interfaces
{   

    /*Этот интерфейс нужен для удобной замены способа хранения настроек. Просто создай класс, реализующий его, его объект передай в
     * init при инициализации  SettingsHelper*/
    public interface SettingsReadWriter //Название временное
    {
        void Write(Settings settings);
        Settings Read();
    }
}
