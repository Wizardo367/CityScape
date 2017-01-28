/*
 *       Class: Game
 *      Author: Harish Bhagat
 *        Year: 2017
 */

// Website used: http://www.oguzkonya.com/2017/01/03/xml-serialization-in-unity/
// Website used: http://answers.unity3d.com/questions/231647/xmlserializer-encoding-issue.html

using System.IO;
using System.Xml.Serialization;
using System.Text;

public class XMLSerializer
{
    public static void Serialize(object item, string path)
    {
        XmlSerializer serializer = new XmlSerializer(item.GetType());
        Encoding encoding = Encoding.GetEncoding("UTF-8");

        using (StreamWriter writer = new StreamWriter(path, false, encoding))
            serializer.Serialize(writer, item);
    }

    public static T Deserialize<T>(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        Encoding encoding = Encoding.GetEncoding("UTF-8");
        T deserialized;

        using (StreamReader reader = new StreamReader(path, encoding))
            deserialized = (T)serializer.Deserialize(reader);

        return deserialized;
    }
}