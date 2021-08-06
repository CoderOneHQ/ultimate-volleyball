# 🏐 Ultimate Volleyball
[![Submissions Open](https://img.shields.io/badge/submissions-TBC-blue)](https://bit.ly/ult-volleyball)
[![Next Deadline](https://img.shields.io/badge/next%20deadline-TBC-orange)](https://www.worldtimeserver.com/convert_time_in_UTC.aspx?y=2021&mo=8&d=7&h=0&mn=0)
[![Suggestions Welcome](https://img.shields.io/badge/suggestions-welcome-success)](https://github.com/CoderOneHQ/ultimate-volleyball/issues)

![Ultimate Volleyball](https://uploads-ssl.webflow.com/5ed1e873ef82ae197179be22/610b97749b9ae970392eccf5_volleyball_2p.gif)

## About
**Ultimate Volleyball** is a competition for intelligent agents trained using [Unity ML-Agents](https://unity.com/products/machine-learning-agents). Submit an agent and compete against others in a 3D volleyball environment.

## Leaderboard
| Rank | Name | Games Played | Winrate |
| --- | --- | --- | --- |
| 1 🥇 | - | 1 | 100%

## Contents
1. [Getting Started](#getting-started)
2. [Heuristic Mode](#heuristic-mode)
3. [Training](#training)
4. [Environment Description](#environment-description)
5. [Submissions](#submissions)

## Getting Started
1. Install the [Unity ML-Agents toolkit](https:github.com/Unity-Technologies/ml-agents) (Release 18+) by following the [installation instructions](https://github.com/Unity-Technologies/ml-agents/blob/release_18_docs/docs/Installation.md).
2. Download or clone this repo containing the `ultimate-volleyball` Unity project.
3. Open the `ultimate-volleyball` project (Unity Hub → Projects → Add → Select root folder for this repo).
4. Load the `Volleyball` scene (Project panel → Assets → Scenes → `Volleyball.unity`).
5. Click the ▶ button at the top of the window. This will run the agent in inference mode using the provided baseline model.

## Heuristic Mode
Running in heuristic mode allows you to control the agent directly as a human player.

1. From Project panel, open Assets → Prefabs → `VolleyballArea.prefab`.
2. In Heirarchy panel, toggle open the `VolleyballArea` game object and click the `Agent` game object.
3. In the Inspector Panel, go to Behavior Parameters → Behavior Type and change from `Default` to `Heuristic Only`. 
4. Save the project (Ctrl/Cmd + S).
5. Click ▶. 
6. Use `←` `→` to rotate, `↑` `↓` to move, and `space` to jump.

## Training

1. If you previously changed Behavior Type to `Heuristic Only`, ensure that the Behavior Type is set back to `Default` (see [Heuristic Mode](#heuristic-mode)).
2. Activate the virtual environment containing your installation of `ml-agents`.
3. Make a copy of the [provided training config file](config/Volleyball.yaml) in a convenient working directory.
4. Run from the command line `mlagents-learn <path to config file> --run-id=VB_01`
    - Replace `<path to config file>` with the actual path to the file in Step 3
5. When you see the message "Start training by pressing the Play button in the Unity Editor", click ▶ within the Unity GUI.
6. From another terminal window, navigate to the same directory you ran Step 4 from, and run `tensorboard --logdir results` to observe the training process. 

For more detailed instructions, check the [ML-Agents getting started guide](https://github.com/Unity-Technologies/ml-agents/blob/release_18_docs/docs/Getting-Started.md).

## Environment Description
Ultimate Volleyball is a 3D multi-agent environment. 

**Goal:** Get the ball to bounce in the opponent's side of the court while preventing the ball bouncing into your own court.

**Action space:**

4 discrete action branches:
- Forward motion (3 possible actions: forward, backward, no action)
- Rotation (3 possible actions: rotate left, rotate right, no action)
- Side motion (3 possible actions: left, right, no action)
- Jump (2 possible actions: jump, no action)

**Observation space:**

Total size: 9
- X, Y, Z location of agent (3)
- X, Z direction velocity of agent (2)
- Y rotation of agent (1)
- X, Y, Z location of ball (3)

**Reward function:**
- +1 for scoring a goal
- -1 when opponent scores a goal

## Submissions
- You are free to change the reward functions, training configuration, and environment.
- You should **not** change the observation input or action space.
- Submit your model's `.onnx` file (e.g. `Volleyball.onnx`) located in `results` → `<run-id>`.

[LINK TO SUBMISSION FORM](https://bit.ly/ult-volleyball)

> ⚠️ We can only run models created using ML-Agent's trainers ([more information](https://github.com/Unity-Technologies/ml-agents/blob/main/docs/Unity-Inference-Engine.md)).

## Questions and feedback
- Join the [Discord](https://discord.gg/NkfgvRN) for any questions and discussions.
- [PRs and suggestions welcome](https://github.com/CoderOneHQ/ultimate-volleyball/issues).

## Sign up for our Beta
If you're interested in multi-agent artificial intelligence competitions, check out what we're building at [Coder One and sign up for our upcoming beta](https://www.gocoder.one).
