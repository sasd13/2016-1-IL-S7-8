using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI2016.Dev.Tests
{
    [TestFixture]
    public class CarStatusTests
    {
        [Test]
        public void CarStatus_basic_flags()
        {
            CarStatus s = CarStatus.IsRunning | CarStatus.IsBrakePedal;

            Assert.That( (s & CarStatus.IsRunning) != 0, "The Car is running!" );
            Assert.That( (s & CarStatus.IsClutchPedal) == 0, "The Clutch pedal is NOT pressed!" );

            // Now we want to stop the car...
            s = s & ~CarStatus.IsRunning;
            Assert.That( (s & CarStatus.IsRunning) == 0, "The Car is NO MORE running!" );
        }

        [Test]
        public void CarStatus_gear_management()
        {
            // Speed n°1
            CarStatus s = CarStatus.IsRunning | (CarStatus)(1 << 5);
            Assert.That( s.GetGear(), Is.EqualTo( 1 ) );
            // Speed n°3
            s = (s & ~CarStatus.GearMask) | (s | (CarStatus)(3 << 5));

            s = s.SetGear( 3 );

            s = CarStatusExtension.SetGear( s, 4 );
        }



    }
}
