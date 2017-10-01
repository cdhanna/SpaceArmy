using Assets;
using Assets.Behavours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public SimpleShip ShipPrefab; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        if (Input.GetButtonDown("FleetA"))
        {
            for (var i = 0; i < 3; i++)
            {

                var ship = Instantiate(ShipPrefab, transform);
                ship.transform.localPosition = new Vector3(14 + i, 7 - i);
                ship.SetBehavour(() => new IBehavour[] {
                        Move.Left(ship),
                        new ShootWeapon(ship, 2),
                        Move.Down(ship),
                        Move.Down(ship),
                        Move.Down(ship),
                        Move.Down(ship),
                        new ShootWeapon(ship, 2),
                        Move.Up(ship),
                        Move.Up(ship),
                        Move.Up(ship),
                        Move.Up(ship),
                        Move.Right(ship)
                    });
            }
        }

	}
}
