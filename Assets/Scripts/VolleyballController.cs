using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolleyballController : MonoBehaviour
{
    public GameObject area;
    [HideInInspector]
    public VolleyballEnvController envController;

    public GameObject purpleGoal;
    public GameObject blueGoal;
    Collider purpleGoalCollider;
    Collider blueGoalCollider;

    void Start()
    {
        envController = GetComponentInParent<VolleyballEnvController>();
        purpleGoalCollider = purpleGoal.GetComponent<Collider>();
        blueGoalCollider = blueGoal.GetComponent<Collider>();
    }

    /// <summary>
    /// Checks whether the ball lands in the blue, purple, or out of bounds area
    /// </summary>
    void OnTriggerEnter(Collider other)
    {

        // if (other.gameObject.CompareTag("boundary"))
        // {
        //     // ball went out of bounds
        //     envController.ResolveGoalEvent(GoalEvent.HitOutOfBounds);
        // }
        if (other.gameObject.CompareTag("blueBoundary"))
        {
            // ball hit into blue side
            envController.AssignRewards(0);
        }
        else if (other.gameObject.CompareTag("purpleBoundary"))
        {
            // ball hit into purple side
            envController.AssignRewards(1);
        }
        else if (other.gameObject.CompareTag("purpleGoal"))
        {
            envController.ResolveGoalEvent(GoalEvent.HitPurpleGoal);
        }
        else if (other.gameObject.CompareTag("blueGoal"))
        {
            envController.ResolveGoalEvent(GoalEvent.HitBlueGoal);
        }

    }


}