using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerHandler : MonoBehaviour {
    public BoxCollider hammerCollider;
    public BoxCollider[] moleColliders;

    // score manager
    public ScoreManager score;

    // trackable handler
    public GroundTrackableHandler trackableHandler;

    // menu
    public MenuManager menu;

    private const string DIGLETT_TAG = "Diglett";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if (!trackableHandler.GetRenderingStarted() || trackableHandler.GetRenderingStopped() || menu.GetStopGame())
            return;

        if (collider.gameObject.tag == DIGLETT_TAG)
        {
            collider.gameObject.GetComponent<Diglet>().Hit();
            GetComponent<AudioSource>().Play();
            score.incrementScore();
        }
    }
}
