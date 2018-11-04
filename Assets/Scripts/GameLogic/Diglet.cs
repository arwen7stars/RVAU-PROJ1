using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diglet : MonoBehaviour {

	// > IdleBot > Ascending > IdleTop > Descending >
    private abstract class State
    {
        // the diglet instance
        protected Diglet diglet;

        // init (should be called by subclasses)
        protected State(Diglet diglet)
        {
            this.diglet = diglet;
        }

        // move to the next state
        public abstract State next();

        // update diglet
        public abstract State update();

    } // State


	private abstract class Idle : State
    {

        public Idle(Diglet diglet)
            : base(diglet)
        {
			diglet.currentSpeed = 0;
			diglet.currentUptime = 0;
        }

    } // Idle


	private class IdleBot : Idle
	{
		// time (second) the last chance check occurred
		private int lastCheck;

		// time (ms of s) diglet should ascend
		private int ascendTime;

		public IdleBot(Diglet diglet)
			: base(diglet)
		{
			lastCheck = -1;
			ascendTime = -1;
        }

		public override State next()
        {
            return new Ascending(diglet);
        }

		public override State update()
		{
			int currentSecond = (int) Time.time;

			// ascend time already calculated
			if (ascendTime >= 0) {
				// should ascend now
				if ((Time.time - currentSecond) * 1000 >= ascendTime) {
					return next();
				}
			}
			// ascend time still not calculated
			else if (currentSecond > lastCheck) {
				lastCheck = currentSecond;
				int chance = Random.Range(0, 100);

				// if diglet should ascend
				if (chance < diglet.currentChance) {
					// random position (ms) in this second to ascend
					ascendTime = Random.Range(0, 1000);
				}
			}

			return this;
		}

	} // IdleBot


	private class IdleTop : Idle
	{
		public IdleTop(Diglet diglet)
			: base(diglet)
		{
			diglet.currentUptime = 0;
			diglet.diglettCollider.enabled = true;
        }

        public override State next()
        {
			diglet.diglettCollider.enabled = false;
            diglet.hit = false;
			return new Descending(diglet);
        }

		public override State update()
		{
			diglet.currentUptime += (int) (Time.deltaTime * 1000);

			if (diglet.currentUptime >= diglet.UPTIME) {
				return next();
			} else {
				return this;
			}
		}

	} // IdleTop


	private class Ascending : State
    {

        public Ascending(Diglet diglet)
            : base(diglet)
        {
			diglet.currentSpeed = diglet.SPEED;
        }

        public override State next()
        {
			return new IdleTop(diglet);
        }

        public override State update()
        {
			float deltaY = diglet.currentSpeed * Time.deltaTime;
			float newY = diglet.transform.localPosition.y + deltaY;

			bool reachedTop = false;
			if (newY > diglet.MAX_Y) {
				deltaY -= newY - diglet.MAX_Y;
				reachedTop = true;
			}

			diglet.transform.localPosition = new Vector3(
				diglet.transform.localPosition.x,
				diglet.transform.localPosition.y + deltaY,
				diglet.transform.localPosition.z);

			if (reachedTop) {
				return next();
			} else {
				return this;
			}
		 }

    } // Ascending


	private class Descending : State
    {

        public Descending(Diglet diglet)
            : base(diglet)
        {
			diglet.currentSpeed = -1 * diglet.SPEED;
        }

        public override State next()
        {
            return new IdleBot(diglet);
        }

        public override State update()
        { 
			float deltaY = diglet.currentSpeed * Time.deltaTime;
			float newY = diglet.transform.localPosition.y + deltaY;

			bool reachedBot = false;
			if (newY < diglet.MIN_Y) {
				deltaY += diglet.MIN_Y - newY;
				reachedBot = true;
			}

			diglet.transform.localPosition = new Vector3(
				diglet.transform.localPosition.x,
				diglet.transform.localPosition.y + deltaY,
				diglet.transform.localPosition.z);

			if (reachedBot) {
				return next();
			} else {
				return this;
			}
		}

    } // Descending

	// y speed of diglet movement
	public float SPEED;                             

	// max y value for diglet
    public float MIN_Y;       

	// min y value for diglet                       
    public float MAX_Y;    

	// chance (0 - 100) diglet will ascend (per second)
	public int CHANCE;

    // if the game platform isn't being shown, don't update
    public TrackableHandler platform;

    // if the hammer isn't being shown, don't update
    public TrackableHandler hammer;

    // menu
    public MenuManager menu;

    // if game hasn't started, don't update
    public Tutorial gameStart;

    // to check if game is over
    public TimerManager timer;

    // diglett collider
    public BoxCollider diglettCollider;

    // time spent up (ms)
    private float UPTIME = StaticSettings.setUptime();

    // current state
    private State state;    

	// how long (ms) diglet has been up for
	private int currentUptime;

	// current diglet y speed;
	private float currentSpeed;

	// current chance of diglet ascending
	private float currentChance;

    // diglett has already been hit
    private bool hit = false;

	// Use this for initialization
	void Start () {
		
		diglettCollider.enabled = false;
		currentChance = CHANCE;
		state = new IdleBot(this);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // if game hasn't started or if game is over, ignore
        if (!gameStart.GetGameStart() || timer.GetGameOver()) return;

        if (!platform.GetRendering() || !hammer.GetRendering() || menu.GetStopGame()) return;
    

        //Debug.Log(UPTIME);
        state = state.update();
	}

	// hit the diglet
	public void Hit() {


        // this check shouldnt be needed
        // but just to be sure...
        if (state.GetType() == typeof(IdleTop)) {
            hit = true;
			state = state.next();
		}
	}

    public void SetUptime(float uptime)
    {
        this.UPTIME = uptime;
    }

    public bool isHit()
    {
        return hit;
    }
}
