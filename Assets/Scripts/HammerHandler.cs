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

        if (collider.gameObject.tag == DIGLETT_TAG)
            score.incrementScore();
    }
}
