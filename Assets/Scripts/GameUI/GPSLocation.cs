using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GPSLocation : MonoBehaviour {

    // object with location text
    public GameObject locationObj;

    // location text UI
    public TextMeshProUGUI textLocation;

    // latitute
    private float latitude;

    // longitude
    private float longitude;

    // time between fetchs of gps location
    private const int WAITING_TIME = 3;

    // Use this for initialization
    void Start () {
        StartCoroutine(GetGPSLocation());
	}

    IEnumerator GetGPSLocation()
    {
        while (true)
        {
            // First, check if user has location service enabled
            if (!Input.location.isEnabledByUser)
                yield break;

            // Start service before querying location
            Input.location.Start();

            // Wait until service initializes
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            // Service didn't initialize in 20 seconds
            if (maxWait < 1)
            {
                Debug.Log("Timed out");
                yield break;
            }

            // Connection has failed
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                Debug.Log("Unable to determine device location");
                yield break;
            }
            else
            {
                locationObj.SetActive(true);
                latitude = Input.location.lastData.latitude;
                longitude = Input.location.lastData.longitude;
           
                string location = "Location: (" + latitude + ", " + longitude + ")";
                textLocation.text = location;

                yield return new WaitForSeconds(WAITING_TIME);
            }
        }
    }
}
