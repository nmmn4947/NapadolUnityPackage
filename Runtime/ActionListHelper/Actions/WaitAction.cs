using Napadol.Tools;
using UnityEngine;

public class WaitAction : Napadol.Tools.ActionPattern.Action
{
    public WaitAction(float duration) : base(true, 0.0f, duration)
    {
        actionName = "Wait";
        easingFunction = Easing.EaseLinear;
    }

    protected override bool UpdateLogicUntilDone(float dt)
    {
        if (timePasses > duration)
        {
            return true;
        }
        return false;
    }

    public override string GetDebugText()
    {
        string s = "";
        s += "Wait";
        s += " ";
        s += percentageDone.ToString("F2");
        s += "\n";
        return s;
    }
}
