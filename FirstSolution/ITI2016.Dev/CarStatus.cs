using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI2016.Dev
{
    /*
    A Car can be in multiple states:
    - The engine is running or not.
    - There may be a speed engaged (typically 5 speeds)
    - The clutch pedal can be pressed or not.
    - The gas pedal can be pressed or not.
    - The brake pedal can be pressed or not.
    - The hand brake can be set or not.

    How do you (efficiently) encode this state?
    */
    public enum CarStatus : byte
    {
        IsRunning = 1,
        IsClutchPedal = 1 << 1,
        IsGasPedal = 1 << 2,
        IsBrakePedal = 1 << 3,
        IsHandBrake = 1 << 4,
        SpeedMask = 7 << 5
    }



}
