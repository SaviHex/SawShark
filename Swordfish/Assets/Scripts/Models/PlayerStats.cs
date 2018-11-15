using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class PlayerStats
    {
        public int TotalPoints { get; set; }
        public int TotalFish { get; set; }
        public int TotalSquid { get; set; }
        public int TotalTorpedo { get; set; }
        public int TotalTorpedoKill { get; set; }
        public int RecordTime { get; set; }
        public int DeathCounter { get; set; }
    }
}
