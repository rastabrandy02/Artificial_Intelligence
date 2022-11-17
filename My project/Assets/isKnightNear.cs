using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Is Knight Near?")]
[Help("Checks whether Knight is near the Whitch.")]
public class IsKnightNear : ConditionBase
{

    
    [InParam("self")]
    public GameObject gameObject;

    [InParam("closeDistance")]
    [Help("The maximun distance to consider that the target is close")]
    public float closeDistance;

    public override bool Check()
    {
        //GameObject knight = GameObject.Find("Knight");
        GameObject ghost = GameObject.FindGameObjectWithTag("Ghost");
        if (ghost == null) return false;
        return !(Vector3.Distance(gameObject.transform.position, ghost.transform.position) < closeDistance);
    }
}