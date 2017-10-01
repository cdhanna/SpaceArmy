using Assets.Behavours;
using Assets.ShipAbilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class SimpleShip : MonoBehaviour,  IShootingShip, IMovingShip, IHittable
    {
        public GameObject BulletPrefab;

        private float fireRate = 1; // 1 second
        public float nextFireTime = 0;
        public Vector2 velocity = new Vector2();

        private IEnumerator<BehavourStatus> iterator;

        public Vector2 Thrusters { get; set; }

        public Vector2 Velocity { get { return new Vector2(velocity.x, velocity.y); } }

        private Vector2 position;
        private Vector2 positionChange;
        public Vector2 Position
        {
            get { return position; }
            set
            {
                positionChange += (value - position);
                position = value;
            }
        }

        public int Health = 100;
        //public Vector2 Position { get { return new Vector2(transform.localPosition.x, transform.localPosition.y); }
        //set
        //    {
        //        transform.localPosition = new Vector3(value.x, value.y, 0);
        //    }
        //}

        public float MaxVelocity { get { return .5f; } }

        public List<IBehavour> Behavours = new List<IBehavour>();
        private IBehavour activeBehavour;
        private Func<IBehavour[]> behavourGenerator;
        private bool loopBehavour = false;

        public void SetBehavour(Func<IBehavour[]> behavourGeneratorFunction, bool loop=true)
        {
            activeBehavour = null;
            behavourGenerator = behavourGeneratorFunction;
            loopBehavour = loop;
            Behavours = behavourGeneratorFunction().ToList();
        }

        public void Start()
        {
            //Behavours = new IBehavour[]
            //{
            //    new ShootWeapon(this, 2),
            //    new MoveTo(this, new Vector2(0,0)),
            //    Move.Up(this),
            //    Move.Up(this),
            //    Move.Up(this),
            //    Move.Right(this),
            //    Move.Right(this),
            //    Move.Right(this),
            //    Move.Down(this),
            //    Move.Down(this),
            //    Move.Down(this),
            //    Move.Down(this),
            //    Move.Down(this),
            //    Move.Down(this),
            //    Move.Right(this),
            //    Move.Right(this),
            //    Move.Up(this),
            //    Move.Up(this),
            //    Move.Up(this),
            //    Move.Left(this),
            //    Move.Left(this),
            //    Move.Left(this),
            //    Move.Left(this),
            //    new ShootWeapon(this, 2),
            //    Move.Right(this),
            //    Move.Right(this),
            //    Move.Up(this),
            //    Move.Up(this),
            //    Move.Up(this),
            //    Move.Up(this),
            //    Move.Left(this),
            //    Move.Left(this),
            //    Move.Down(this),
            //    Move.Down(this),
            //}.ToList();
        }

        public void Update()
        {
            nextFireTime -= Time.deltaTime;
            transform.localPosition = new Vector3(transform.localPosition.x + positionChange.x, transform.localPosition.y + positionChange.y, 0);
            positionChange = Vector2.zero;
            //transform.localPosition = new Vector3(Position.x, Position.y, 0);
            

            if (iterator != null && iterator.MoveNext())
            {
                var result = iterator.Current;
                if (result == BehavourStatus.DONE)
                {
                    //Debug.Log("Done with behavour " + activeBehavour.GetType().Name);
                    iterator = null;
                }
            } else if (Behavours.Count > 0)
            {
                activeBehavour = Behavours[0];
                Behavours.RemoveAt(0);
                iterator = activeBehavour.Behave().GetEnumerator();
                //Debug.Log("Starting behavour " + activeBehavour.GetType().Name);
            } else if (Behavours.Count == 0 && loopBehavour == true)
            {
                Behavours = behavourGenerator().ToList();
            }

            if (Health <= 0)
            {
                Destroy(gameObject);
            }
            //// max out thursters
            //if (Velocity.magnitude > MaxVelocity)
            //{
            //    velocity = velocity.normalized * MaxVelocity;
            //}


            //velocity += Thrusters;
            //transform.localPosition = new Vector3(transform.position.x, transform.position.y, 0) + new Vector3(velocity.x, velocity.y, 0);
        }

        public bool CanShoot()
        {
            return nextFireTime < 0;
        }

        public void Shoot()
        {

            if (!CanShoot())
            {
                return;
            }

            //Debug.Log("Ship fire!");
            nextFireTime = fireRate;

            var b = GameObject.Instantiate(BulletPrefab, transform.parent);
            b.transform.localPosition = transform.localPosition + new Vector3(-.5f, 0, 0);
            b.GetComponent<BulletMovement>().Velocity = new Vector2(-2, 0) * .1f;
            b.GetComponent<BulletHit>().Exclusions.Add(gameObject);
            //var bullet = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //bullet.transform.parent = transform.parent;
            //bullet.transform.localPosition = transform.localPosition;
            //bullet.transform.localScale = new Vector3(1, 1, 1) * .1f;
        }

        public void OnHit()
        {
            Health -= 100;
            //throw new NotImplementedException();
        }
    }
}
