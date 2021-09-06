# 🏐 Ultimate Volleyball

![Ultimate Volleyball](https://uploads-ssl.webflow.com/5ed1e873ef82ae197179be22/612dc700708be07a02a57f58_ultimate-volleyball-v2.gif)

## About
**Ultimate Volleyball** is a multi-agent reinforcement learning environment built on [Unity ML-Agents](https://unity.com/products/machine-learning-agents).

You're welcome to submit a trained model which we'll play against others and add to the [leaderboard](#leaderboard).

> **Version**: up-to-date with ML-Agents Release 18

> ✨ **NEW:** The latest version of this environment includes a Self-Play example. 

## Contents
1. [Leaderboard](#leaderboard)
1. [Getting started](#getting-started)
1. [Training](#training)
1. [Environment description](#environment-description)
1. [Submissions](#submissions)
1. [Roadmap](#roadmap)
1. [Questions and feedback](#questions-and-feedback)

## Leaderboard
| Rank | Name | Winrate | Details | Clip |
| --- | --- | --- | --- | --- | 
| 1 🥇 | melonietr | 73.1% | Self-Play PPO, 80M steps (`Submissions/melonietr.onnx`) | ![melonietr](https://uploads-ssl.webflow.com/5ed1e873ef82ae197179be22/6135f9ab5c8d9753a30d47ec_melonietr.gif) *Playing as: Purple* 🟪
| 2 | VolleyAgent | 71.4% | PPO, 20M steps (`Volleyball.onnx`) | ![VolleyAgent](https://uploads-ssl.webflow.com/5ed1e873ef82ae197179be22/6131c77c9979f7221a35fbf3_match2.gif) *Playing as: Blue* 🟦
| 3 | SelfPlayAgent | 69.8% | Self-Play PPO, 60M steps (`Volleyball_SelfPlay.onnx`) | ![SelfPlayAgent](https://uploads-ssl.webflow.com/5ed1e873ef82ae197179be22/6131c9d8785000ce35f459a9_match_2_2.gif) *Playing as: Purple* 🟪
| 4 | Random Agent | 15.7% | (`Volleyball_Random.onnx`) | ![Random Agent vs Volleybot](https://uploads-ssl.webflow.com/5ed1e873ef82ae197179be22/6131c77c057cdd3280bfc499_match1.gif) *Playing as: Blue* 🟦

## Getting Started
1. Install the [Unity ML-Agents toolkit](https:github.com/Unity-Technologies/ml-agents) (Release 18+) by following the [installation instructions](https://github.com/Unity-Technologies/ml-agents/blob/release_18_docs/docs/Installation.md).
2. Download or clone this repo containing the `ultimate-volleyball` Unity project.
3. Open the `ultimate-volleyball` project in Unity (Unity Hub → Projects → Add → Select root folder for this repo).
4. Load the `VolleyballMain` scene (Project panel → Assets → Scenes → `VolleyballMain.unity`).
5. Click the ▶ button at the top of the window. This will run the agent in inference mode using the provided baseline model.

## Training

1. If you previously changed Behavior Type to `Heuristic Only`, ensure that the Behavior Type is set back to `Default` (see [Heuristic Mode](#heuristic-mode)).
2. Activate the virtual environment containing your installation of `ml-agents`.
3. Make a copy of the [provided training config file](config/Volleyball.yaml) in a convenient working directory.
4. Run from the command line `mlagents-learn <path to config file> --run-id=<some_id> --time-scale=1`
    - Replace `<path to config file>` with the actual path to the file in Step 3
5. When you see the message "Start training by pressing the Play button in the Unity Editor", click ▶ within the Unity GUI.
6. From another terminal window, navigate to the same directory you ran Step 4 from, and run `tensorboard --logdir results` to observe the training process. 

For more detailed instructions, check the [ML-Agents getting started guide](https://github.com/Unity-Technologies/ml-agents/blob/release_18_docs/docs/Getting-Started.md).

## Self-Play
To enable self-play:
1. Set either Purple or Blue Agent Team ID to 1.
![Set Team ID](https://uploads-ssl.webflow.com/5ed1e873ef82ae197179be22/6131cc22959cd47d4b359382_selfplay.jpg)
2. Include the self-play hyperparameter hierarchy in your trainer config file, or use the provided file in `config/Volleyball_SelfPlay.yaml` ([ML-Agents Documentation](https://github.com/Unity-Technologies/ml-agents/blob/main/docs/Learning-Environment-Design-Agents.md#teams-for-adversarial-scenarios))
3. Set your reward function in `ResolveEvent()` in `VolleyballEnvController.cs`.

## Environment Description
**Goal:** Get the ball to bounce in the opponent's side of the court while preventing the ball bouncing into your own court.

**Action space:**

4 discrete action branches:
- Forward motion (3 possible actions: forward, backward, no action)
- Rotation (3 possible actions: rotate left, rotate right, no action)
- Side motion (3 possible actions: left, right, no action)
- Jump (2 possible actions: jump, no action)

**Observation space:**

Total size: 11
- Agent Y-rotation (1)
- Normalised directional vector from agent to ball (3)
- Distance from agent to ball (1)
- Agent X, Y, Z velocity (3)
- Ball X, Y, Z relative velocity (3)

**Reward function:**

The project contains some examples of how the reward function can be defined.
The base example gives a +1 reward each time the agent hits the ball over the net.

## Submissions

If you've trained a Volleyball agent and would like to share it, please follow the instructions below:

- Note that only models created using ML-Agent's trainers will work ([more information](https://github.com/Unity-Technologies/ml-agents/blob/main/docs/Unity-Inference-Engine.md)).
- Please ensure you're using the latest release of Unity ML-Agents ([Release 18](https://github.com/Unity-Technologies/ml-agents#releases--documentation))
- You can change the reward functions, training configuration, and environment.
- Leave the observation input or action space unchanged.
- Create a PR and add your model's `.onnx` file to the `Submissions` folder.

## Questions and feedback
This project is managed by the team at [Coder One](https://www.gocoder.one).  Please join our [Discord](https://discord.gg/2Z695VGwyf) for any questions, discussions, and feedback.
