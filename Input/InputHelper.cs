using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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

namespace Ricoh2DFramework.Input
{
    public class InputHelper
    {
        KeyboardState currentKeyState;
        KeyboardState previousKeyState;

        Dictionary<PlayerIndex,GamePadState> currentGamePadState = new Dictionary<PlayerIndex,GamePadState>();
        Dictionary<PlayerIndex, GamePadState> previousGamePadState = new Dictionary<PlayerIndex, GamePadState>();

        MouseState currentMouseState;
        MouseState previousMouseState;

        PlayerIndex index = PlayerIndex.One;

        public bool InvertedLeft = false;
        public bool InvertedRight = false;

        public PlayerIndex GamepadIndex
        {
            get { return index; }
            set { index = value; }
        }

        public InputHelper() { }

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

            if (currentGamePadState.Count == 0 && previousGamePadState.Count == 0)
            {
                currentGamePadState.Add(PlayerIndex.One, GamePad.GetState(PlayerIndex.One));
                previousGamePadState.Add(PlayerIndex.One, GamePad.GetState(PlayerIndex.One));
                currentGamePadState.Add(PlayerIndex.Two, GamePad.GetState(PlayerIndex.Two));
                previousGamePadState.Add(PlayerIndex.Two, GamePad.GetState(PlayerIndex.Two));
                currentGamePadState.Add(PlayerIndex.Three, GamePad.GetState(PlayerIndex.Three));
                previousGamePadState.Add(PlayerIndex.Three, GamePad.GetState(PlayerIndex.Three));
                currentGamePadState.Add(PlayerIndex.Four, GamePad.GetState(PlayerIndex.Four));
                previousGamePadState.Add(PlayerIndex.Four, GamePad.GetState(PlayerIndex.Four));
            }
            else
            {
                previousGamePadState[PlayerIndex.One] = currentGamePadState[PlayerIndex.One];
                currentGamePadState[PlayerIndex.One] = GamePad.GetState(PlayerIndex.One);
                previousGamePadState[PlayerIndex.Two] = currentGamePadState[PlayerIndex.Two];
                currentGamePadState[PlayerIndex.Two] = GamePad.GetState(PlayerIndex.Two);
                previousGamePadState[PlayerIndex.Three] = currentGamePadState[PlayerIndex.Three];
                currentGamePadState[PlayerIndex.Three] = GamePad.GetState(PlayerIndex.Three);
                previousGamePadState[PlayerIndex.Four] = currentGamePadState[PlayerIndex.Four];
                currentGamePadState[PlayerIndex.Four] = GamePad.GetState(PlayerIndex.Four);
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

        public bool isGamePadConnected(PlayerIndex Index)
        {
            return GamePad.GetState(Index).IsConnected;
        }

        public bool isGamePadButtonDown(Buttons button)
        {
            return currentGamePadState[index].IsButtonDown(button);
        }

        public bool isGamePadButtonUp(Buttons button)
        {
            return currentGamePadState[index].IsButtonUp(button);
        }

        public bool isGamePadButtonPressed(Buttons button)
        {
            return (previousGamePadState[index].IsButtonUp(button) && currentGamePadState[index].IsButtonDown(button));
        }

        public bool isGamePadButtonReleased(Buttons button)
        {
            return (previousGamePadState[index].IsButtonDown(button) && currentGamePadState[index].IsButtonUp(button));
        }

        public Vector2 LeftAnalogStick
        {
            get 
            {
                if (InvertedLeft)
                    return new Vector2(currentGamePadState[index].ThumbSticks.Left.X,
                        -currentGamePadState[index].ThumbSticks.Left.Y);
                else
                    return currentGamePadState[index].ThumbSticks.Left; 
            }
        }

        public Vector2 RightAnalogStick
        {
            get 
            {
                if (InvertedRight)
                    return new Vector2(currentGamePadState[index].ThumbSticks.Right.X,
                        -currentGamePadState[index].ThumbSticks.Right.Y);
                else
                    return currentGamePadState[index].ThumbSticks.Right; 
            }
        }

        public float LeftTrigger
        {
            get { return currentGamePadState[index].Triggers.Left; }
        }

        public float RightTrigger
        {
            get { return currentGamePadState[index].Triggers.Right; }
        }

        public bool isMouseButtonDown(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LEFT:
                    return currentMouseState.LeftButton == ButtonState.Pressed;
                case MouseButtons.MIDDLE:
                    return currentMouseState.MiddleButton == ButtonState.Pressed;
                case MouseButtons.RIGHT:
                    return currentMouseState.RightButton == ButtonState.Pressed;
                case MouseButtons.XBUTTON1:
                    return currentMouseState.XButton1 == ButtonState.Pressed;
                case MouseButtons.XBUTTON2:
                    return currentMouseState.XButton2 == ButtonState.Pressed;
                default:
                    return false;
            }
        }

