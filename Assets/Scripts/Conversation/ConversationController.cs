using UnityEngine;

namespace PotionTycoon.VN.Conversation
{
    public class ConversationController
    {
        private string _myName;

        private NPCExpressions _expression;
        private TextBoxController _textBox;

        private ConversationModel _conversation;
        private int? _currentPosition = 0;

        public ConversationController(string name, NPCExpressions npc, TextBoxController tbc)
        {
            _myName = name;
            _expression = npc;
            _textBox = tbc;

            if(_conversation == null)
            {
                /*
                 * Loads the text from a file.
                 * Could also be load from a loader class or a database
                 * 
                 * */
                var st = Application.dataPath + @"\Game\Conversations\XML\NewConversationGraph.xml";
                _conversation = XmlToConversation.Convert(st);
            }
            NextText();

        }

        public void NextText()
        {
            if (_currentPosition != null)
            {
                var model = _conversation.TheConversation[_currentPosition.Value];
                _textBox.ChangeName(_myName);
                _textBox.ChangeText(model.Text);
                _expression.ChangeExpression(model.Expression);

                if(model.Next[0] != null)
                {
                    _currentPosition = model.Next[0];
                }

            }
            else
            {                
                //End of Conversation
            }
        }
    }
}
