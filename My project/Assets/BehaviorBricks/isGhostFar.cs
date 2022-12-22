using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Is Ghost Far?")]
[Help("Checks whether Ghost is far the Knight.")]
public class IsGhostFar : ConditionBase
{
    [InParam("target")]
    [Help("Target to check the distance")]
    public GameObject target;

    [InParam("self")]
    [Help("gameobject itself")]
    public GameObject self;
    ///<value>Input maximun distance Parameter to consider that the target is close.</value>
    [InParam("farDistance")]
    [Help("The minimum distance to consider that the target is far")]
    public float farDistance;
    public override bool Check()
    {

        return (self.transform.position - target.transform.position).sqrMagnitude > farDistance * farDistance;
    }
}
