using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MusicStreaming
{
    [Serializable]
    public class LocalPlaylist
    {
        public string Name { get; set; }
        [XmlElement("Track")]
        public ObservableCollection<LocalTrack> Tracks;
        [XmlIgnore]
        public int CurrentIndex { get; set; }

        public LocalPlaylist()
        {
            CurrentIndex = 0;
        }

        public LocalPlaylist(string file)
        {
            LocalPlaylist playList = Read(file);
            Name = playList.Name;
            Tracks = playList.Tracks;
            CurrentIndex = 0;
        }

        public static LocalPlaylist Read(string file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LocalPlaylist));
            LocalPlaylist playList = new LocalPlaylist();
            using (StreamReader reader = new StreamReader(file))
            {
                playList = (LocalPlaylist)serializer.Deserialize(reader);
            }
            return playList;
        }

        public void Save(string file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LocalPlaylist));
            using (StreamWriter writer = new StreamWriter(file))
            {
                serializer.Serialize(writer, this);
            }
        }

        public bool Add(LocalTrack track)
        {
            if (Tracks == null)
                Tracks = new ObservableCollection<LocalTrack>();
            if (Tracks.Select(t => t.Id).Contains(track.Id))
                return false;
            Tracks.Add(track);
            return true;
        }
    } 
}
