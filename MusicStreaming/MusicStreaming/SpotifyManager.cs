using MusicStreaming;
using SpotifyAPI.Local;
using SpotifyAPI.Local.Enums;
using SpotifyAPI.Local.Models;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ApiManagers
{
    public class SpotifyManager : StreamingSystemManager
    {
        private readonly SpotifyLocalAPI m_spotifyLocalAPI = new SpotifyLocalAPI();
        private SpotifyWebAPI m_spotifyWebAPI;
        private Track m_currentTrack;

        private static SpotifyManager m_instance;

        private SpotifyManager()
        {
            m_spotifyLocalAPI.OnPlayStateChange += OnPlayStateChangeHandler;
            m_spotifyLocalAPI.OnTrackChange += OnTrackChangeHandler;
            m_spotifyLocalAPI.OnTrackTimeChange += OnTrackTimeChangeHandler;
            m_spotifyLocalAPI.OnVolumeChange += OnVolumeChangeHandler;
        }
        /// <summary>
        /// permet d obtenir l instance unique(Singleton)
        /// </summary>
        /// <returns>SpotifyManager</returns>
        public static SpotifyManager GetInstance()
        {
            if (m_instance == null)
                m_instance = new SpotifyManager();
            return m_instance;
        }

        /// <summary>
        /// fait l authentification pour Spotify, retourne True si l operation reussi 
        /// et false sinon
        /// </summary>
        /// <returns>Task<bool></returns>
        public override async Task<bool> RunAuthentication()
        {
            WebAPIFactory webApiFactory = new WebAPIFactory(
                "http://localhost",
                7000,
                "8554a963221c499fa356f8b4a95e79f8",
                Scope.UserReadPrivate | Scope.UserReadEmail | Scope.PlaylistReadPrivate | Scope.UserLibraryRead |
                Scope.UserReadPrivate | Scope.UserFollowRead | Scope.UserReadBirthdate | Scope.UserTopRead | Scope.PlaylistReadCollaborative |
                Scope.UserReadRecentlyPlayed | Scope.UserReadPlaybackState | Scope.UserModifyPlaybackState);

            try
            {
                m_spotifyWebAPI = await webApiFactory.GetWebApi();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (m_spotifyWebAPI == null)
                return false;
            return true;
        }


        /// <summary>
        /// permet de se connecter a Spotify, retourne True si l operation reussi et
        /// false sinon
        /// </summary>
        /// <returns>bool</returns>
        public override bool ConnectClient()
        {
            if (!SpotifyLocalAPI.IsSpotifyRunning() || !SpotifyLocalAPI.IsSpotifyWebHelperRunning())
            {
                return false;
            }
            bool successful;
            try
            {
                successful = m_spotifyLocalAPI.Connect();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (successful)
            {
                m_spotifyLocalAPI.ListenForEvents = true;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Permet de chercher une piste par mot cle et retourne une liste
        /// des pistes trouvees
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns>Task<List<LocalTrack>></returns>
        public override async Task<List<LocalTrack>> SearchTrack(string keyWord)
        {
            SearchItem search = await m_spotifyWebAPI.SearchItemsAsync(keyWord, SearchType.Track);
            return new List<LocalTrack>(search.Tracks.Items.Select(t => ToLocalTrack(t)).ToList());
        }

        private LocalTrack ToLocalTrack(FullTrack fullTrack)
        {
            LocalTrack track = new LocalTrack();
            track.Name = fullTrack.Name;
            track.Album = fullTrack.Album.Name;
            track.Artist = fullTrack.Artists.FirstOrDefault().Name;
            track.Id = fullTrack.ExternUrls["spotify"];
            track.Duration = (fullTrack.DurationMs / (10 * 60)) / 100.0;
            track.Type = StreamingSystemType.Spotify;
            track.Image = fullTrack.Album.Images.Count > 0 ? fullTrack.Album.Images[0].Url : null;
            return track;
        }

        /// <summary>
        /// permet d obtenir une reference vers la piste jouee actuellement
        /// </summary>
        /// <returns>LocalTrack</returns>
        public override LocalTrack GetCurrentTrack()
        {
            LocalTrack track = new LocalTrack();

            StatusResponse status = m_spotifyLocalAPI.GetStatus();
            if (status != null && status.Track != null && !status.Track.IsAd())
            {
                track.Duration = (int)status.Track.Length / 60.0;
                track.Image = status.Track.GetAlbumArtUrl(AlbumArtSize.Size160);
                track.Name = status.Track.TrackResource?.Name;
                track.Artist = status.Track.ArtistResource?.Name;
                track.Album = status.Track.AlbumResource?.Name;
                track.Type = StreamingSystemType.Spotify;
            }
            return track;
        }

        /// <summary>
        /// permet de jouer une jouer une piste de Spotify
        /// </summary>
        /// <param name="track"></param>
        /// <returns></returns>
        public override async Task Play(LocalTrack track)
        {
            await m_spotifyLocalAPI.PlayURL(track.Id);
        }


        /// <summary>
        /// permet de mettre en pause la lecture d une piste
        /// </summary>
        /// <returns></returns>
        public override async Task Pause()
        {
            await m_spotifyLocalAPI.Pause();
        }



        private void OnTrackChangeHandler(object sender, TrackChangeEventArgs e)
        {
            m_currentTrack = e.NewTrack;
            OnTrackChangeHandler();
        }

        private void OnVolumeChangeHandler(object sender, VolumeChangeEventArgs e)
        {
            OnVolumeChangeHandler();
        }

        private void OnPlayStateChangeHandler(object sender, PlayStateEventArgs e)
        {
            OnPlayStateChangeHandler(e.Playing);
        }

        private void OnTrackTimeChangeHandler(object sender, TrackTimeChangeEventArgs e)
        {
            OnTrackTimeChangeHandler((int)e.TrackTime);
        }
    }
}
