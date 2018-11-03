using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class GroundTrackableHandler : DefaultTrackableEventHandler
{
    // checks if game platform is being rendered
    private bool rendering = false;

    // if game platform's image target is found
    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();

        rendering = true;
    }

    // if game platform's image target is lost
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();

        rendering = false;
    }

    // get rendering bool variable
    public bool GetRendering()
    {
        return rendering;
    }
}
