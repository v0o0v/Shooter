！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
	2D Flight Shooting Starter Kit
Copyright 2013 Indie Developer Partners

website : http://www.indp.kr
Contact : support@indp.kr

Version 1.5
！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！

Thank you for buying 2D Flight Shooting Starter Kit!
If you have any questions, suggestions, comments or feature requests, please contact us to support@idp.kr.

--------------------------------------
This Package includes:
--------------------------------------
- "_Tutorial" folder : Includes 4 Game scenes let you know the game development process step-by-step.
- "Animations" folder : Includes animation clip used enemy airplane's move. Make other movement with this clips.
- "Prefabs" folder : Includes prefabs such as explosions effects, bullet firing effects, enemy airplane etc. 
                     And All could be edited for other purpose.
- "Sounds" folder : Includes all the sound resources used for this game.
- "Textures" folder : Includes all the textures used for this project.

- Demo Scene : Play the demo scenes "02_GamePlay" in the Assets/_Scenes folder.

-------------------------------------
Scripts & System
-------------------------------------
1. Managers
1.1 Background Scroll (BgScroller.cs): 
  - BG Scroller is implemented with two background sprites and various cloud sprites.
    Size and speed can be changed for your own purpose.

1.2 Enemy Spawn System (EnemySpawnManager.cs):
  - Enemy position to spawn can be assigned with transform. (enemySpawnPos)
  - Enemy can be spawned in the order specified, for each groups. (enemySpawnInfos)
	 - SpawnEnemyPoolName: The Name of Object pool to spawn.
	 - SpawnTime: Delay time to spawn enemy. (second)
	 - SpawnPos: Position index for spawning enemy.
	 - IsBoss: If true, the object is set as "Boss". And other enemy group don't appear until this boss die.

1.3 Object pool system (ObjectPool.cs):
-> To reduce the heavy load of Instantiate and Destroy, Disable unused object and Enable the object when needed.
  - Active Transform: Parent object for enabled objects.
  - InActive Transform: Parent object for disabled objects.
  - Pool Info
	 - Name: The object name in the object pool.
	 - Pool Count: Number of prefabs to spawn in advance in the object pool.
	 - Pool Object: Original prefabs to be spawned in the object pool.

2. Player
  - Control (PlayerController.cs): Player can be moved by dragging of the mouse or touch.
  - Collision event detector (PlayerController.cs): Collision detection with Enemy, Bullet, Item etc.
  - Shooting bullet (PlayerFire.cs): player shoots the bullets by each level.
  - Player Data (PlayerManager.cs): Manages Player's data such as HP, Life, Bomb, Level etc.

3. Normal Enemy
  - Enemy Control (EnemyController.cs): Manages enemy's hp, and collision detection.
  - Shooting Bullet (EnemyFire.cs): Manages enemy's fire function.
  - Move Sprite Animation (EnemyMoveSpriteAnimation.cs): changes enemy's sprites according to it's move direction.

4. Boss Enemy
  - Control (BossController.cs): Manages boss enemy's hp, and collision detection.
  - Shooting Bullet (EnemyFire.cs): Manages enemy's fire function.

---------------------------------------
How to use
--------------------------------------
Check the demo scene (02_GamePlay scene) and get the idea how to use this package.
This Package is executable on android and iOS.

-------------------------------------
History
-------------------------------------
v1.5
- bug fix: Disable fire bomb during playing player's flying animation.
- Add "Boundary" class to clamp player's position in the screen. (PlayerController.cs)
- Unity5 package uploaded.

v1.4
- All asset converted from NGUI features to Unity2D and UGUI features.

v1.3
- bug fix: fixed Crash on android.

v1.2
- Bug fix: blinks of an animation for Enemy type 4 corrected.

v1.1
- Initial version sumitted.
-------------------------------------