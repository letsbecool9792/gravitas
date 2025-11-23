#Gravitas

This project simulates a simplified 3D **gravitational system** where celestial bodies attract each other using **Newton’s Law of Universal Gravitation**.  
Bodies can be manually spawned with custom properties through a **UI system**, and the simulation can be started or paused using buttons.

---

## ⬇️ Download latest release:
[Download](https://drive.google.com/file/d/1fGxHeAU_WmdFGACZTprpg8hFjXQDPP5C/view?usp=sharing)

---

## 🪐 Features Implemented

### 1. **Dynamic Body Spawning**
- Bodies can be added to the simulation from a UI modal.  
- Each spawned body allows custom inputs for:
  - Mass  
  - Radius  
  - Position (X, Y, Z)  
  - Initial Velocity (X, Y, Z)
- Bodies automatically scale in size and mass after spawning.

### 2. **Simulation Control**
- Simulation starts **only when the “Start” button** is pressed.  
- Before that, time is paused (`Time.timeScale = 0`), letting the user spawn as many bodies as needed.
- Once started, all active bodies are woken up, and gravity begins to act on them.

### 3. **Realistic Gravitational Physics**
- Uses Newton’s law of gravitation:

  `F = G * (m1 * m2) / r²`

- Implemented in Unity with adjustable constants:
  - `G` — Gravitational constant (6.67408×10⁻¹¹)
  - `gravityScale` — Multiplier to bring values to a simulation-friendly scale (default: 1e6)
  - `minDistance` — Prevents division by zero or explosion at close range
- All forces are applied via Unity’s physics engine using `Rigidbody.AddForce()`.

### 4. **Customizable Bodies**
Each celestial body:
- Has a visible color (currently set to white; customizable later)
- Uses physical radius for visuals and collisions
- Maintains its own mass and velocity through initialization

---

## 🧠 Current Behavior

- **Bodies spawn** into an empty space until simulation starts.  
- When simulation begins:
  - The gravitational attraction starts between all active bodies.
  - Bodies move naturally based on their mass, distance, and velocity.
- Mass ratio affects motion:
  - If one body is much heavier, it stays nearly stationary while lighter ones orbit it.
  - If masses are similar, both revolve around a shared center of mass.

---

## ⚙️ Scripts Overview

| Script | Purpose |
|--------|----------|
| **SimulationManager.cs** | Controls the UI, body spawning, and simulation start/stop. Manages all active bodies. |
| **CelestialBody.cs** | Handles individual body setup — mass, radius, velocity, and color initialization. |
| **GravityManager.cs** | Calculates gravitational forces between all active bodies every physics frame and applies them. |

---

## 🧩 How to Use

1. **Enter Play Mode**
2. In the main menu:
   - Click **“Spawn”** → input desired body values.
   - Confirm to add the body into the scene.
3. Repeat for as many bodies as desired.
4. Once ready, click **“Start Simulation.”**
5. Observe the gravitational motion unfold in 3D space.

---

## 🚧 Future Improvements

- Add **camera controls** (free-fly or orbital view).
- Add **color picker** for customizing body appearance.
- Add **pause/reset** buttons.
- Display **body trails** and names dynamically.
- Implement **energy and orbit visualization** (optional stretch goal).

---

## 🪄 Example Setup

To get a simple two-body orbit:
- Body A: `mass = 1e+8`, `radius = 2`, `position = (0, 0, 0)`, `velocity = (0, 0, 0)`
- Body B: `mass = 1e+6`, `radius = 0.6`, `position = (0, 0, 10)`, `velocity = (20, 0, 0)`
- Gravity Scale: `1e+6`

Expected result:  
Body A remains nearly stationary, while Body B orbits around it in an elliptical path.

---

## 🧾 Notes

- If `gravityScale` is too low, bodies will drift linearly with almost no visible attraction.  
- Higher `gravityScale` values strengthen gravitational pull but may cause instability if too high.  
- Adjust initial velocities for stable orbits or dynamic collisions.

---

Built with **Unity** and **C#**, 2025.

