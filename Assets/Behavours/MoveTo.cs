using Assets.ShipAbilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Behavours
{

    class Move : IBehavour
    {

        public static Move Left(IMovingShip ship)
        {
            return new Move(ship,  -new Vector2(1, 0));
        }

        public static Move Right(IMovingShip ship)
        {
            return new Move(ship,  new Vector2(1, 0));
        }

        public static Move Up(IMovingShip ship)
        {
            return new Move(ship, new Vector2(0, 1));
        }

        public static Move Down(IMovingShip ship)
        {
            return new Move(ship, -new Vector2(0, 1));
        }
        public IMovingShip Ship { get; set; }
        public Vector2 Diff { get; set; }

        public Move(IMovingShip ship, Vector2 diff)
        {
            Ship = ship;
            Diff = diff;
        }

        public IEnumerable<BehavourStatus> Behave()
        {
            var moveTo = new MoveTo(Ship, Ship.Position + Diff);
            return moveTo.Behave();
        }
    }

    class MoveTo : IBehavour
    {
        public IMovingShip Ship { get; set; }
        public Vector2 Target { get; set; }

        private float startTime;

        public MoveTo(IMovingShip ship, Vector2 position)
        {
            Ship = ship;
            Target = position;
        }

        public IEnumerable<BehavourStatus> Behave()
        {
            var time = 0f;

            var a = 2;
            var startPosition = Ship.Position;
            var diff = (Target - Ship.Position);
            var duration = diff.magnitude / 6;
            while (true)
            {
                time += Time.deltaTime;
                var ratio = Math.Min(1, time / duration);

                var xa = Math.Pow(ratio, a);
                var output = xa / (xa + Math.Pow(1 - xa, a));

                Ship.Position = startPosition + diff * (float)output;

                if (ratio >= 1f)
                {
                    yield return BehavourStatus.DONE;
                }

                yield return BehavourStatus.WORKING;
            }
        }

       
    }
}
