using UnityEngine;
using UnityEngine.UI;

namespace PotionTycoon.VN.Conversation
{
    public class TextBoxController : MonoBehaviour
    {
        public Text _nameField;
        public Text _textField;

        public void ChangeText(string text)
        {
            _textField.text = text;
        }

        public void ChangeName(string name)
        {
            _nameField.text = name;
        }
    }
}
