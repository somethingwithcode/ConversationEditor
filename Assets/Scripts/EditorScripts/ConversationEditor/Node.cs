using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Linq;
using PotionTycoon.VN.Conversation;

namespace PotionTycoon.EditorScripts.ConversationEditor
{
    [Serializable]
    public class Node
    {
        public SerializableRect NodeWindow = new SerializableRect(50, 50, 0, 0);

        public int WindowID { get; set; }

        //The Object containing the actual information 
        public ConversationPart ConPart { get; set; }

        //Helper for connecting the actual dialogue objects together
        private static Node _ACTIVENODE;
        private static int _ANSWERNUMBER;

        public string[] options;
        public int selected = 0;

        public List<NodeConnection> nodeConnections = new List<NodeConnection>();

        public Node()
        {

        }

        public Node(int windowNumber)
        {
            ConPart = new ConversationPart(windowNumber);
            WindowID = windowNumber;


            if (options == null)
            {
                var enumAsStringArray = Enum.GetValues(typeof(Expressions));
                options = new string[enumAsStringArray.Length];
                for (int i = 0; i < enumAsStringArray.Length; i++)
                {
                    options[i] = enumAsStringArray.GetValue(i).ToString();
                }
            }
        }

        public void OnGUI()
        {
            NodeWindow = GUILayout.Window(WindowID, NodeWindow, DrawNode, "");

            //Draw the connections
            foreach (NodeConnection connection in nodeConnections.Where(x => x != null))
            {
                if (connection.Destination != null)
                {
                    NodeConnection.DrawConnected(connection.Source, connection.Destination.NodeWindow, NodeWindow);
                }
                else
                {
                    NodeConnection.DrawConnected(connection.Source, new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 1, 1), NodeWindow);
                }
            }
        }

        public void ClearConnection()
        {
            for (int i = 0; i < nodeConnections.Count; i++)
            {
                if (nodeConnections[i] != null)
                {
                    if (nodeConnections[i].Destination == null)
                    {
                        nodeConnections[i] = null;
                        _ACTIVENODE = null;
                    }
                }
            }
        }

        /// <summary>
        /// Setup for the Node itself
        /// </summary>
        /// <param name="id"></param>
        public void DrawNode(int id)
        {
            if (Event.current.type == EventType.MouseDown)
            {
                if (_ACTIVENODE != null && _ACTIVENODE != this)
                {
                    //connection logic
                    _ACTIVENODE.nodeConnections[_ANSWERNUMBER].Destination = this;
                    _ACTIVENODE.ConPart.Next[_ANSWERNUMBER] = ConPart.PartID;
                    _ACTIVENODE = null;
                }
                else
                {
                    ClearConnection();
                }
            }

            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            //ModelArea
            EditorGUILayout.LabelField("Expression: ");
            GUILayout.FlexibleSpace();
            selected = EditorGUILayout.Popup(selected, options, GUILayout.MinWidth(60f));
            ConPart.Expression = (Expressions)selected;
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("Main Text: ");
            ConPart.Text = EditorGUILayout.TextArea(ConPart.Text, GUILayout.MinHeight(60f));

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Answer", EditorStyles.miniButtonLeft))
            {
                ConPart.Choices.Add("");
                ConPart.Next.Add(-1);
                nodeConnections.Add(null);
            }
            if (GUILayout.Button("Remove Answer", EditorStyles.miniButtonRight))
            {
                if(ConPart.Choices.Count > 0)
                {
                    ConPart.Choices.RemoveAt(ConPart.Choices.Count - 1);
                    ConPart.Next.RemoveAt(ConPart.Next.Count - 1);

                    nodeConnections.RemoveAt(nodeConnections.Count - 1);
                }
            }
            EditorGUILayout.EndHorizontal();


            for (int i = 0; i < ConPart.Choices.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                ConPart.Choices[i] = EditorGUILayout.TextField(ConPart.Choices[i]);
                Rect tempRect = new Rect(GUILayoutUtility.GetLastRect().position.x + GUILayoutUtility.GetLastRect().width + 21, GUILayoutUtility.GetLastRect().position.y + 8, 16, 16);

                //connection from
                if (GUILayout.Button("", EditorStyles.radioButton, GUILayout.Width(16), GUILayout.Height(16)))
                {
                    _ACTIVENODE = this;
                    _ANSWERNUMBER = i;
                    if (nodeConnections[i] != null)
                    {
                        nodeConnections[i].Source = tempRect;
                        nodeConnections[i].Destination = null;
                    }
                    else
                    {
                        nodeConnections[i] = new NodeConnection(tempRect, null);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();

            GUI.DragWindow();
        }
    }
}
