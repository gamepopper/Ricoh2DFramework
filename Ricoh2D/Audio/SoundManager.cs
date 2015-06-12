using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

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

namespace Ricoh2DFramework.Audio
{
    public class SoundManager
    {
        private Dictionary<string, SoundEffect> SoundEffectList;
        private Dictionary<string, SoundEffectInstance> SoundEffectInstanceList;

        public SoundManager()
        {
            SoundEffectList = new Dictionary<string, SoundEffect>();
            SoundEffectInstanceList = new Dictionary<string, SoundEffectInstance>();
        }

        public void Add(string key, SoundEffect value)
        {
            if (!SoundEffectList.ContainsKey(key))
                SoundEffectList.Add(key, value);
        }

        public void Remove(string key, bool instanceOnly=false)
        {
            if (!instanceOnly)
            {
                SoundEffect effect = Get(key);
                if (effect != null)
                    effect.Dispose();
            }
            Stop(key);
        }

        public void Clear()
        {
            foreach (string key in SoundEffectList.Keys)
            {
                Remove(key);
            }
            SoundEffectList.Clear();
            SoundEffectInstanceList.Clear();
        }

        private SoundEffect Get(string key)
        {
            SoundEffect effect;
            SoundEffectList.TryGetValue(key, out effect);
            return effect;
        }

        public SoundEffectInstance GetInstance(string key)
        {
            SoundEffectInstance effect;
            SoundEffectInstanceList.TryGetValue(key, out effect);
            return effect;
        }

        public void Play(string key, float volume=1, float pitch = 0, float pan = 0, bool looped=false)
        {
            SoundEffect effect = Get(key);
            if (effect != null)
            {
                if (looped == false)
                {
                    effect.Play(volume, pitch, pan);
                }
                else
                {
                    SoundEffectInstance effectInstance;

                    if (SoundEffectInstanceList.ContainsKey(key))
                    {
                        effectInstance = GetInstance(key);
                        if (effectInstance.State != SoundState.Playing)
                        {
                            effectInstance.Volume = volume;
                            effectInstance.IsLooped = looped;
                            effectInstance.Pitch = pitch;
                            effectInstance.Pan = pan;
                        }
                    }
                    else
                    {
                        effectInstance = effect.CreateInstance();
                        effectInstance.Volume = volume;
                        effectInstance.IsLooped = looped;
                        effectInstance.Pitch = pitch;
                        effectInstance.Pan = pan;
                        SoundEffectInstanceList.Add(key, effectInstance);
                    }

                    effectInstance.Play();
                }
            }
        }

        public void Stop(string key)
        {
            SoundEffectInstance effect = GetInstance(key);

            if (effect != null)
            {
                effect.Stop();
                effect.Dispose();
                SoundEffectInstanceList.Remove(key);
            }
        }
    }
}
