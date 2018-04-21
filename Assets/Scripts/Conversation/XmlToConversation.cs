using System.IO;
using System.Xml.Serialization;

namespace PotionTycoon.VN.Conversation
{
    /// <summary>
    /// Class to convert a XML file to an ConversationModel
    /// </summary>
    public static class XmlToConversation
    {
        public static ConversationModel Convert(string filepath)
        {
            ConversationModel newModel;
            using (FileStream fileStream = new FileStream(filepath, FileMode.Open))
            {
                XmlSerializer xmls = new XmlSerializer(typeof(ConversationModel));
                newModel = (ConversationModel) xmls.Deserialize(fileStream);
                fileStream.Close();
            }
            return newModel;
        }
    }
}
