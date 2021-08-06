using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public enum Team
{
    Blue = 0,
    Purple = 1
}

public enum GoalEvent
{
    HitPurpleGoal = 0,
    HitBlueGoal = 1,
    HitOutOfBounds = 2
}

public class VolleyballEnvController : MonoBehaviour
{
    // Control ball spawn behavior
    enum BallSpawnSide
    {
        BlueSide = 0,
        PurpleSide = 1,
    }
    Vector3 ballStartingPos;

    VolleyballSettings volleyballSettings;

    public VolleyballAgent blueAgent;
    public VolleyballAgent purpleAgent;

    Rigidbody blueAgentRb;
    Rigidbody purpleAgentRb;
    Vector3 blueStartingPos;
    Quaternion blueStartingRot;
    Vector3 purpleStartingPos;
    Quaternion purpleStartingRot;

    public GameObject ball;
    Rigidbody ballRb;

    public GameObject blueGoal;
    public GameObject purpleGoal;

    Renderer blueGoalRenderer;

    Renderer purpleGoalRenderer;

    Team lastHitter;

    private int resetTimer;
    public int MaxEnvironmentSteps;

    void Start()
    {
        // Used to control agent & ball starting positions
        blueAgentRb = blueAgent.GetComponent<Rigidbody>();
        purpleAgentRb = purpleAgent.GetComponent<Rigidbody>();
       
        blueStartingPos = blueAgent.transform.position;
        blueStartingRot = blueAgent.transform.rotation;

        purpleStartingPos = purpleAgent.transform.position;
        purpleStartingRot = purpleAgent.transform.rotation;

        ballRb = ball.GetComponent<Rigidbody>();

        // Render ground to visualise which agent scored
        blueGoalRenderer = blueGoal.GetComponent<Renderer>();
        purpleGoalRenderer = purpleGoal.GetComponent<Renderer>();

        volleyballSettings = FindObjectOfType<VolleyballSettings>();

        ResetScene();
    }

    /// <summary>
    /// Tracks which agent last had control of the ball
    /// </summary>
    public void UpdateLastHitter(Team team)
    {
        lastHitter = team;
    }

    /// <summary>
    /// Resolves which agent should score
    /// </summary>
    public void ResolveGoalEvent(GoalEvent goalEvent)
    {
        if (goalEvent == GoalEvent.HitPurpleGoal)
        {
            purpleAgent.AddReward(1f);
            blueAgent.AddReward(-1f);
            StartCoroutine(GoalScoredSwapGroundMaterial(volleyballSettings.purpleGoalMaterial, purpleGoalRenderer, .5f));
        }
        else if (goalEvent == GoalEvent.HitBlueGoal)
        {
            blueAgent.AddReward(1f);
            purpleAgent.AddReward(-1f);
            StartCoroutine(GoalScoredSwapGroundMaterial(volleyballSettings.blueGoalMaterial, blueGoalRenderer, .5f));
        }
        else if (goalEvent == GoalEvent.HitOutOfBounds)
        {
            if (lastHitter == Team.Blue)
            {
                // penalize blue agent
                purpleAgent.SetReward(0.5f);
                blueAgent.SetReward(-0.5f);
                Debug.Log("Out of bounds by " + Team.Blue);
            }
            else if (lastHitter == Team.Purple)
            {
                // penalize purple agent
                blueAgent.SetReward(0.5f);
                purpleAgent.SetReward(-0.5f);
            }
        }

        blueAgent.EndEpisode();
        purpleAgent.EndEpisode();
        ResetScene();
    }

    /// <summary>
    /// Randomises whether blue/purple starts with the ball.
    /// </summary>
    void ResetBall()
    {
        // 0 = spawn blue side, 1 = spawn purple side
        var ballSpawnSide = Random.Range(0,2);
        var randomPosX = Random.Range(-2f, 2f);
        var randomPosZ = Random.Range(6f, 10f);
        var randomPosY = Random.Range (5f, 7f);

        if (ballSpawnSide == 0)
        {
            ball.transform.localPosition = new Vector3(randomPosX, randomPosY, randomPosZ);
            // ball.transform.localPosition = new Vector3(0, 7f, 8);
            lastHitter = Team.Blue;
        }
        else if (ballSpawnSide == 1)
        {
            ball.transform.localPosition = new Vector3(randomPosX, randomPosY, -1*randomPosZ);
            // ball.transform.localPosition = new Vector3(0, 7f, -8);
            lastHitter = Team.Purple;
        }

        ballRb.angularVelocity = Vector3.zero;
        ballRb.velocity = Vector3.zero;
    }

    /// <summary>
    /// Changes the color of the ground for a moment.
    /// </summary>
    /// <returns>The Enumerator to be used in a Coroutine.</returns>
    /// <param name="mat">The material to be swapped.</param>
    /// <param name="time">The time the material will remain.</param>
    IEnumerator GoalScoredSwapGroundMaterial(Material mat, Renderer renderer, float time)
    {
        renderer.material = mat;
        yield return new WaitForSeconds(time); // wait for 2 sec
        renderer.material = volleyballSettings.defaultMaterial;
    }

    /// <summary>
    /// Called every step. Control max env steps.
    /// </summary>
    void FixedUpdate()
    {
        resetTimer += 1;
        if (resetTimer >= MaxEnvironmentSteps && MaxEnvironmentSteps > 0)
        {
            blueAgent.EpisodeInterrupted();
            purpleAgent.EpisodeInterrupted();
            ResetScene();
        }

    }

    public void ResetScene()
    {
        resetTimer = 0;

        // randomise blue spawn
        var blueRandomPosX = Random.Range(-2f, 2f);
        var blueRandomPosZ = Random.Range(4, 10f);
        var blueRandomPosY = Random.Range(1f, 3.75f); // depends on jump height
        var blueRandomRot = Random.Range(135f, 225f);

        blueAgent.transform.localPosition = new Vector3(blueRandomPosX, blueRandomPosY, blueRandomPosZ);
        // blueAgent.transform.localPosition = new Vector3(0, 1.5f, 8);
        blueAgent.transform.eulerAngles = new Vector3(0, blueRandomRot, 0);
        blueAgentRb.velocity = default(Vector3);

        // randomise purple spawn
        var purpleRandomPosX = Random.Range(-2f, 2f);
        var purpleRandomPosZ = Random.Range(4, 10f);
        var purpleRandomPosY = Random.Range(1f, 3.75f); // depends on jump height
        var purpleRandomRot = Random.Range(-45f, 45f);

        purpleAgent.transform.localPosition = new Vector3(purpleRandomPosX, purpleRandomPosY, -1*purpleRandomPosZ);
        // blueAgent.transform.localPosition = new Vector3(0, 1.5f, 8);
        purpleAgent.transform.eulerAngles = new Vector3(0, -1*purpleRandomRot, 0);
        purpleAgentRb.velocity = default(Vector3);

        // reset ball to starting conditions
        ResetBall();
    }
}
