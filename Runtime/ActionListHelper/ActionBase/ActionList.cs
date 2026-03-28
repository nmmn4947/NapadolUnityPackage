using System.Collections.Generic;
using System;
using UnityEngine;
using Napadol.Tools.ActionPattern;
using Action = Napadol.Tools.ActionPattern.Action;

public class ActionList
{
    private List<Action> actions = new List<Action>();
	private Dictionary<Type, Action> uniques = new Dictionary<Type, Action>();
    
    public void RunActions(float dt)
    {
        List<int> indexesToKill = new List<int>();
        for(int i = 0; i < actions.Count; i++)
        {
            if (actions[i].UpdateUntilDone(dt))
            {
				if (actions[i].unique)
                {
                    uniques.Remove(actions[i].GetType());
                }
                indexesToKill.Add(i);
            }

            if (actions[i]==null)
            {
                Debug.Log("null action");
                continue;
            }
            if (actions[i].blocking)
            {
                break;
            }
        }

        for (int i = indexesToKill.Count - 1; i >= 0; i--)
        {
            actions.RemoveAt(indexesToKill[i]);
        }
    }
    public void AddAction(Action action)
    {
        if (action.unique)
        {
            Type t = action.GetType();
            if (uniques.ContainsKey(t))
            {
                return;
            }
            uniques[t] = action;
        }
        actions.Add(action);
    }

    public void AddTailAction()
    {
        
    }

    public void AddHeadAction(Action action)
    {
        actions.Insert(0, action);
    }


    public void ClearAfterThisAction()
    {
        
    }
    
    public bool IsEmpty()
    {
        if (actions.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public int GetActionListCount()
    {
        return actions.Count;
    }

    public List<Action> GetTheList()
    {
        return actions;
    }

    public void ClearActions()
    {
        actions.Clear();
    }
}
