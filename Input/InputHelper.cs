using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Ricoh2DFramework.Input
{
    public class InputHelper
    {
        KeyboardState currentKeyState;
        KeyboardState previousKeyState;

        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        MouseState currentMouseState;
        MouseState previousMouseState;

        PlayerIndex index = PlayerIndex.One;


        public void Update()
        {
            if (currentKeyState == null && previousKeyState == null)
            {
                currentKeyState = previousKeyState = Keyboard.GetState();
            }
            else
            {
                previousKeyState = currentKeyState;
                currentKeyState = Keyboard.GetState();
            }

            if (currentMouseState == null && previousMouseState == null)
            {
                currentMouseState = previousMouseState = Mouse.GetState();
            }
            else
            {
                previousMouseState = currentMouseState;
                currentMouseState = Mouse.GetState();
            }

            if (currentGamePadState == null && previousGamePadState == null)
            {
                currentGamePadState = previousGamePadState = GamePad.GetState(index);
            }
            else
            {
                previousGamePadState = currentGamePadState;
                currentGamePadState = GamePad.GetState(index);
            }
        }

        public bool isKeyDown(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }

        public bool isKeyUp(Keys key)
        {
            return currentKeyState.IsKeyUp(key);
        }

        public bool isKeyPressed(Keys key)
        {
            return (previousKeyState.IsKeyUp(key) && currentKeyState.IsKeyDown(key));
        }

        public bool isKeyReleased(Keys key)
        {
            return (previousKeyState.IsKeyDown(key) && currentKeyState.IsKeyUp(key));
        }

        public bool isGamePadButtonDown(Buttons button)
        {
            return currentGamePadState.IsButtonDown(button);
        }

        public bool isGamePadButtonUp(Buttons button)
        {
            return currentGamePadState.IsButtonUp(button);
        }

        public bool isGamePadButtonPressed(Buttons button)
        {
            return (previousGamePadState.IsButtonUp(button) && currentGamePadState.IsButtonDown(button));
        }

        public bool isGamePadButtonReleased(Buttons button)
        {
            return (previousGamePadState.IsButtonDown(button) && currentGamePadState.IsButtonUp(button));
        }


    }
}
