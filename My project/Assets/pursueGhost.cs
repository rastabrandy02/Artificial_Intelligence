using UnityEngine;
using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // BasePrimitiveAction
using UnityEngine.AI;

[Action("MyActions/PursueGhost")]
[Help("Pursues a ghost")]
public class pursueGhost : BasePrimitiveAction
{
    [InParam("self")]
    public GameObject gameObject;

    public override TaskStatus OnUpdate()
    {
        GameObject ghost = GameObject.FindGameObjectWithTag("Ghost");

        gameObject.GetComponent<NavMeshAgent>().SetDestination(ghost.transform.position);

        return TaskStatus.COMPLETED;
    }
}
