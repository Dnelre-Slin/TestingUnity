using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTypeHandler
{
    [System.Flags]
    public enum ActionType
    {
        None = 0,
        Started = 1,
        Performed = 2,
        Canceled = 4
    }

    static public bool IsStarted(ActionType type)
    {
        return (ActionType.Started & type) == ActionType.Started;
    }
    static public bool IsPerformed(ActionType type)
    {
        return (ActionType.Performed & type) == ActionType.Performed;
    }
    static public bool IsCanceled(ActionType type)
    {
        return (ActionType.Canceled & type) == ActionType.Canceled;
    }
}
