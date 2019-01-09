# Node based conversation editor for Unity

## Attention

This is only a prototype and not production ready. While it is usable in its current state there are bugs and flaws. 
I created this as a proof of concept to show a way to create a simple editor with branching dialogue.

## What is this?

Its a node based editor for in-game conversations in the style of many JRPGs or Visual Novels. 
The editor is already able to create branching dialog but there is currently no code to access this ingame.

![Node Editor](https://github.com/somethingwithcode/ConversationEditor/blob/master/Images/choice.jpg)

For each Node you can choose an expression for the character

![Expression](https://github.com/somethingwithcode/ConversationEditor/blob/master/Images/expressions.jpg)

The Image for the selected expression will be shown with the Dialogue. 

![Dialogue](https://github.com/somethingwithcode/ConversationEditor/blob/master/Images/Dialogue1.JPG)

## Who is it for?

As I said above it's not production ready. You can use it as a starting point, as inspiration or just to look at if you are curious.

## How to use it?

Currently only one conversation gets loaded (Game/Conversations/XML/NewConversationGraph.xml)
To change this go to the ConversationController.cs and change the path. 

### Open the Editor
In your menu bar click Windows-ConversationEditor to open the Editor.

### Load a previous conversation:
1. Click load
2. Navigate to Game/Conversations 
3. Open the NewConversationGraph.asset file

### Create a new Character:
You need several character sprites with the expressions you want.
In the folder Game/Prefabs you can find the Template_NPC prefab. You use this to create a new character.
1. Drag it to your scene, and give it a name in the ConversationStart script.
2. Go to the CanvasNPC child object and drag your sprites to the correct expression
2. Drag the prefab to your Project folder to create a new prefab for your new character
