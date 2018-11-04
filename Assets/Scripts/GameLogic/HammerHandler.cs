using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerHandler : MonoBehaviour {

    // hammer collider
    public BoxCollider hammerCollider;

    // digletts' colliders
    public BoxCollider[] moleColliders;

    // score manager
    public ScoreManager score;

    // if the game platform isn't being shown, don't update
    public TrackableHandler platform;

    // if hammer isn't being shown, don't update
    public TrackableHandler hammer;

    // menu
    public MenuManager menu;

    // if game hasn't started, don't update
    public Tutorial gameStart;

    // to check if game is over
    public TimerManager timer;

    // diglett tag
    private const string DIGLETT_TAG = "Diglett";

    // if there is a collision with the hammer's collider
    void OnTriggerEnter(Collider collider)
    {
        // if game hasn't started or if game is over, ignore
        if (!gameStart.GetGameStart() || timer.GetGameOver()) return;
        
        // if platform isn't being rendered or menu is being shown, ignore collison
        if (!platform.GetRendering() || !hammer.GetRendering() || menu.GetStopGame()) return;

        Vector3 direction = collider.gameObject.transform.position - transform.position;
        if (Vector3.Dot(transform.forward, direction) > 0)
        {
            Debug.Log("Back");
        }
        if (Vector3.Dot(transform.forward, direction) < 0)
        {
            Debug.Log("Front");
        }
        if (Vector3.Dot(transform.forward, direction) == 0)
        {
            Debug.Log("Side");
        }

        if (collider.gameObject.tag == DIGLETT_TAG)
        {
            collider.gameObject.GetComponent<Diglet>().Hit();
            collider.gameObject.GetComponent<BoxCollider>().enabled = false;

            GetComponent<AudioSource>().Play();
            score.incrementScore();
        }
    }
}
