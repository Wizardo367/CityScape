/*
 *       Class: XMLSerializer
 *      Author: Harish Bhagat
 *        Year: 2017
 */

// Website used: http://www.oguzkonya.com/2016/01/03/xml-serialization-in-unity/
// Website used: http://answers.unity3d.com/questions/231647/xmlserializer-encoding-issue.html

using System.IO;
using System.Xml.Serialization;
using System.Text;

/// <summary>
/// Serializes and Deserializes XML files.
/// </summary>
public class XMLSerializer
{
	/// <summary>
	/// Serializes the specified item to the file at the specified path.
	/// </summary>
	/// <param name="item">The item.</param>
	/// <param name="path">The path.</param>
	public static void Serialize(object item, string path)
    {
        XmlSerializer serializer = new XmlSerializer(item.GetType());
        Encoding encoding = Encoding.GetEncoding("UTF-8");

        using (StreamWriter writer = new StreamWriter(path, false, encoding))
            serializer.Serialize(writer, item);
    }

	/// <summary>
	/// Deserializes the specified item from the file at the specified path.
	/// </summary>
	/// <typeparam name="T">Data type.</typeparam>
	/// <param name="path">The path.</param>
	/// <returns>Deserialized object.</returns>
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