using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction

[Action("MyActions/SpawnGhost")]
[Help("Spawns a ghost")]
public class SpawnGhost : BasePrimitiveAction
{
    [InParam("game object")]
    
    public GameObject ghost;

    public override TaskStatus OnUpdate()
    {
        
        GameObject.Instantiate(ghost);
        return TaskStatus.COMPLETED;
    }
}
