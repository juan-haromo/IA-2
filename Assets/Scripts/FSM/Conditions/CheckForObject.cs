using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CheckObject", menuName = "FSM/Conditions/CheckObject")]
public class CheckForObject : Condition
{
    public LayerMask objectMask;
    public float viewAngle;
    public float viewDistance;
    public string objectName;
    public override bool Check(StateMachine stateMachine)
    {
        Debug.DrawLine(stateMachine.transform.position, stateMachine.transform.position + (stateMachine.transform.forward * viewDistance));
        List<Transform> objects = FindObject(stateMachine.gameObject.transform);
        if (objects.Count > 0)
        {
            stateMachine.blackBoard.SetValue<Vector3>(objectName, objects[0].position);
            return true;
        }
        return false;
    }

    public List<Transform> FindObject(Transform origin)
    {
        List<Transform> detectedObjects = new List<Transform>();
        //Detect all objects in range
        Collider[] objectsInRadius = Physics.OverlapSphere(origin.position, viewDistance, objectMask);

        foreach (Collider col in objectsInRadius)
        {
            //Check if object is in field of vie
            Vector3 dir = (col.gameObject.transform.position - origin.position).normalized;
            float angleToObject = Vector3.Angle(dir, origin.forward);
            if (angleToObject > viewAngle / 2) { continue; }
            //Check if object is in line of sight
            RaycastHit hit;
            if (!Physics.Raycast(origin.position, dir, out hit, viewDistance)) { continue; }
            if (hit.collider != col) { continue; }
            detectedObjects.Add(col.gameObject.transform);
        }

        return detectedObjects;
    }
}
