# Asteroid-Classic

https://github.com/pun777chy/Asteroid-Classic/assets/6859320/3fbc7376-9076-41d6-acac-b17a21e35f93

- It is sample game project based on classic Asteroid made on Unity 2021.3.10f1.
- The project employed the Zenject framework for implementing dependency injection and inversion of control.
- Despite the simplicity of the sample, the project adhered closely to SOLID principles.
- Zenject signals were utilized to establish decoupled communication between various classes.
- The GameScene was loaded through Addressables.
- In the current build, Addressables have been deactivated, but they are functional in the editor for testing purposes, thanks to the "Use Asset Database" setting in Play Mode Script.
- A unit test was conducted to verify the binding of the AsteroidSpawner class (additional tests may be added in the future).
- The project features a singular Controller responsible for managing the Game States and updating the corresponding Views.
- ****PLEASE IGNORE THE UI. UI programming is my experties but I had to plan to implement UI gracefully.****


   **PACKAGES ADDED**
  - Zenject
  - Addressables
  - ZenjectTestFramework

  ** **CHALLENGES****
  - Since implementing the game mechanics was easy, most of my time spent was on designing architure.
  - **I kept every class implementation at it's basic level so it would be easy to understand.**
 
   BUILD
https://github.com/pun777chy/Asteroid-Classic/tree/main/Build
