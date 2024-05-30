using System;

[Serializable]
public class TreeSettings
{
    public float TreeShootInterval;
    public float TreeDamage;

    public TreeSettings(TreeSettings treeSettings)
    {
        TreeShootInterval = treeSettings.TreeShootInterval;
        TreeDamage = treeSettings.TreeDamage;
    }
}