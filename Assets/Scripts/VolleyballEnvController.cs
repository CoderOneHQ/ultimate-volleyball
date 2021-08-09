using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public List<VolleyballAgent> AgentsList = new List<VolleyballAgent>();  

    Rigidbody blueAgentRb;
    Rigidbody purpleAgentRb;

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

        foreach (var agent in AgentsList)
        {
            // randomise starting positions and rotations
            var randomPosX = Random.Range(-2f, 2f);
            var randomPosZ = Random.Range(-2f, 2f);
            var randomPosY = Random.Range(0.5f, 3.75f); // depends on jump height
            var randomRot = Random.Range(-45f, 45f);

            agent.transform.localPosition = new Vector3(randomPosX, randomPosY, randomPosZ);
            agent.transform.eulerAngles = new Vector3(0, randomRot, 0);
            
            agent.GetComponent<Rigidbody>().velocity = default(Vector3);
        }

        // reset ball to starting conditions
        ResetBall();
    }
}
