using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftObject : MonoBehaviour {

    public float DriftSpeed = .05f;
    public float LeftLimit = -15;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        var toKill = new List<Transform>();
        for (var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);


            child.localPosition = new Vector3(child.localPosition.x - DriftSpeed, child.localPosition.y, child.localPosition.z);


            if (child.localPosition.x < LeftLimit)
            {
                toKill.Add(child);
            }

        }
        toKill.ForEach(t => Destroy(t.gameObject));

	}
}
