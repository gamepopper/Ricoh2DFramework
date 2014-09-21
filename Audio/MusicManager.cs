using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

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
    public class MusicManager
    {
        private Dictionary<string, Song> SongList;

        public MusicManager()
        {
            SongList = new Dictionary<string, Song>();
        }

        public void Add(string key, Song value)
        {
            if (!SongList.ContainsKey(key))
            {
                SongList.Add(key, value);
            }
        }

        public Song Get(string key)
        {
            Song song;
            SongList.TryGetValue(key, out song);

            return song;
        }

        public void Play(string key, bool looped = false)
        {
            Song song = Get(key);

            if (song != null)
            {
                if (MediaPlayer.Queue.ActiveSong != song)
                {
                    MediaPlayer.IsRepeating = looped;
                    MediaPlayer.Play(song);
                }
            }
        }
    }
}
