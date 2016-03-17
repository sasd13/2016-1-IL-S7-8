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
        GearMask = 7 << 5
    }

    public enum CarGear
    {
        None,
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4,
        Fifth = 5,
        Reverse = 6
    }

    /// <summary>
    /// This class is never used for itself. It is used through the extension methods
    /// it defines.
    /// An extension method is a static method that MUST be in a static class.
    /// </summary>
    public static class CarStatusExtension
    {
        /// <summary>
        /// Gets the speed as a <see cref="CarGear"/>.
        /// </summary>
        /// <param name="s">This status.</param>
        /// <returns>The gear.</returns>
        public static CarGear GetGear( this CarStatus s )
        {
            return (CarGear)((int)s >> 5);
        }

        /// <summary>
        /// Sets a gear in a status (returning the new status).
        /// </summary>
        /// <param name="s"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        public static CarStatus SetGear( this CarStatus s, CarGear g )
        {
            return SetGear( s, (int)g );
        }

        /// <summary>
        /// Sets a gear in a status (returning the new status).
        /// </summary>
        /// <param name="s"></param>
        /// <param name="g"></param>
        /// <returns></returns>
        public static CarStatus SetGear( this CarStatus s, int gear )
        {
            return (s & ~CarStatus.GearMask) | (s | (CarStatus)(gear << 5));
        }

    }



}
