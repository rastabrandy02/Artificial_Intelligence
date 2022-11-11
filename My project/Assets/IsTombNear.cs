using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Is Tomb Near?")]
[Help("Checks whether Whitch is near the Tomb.")]
public class IsTombNear : ConditionBase
{
    public override bool Check()
    {
        GameObject whitch = GameObject.Find("Whitch");
        GameObject tomb = GameObject.Find("Tomb");
        return Vector3.Distance(whitch.transform.position, tomb.transform.position) < 10f;
    }
}