using SpotifyAPI.Local;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace MusicStreaming
{
    public class LocalTrack
    {
        /// <summary>
        /// liste des properties (vocabulaire du langage C#)
        /// </summary>
        public string Id { get; set; }
        public string Name { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        [XmlIgnore]
        public string Image { get; set; }
        public double Duration { get; set; }
        public string DurationMin { get { return Duration + " Min"; } }
        public StreamingSystemType Type { get; set; }
    }
}