        public bool isMouseButtonUp(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LEFT:
                    return currentMouseState.LeftButton == ButtonState.Released;
                case MouseButtons.MIDDLE:
                    return currentMouseState.MiddleButton == ButtonState.Released;
                case MouseButtons.RIGHT:
                    return currentMouseState.RightButton == ButtonState.Released;
                case MouseButtons.XBUTTON1:
                    return currentMouseState.XButton1 == ButtonState.Released;
                case MouseButtons.XBUTTON2:
                    return currentMouseState.XButton2 == ButtonState.Released;
                default:
                    return false;
            }
        }

        public bool isMouseButtonPressed(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LEFT:
                    return (previousMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed);
                case MouseButtons.MIDDLE:
                    return (previousMouseState.MiddleButton == ButtonState.Released && currentMouseState.MiddleButton == ButtonState.Pressed);
                case MouseButtons.RIGHT:
                    return (previousMouseState.RightButton == ButtonState.Released && currentMouseState.RightButton == ButtonState.Pressed);
                case MouseButtons.XBUTTON1:
                    return (previousMouseState.XButton1 == ButtonState.Released && currentMouseState.XButton1 == ButtonState.Pressed);
                case MouseButtons.XBUTTON2:
                    return (previousMouseState.XButton2 == ButtonState.Released && currentMouseState.XButton2 == ButtonState.Pressed);
                default:
                    return false;
            }
        }

        public bool isMouseButtonReleased(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LEFT:
                    return (previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released);
                case MouseButtons.MIDDLE:
                    return (previousMouseState.MiddleButton == ButtonState.Pressed && currentMouseState.MiddleButton == ButtonState.Released);
                case MouseButtons.RIGHT:
                    return (previousMouseState.RightButton == ButtonState.Pressed && currentMouseState.RightButton == ButtonState.Released);
                case MouseButtons.XBUTTON1:
                    return (previousMouseState.XButton1 == ButtonState.Pressed && currentMouseState.XButton1 == ButtonState.Released);
                case MouseButtons.XBUTTON2:
                    return (previousMouseState.XButton2 == ButtonState.Pressed && currentMouseState.XButton2 == ButtonState.Released);
                default:
                    return false;
            }
        }

        public Vector2 MouseCoordinates
        {
            get { return new Vector2(currentMouseState.X, currentMouseState.Y); }
        }

        public Vector2 MouseVelocity
        {
            get
            {
                Vector2 prevPos = new Vector2(previousMouseState.X, previousMouseState.Y);
                Vector2 currPos = new Vector2(currentMouseState.X, currentMouseState.Y);

                return currPos - prevPos;
            }
        }

        public int MouseScrollWheel
        {
            get { return currentMouseState.ScrollWheelValue; }
        }

        public int MouseScrollWheelVelocity
        {
            get { return currentMouseState.ScrollWheelValue - previousMouseState.ScrollWheelValue; }
        }
    }
}
