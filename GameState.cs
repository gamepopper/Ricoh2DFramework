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

namespace Ricoh2DFramework
{
    public class GameState
    {
        protected RGame rGame;

        public GameState()
        {

        }

        public GameState(RGame game)
        {
            rGame = game;
            Initialize();
            LoadContent(game.Content);
        }

        public void setGame(RGame game)
        {
            if (rGame == null)
            {
                rGame = game;
            }
        }

        public virtual void Initialize()
        {

        }

        public virtual void LoadContent(ContentManager Content)
        {

        }

        public virtual void UnloadContent()
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
