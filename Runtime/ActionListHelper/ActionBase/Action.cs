using UnityEngine;
using System;
using System.Collections.Generic;

namespace Napadol.Tools.ActionPattern{
	public abstract class Action
	{
	// GENERAL ACTION VARIABLE
	public bool blocking;
	public float delay;
	public float timePasses = 0.0f;
	public float duration;
	public float percentageDone;
    private float clampTimePasses;
	public float easingTimePasses;
    public string actionName;
    protected Func<float, float> easingFunction;
    protected GameObject subject;
    private bool runEnterOnce = false;
    protected bool isDone = false; //for using in Derived UpdateLogicUntilDone
    protected bool isReverse = false;
    private bool isReversing = false;

    protected Action()
    {
        this.blocking = false;
        this.delay = 0f;
        this.actionName = GetType().Name;
        this.easingFunction = null;
    }
    
    protected Action(float duration)
    {
        this.duration = duration;
        this.blocking = false;
        this.delay = 0f;
        this.actionName = GetType().Name;
        this.easingFunction = null;
    }
    protected Action(GameObject subject, float duration)
    {
        this.subject = subject;
        this.duration = duration;
        this.blocking = false;
        this.delay = 0f;
        this.actionName = GetType().Name;
        this.easingFunction = null;
    }

    #region Builders
    public Action Block()
    {
        this.blocking = true;
        return this;
    }

    public Action Block(bool b)
    {
        this.blocking = b;
        return this;
    }

    public Action Delay(float delay)
    {
        this.delay = delay;
        return this;
    }

    public Action Easer(Func<float, float> easingFunction)
    {
        this.easingFunction = easingFunction;
        return this;
    }

    public Action Reverse()
    {
        this.isReverse = true;
        return this;
    }
    
    #endregion
    
    protected Action(bool blocking, float delay, float duration)
    {
        this.subject = null;
        this.blocking = blocking;
        this.delay = delay;
        this.duration = duration;
    }
    
    protected Action(GameObject subject, bool blocking, float delay, float duration, Func<float, float> easingFunction)
    {
        this.subject = subject;
        this.blocking = blocking;
        this.delay = delay;
        this.duration = duration;
        this.easingFunction = easingFunction;
    }

    static public void SynchronizeDurationFirstToSecond(Action action1, Action action2)
    {
        action2.duration = action1.duration;
    }

    public void SynchronizeDurationFromThisAction(Action action)
    {
        duration = action.duration;
    }
    
    protected abstract bool UpdateLogicUntilDone(float dt);
    
    public bool UpdateUntilDone(float dt)
    {
        if (RunDelayUntilDone(dt))
        {
            Enter(); // will not run after the first frame
            if (percentageDone < 1.0f)
            {
                timePasses += dt; //update timePasses
                percentageDone = Mathf.Clamp01(timePasses / duration);
                if (!isReversing)
                {
                    clampTimePasses = Mathf.Clamp01(timePasses / duration);
                }
                else
                {
                    clampTimePasses = Mathf.Clamp01(1 - (timePasses / duration));
                }
                easingTimePasses = easingFunction?.Invoke(clampTimePasses) ?? clampTimePasses;
                if (easingFunction == null)
                {
                    Debug.LogError("easingFunction is null : " + actionName);
                }
                
                //percentageDone = timePasses / duration; //Updating percentageDone 0 - 1.
                if (timePasses > duration)
                {
                    isDone = true;
                    percentageDone = 1.0f;
                }

                UpdateLogicUntilDone(dt);
                return false;
            }
            else
            {
                if (isReverse)
                {
                    timePasses = 0f;
                    isReverse = false;
                    isReversing = true;
                    return false;
                }
                else
                {
                    RunOnceAfterUpdate();
                    return true;
                }
            }
        }
        else
        {
            return false;
        }
    }

    protected virtual void RunOnceBeforeUpdate() { }
    protected virtual void RunOnceAfterUpdate() { }

    private void Enter()
    {
        if (!runEnterOnce)
        {
            RunOnceBeforeUpdate();
            runEnterOnce = true;
        }
    }
    
    private bool RunDelayUntilDone(float dt)
    {
        delay -= dt;
        if (delay <= 0.0f)
        {
            return true;
        }
        return false;
    }
    
    protected float GetTimeLeft()
    {
        if (duration >= float.MaxValue)
        {
            return float.MaxValue;
        }
        
        if (timePasses > duration)
        {
            return 0f;
        }
        else
        {
            return duration - timePasses;
        }
    }

    public virtual string GetDebugText()
    {
        string s = "";
        s += actionName;
        s += " [";
        if (subject != null)
        {
            s += subject.name;
        }
        else
        {
            s += "null";
        }
        s += "] ";
        s += percentageDone.ToString("F2");
        s += "\n";
        return s;
    }
}
}

