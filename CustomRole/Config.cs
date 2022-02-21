using Qurre.API.Addons;
using Qurre.API.Objects;
using System.Collections.Generic;
using System.ComponentModel;

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
                name_role = "Janitor",
                info_console = "<color=yellow>You're a janitor. You clean up here. You have more HP and a janitor card.</color>",
                broadcast_role = "<color=yellow>You're a janitor. You clean up here.\n You have more HP and a janitor card.</color>",
                items_role = 
                {
                    ItemType.KeycardJanitor,
                    ItemType.Flashlight
                },
                class_role = RoleType.ClassD,
                hp_role = 120,
                who_canbe = RoleType.ClassD,
                spawn_chance_role = 100,
                spawn_room = RoomType.LczToilets,
                howmuchpeople_canbe = 1
            }
        };
        public class MakeRole
        {
            public string name_role { get; set; }
            public string info_console { get; set; }
            public string broadcast_role { get; set; }
            public List<ItemType> items_role { get; set; } = new List<ItemType>() { };
            public RoleType class_role { get; set; }
            public int hp_role { get; set; }
            public RoleType who_canbe { get; set; }
            public int spawn_chance_role { get; set; }
            public RoomType spawn_room { get; set; }
            public int howmuchpeople_canbe { get; set; }
        }
    }
}
