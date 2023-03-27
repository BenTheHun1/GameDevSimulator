using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Assets/Upgrade")]
public class UpgradeObject : ScriptableObject
{
    public string nam;
    public string desc;

    public int startingCost;

    public type upgradeType;
    public int upgradeValue;

    public int eraRequired;
    public bool isProject;
    public float timeToFinish;
}
