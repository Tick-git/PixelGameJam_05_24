using UnityEngine;

public static class TransformHelper
{
    public static Transform FindRootTransform(Transform transform)
    {
        Transform currentTransform = transform;

        while (currentTransform.parent != null)
        {
            currentTransform = currentTransform.parent;
        }

        return currentTransform;
    }
}
