using MusicStreaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManagers
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
        /// <summary>
        /// methode abstraite implementee par les differents managers pour s'authentifier
        /// retourne True si l'operation reussi et False sinon
        /// </summary>
        /// <returns>Task<bool></returns>
        public abstract Task<bool> RunAuthentication();
        /// <summary>
        /// methode abstraite implementee par les differents managers pour se connecter
        /// a un client, retourne True si l'operation reussi et False sinon
        /// </summary>
        /// <returns>bool</returns>
        public abstract bool ConnectClient();
        /// <summary>
        /// cherche une piste selon un mot cle, et retourne une liste contenant 
        /// les pistes trouvees
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns>Task<List<LocalTrack>></returns>
        public abstract Task<List<LocalTrack>> SearchTrack(string keyWord);
        /// <summary>
        /// retourne la piste courante
        /// </summary>
        /// <returns>LocalTrack</returns>
        public abstract LocalTrack GetCurrentTrack();
        /// <summary>
        /// Permet de jouer une piste
        /// </summary>
        /// <param name="track"></param>
        /// <returns>Task</returns>
        public abstract Task Play(LocalTrack track);
        /// <summary>
        /// mettre en pause la lecture d une piste 
        /// </summary>
        /// <returns></returns>
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
