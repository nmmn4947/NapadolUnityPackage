using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Napadol.Tools;

public class ActionListManager : MonoBehaviour
{
    public ActionList actionList;
    [HideInInspector]
    public float timeMultiplier = 1;
    protected float averageLerpTime = 0.5f;
    
    private void Awake()
    {
        actionList = new ActionList();
    }

    protected virtual void Update()
    {
        actionList.RunActions(Time.deltaTime * timeMultiplier);
    }

    public void LerpTimeMultiplier(float targetTime)
    {
        StartCoroutine(LerpingMultiplier(targetTime));
    }

    IEnumerator LerpingMultiplier(float targetTime)
    {
        float timer = 0;
        float originalTime = timeMultiplier;
        while (averageLerpTime >= timer)
        {
            timer += Time.deltaTime; // will not be effected by other things
            float t = Mathf.Clamp01(timer / averageLerpTime);
            
            timeMultiplier = Mathf.Lerp(originalTime, targetTime, Easing.EaseOutCirc(t));
            yield return null;
        }
    }
}

