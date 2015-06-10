using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ricoh2DFramework.Collisions;

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
    public class Sprite : RObject
    {
        public Texture2D Texture;
        public AnimationManager Animation;

        public Sprite(Texture2D texture, int width = 0, int height = 0) : base()
        {
            this.Texture = texture;
            this.width = width;
            this.height = height;
            if (this.width == 0){this.width = texture.Width;}
            if (this.height == 0){this.height = texture.Height;}

            origin = new Vector2(this.width / 2, this.height / 2);

            Animation = new AnimationManager(texture.Height/this.height, texture.Width/this.width);

            Collider.SetSize(width, height);
            Collider.SetOrigin(origin);
            Collider.SetTransform(position, rotation, scale);
        }

        public override void Update(GameTime gameTime)
        {
            Animation.Update(gameTime);

            if (Animation.currAnim == null || Animation.currAnimName.Equals(""))
            {
                Collider.ColourData = new Color[width * height];
                Texture.GetData(0, Collider.RenderBox, Collider.ColourData, 0, Collider.ColourData.Length);
            }
            else
            {
                Collider.ColourData = new Color[width * height];
                Texture.GetData(0, new Rectangle(Animation.getCurrentColumn() * width, Animation.getCurrentRow() * height, width, height), Collider.ColourData, 0, Collider.ColourData.Length);
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int renderX = Animation.getCurrentColumn() * width;
            int renderY = Animation.getCurrentRow() * height;

            spriteBatch.Draw(Texture, position, new Rectangle(renderX, renderY, width, height), color * opacity, rotation, origin, scale, spriteEffects, depth);

            base.Draw(spriteBatch);
        }
    }
}
