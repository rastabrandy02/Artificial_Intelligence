using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Is Knight Near?")]
[Help("Checks whether Knight is near the Whitch.")]
public class IsKnightNear : ConditionBase
{
    public override bool Check()
    {
        GameObject whitch = GameObject.Find("Whitch");
        GameObject knight = GameObject.Find("Knight");
        return Vector3.Distance(whitch.transform.position, knight.transform.position) < 10f;
    }
}