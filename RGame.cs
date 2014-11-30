﻿using System;
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
    public class RGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background;
        IRState currentState;
        bool dirtyGraphics = false;

        public RGame(IRState initialState, int ScreenWidth = 1280, int ScreenHeight= 720)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            RGlobal.Game = this;

            RGlobal.Resolution = new Resolution(this, ScreenWidth, ScreenHeight);
            RGlobal.MainCamera = new Camera2D(RGlobal.Resolution);
            RGlobal.Cameras = new List<Camera2D>();
            RGlobal.Cameras.Add(RGlobal.MainCamera);

            RGlobal.Music = new MusicManager();
            RGlobal.Sound = new SoundManager();

            RGlobal.Input = new InputHelper();

            currentState = initialState;
        }

        public RGame(IRState initialState, int ScreenWidth, int ScreenHeight, int VirtualWidth, int VirtualHeight)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            RGlobal.Game = this;

            RGlobal.Resolution = new Resolution(this, VirtualWidth, VirtualHeight, ScreenWidth, ScreenHeight);
            RGlobal.MainCamera = new Camera2D(RGlobal.Resolution);
            RGlobal.Cameras = new List<Camera2D>();
            RGlobal.Cameras.Add(RGlobal.MainCamera);

            RGlobal.Music = new MusicManager();
            RGlobal.Sound = new SoundManager();

            RGlobal.Input = new InputHelper();

            currentState = initialState;
        }

        protected override void Initialize()
        {
            this.graphics.PreferredBackBufferWidth = RGlobal.Resolution.ScreenWidth;
            this.graphics.PreferredBackBufferHeight = RGlobal.Resolution.ScreenHeight;
            this.graphics.ApplyChanges();

            RGlobal.Resolution.Initialise();

            RGlobal.MainCamera.Position = new Vector2(
                RGlobal.Resolution.VirtualWidth / 2,
                RGlobal.Resolution.VirtualHeight / 2);
            RGlobal.MainCamera.RecalculateTransformationMatrix();

            RGlobal.Input = new InputHelper();
            RGlobal.Input.Update();

            Primitives.Initialise(GraphicsDevice);

            currentState.Initialize();

            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += new EventHandler<EventArgs>(OnResize);

            base.Initialize();
        }

        protected void OnResize(object sender, EventArgs e)
        {
            RGlobal.Resolution.ScreenWidth = Window.ClientBounds.Width;
            RGlobal.Resolution.ScreenHeight = Window.ClientBounds.Height;

            RGlobal.Resolution.Initialise();
            RGlobal.MainCamera.RecalculateTransformationMatrix();

            dirtyGraphics = true;
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
            if (dirtyGraphics)
            {
                this.graphics.ApplyChanges();
                dirtyGraphics = false;
            }
            
            RGlobal.Input.Update();
            
            currentState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            RGlobal.Resolution.BeginDraw();
            spriteBatch.Begin();
            Primitives.DrawRectangle(spriteBatch, new Rectangle(0, 0, RGlobal.Resolution.ScreenWidth, RGlobal.Resolution.ScreenHeight), RGlobal.BackgroundColor, true);
            spriteBatch.End();
            currentState.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        public void SwitchState(IRState newState)
        {
            currentState.UnloadContent();
            currentState = newState;
        }
    }
}
