using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Ricoh2DFramework;
using Ricoh2DFramework.Audio;
using Ricoh2DFramework.Collisions;
using Ricoh2DFramework.Graphics;
using Ricoh2DFramework.Input;

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

namespace Ricoh2DFramework
{
    public class RGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public MusicManager Music;
        public SoundManager Sound;

        public InputHelper Input;

        public Camera2D Camera;
        public Resolution Resolution;

        public Color BackgroundColor = Color.Black;

        GameState currentState;

        public RGame(GameState initialState, int ScreenWidth = 1280, int ScreenHeight= 720)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Resolution = new Resolution(this, ScreenWidth, ScreenHeight, ScreenWidth, ScreenHeight);
            Camera = new Camera2D(Resolution);

            initialState.setGame(this);
            currentState = initialState;
        }

        public RGame(GameState initialState, int ScreenWidth, int ScreenHeight, int VirtualWidth, int VirtualHeight)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Resolution = new Resolution(this, VirtualWidth, VirtualHeight, ScreenWidth, ScreenHeight);
            Camera = new Camera2D(Resolution);

            initialState.setGame(this);
            currentState = initialState;
        }

        protected override void Initialize()
        {
            this.graphics.PreferredBackBufferWidth = Resolution.ScreenWidth;
            this.graphics.PreferredBackBufferHeight = Resolution.ScreenHeight;
            this.graphics.ApplyChanges();

            Resolution.Initialise();

            Camera.Position = new Vector2(
                Resolution.VirtualWidth / 2,
                Resolution.VirtualHeight / 2);
            Camera.RecalculateTransformationMatrix();

            Input = new InputHelper();
            Input.Update();

            currentState.Initialize();

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            currentState.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            currentState.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            
            currentState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(BackgroundColor);
            Resolution.BeginDraw();
            currentState.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        public void SwitchState(GameState newState)
        {
            currentState.UnloadContent();
            currentState = newState;
        }
    }
}
