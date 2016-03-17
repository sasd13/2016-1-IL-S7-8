using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI2016.Dev
{
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
        None = 0,
        Gear1 = 1,
        Gear2 = 2,
        Gear3 = 3,
        Gear4 = 4,
        Gear5 = 5,
        Reverse = 6
    }

    public static class CarStatusExtension
    {
        public static CarGear GetGear(this CarGear c)
        {
            return CarGear.None;
        }

        public static CarGear SetGear(this CarGear c, int value)
        {
            return CarGear.None;
        }
    }
}

