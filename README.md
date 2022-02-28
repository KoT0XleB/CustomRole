# CustomRole
## You can create a custom role in the game.
## Вы можете создать кастомную роль в игре.
### For example: Janitor
```
- Name_Role: Janitor
  Info_Console: <color=yellow>You're a janitor. You clean up here. Check more info in [~]</color>
  Broadcast_Role: >-
    <color=yellow>You're a janitor. You clean up here.
     You have more HP and a janitor card.</color>
  Role_Color: yellow
  Items_Role:
  - KeycardJanitor
  - Flashlight
  Class_Role: ClassD
  Hp_Role: 120
  Who_CanBe:
  - ClassD
  - Scientist
  Spawn_Chance_Role: 30
  Spawn_Room: LczToilets
  RoomSurface_SpawnPosition:
    x: 0
    y: 0
    z: 0
  HowNeedPeople_ToSpawnRole: 1
  HowMuchPeople_CanBeSpawn: 1
  NameOfEvent_ForSpawn: RoundStart
```
![](https://github.com/KoT0XleB/CustomRole/blob/main/CustomRole.jpg)
