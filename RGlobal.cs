using System;
using System.Collections.Generic;
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
    public class RGlobal
    {
        public static MusicManager Music = new MusicManager();
        public static SoundManager Sound = new SoundManager();

        public static InputHelper Input = new InputHelper();

        public static List<Camera2D> Cameras = new List<Camera2D>();
        public static Camera2D camera = new Camera2D();
        public static Resolution Resolution = new Resolution();

        public static Color BackgroundColor = Color.Black;
    }
}
