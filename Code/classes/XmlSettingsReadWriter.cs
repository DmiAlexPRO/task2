using System;
using System.IO;
using System.Web.Mvc;
using System.Xml.Serialization;
using task2.Code.interfaces;
using task2.Models;

namespace task2.Code.classes
{
    /*Реализует SettingsReadWriter, сохраняет объект с настройками в файл XML или считывает найстройки из этого файла*/
    public class XmlSettingsReadWriter:SettingsReadWriter
    {
        private Controller controller;
        public XmlSettingsReadWriter(Controller _controller)
        {
            controller = _controller ?? throw new ArgumentNullException(nameof(_controller));
        }

        public void Write(Settings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            XmlSerializer formatter = new XmlSerializer(typeof(Settings));

            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream(controller.Server.MapPath("~/App_Data//Settings.xml"), FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, settings);
            }
        }

        public Settings Read()
        {
            // передаем в конструктор тип класса
            XmlSerializer formatter = new XmlSerializer(typeof(Settings));
            Settings settings;
            // десериализация
            using (FileStream fs = new FileStream(controller.Server.MapPath("~/App_Data//Settings.xml"), FileMode.OpenOrCreate))
            {
                try
                {
                    settings = (Settings)formatter.Deserialize(fs);
                }
                finally
                {
                    //  
                }
            }

            return settings;
        }

    }
}