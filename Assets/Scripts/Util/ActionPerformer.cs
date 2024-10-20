using System;
using System.Collections.Generic;

class ActionPerformer
{
    public static void PerformActionOnObjects<T>(IEnumerable<T> objectToPerform, Action<T> action)
    {
        foreach (T someObject in objectToPerform)
        {
            action(someObject);
        }
    }
}
