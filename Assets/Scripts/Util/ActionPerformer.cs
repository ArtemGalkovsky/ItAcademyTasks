using System;
using System.Collections.Generic;

class ActionPerformer
{
    public static void PerformActionOnObjects<T>(IEnumerable<T> objectsToPerform, Action<T> action)
    {
        foreach (T someObject in objectsToPerform)
        {
            action(someObject);
        }
    }
}
