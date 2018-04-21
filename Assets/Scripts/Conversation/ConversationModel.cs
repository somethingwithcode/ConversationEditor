using System;
using System.Collections.Generic;

namespace PotionTycoon.VN.Conversation
{
    /// <summary>
    /// Holds the full conversation.
    /// </summary>
    [Serializable]
    public class ConversationModel 
    {
        public List<ConversationPart> TheConversation { get; set; }

        public ConversationModel()
        {
            TheConversation = new List<ConversationPart>();
        }
    }

    /// <summary>
    /// Helper class that holds the data for the current conversation (text, expression changes...)
    /// </summary>
    [Serializable]
    public class ConversationPart
    {
        public int PartID { get; set; }
        public string Text { get; set; }
        public Expressions Expression { get; set; }
        public List<string> Choices;
        public List<int?> Next;

        public ConversationPart()
        {
        }

        public ConversationPart(int id)
        {
            PartID = id;
            Choices = new List<string>();
            Next = new List<int?>();

        }
    }
}
