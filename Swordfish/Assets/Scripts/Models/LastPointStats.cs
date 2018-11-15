using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPointStats
{
    private static LastPointStats instance;

    public int Fish;
    public int Squid;
    public int Torpedo;
    public int TorpedoKill;
    public int Time;
    public int Total;

    public static LastPointStats Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new LastPointStats();
            }

            return instance;
        }
    }

    private LastPointStats() { }
}
