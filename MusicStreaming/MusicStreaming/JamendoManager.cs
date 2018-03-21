using JamendoApi;
using JamendoApi.ApiCalls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JamendoApi.ApiEntities;
using JamendoApi.ApiCalls.Tracks;
using JamendoApi.ApiEntities.Tracks;
using JamendoApi.ApiCalls.Parameters;
using MusicStreaming;

namespace ApiManagers
{
    public class JamendoManager : StreamingSystemManager
    {
        JamendoApiClient m_jamedoApiClient;

        private static JamendoManager m_instance;

        /// <summary>
        /// Private constructor to have a singleton
        /// </summary>
        private JamendoManager()
        {
            m_jamedoApiClient = new JamendoApiClient("bc2eac68");
        }

        /// <summary>
        /// Allow to get an instance to the current instance
        /// </summary>
        /// <returns></returns>
        public static JamendoManager GetInstance()
        {
            if (m_instance == null)
                m_instance = new JamendoManager();
            return m_instance;
        }

        /// <summary>
        /// Establish authentification to the 
        /// </summary>
        /// <returns></returns>
        public override async Task<bool> RunAuthentication()
        {
            if (m_jamedoApiClient == null)
            {
                await Task.Run(() => m_jamedoApiClient = new JamendoApiClient("bc2eac68"));
            }

            if (m_jamedoApiClient == null)
                return false;
            return true;
        }

        public override bool ConnectClient()
        {
            return true;
        }

        public override async Task<List<LocalTrack>> SearchTrack(string keyWord)
        {
            TracksCall call = new TracksCall();
            call.Name = new NameParameter( keyWord);
            BasicTrack[] search = (await m_jamedoApiClient.CallAsync(call)).Results;
            return new List<LocalTrack>(search.Select(t => ToLocalTrack(t)).ToList());
        }

        private LocalTrack ToLocalTrack(BasicTrack basicTrack)
        {
            LocalTrack track = new LocalTrack();
            track.Name = basicTrack.Name;
            track.Album = basicTrack.AlbumName;
            track.Artist = basicTrack.ArtistName;
            track.Id = basicTrack.Id.ToString();
            track.Duration = (basicTrack.Duration * 10 / 6) / 100.0;
            track.Type = StreamingSystemType.Jamendo;
            track.Image = basicTrack.Image;
            return track;
        }

        public override LocalTrack GetCurrentTrack()
        {
            return null;
        }

        public override async Task Play(LocalTrack track)
        {
        }

        public override async Task Pause()
        {
        }
    }
}
