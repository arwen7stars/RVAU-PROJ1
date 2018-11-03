using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class GroundTrackableHandler : DefaultTrackableEventHandler
{
    private bool renderingStarted = false;
    private bool renderingStopped = true;

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();

        renderingStarted = true;

        if (renderingStopped)
        {
            renderingStopped = false;
        }
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();

        renderingStopped = true;
    }

    public bool GetRenderingStarted()
    {
        return renderingStarted;
    }

    public bool GetRenderingStopped()
    {
        return renderingStopped;
    }
}
