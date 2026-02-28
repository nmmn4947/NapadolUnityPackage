using System;
using UnityEngine;

public class ScaleAction : Napadol.Tools.ActionPattern.Action
{
    private Vector3 finalScale;
    private Transform subjectTransform;
    private Vector3 originalScale;

    public ScaleAction(GameObject subject, Vector3 finalScale, float duration)
    {
        this.finalScale = finalScale;
        this.duration = duration;
        subjectTransform = subject.transform;
    }

    #region Builders

    #endregion
    
    public ScaleAction(GameObject subject, bool blocking, float delay, Vector2 finalScale, float duration, Func<float ,float> easingFunc) : base(subject, blocking, delay, duration, easingFunc)
    {
        this.finalScale = finalScale;
        this.duration = duration;
        subjectTransform = subject.transform;
        originalScale = subjectTransform.localScale;
        actionName = "Scale";
    }

    protected override void RunOnceBeforeUpdate()
    {
        originalScale = subjectTransform.localScale;
    }
    
    protected override bool UpdateLogicUntilDone(float dt)
    {
        return ScaleUntilFinalScale();
    }

    

    //EaseOutExpo
    private bool ScaleUntilFinalScale()
    {
        subjectTransform.localScale = new Vector3(Mathf.Lerp(originalScale.x, finalScale.x, easingTimePasses),
                                                  Mathf.Lerp(originalScale.y, finalScale.y, easingTimePasses), 
                                                  Mathf.Lerp(originalScale.z, finalScale.z, easingTimePasses));
        
        // Snap to final scale when very close or time is up
        if (percentageDone >= 1.0f || easingTimePasses >= 0.999f)
        {
            subjectTransform.localScale = new Vector3(finalScale.x, finalScale.y, finalScale.z);
        }

        return percentageDone >= 1.0f;
    }
}
