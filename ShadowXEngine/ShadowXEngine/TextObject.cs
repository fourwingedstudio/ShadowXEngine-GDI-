using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowXEngine
{
    class TextObject
    {
        private Vector2 position;
        private int fontsize;
        private string font;
        private string text;
        private Color color;
        public TextObject(Vector2 position, int fontsize, string font, string text, Color color)
        {
            this.position = position;
            this.fontsize = fontsize;
            this.font = font;
            this.text = text;
            this.color = color;
            Engine.CreateTextObject(this);
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
        public int FontSize
        {
            get
            {
                return fontsize;
            }
            set
            {
                fontsize = value;
            }
        }
        public string Font
        {
            get
            {
                return font;
            }
            set
            {
                font = value;
            }
        }
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }
        public Color FontColor
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }
    }
}
