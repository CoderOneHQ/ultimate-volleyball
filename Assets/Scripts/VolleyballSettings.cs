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

    // Original values
    // Vector3 m_OriginalGravity;

    // [Tooltip("Increase or decrease the scene gravity. Use ~3x to make things less floaty")]
    // public float gravityMultiplier = 2.7f;

    // public void Awake()
    // {
    //     // Save the original values
    //     m_OriginalGravity = Physics.gravity;

    //     // Override
    //     Physics.gravity *= gravityMultiplier;

    //     // Make sure the Academy singleton is initialized first, since it will create the SideChannels.
    //     Academy.Instance.EnvironmentParameters.RegisterCallback("gravity", f => { Physics.gravity = new Vector3(0, -f, 0); });
    // }

    // public void OnDestroy()
    // {
    //     Physics.gravity = m_OriginalGravity;
    // }
}
