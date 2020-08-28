using System;
using System.IO;
using System.Web.Mvc;
using System.Xml.Serialization;
using task2.Models;

namespace task2.Code
{
    public class XMLSettingsWriter
    {
        private Controller controller;
        public XMLSettingsWriter(Controller _controller)
        {
            controller = _controller;
        }
        public void Write(Settings settings)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Settings));

            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream(controller.Server.MapPath("~/App_Data//Settings.xml"), FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, settings);
            }
        }
    }
}