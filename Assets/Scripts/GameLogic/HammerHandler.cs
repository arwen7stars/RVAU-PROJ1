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

    // trackable handler
    public GroundTrackableHandler trackableHandler;

    // menu
    public MenuManager menu;

    // to check if game is over
    public TimerManager timer;

    // diglett tag
    private const string DIGLETT_TAG = "Diglett";

    // if there is a collision with the hammer's collider
    void OnTriggerEnter(Collider collider)
    {
        // if platform isn't being rendered or menu is being shown, ignore collison
        if (!trackableHandler.GetRendering() || menu.GetStopGame() || timer.GetGameOver())
        {
            return;
        }

        if (collider.gameObject.tag == DIGLETT_TAG)
        {
            collider.gameObject.GetComponent<Diglet>().Hit();
            GetComponent<AudioSource>().Play();
            score.incrementScore();
        }
    }
}
