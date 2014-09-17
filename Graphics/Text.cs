using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ricoh2DFramework.Graphics
{
    public class Text
    {     
        protected SpriteFont Font;
        protected String textString;
        protected TextAlignment alignment;
        protected Vector2 position;
        protected float rotation;
        protected Vector2 scale;
        protected SpriteEffects spriteEffects;
        protected Color color;
        protected float opacity;
        protected float depth;

        protected int width;
        protected int height;
        protected Vector2 origin;

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
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                dirtyTransform = true;
            }
        }
        public float Rotation
        {
            get { return rotation; }
            set
            {
                rotation = value;

                if (rotation >= 2 * MathHelper.Pi)
                    rotation -= 2 * MathHelper.Pi;
                else if (rotation < 0)
                    rotation += 2 * MathHelper.Pi;

                dirtyTransform = true;
            }
        }
        public Vector2 Scale
        {
            get { return scale; }
            set
            {
                scale = value;
                dirtyTransform = true;
            }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public float Opacity
        {
            get { return opacity; }
            set { opacity = value; }
        }
        public float Depth
        {
            get { return depth; }
            set { depth = value; }
        }

        public int Width
        {
            get { return width; }
            set 
            { 
                width = value; 
                dirtyTransform = true; 
            }
        }
        public int Height
        {
            get { return height; }
        }
        public Vector2 Origin
        {
            get { return origin; }
            set
            {
                origin = value;
            }
        }

        protected bool dirtyTransform = true;

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
