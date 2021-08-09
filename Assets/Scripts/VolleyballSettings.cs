using UnityEngine;
using Unity.MLAgents;

public class VolleyballSettings : MonoBehaviour
{
    public float agentRunSpeed = 1.5f;
    public float agentJumpHeight = 2.75f;
    public float agentJumpVelocity = 777;
    public float agentJumpVelocityMaxChange = 10;

    public Material blueGoalMaterial;
    public Material purpleGoalMaterial;
    public Material defaultMaterial;

    // This is a downward force applied when falling to make jumps look
    // less floaty
    public float fallingForce = 150;
}
