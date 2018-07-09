namespace WpfApplication.Helper
{
    #region Using

    using System;
    using System.IO;
    using System.Xml;

    #endregion

    public static class XmlSerializer
    {
        //public static void SaveData<T>(T model, string path)
        //{
        //    try
        //    {

        //        System.Xml.Serialization.XmlSerializer xsSubmit = new System.Xml.Serialization.XmlSerializer(typeof(T));
  
        //        using (var sww = new StringWriter())
        //        {
        //            using (XmlWriter writer = XmlWriter.Create(sww))
        //            {
        //                xsSubmit.Serialize(writer, model);
        //                var str = sww.ToString();
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }
        //}

        public static T LoadData<T>(string path)
        {
            if(!File.Exists(path))
                throw new ArgumentException("Kriva putanja");
            

            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (XmlReader reader = XmlReader.Create(path))
            {
                return (T) ser.Deserialize(reader);
            }
        }
    }
}