using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using task2.Models;

namespace task2.Code
{
    public class XMLSettingsReader
    {

        private Controller controller;
        public XMLSettingsReader(Controller _controller)
        {
            controller = _controller;
        }

        public Settings Read()
        {
            // передаем в конструктор тип класса
            XmlSerializer formatter = new XmlSerializer(typeof(Settings));


            Settings settings;
            // десериализация
            using (FileStream fs = new FileStream(controller.Server.MapPath("~/App_Data//Settings.xml"), FileMode.OpenOrCreate))
            {
                 settings = (Settings)formatter.Deserialize(fs);
            }

            return settings;
        }
    }
}