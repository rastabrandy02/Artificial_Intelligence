using UnityEngine;

using Pada1.BBCore;
using Pada1.BBCore.Framework;

[Condition("MyConditions/Is Knight Near?")]
[Help("Checks whether Knight is near the Whitch.")]
public class IsKnightNear : ConditionBase
{

    [InParam("target")]
    public GameObject knight;
    [InParam("self")]
    public GameObject gameObject;
    public override bool Check()
    {
        //GameObject knight = GameObject.Find("Knight");
        return !(Vector3.Distance(gameObject.transform.position, knight.transform.position) < 10f);
    }
}