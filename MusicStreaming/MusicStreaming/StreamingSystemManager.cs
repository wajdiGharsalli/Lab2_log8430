using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStreaming
{
    public abstract class StreamingSystemManager
    {
        public delegate void OnPlayStateChangeLocalHandler(bool isPlaying);
        public delegate void OnTrackTimeChangeLocalHandler(int time);
        public delegate void OnVolumeChangeLocalHandler();
        public delegate void OnTrackChangeLocalHandler();

        public event OnPlayStateChangeLocalHandler OnPlayStateChange;
        public event OnTrackTimeChangeLocalHandler OnTrackTimeChange;
        public event OnVolumeChangeLocalHandler OnVolumeChange;
        public event OnTrackChangeLocalHandler OnTrackChange;

        public abstract Task<bool> RunAuthentication();

        public abstract bool ConnectClient();

        public abstract Task<List<LocalTrack>> SearchTrack(string keyWord);

        public abstract LocalTrack GetCurrentTrack();

        public abstract Task Play(LocalTrack track);

        public abstract Task Pause();

        protected void OnTrackChangeHandler()
        {
            OnTrackChange?.Invoke();
        }

        protected void OnVolumeChangeHandler()
        {
            OnVolumeChange?.Invoke();
        }

        protected void OnPlayStateChangeHandler(bool isPlaying)
        {
            OnPlayStateChange?.Invoke(isPlaying);
        }

        protected void OnTrackTimeChangeHandler(int time)
        {
            OnTrackTimeChange?.Invoke(time);
        }
    }
}
