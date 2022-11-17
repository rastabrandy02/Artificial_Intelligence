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
    public int maxInstances = 3;
    int currentInstances = 0;

    public override TaskStatus OnUpdate()
    {
        if(currentInstances < maxInstances)
        {
            Vector3 spawnPos = new Vector3(25.0f, 5.0f, 120.0f);
            Quaternion spawnRot = Quaternion.identity;
            GameObject.Instantiate(ghost, spawnPos, spawnRot);

            currentInstances++;
        }
        
        return TaskStatus.COMPLETED;
    }
}
