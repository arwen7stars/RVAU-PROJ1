using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackableHandler : DefaultTrackableEventHandler
{
    // checks if target was found for the first time
    private bool found = false;

    // checks if target is being rendered
    private bool rendering = false;

    // if image target is found
    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();

        rendering = true;

        if (!found)
        {
            found = true;
        }
    }

    // if image target is lost
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();

        rendering = false;
    }

    public bool GetFound()
    {
        return found;
    }

    // get rendering bool variable
    public bool GetRendering()
    {
        return rendering;
    }
}
