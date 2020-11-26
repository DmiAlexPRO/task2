using NLog;
using System;
using System.IO;
using System.Web.Mvc;
using System.Xml.Serialization;
using task2.Code.interfaces;
using task2.Models;

namespace task2.Code.classes
{
    /*Implements SettingsReadWriter, saves an object with settings to an XML file,
     * or reads add-ins from this file*/
    public class XmlSettingsReadWriter:SettingsReadWriter
    {
        private Controller controller;
        private Logger logger;
        public XmlSettingsReadWriter(Controller _controller)
        {
            logger = LogManager.GetCurrentClassLogger();
            controller = _controller ?? throw new ArgumentNullException(nameof(_controller));
        }

        public void Write(Settings settings)
        {
            if (settings is null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            XmlSerializer formatter = new XmlSerializer(typeof(Settings));

            //we get the stream where we will write the serialized object
            using (FileStream fs = new FileStream(
                controller.Server.MapPath("~/App_Data//Settings.xml"), FileMode.Create))
            {
                formatter.Serialize(fs, settings);
            }
        }

        public Settings Read()
        {
            // passing the class type to the constructor
            XmlSerializer formatter = new XmlSerializer(typeof(Settings));
            Settings settings;
            // deserialization
            using (FileStream fs = new FileStream(
                controller.Server.MapPath("~/App_Data//Settings.xml"), FileMode.OpenOrCreate))
            {
                try
                {
                    settings = (Settings)formatter.Deserialize(fs);
                }
                catch(Exception ex)
                {   
                    logger.Debug(ex.Message);
                    throw ex;
                }
            }

            return settings;
        }

    }
}