using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using PotionTycoon.VN.Conversation;
using System.Xml.Serialization;

namespace PotionTycoon.EditorScripts.ConversationEditor
{
    /// <summary>
    /// This sets up the main window in which the nodes are displayed
    /// </summary>
    [Serializable]
    public class ConversationEditor : EditorWindow
    {
        private List<Node> _theGraph = new List<Node>();
        bool isLoading = false;

        /// <summary>
        /// Shows the window in the menu
        /// </summary>
        [MenuItem("Window/ConversationEditor")]
        static void Init()
        {
            ConversationEditor conversationWindow = (ConversationEditor)GetWindow(typeof(ConversationEditor));
            conversationWindow.titleContent.text = "Conversation Editor";
            conversationWindow.Show();
        }

        /// <summary>
        /// Gets called once a frame
        /// </summary>
        private void OnGUI()
        {
            if (!isLoading)
            {
                DrawToolbar();

                BeginWindows();
                foreach (Node node in _theGraph)
                {
                    node.OnGUI();
                    Repaint();
                }
                EndWindows();
            }
        }

        /// <summary>
        /// Setup for the Toolbar goes in here
        /// </summary>
        private void DrawToolbar()
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar);

            if (GUILayout.Button("New Node", EditorStyles.toolbarButton))
            {
                _theGraph.Add(new Node(_theGraph.Count));
            }
            if (GUILayout.Button("Load", EditorStyles.toolbarButton))
            {
                LoadGraph();
            }
            if (GUILayout.Button("Save", EditorStyles.toolbarButton))
            {
                SaveGraph();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

        }

        /// <summary>
        /// Loads the nodes
        /// </summary>
        private void LoadGraph()
        {
            isLoading = true;
            if (_theGraph.Count != 0)
            {
                _theGraph.Clear();
            }

            var filepath = EditorUtility.OpenFilePanel("Open Graph", Application.dataPath, "asset");
            //var filepath = "Assets/Game/Conversations/MyTest.asset";
            if (filepath.Length != 0)
            {
                Debug.Log("File loading at " + filepath);
                using (FileStream fileStream = new FileStream(filepath, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    _theGraph = (List<Node>)bf.Deserialize(fileStream);
                    fileStream.Close();
                }
            }
            else
            {
                Debug.Log("Loading aborted");
            }
            isLoading = false;
        }

        /// <summary>
        /// Saves the nodes to a file and creates a .xml file with the same name for loading at runtime
        /// </summary>
        private void SaveGraph()
        {
            var filepath = EditorUtility.SaveFilePanel("Save Graph", Application.dataPath, "NewConversationGraph", "asset");
            //var filepath = "Assets/Game/Conversations/MyTest.asset";
            if (filepath.Length != 0)
            {
                using (FileStream fileStream = new FileStream(filepath, FileMode.Create))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fileStream, _theGraph);
                    fileStream.Close();
                }

                var fileInfo = new FileInfo(filepath);

                var dir = Path.Combine(fileInfo.DirectoryName, "XML");

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                ConversationModel myModel = new ConversationModel();
                for (int i = 0; i < _theGraph.Count; i++)
                {
                    myModel.TheConversation.Add(_theGraph[i].ConPart);
                }

                using (FileStream fileStream = new FileStream(Path.Combine(dir, Path.GetFileNameWithoutExtension(filepath) + ".xml"), FileMode.Create))
                {
                    XmlSerializer xmls = new XmlSerializer(typeof(ConversationModel));
                    xmls.Serialize(fileStream, myModel);
                    fileStream.Close();
                }
                Debug.Log("File saving at " + filepath);
            }
            else
            {
                Debug.Log("Saving aborted");
            }
        }
    }
}
