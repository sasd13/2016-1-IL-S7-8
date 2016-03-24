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
        IsRunning = 1,                  //00000001
        IsCluctchPedal = 1 << 1,        //00000010
        IsGasPedal = 1 << 2,            //00000100
        IsBrakePedal = 1 << 3,          //00001000
        IsHandBrake = 1 << 4,           //00010000
        GearMask = 7 << 5               //11100000  //000 : Gear none (Vitesse nulle)
                                                    //001 : Gear 1 (Vitesse 1)
                                                    //010 : Gear 2 (Vitesse 2)
                                                    //011 : Gear 3 (Vitesse 3)
                                                    //100 : Gear 4 (Vitesse 4)
                                                    //101 : Gear 5 (Vitesse 5)
                                                    //110 : Reverse (Marche arriere)
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
