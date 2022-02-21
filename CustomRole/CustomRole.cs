using MEC;
using Qurre;
using Qurre.API.Events;
using System;
using System.Linq;
using System.Collections.Generic;

using Player = Qurre.API.Player;
using Random = UnityEngine.Random;

namespace CustomRole
{
    public class CustomRole : Plugin
    {
        public override string Developer => "KoT0XleB#4663";
        public override string Name => "CustomRole";
        public override Version Version => new Version(1, 0, 0);
        public override int Priority => 20;
        public override void Enable() => RegisterEvents();
        public override void Disable() => UnregisterEvents();
        public Config CustomConfig { get; private set; }
        public void RegisterEvents() 
        {
            CustomConfig = new Config();
            CustomConfigs.Add(CustomConfig);
            if (!CustomConfig.IsEnable) return;

            Qurre.Events.Round.Start += OnRoundStarted;
            Qurre.Events.Player.Dead += OnDead;
            Qurre.Events.Player.RoleChange += OnRoleChanging;
        }
        public void UnregisterEvents()
        {
            CustomConfigs.Remove(CustomConfig);
            if (!CustomConfig.IsEnable) return;

            Qurre.Events.Round.Start -= OnRoundStarted;
            Qurre.Events.Player.Dead -= OnDead;
            Qurre.Events.Player.RoleChange -= OnRoleChanging;
        }
        public void OnRoundStarted()
        {
            foreach (var list in CustomConfig.MakingRoles)
            {
                if (list.spawn_chance_role >= Random.Range(0, 100))
                {
                    var people_count = list.howmuchpeople_canbe;
                    while (people_count > 0)
                    {
                        Player player = GetRandomPlayer();
                        if (player.Team != Team.SCP && player.Role == list.who_canbe)
                        {
                            PlayerList.Contains(player);

                            player.ClearBroadcasts();
                            player.Broadcast(list.broadcast_role, 10);
                            player.SendConsoleMessage(list.info_console, "green");

                            player.Role = list.class_role;
                            player.RoleName = list.name_role;
                            player.MaxHp = list.hp_role;
                            player.Hp = player.MaxHp;

                            foreach (var item in list.items_role)
                            {
                                player.AddItem(item);
                            }

                            Timing.CallDelayed(1f, () =>
                            {
                                player.TeleportToRoom(list.spawn_room);
                            });
                        }
                        PlayerList.Remove(player);
                        people_count--;
                    }
                }
            }
        }
        public void OnDead(DeadEvent ev)
        {
            if (PlayerList.Contains(ev.Target))
            {
                PlayerList.Remove(ev.Target);
                ev.Target.RoleName = string.Empty;
            }
        }
        public void OnRoleChanging(RoleChangeEvent ev)
        {
            if (PlayerList.Contains(ev.Player))
            {
                PlayerList.Remove(ev.Player);
                ev.Player.RoleName = string.Empty;
            }
        }
        public Player GetRandomPlayer()
        {
            Player player = Player.Get(Random.Range(2, Player.List.Count()));
            if (!PlayerList.Contains(player))
            {
                return player;
            }
            else return GetRandomPlayer();
        }
        public List<Player> PlayerList { get; set; } = new List<Player>();
    }
}
