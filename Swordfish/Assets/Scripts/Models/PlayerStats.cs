using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Models
{
    public class PlayerStats
    {
        private static PlayerStats instance;

        public int TotalPoints;
        public int TotalFish;
        public int TotalSquid;
        public int TotalTorpedo;
        public int RecordTime;
        public int DeathCounter;

        public static PlayerStats Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new PlayerStats();

                }

                return instance;
            }
            set
            {
                instance = value;
            }
        }

        private PlayerStats() { }
    }
}
