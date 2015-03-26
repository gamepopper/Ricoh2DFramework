using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
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
        public static Game Game;

        public static Resolution Resolution;
        public static List<Camera2D> Cameras;
        public static Camera2D MainCamera;

        public static MusicManager Music;
        public static SoundManager Sound;

        public static InputHelper Input;

        public static Color BackgroundColor = Color.Black;
    }
}
