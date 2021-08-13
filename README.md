# 🏐 Ultimate Volleyball
[![Submissions Open](https://img.shields.io/badge/submissions-open-green)](https://github.com/CoderOneHQ/ultimate-volleyball/issues/new?assignees=joooyzee&labels=submission&template=agent-submission.md&title=%5BSUBMISSION%5D)
[![Suggestions Welcome](https://img.shields.io/badge/suggestions-welcome-success)](https://github.com/CoderOneHQ/ultimate-volleyball/issues)

![Ultimate Volleyball](https://uploads-ssl.webflow.com/5ed1e873ef82ae197179be22/6115ddedda18aab700dfb75c_ultimate-volleyball-trained.gif)

## About
**Ultimate Volleyball** is a 3D physics-based multi-agent environment built on [Unity ML-Agents](https://unity.com/products/machine-learning-agents). You're welcome to submit a trained agent to play against others (see [Submission Guidelines](#submission-guidelines)).

## Contents
1. [Release notes](#release-notes)
1. [Leaderboard](#leaderboard)
1. [Recent games](#recent-games)
1. [Getting started](#getting-started)
1. [Heuristic mode](#heuristic-mode)
1. [Training](#training)
1. [Environment description](#environment-description)
1. [Submission guidelines](#submission-guidelines)
1. [Future improvements](#future-improvements)
1. [Questions and feedback](#questions-and-feedback)
1. [Coder One Beta](#looking-for-more)

## Release Notes
> ⚠️ Please note this is still a WIP project. The environment will be improved over time. Feel free to make a suggestion by raising an issue or PR.

| ver. | Notes |
| --- | --- |
| 1.0 (current) | <p>Initial stable release. Submissions welcome!</p> <p>Tested on Windows only.</p> <p>**Note:** This version contains an invisible boundary (can be toggled off) to make it less punishing for learning agents. Opponent information is also not included in the vector observations (planned for next release). </p> |

## Leaderboard
Please see the [submission guidelines](#submission-guidelines).
| Rank | Name | Winrate | Details |
| --- | --- | --- | --- |
| 1 🥇 | Volleybot | 84.9% | PPO, 20M steps |
| 2 | Ballboy | 67.2% | PPO, 5M steps |
| 3 | Random Agent | 6.37% | It's pretty random | 

## Recent Games
| Purple | Blue | Clip |
| --- | --- | --- |
| Ballboy | Volleybot | ![Ballboy vs Volleybot (Blue)](https://uploads-ssl.webflow.com/5ed1e873ef82ae197179be22/611606ab086c3e61eb8b9b3a_vb_26_5M_v_26_20M.gif) |
| Random Agent | Volleybot | ![Random Agent vs Volleybot](https://uploads-ssl.webflow.com/5ed1e873ef82ae197179be22/6116072f73d123ce5b020195_vb_20_26M_v_26_20M.gif) |


## Getting Started
1. Install the [Unity ML-Agents toolkit](https:github.com/Unity-Technologies/ml-agents) (Release 18+) by following the [installation instructions](https://github.com/Unity-Technologies/ml-agents/blob/release_18_docs/docs/Installation.md).
2. Download or clone this repo containing the `ultimate-volleyball` Unity project.
3. Open the `ultimate-volleyball` project (Unity Hub → Projects → Add → Select root folder for this repo).
4. Load the `Volleyball` scene (Project panel → Assets → Scenes → `VolleyballMain.unity`).
5. Click the ▶ button at the top of the window. This will run the agent in inference mode using the provided baseline model.

## Heuristic Mode
Running in heuristic mode allows you to control the agent directly as a human player.

1. From Project panel, open Assets → Prefabs → `VolleyballArea.prefab`.
1. In Heirarchy panel, toggle open the `VolleyballArea` game object and click the `Agent` game object.
1. In the Inspector Panel, go to Behavior Parameters → Behavior Type and change from `Default` to `Heuristic Only`. 
1. Recommended: go back to the Scene view, and turn on the `Main Camera` for a better POV.
1. Save the project (Ctrl/Cmd + S).
1. Click ▶. 
1. Use `a` `d` to rotate, `w` `d` to move, and `space` to jump.

## Training

1. If you previously changed Behavior Type to `Heuristic Only`, ensure that the Behavior Type is set back to `Default` (see [Heuristic Mode](#heuristic-mode)).
2. Activate the virtual environment containing your installation of `ml-agents`.
3. Make a copy of the [provided training config file](config/Volleyball.yaml) in a convenient working directory.
4. Run from the command line `mlagents-learn <path to config file> --run-id=<some_id> --time-scale=1`
    - Replace `<path to config file>` with the actual path to the file in Step 3
5. When you see the message "Start training by pressing the Play button in the Unity Editor", click ▶ within the Unity GUI.
6. From another terminal window, navigate to the same directory you ran Step 4 from, and run `tensorboard --logdir results` to observe the training process. 

For more detailed instructions, check the [ML-Agents getting started guide](https://github.com/Unity-Technologies/ml-agents/blob/release_18_docs/docs/Getting-Started.md).

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

If training a competitive agent, consider using a simple reward like:
- +1 for scoring a goal
- -1 when opponent scores a goal

## Submission Guidelines

> ⚠️ Only models created using ML-Agent's trainers will work ([more information](https://github.com/Unity-Technologies/ml-agents/blob/main/docs/Unity-Inference-Engine.md)).

- Please ensure you're using the latest release of Unity ML-Agents ([Release 18](https://github.com/Unity-Technologies/ml-agents#releases--documentation))
- You can change the reward functions, training configuration, and environment.
- Leave the observation input or action space unchanged.
- Submit your model's `.onnx` file (e.g. `Volleyball.onnx`) located in `results` → `<run-id>`.

[Submit your agent here](https://github.com/CoderOneHQ/ultimate-volleyball/issues/new?assignees=joooyzee&labels=submission&template=agent-submission.md&title=%5BSUBMISSION%5D).

## Future improvements
Feel free to raise a PR or issue for any suggestions.

- Add raycast option to observations
- Add opponent position to vector observations
- Improve net and ball physics
- Add out of bounds area
- Add executable for training

## Questions and feedback
- Join our [Discord](https://discord.gg/NkfgvRN) for any questions and discussions.
- [PRs and suggestions welcome](https://github.com/CoderOneHQ/ultimate-volleyball/issues).

## Looking for more?
If you're interested in reinforcement learning and game-playing agents, please check out our [upcoming multi-agent AI competition](https://www.gocoder.one) launching late 2021.