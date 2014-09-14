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
