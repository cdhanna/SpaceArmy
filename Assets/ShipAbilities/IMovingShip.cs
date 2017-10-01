using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.ShipAbilities
{
    interface IMovingShip
    {
        Vector2 Thrusters { get; set; }

        Vector2 Velocity { get; }
        Vector2 Position { get; set; }
        float MaxVelocity { get; }
    }
}
