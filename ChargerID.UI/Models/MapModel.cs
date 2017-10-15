using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChargerID.UI.Models
{
    public class MapModel
    {
        public List<MapData> MapDataList { get; set; }
    }

    public class MapData
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int Stations { get; set; }
        public int Ports { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}