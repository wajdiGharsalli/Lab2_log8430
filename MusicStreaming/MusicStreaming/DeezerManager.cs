using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E.Deezer;         //Main Deezer client + API access classes
using E.Deezer.Api;    //The Deezer API Objects (Artist, Album, ... etc)

namespace MusicStreaming
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

        public static DeezerManager GetInstance()
        {
            if (m_instance == null)
                m_instance = new DeezerManager();
            return m_instance;
        }

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
            track.Duration = deezerTrack.Duration;
            track.Type = StreamingSystemType.Deezer;
            track.Image = deezerTrack.Album.GetPicture(PictureSize.Medium);
            return track;
        }

        public override LocalTrack GetCurrentTrack()
        {
            return new LocalTrack();
        }

        public override async Task Play(LocalTrack track)
        {
            await Task.Run(() => { });
        }

        public override async Task Pause()
        {
            await Task.Run(() => { });
        }
    }
}
