using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*The MIT License (MIT)

Copyright (c) 2014 Gamepopper

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

namespace Ricoh2DFramework.Graphics
{
    public class Text : RBasic
    {     
        protected SpriteFont Font;
        protected String textString;
        protected TextAlignment alignment;

        public String TextString
        {
            get
            {
                return textString;
            }
            set
            {
                textString = value;
                dirtyTransform = true;
            }
        }
        public TextAlignment Alignment
        {
            get { return alignment; }
            set
            {
                alignment = value;
                dirtyTransform = true;
            }
        }
        public bool Multiline = false;

        public Text(SpriteFont Font, String Text, int Width, TextAlignment alignment=TextAlignment.LEFT)
        {
            this.Font = Font;
            this.textString = Text;
            this.alignment = alignment;
            this.width = Width;

            position = Vector2.Zero;
            rotation = 0;
            scale = new Vector2(1, 1);
            spriteEffects = SpriteEffects.None;
            color = Color.White;
            opacity = 1;
            depth = 0;

            origin = Vector2.Zero;

            dirtyTransform = true;
        }

        protected string[] WrapText(string text)
        {
            //Original code from xnawiki.com
            
            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0f;
            float spaceWidth = Font.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = Font.MeasureString(word);

                if (lineWidth + size.X < Width)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    sb.Append("\n" + word + " ");
                    lineWidth = size.X + spaceWidth;
                }
            }

            return sb.ToString().Split('\n');
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            String[] outputText;
            if (Multiline)
                outputText = WrapText(textString);
            else
                outputText = new String[]{textString};

            height = 0;

            for (int i = 0; i < outputText.Length; i++)
            {
                Vector2 textSize = Font.MeasureString(outputText[i]);
                float alignAmount = 0;

                height += (int)textSize.Y;
                
                if (alignment == TextAlignment.LEFT)
                {
                     alignAmount = 0;
                }
                else if (alignment == TextAlignment.CENTER)
                {
                    alignAmount = Width/2 - textSize.X/2;
                }
                else
                {
                    alignAmount = Width - textSize.X;
                }

                spriteBatch.DrawString(Font, outputText[i], new Vector2(position.X + alignAmount, position.Y + (i * textSize.Y)), color * opacity, rotation, origin, scale, spriteEffects, depth);
            }

            dirtyTransform = false;
        }
    }
}
