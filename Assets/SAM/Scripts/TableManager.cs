using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public bool seatAvailable = true;



    public bool SeatAvailable()
    {
        return seatAvailable;
    }

    public void OccupySeat()
    {
        seatAvailable = false;
    }

    public void FreeSeat()
    {
        seatAvailable = true;
    }
}
