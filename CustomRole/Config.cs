using Qurre.API.Addons;
using Qurre.API.Objects;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace CustomRole
{
    public class Config : IConfig
    {
        [Description("Plugin Name")]
        public string Name { get; set; } = "CustomRole";
        [Description("Enable the plugin?")]
        public bool IsEnable { get; set; } = true;
        [Description("List for creating a custom roles (u can make 10+ roles). For example, janitor:")]
        public List<MakeRole> MakingRoles { get; set; } = new List<MakeRole>()
        {
            new MakeRole()
            {
                Name_Role = "Janitor",
                Info_Console = "<color=yellow>You're a janitor. You clean up here. You have more HP and a janitor card.</color>",
                Broadcast_Role = "<color=yellow>You're a janitor. You clean up here.\n You have more HP and a janitor card.</color>",
                Role_Color = "yellow",
                Items_Role = 
                {
                    ItemType.KeycardJanitor,
                    ItemType.Flashlight
                },
                Class_Role = RoleType.ClassD,
                Hp_Role = 120,
                Who_CanBe =
                {
                    RoleType.ClassD,
                    RoleType.Scientist
                },
                Spawn_Chance_Role = 30,
                Spawn_Room = RoomType.LczToilets,
                RoomSurface_SpawnPosition = new Vector3(0, 0, 0),
                HowNeedPeople_ToSpawnRole = 1,
                HowMuchPeople_CanBeSpawn = 1,
                NameOfEvent_ForSpawn = "RoundStart"
            },
            new MakeRole()
            {
                Name_Role = "FBI",
                Info_Console = "You have become a FBI\nHelp MTF and save the scientists.",
                Broadcast_Role = "You the FBI. Press [`] for more information.",
                Role_Color = "yellow",
                Items_Role =
                {
                    ItemType.GunE11SR,
                    ItemType.GunCOM18,
                    ItemType.KeycardChaosInsurgency,
                    ItemType.Medkit,
                    ItemType.Flashlight,
                    ItemType.GrenadeHE,
                },
                Class_Role = RoleType.NtfSpecialist,
                Hp_Role = 120,
                Who_CanBe =
                {
                    //RoleType.Spectator
                    RoleType.NtfCaptain,
                    RoleType.NtfPrivate,
                    RoleType.NtfSergeant,
                    RoleType.NtfSpecialist
                },
                Spawn_Chance_Role = 50,
                Spawn_Room = RoomType.Surface,
                RoomSurface_SpawnPosition = new Vector3(0f, 0f, 0f),
                HowNeedPeople_ToSpawnRole = 10,
                HowMuchPeople_CanBeSpawn = 10,
                NameOfEvent_ForSpawn = "TeamRespawn"
            }
        };
        public class MakeRole
        {
            public string Name_Role { get; set; }
            public string Info_Console { get; set; }
            public string Broadcast_Role { get; set; }
            public string Role_Color { get; set; }
            public List<ItemType> Items_Role { get; set; } = new List<ItemType>() { };
            public RoleType Class_Role { get; set; }
            public int Hp_Role { get; set; }
            public List<RoleType> Who_CanBe { get; set; } = new List<RoleType>() { };
            public int Spawn_Chance_Role { get; set; }
            public RoomType Spawn_Room { get; set; }
            public Vector3 RoomSurface_SpawnPosition { get; set; } = new Vector3(0, 0, 0);
            public int HowNeedPeople_ToSpawnRole { get; set; }
            public int HowMuchPeople_CanBeSpawn { get; set; }
            public string NameOfEvent_ForSpawn { get; set; }
        }
    }
}
