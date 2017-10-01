using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRowControl : MonoBehaviour {


    public int ActionCount;
    public GameObject TimeSlots;
    public GameObject ActionPrefab;

    private Dictionary<int, Vector3> actionSlotToPosition = new Dictionary<int, Vector3>();

	// Use this for initialization
	void Start () {

        //SetActions();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3 GetActionSlotPosition(int col)
    {
        return actionSlotToPosition[col];
    }

    public float SetActions()
    {
        actionSlotToPosition.Clear();

        // delete all children in timeslots
        for (var i = 0; i < TimeSlots.transform.childCount; i++)
        {
            Destroy(TimeSlots.transform.GetChild(i).gameObject);
        }

        // add actions
        for (var i = 0; i < ActionCount; i++)
        {
            var action = Instantiate(ActionPrefab, TimeSlots.transform);
            action.transform.localPosition += new Vector3(i * (action.transform.lossyScale.x + .1f), 0);
            //var r = 25f;

            //var origin = new Vector2(0, -15f);
            //var next = new Vector2(
            //    action.transform.localPosition.x, 
            //    -r + Mathf.Sqrt( (r*r) - Mathf.Pow(action.transform.localPosition.x - origin.x, 2))
            //    );


            //action.transform.localPosition += new Vector3(0, 0, next.y);
            //action.transform.rotation = (Quaternion.LookRotation(new Vector3(-5.74f, 5.14f, -15.4f), Vector3.up));
            //action.transform.Rotate(Vector3.up, Mathf.Rad2Deg * -Mathf.Atan2(next.y, next.x) );

            actionSlotToPosition.Add(i, action.transform.position);
        }

        return (ActionCount - 1) * (ActionPrefab.transform.lossyScale.x + .1f);
    }
}
