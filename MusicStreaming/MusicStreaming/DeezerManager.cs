using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E.Deezer;         //Main Deezer client + API access classes
using E.Deezer.Api;    //The Deezer API Objects (Artist, Album, ... etc)
using MusicStreaming;

namespace ApiManagers
{
    public class DeezerManager : StreamingSystemManager
    {
        Deezer m_deezer = DeezerSession.CreateNew();

        private static DeezerManager m_instance;

        private DeezerManager()
        {
            /*m_spotifyLocalAPI.OnPlayStateChange += OnPlayStateChangeHandler;
            m_spotifyLocalAPI.OnTrackChange += OnTrackChangeHandler;
            m_spotifyLocalAPI.OnTrackTimeChange += OnTrackTimeChangeHandler;
            m_spotifyLocalAPI.OnVolumeChange += OnVolumeChangeHandler;*/
        }

        /// <summary>
        /// permet d obtenir une reference vers l instance unique (Singleton)
        /// </summary>
        /// <returns>DeezerManager</returns>
        public static DeezerManager GetInstance()
        {
            if (m_instance == null)
                m_instance = new DeezerManager();
            return m_instance;
        }


        /// <summary>
        /// permet de s authentifier aupres de Deezer, retourne true si l operation
        /// reussie et false sinon
        /// </summary>
        /// <returns></returns>
        public override async Task<bool> RunAuthentication()
        {
            if (m_deezer == null)
            {
                await Task.Run( () => m_deezer = DeezerSession.CreateNew());
            }

            if (m_deezer == null)
                return false;
            return true;
        }


        /// <summary>
        /// permet de se connecter a Deezer, retourne true si la connection reussi et
        /// false sinon
        /// </summary>
        /// <returns></returns>
        public override bool ConnectClient()
        {
            if (m_deezer == null)
            {
                m_deezer = DeezerSession.CreateNew();
            }

            if (m_deezer == null)
                return false;
            return true;
        }


        /// <summary>
        /// permet de chercher des pistes par mots cles aupres de Deezer
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns>Task<List<LocalTrack>></returns>
        public override async Task<List<LocalTrack>> SearchTrack(string keyWord)
        {
            var search = await m_deezer?.Search.Tracks(keyWord);
            return new List<LocalTrack>(search.Select(t => ToLocalTrack(t)).ToList());
        }

        private LocalTrack ToLocalTrack(ITrack deezerTrack)
        {
            LocalTrack track = new LocalTrack();
            track.Name = deezerTrack.Title;
            track.Album = deezerTrack.Album.Title;
            track.Artist = deezerTrack.Artist.Name;
            track.Id = deezerTrack.Id.ToString();
            track.Duration = (deezerTrack.Duration * 10 / 6) / 100.0;
            track.Type = StreamingSystemType.Deezer;
            track.Image = deezerTrack.Album.GetPicture(PictureSize.Medium);
            return track;
        }


        /// <summary>
        /// permet d obtenir une reference vers la piste jouee actuellement
        /// </summary>
        /// <returns>LocalTrack</returns>
        public override LocalTrack GetCurrentTrack()
        {
            return new LocalTrack();
        }


        /// <summary>
        /// permet de jouer une piste de Deezer
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        public override async Task Play(LocalTrack track)
        {
            await Task.Run(() => { });
        }


        /// <summary>
        /// permet de mettre en pause la lecture d une piste Deezer
        /// </summary>
        /// <returns></returns>
        public override async Task Pause()
        {
            await Task.Run(() => { });
        }
    }
}
