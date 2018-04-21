using UnityEngine;

namespace PotionTycoon.VN.Conversation
{
    public class ConversationStart : MonoBehaviour
    {
        public string Name;
        // Holds the Pictures of this NPC
        public GameObject CanvasNPC;
        public GameObject Textbox;

        private ConversationController _cController;

        private void Start()
        {
            _cController = new ConversationController(Name, CanvasNPC.GetComponent<NPCExpressions>(), Textbox.GetComponent<TextBoxController>());
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _cController.NextText();
            }
        }


    }
}
