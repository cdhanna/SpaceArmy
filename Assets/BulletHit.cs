using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    void OnHit();
}

public class BulletHit : MonoBehaviour {

    public List<GameObject> Exclusions = new List<GameObject>();
    private bool Used = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        

	}

    void OnTriggerEnter(Collider other)
    {
        
        var b = (other.gameObject.layer & LayerMask.NameToLayer("Destroyable")) == LayerMask.NameToLayer("Destroyable");
        if (b && Used == false)
        {
            if (Exclusions.Contains(other.gameObject) == false)
            {
                //Debug.Log("Got trigger " + other.gameObject.name);
                var hit = other.gameObject.GetComponent<IHittable>();
                hit.OnHit();
                Destroy(gameObject);
                Used = true;
            }

        }
        //other.gameObject.layer

    }
    
}
