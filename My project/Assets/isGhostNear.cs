using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Is Ghost Near?")]
[Help("Checks whether Ghost is near the Knight.")]
public class IsGhostNear : ConditionBase
{
    public override bool Check()
    {
        GameObject ghost = GameObject.Find("Ghost");
        GameObject knight = GameObject.Find("Knight");
        return Vector3.Distance(ghost.transform.position, knight.transform.position) < 10f;
    }
}
