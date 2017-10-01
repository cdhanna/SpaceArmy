using Assets.ShipAbilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Behavours
{
    class ShootWeapon : IBehavour
    {

        private int TimesToFire { get; set; }
        private IShootingShip Ship { get; set; }

        public ShootWeapon(IShootingShip ship, int timesToFire)
        {
            Ship = ship;
            TimesToFire = timesToFire;
        }

        public IEnumerable<BehavourStatus> Behave()
        {

            do
            {
                if (Ship.CanShoot())
                {
                    Ship.Shoot();
                    TimesToFire -= 1;
                }
                yield return BehavourStatus.WORKING;

            } while (TimesToFire > 0);
            yield return BehavourStatus.DONE;
        }
        
    }
}
