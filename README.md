# Ledger Lines

Ledger Lines is an immersive PCVR simulation that gamifies budgeting by turning monetary costs into physical exertion. Developed in Unity, this project replaces the cognitive burden of mental math with direct visual guidance and spatial interaction.

## Core Features

- **Embodied Friction** — Expensive choices generate straight, easy paths. Budget-friendly choices generate complex, jagged paths that require physical stamina and precise motor control to trace.
- **Physics-Free Tracking** — To ensure high frame rates, the core tracing mechanic completely bypasses the Unity physics engine. It utilizes pure mathematical proximity checks to track the XR hand controller.
- **Modular Scenario Data** — Powered by ScriptableObjects, allowing for easy expansion of daily challenges without modifying the runtime code.
- **AI Audio Companion** — A spatialized robotic companion provides contextual advice for every scenario, with voice lines generated via ElevenLabs.
- **The Emergency Protocol** — A final narrative climax that evaluates the saved funds of the player against an unexpected expense, teaching the realities of the poverty cycle.

## Project Architecture

The architecture focuses on modularity and performance in virtual reality.

| Component | Responsibility |
|---|---|
| `DayProgressionManager` | Controls the flow of time and feeds ScriptableObject data to the physical nodes. |
| `OptionNode` | Procedurally generates the `LineRenderer` paths based on cost and difficulty variables. |
| `FinanceManager` | Tracks the monthly stipend and dynamically updates the virtual laptop display. |
| `TipBotManager` | Handles the spatial audio playback and visual sprite toggling for the AI companion. |
