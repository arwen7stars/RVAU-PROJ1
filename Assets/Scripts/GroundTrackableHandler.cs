using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class GroundTrackableHandler : DefaultTrackableEventHandler
{
    public MoleMovement moleMovement;

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();

        moleMovement.SetRenderingStarted(true);

        if (moleMovement.GetRenderingStopped())
        {
            moleMovement.SetRenderingStopped(false);
        }
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();

        moleMovement.SetRenderingStopped(true);
    }
}
