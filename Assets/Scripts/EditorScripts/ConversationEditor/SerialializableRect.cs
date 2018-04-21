using System;
using UnityEngine;

namespace PotionTycoon.EditorScripts.ConversationEditor
{
    [Serializable]
    public class SerializableRect
    {
        private float _positionX;
        private float _positionY;
        private float _width;
        private float _height;

        public SerializableRect(float x, float y, float width, float height)
        {
            _positionX = x;
            _positionY = y;
            _width = width;
            _height = height;
        }

        public SerializableRect(Rect MyRect)
        {
            _positionX = MyRect.x;
            _positionY = MyRect.y;
            _width = MyRect.width;
            _height = MyRect.height;
        }

        // Allows conversation from SerializableRect to Rect
        public static implicit operator Rect(SerializableRect rect)
        {
            return new Rect(rect._positionX, rect._positionY, rect._width, rect._height);
        }

        // Allows conversation from Rect to SerializableRect
        public static implicit operator SerializableRect(Rect rect)
        {
            return new SerializableRect(rect);
        }
    }
}
