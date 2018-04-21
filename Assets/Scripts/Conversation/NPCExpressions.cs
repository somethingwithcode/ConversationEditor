using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace PotionTycoon.VN.Conversation
{
    public enum Expressions
    {
        Normal, Angry, Annoyed, Smile, Laughing, Relief
    }

    public class NPCExpressions : MonoBehaviour
    {
        public Sprite Normal;
        public Sprite Angry;
        public Sprite Annoyed;
        public Sprite Smile;
        public Sprite Laughing;
        public Sprite Relief;

        private Image _currentExpression;

        private void Awake()
        {
            _currentExpression = GetComponentInChildren<Image>();
        }

        public void ChangeExpression(Expressions exp)
        {
            switch (exp)
            {
                case Expressions.Normal:
                    _currentExpression.sprite = Normal;
                    break;
                case Expressions.Angry:
                    _currentExpression.sprite = Angry;
                    break;
                case Expressions.Annoyed:
                    _currentExpression.sprite = Annoyed;
                    break;
                case Expressions.Smile:
                    _currentExpression.sprite = Smile;
                    break;
                case Expressions.Laughing:
                    _currentExpression.sprite = Laughing;
                    break;
                case Expressions.Relief:
                    _currentExpression.sprite = Relief;
                    break;
                default:
                    _currentExpression.sprite = Normal;
                    break;
            }
        }
    }
}
