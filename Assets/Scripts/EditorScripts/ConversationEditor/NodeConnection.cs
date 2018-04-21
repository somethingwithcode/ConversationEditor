using PotionTycoon.EditorScripts.ConversationEditor;
using System;
using UnityEditor;
using UnityEngine;

namespace PotionTycoon.EditorScripts.ConversationEditor
{
    [Serializable]
    public class NodeConnection
    {
        public NodeConnection()
        {

        }
        public NodeConnection(SerializableRect from, Node to)
        {
            Source = from;
            Destination = to;
        }

        public SerializableRect Source { get; set; }
        //public Rect destination { get; set; }
        public Node Destination { get; set; }

        /// <summary>
        /// Draws the connection between nodes
        /// </summary>
        /// <param name="fromRect"></param>
        /// <param name="toRect"></param>
        /// <param name="nodeWindow"></param>
        public static void DrawConnected(Rect fromRect, Rect toRect, Rect nodeWindow)
        {
            Vector2 from = fromRect.position + nodeWindow.position;
            Vector2 to = toRect.position;
            Handles.DrawBezier(
                                new Vector3(from.x, from.y, 0),
                                new Vector3(to.x, to.y, 0),
                                new Vector3(from.x, from.y, 0.0f) + Vector3.right * 50.0f,
                                new Vector3(to.x, to.y, 0.0f) + Vector3.left * 50.0f,
                Color.black, null, 3.0f);
        }
    }
}
