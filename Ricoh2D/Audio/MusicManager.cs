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
        private Dictionary<string, SongCollection> SongCollectionList;

        public MusicManager()
        {
            SongList = new Dictionary<string, Song>();
            SongCollectionList = new Dictionary<string, SongCollection>();
        }

        public void Add(string key, Song value)
        {
            if (!SongList.ContainsKey(key) && !SongCollectionList.ContainsKey(key))
            {
                SongList.Add(key, value);
            }
        }

        public void Add(string key, SongCollection value)
        {
            if (!SongList.ContainsKey(key) && !SongCollectionList.ContainsKey(key))
            {
                SongCollectionList.Add(key, value);
            }
        }

        public Song Get(string key)
        {
            Song song;
            SongList.TryGetValue(key, out song);

            return song;
        }

        public SongCollection GetCollection(string key)
        {
            SongCollection song;
            SongCollectionList.TryGetValue(key, out song);

            return song;
        }

        public void Play(string key, float volume = 1.0f, bool looped = false, bool shuffle = false)
        {
            Song song = Get(key);

            if (song != null)
            {
                if (MediaPlayer.Queue.ActiveSong != song)
                {
                    MediaPlayer.Volume = volume;
                    MediaPlayer.IsShuffled = shuffle;
                    MediaPlayer.IsRepeating = looped;
                    MediaPlayer.Play(song);
                }
            }

            SongCollection songCollection = GetCollection(key);
            if (songCollection != null)
            {
                if (MediaPlayer.Queue.ActiveSong != songCollection[0])
                {
                    MediaPlayer.Volume = volume;
                    MediaPlayer.IsShuffled = shuffle;
                    MediaPlayer.IsRepeating = looped;
                    MediaPlayer.Play(songCollection);
                }
            }
        }

        public void Pause()
        {
            if (MediaPlayer.State == MediaState.Playing)
                MediaPlayer.Pause();
            else
                MediaPlayer.Resume();
            
        }

        public void Stop()
        {
            MediaPlayer.Stop();
        }
    }
}
