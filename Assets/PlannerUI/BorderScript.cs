using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // drop border
        //transform.Find("Border/LowerLeftCorner").localPosition -= new Vector3(0, (ShipCount - 1) * (Rows.transform.lossyScale.y + .5f));
        //transform.Find("Border/LowerRightCorner").localPosition -= new Vector3(0, (ShipCount - 1) * (Rows.transform.lossyScale.y + .5f));
        //transform.Find("Border/LowerRightCorner").localPosition += new Vector3(right, 0);
        //transform.Find("Border/TopRightCorner").localPosition += new Vector3(right, 0);

        var right = 0f;
        var top = 0f;
        var bottom = float.MaxValue;
        var left = float.MaxValue;

        var childrenQueue = new Queue<Transform>();
        var enqueueChildren = new Action<Transform>(t =>
        {
            //foreach(var borderExtender in t.GetComponentsInChildren<BorderExtender>())
            //{
            //    childrenQueue.Enqueue(borderExtender.transform);
            //}
            for (var i = 0; i < t.childCount; i++)
            {
                //var child = t.GetChild(t);
                //t.GetComponentsInChildren<BorderExtender>();
                childrenQueue.Enqueue(t.GetChild(i));
            }
        });

        enqueueChildren(transform);

        while (childrenQueue.Count > 0)
        {
            var child = childrenQueue.Dequeue();
            enqueueChildren(child);
            if (child.GetComponent<BorderExtender>() != null)
            {
                right = Math.Max(right, child.position.x + child.lossyScale.x/2);
                top = Math.Max(top, child.position.y + child.lossyScale.y/2);
                bottom = Math.Min(bottom, child.position.y - child.lossyScale.y/2);
                left = Math.Min(left, child.position.x - child.lossyScale.x/2f);
            }
        }

        transform.Find("Border/TopRightCorner").position = new Vector3(right + .5f, top + 1);
        transform.Find("Border/LowerRightCorner").position = new Vector3(right + .5f, bottom - .5f);
        transform.Find("Border/LowerLeftCorner").position = new Vector3(left - 1, bottom - .5f);
        transform.Find("Border/TopLeftCorner").position = new Vector3(left - 1, top + 1);

    }
}
