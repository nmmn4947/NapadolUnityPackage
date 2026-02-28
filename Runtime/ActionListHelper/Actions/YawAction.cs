using UnityEngine;

public class YawAction : Napadol.Tools.ActionPattern.Action
{
    private Transform subjectTransform;
    private float angleCalculation;
    private int rightMultiplier = 1;
    private Quaternion startingAngle;
    
    public YawAction(GameObject subject, float goalAngle, float duration) : base(subject, duration)
    {
        subjectTransform = subject.transform;
        angleCalculation = goalAngle;
    }

    #region Builders

    public YawAction RotateRight()
    {
        rightMultiplier = -1;
        return this;
    }
    public YawAction AddLoop(int loopCountMultiplier)
    {
        angleCalculation += (loopCountMultiplier * 360.0f);
        return this;
    }

    #endregion

    protected override void RunOnceBeforeUpdate()
    {
        startingAngle = subjectTransform.localRotation;
    }
    
    protected override bool UpdateLogicUntilDone(float dt)
    {
        float targetY = startingAngle.eulerAngles.y + (angleCalculation * rightMultiplier);

        Quaternion targetRotation = Quaternion.Euler(
            startingAngle.eulerAngles.x,
            targetY,
            startingAngle.eulerAngles.z
        );
        
        subjectTransform.localRotation = Quaternion.Slerp(startingAngle, targetRotation, easingTimePasses);
        return percentageDone >= 1f;
    }
}
