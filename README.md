# Physics Invaders (Space Invaders with Physics) 

# Gameplay Overview 
Physics Invaders is based on the game Space Invaders, developed by Atari in 1978. However, unlike the original game, this version has physics and is in 3D! 
It mirrors the classic game elements: there are rows of invaders trying to destroy the player's spacecraft, and the player must destroy them or be destroyed. 
The goal of the game is to defeat all the aliens and/or attain a high score by defeating the invaders and the mystery ship. 

# Controls 
The player controls a spaceship using the LEFT/RIGHT or A/D keys and the W or UP keys to shoot the aliens. 

# Game Screens 
Title Screen: 

The player begins on this screen and has the option to play the game, check out the high scores, or quit. 

Gameplay screen: 

This is the screen where the player is able to play the game. They can play until they lose all their lives/allow the aliens to reach their ship (resulting in a game over) or by defeating all the aliens.
The highest score is visible in the middle of the screen, and the player can view their current score on the top right. They can also see the number of lives they have on the top left.

Score Screen: 

This screen shows the top 5 scores! These scores are retained even if the game is quit through the use of playerprefs. 

# Game Elements 
Invaders: 

The aliens that the player needs to defeat. They spawn in the top middle of the screen and move towards the right of the screen. Then, when they hit the right boundary, they go down a level towards the player and switch direction. This process repeats until they are destroyed or they reach the player. When a bullet from the player hits the aliens, the rigid body will be enabled, and they will fall. If another object hits them, then they will rotate in place. Depending on which alien they defeat, the player gets a different point value (10, 20, or 30). The invaders can also spawn bullets that can hit the player's ship and cause them to lose a life. 

UFO/Mystery Ship: 

This ship spawns every 5 seconds and moves from the left to the right of the screen. If the player defeats the UFO, they will get 100 points. The UFO will also fall to the ground and have a rigid body enabled. 

Player ship: 

The player ship is located at the bottom of the screen and can be moved using key controls. The player can also shoot bullets using the ship. The ship's movement is affected by debris- if there is a lot of debris in the way, then the ship will move less slowly unless the debris is pushed off the edge. 

Shields: 

The shields are static and are placed above the ship's location. If they are hit by the player ship bullets or the alien bullets, then they will break apart and become debris on the ground.  

Win State: 

If all the aliens are defeated, then there will be corresponding text. 

If a high score is reached, then there will be corresponding text and user input to add a screen name for the leaderboard. 

Loss State: 

If the player loses all their lives or the aliens reach the bottom of the screen, then they will have game over text.

If a high score is reached, then there will be corresponding text and user input to add a screen name for the leaderboard. 

Sound Effects: 

The following have sound effects:  player bullets, invader bullets, invader death, player death, rocket death, UFO death, and missile bullets.

# Physics Elements & Effects 

3D Effect: 

When a projectile hits the player, the camera will shake back and forth. 

Particle Effect: 

When a projectile hits the player's ship, the ship will disappear, and there will be a particle effect explosion. 

Physics Elements: 

The aliens, bullets, and UFO (when hit/deactivated) will fall to the ground and become "debris," which can slow down the player ship. The shield blocks when hit will become smaller blocks that have the same effect. 

# New Features 
New Resource: Barrage of Missiles 

If the player hits the red ship teleporting around the screen, they can unleash a barrage of missiles that can attack both friend and foe (the invaders and their spaceship). 

These missiles spawn at the top of the screen (such that their position can hit the invaders and the ship).

The ship is a one-time resource and does not respawn!
As such, the player needs to plan when to utilize this resource (since destroying multiple invaders simultaneously will also speed up the invader movement). 

There is a unique sound effect that plays when the rocket is destroyed and another that plays when the missiles are launched. The missiles have a red color to distinguish them from the other types of bullets. 

New Goal: Bullet Power-Up 

If the player defeats a column of invaders, they will attain a bullet power-up. This allows the player to have infinite bullets for a set amount of time (2.0 seconds). This power-up can be acquired whenever a column is defeated. 
As a note, I found that once this power-up is attained, it is very easy to keep attaining it (defeat columns). However, I decided to allow this to happen since the game is still challenging (due to the physics obstacles and the invaders speeding up when killed). 

# Gameplay Video 


# IOS Build Proof

Although it does not look great, here is the IOS build: 

# Credits for Materials Used

Texture for shields: https://www.freepik.com/free-vector/abstract-futuristic-circuit-board-lights-moving-black-surface-poster_36152349.htm#query=sci%20fi%20texture&position=6&from_view=keyword&track=ais_hybrid&uuid=1e53c56c-36ef-4ef7-970c-43cd4939e985

Invader Model 1: https://free3d.com/3d-model/space-invader-42443.html

Invader Model 2: https://free3d.com/3d-model/space-invader-84114.html

Invader Model 3: https://free3d.com/3d-model/space-invader-26504.html

Spaceship Model: https://quaternius.itch.io/lowpoly-spaceships

Rocket Model: https://sketchfab.com/3d-models/toy-rocket-48a2138808574d39bd671ff75dd4d4d5

Bullet Sound Effect 1: https://pixabay.com/sound-effects/laser-zap-90575/

Bullet Sound Effect 2: https://pixabay.com/sound-effects/laser-45816/

Bullet Sound Effect 3: https://pixabay.com/sound-effects/laser-shot-ingame-230500/

Explosion Sound 1: https://pixabay.com/sound-effects/metal-design-explosion-13491/

Explosion Sound 2: https://pixabay.com/sound-effects/medium-explosion-40472/

Explosion Sound 3: https://pixabay.com/sound-effects/ding-101377/
