using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerHandler : MonoBehaviour {
    public BoxCollider hammerCollider;
    public BoxCollider[] moleColliders;

    public ScoreManager score;

    private const string DIGLETT_TAG = "Diglett";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log(collider.gameObject.tag);

        Vector3 direction = collider.gameObject.transform.position - transform.position;
        if (Vector3.Dot(transform.forward, direction) > 0)
        {
            print("Back");
        }
        if (Vector3.Dot(transform.forward, direction) < 0)
        {
            print("Front");
        }
        if (Vector3.Dot(transform.forward, direction) == 0)
        {
            print("Side");
        }

        if (collider.gameObject.tag == DIGLETT_TAG)
            score.incrementScore();
    }
}
