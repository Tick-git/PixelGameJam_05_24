using System;
using UnityEngine;

public class TreeActivationBehavior : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<TreeDamageBehavior>().enabled = false;
    }

    public void SetTreeActive()
    {
        GetComponent<TreeDamageBehavior>().enabled = true;
    }
}
