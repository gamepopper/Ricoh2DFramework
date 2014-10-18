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

        public override Color[] ColourData
        {
            get
            {
                if (Animation.currAnim == null || Animation.currAnimName.Equals(""))
                {
                    return base.ColourData;
                }

                Color[] newColour = new Color[width * height];

                Texture.GetData(0, new Rectangle(Animation.getCurrentColumn() * width, Animation.getCurrentRow() * height, width, height), newColour, 0, newColour.Length);

                return newColour;
            }
        }

        public Sprite(Texture2D texture, int width = 0, int height = 0) : base()
        {
            this.Texture = texture;
            this.width = width;
            this.height = height;
            if (this.width == 0){this.width = texture.Width;}
            if (this.height == 0){this.height = texture.Height;}

            origin = new Vector2(this.width / 2, this.height / 2);

            Animation = new AnimationManager(texture.Height/this.height, texture.Width/this.width);

            collisionBox = new Rectangle((int)(position.X - origin.X), (int)(position.Y - origin.Y), width, height);
            collisionCircle = new Circle(position, width / 2);
            collisionPolygon = new Polygon(this.width, this.height);
            colourData = new Color[texture.Width * texture.Height];
            texture.GetData(colourData);
        }

        public override void Update(GameTime gameTime)
        {
            Animation.Update(gameTime);

            if (dirtyTransform)
            {
                Transform = Matrix.Identity;
                Transform *= Matrix.CreateTranslation(new Vector3(-origin, 0));
                Transform *= Matrix.CreateScale(new Vector3(scale, 1));
                Transform *= Matrix.CreateRotationZ(rotation);
                Transform *= Matrix.CreateTranslation(new Vector3(position, 0));

                Vector2 centrePos = PositionCentre;

                collisionBox.X = (int)(position.X - origin.X);
                collisionBox.Y = (int)(position.Y - origin.Y);
                collisionBox.Width = width;
                collisionBox.Height = height;
                collisionBox.Inflate(collisionOffset, collisionOffset);

                collisionCircle.Position = centrePos;
                collisionCircle.Radius = (width / 2) + collisionOffset;
                collisionCircle.Radius *= (scale.X + scale.Y) / 2;

                collisionPolygon.Position = centrePos;
                collisionPolygon.Rotation = rotation;
                collisionPolygon.Scale = scale;

                dirtyTransform = false;
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
