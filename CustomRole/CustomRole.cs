using MEC;
using Qurre;
using Qurre.API;
using Qurre.API.Events;
using Qurre.API.Objects;
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
        public override Version Version => new Version(1, 1, 0);
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
            Qurre.Events.Round.TeamRespawn += OnTeamRespawning;

            Qurre.Events.Player.Dead += OnDead;
            Qurre.Events.Player.RoleChange += OnRoleChanging;
        }
        public void UnregisterEvents()
        {
            CustomConfigs.Remove(CustomConfig);
            if (!CustomConfig.IsEnable) return;

            Qurre.Events.Round.Start -= OnRoundStarted;
            Qurre.Events.Alpha.Detonated -= OnDetonated;
            Qurre.Events.Round.TeamRespawn -= OnTeamRespawning;

            Qurre.Events.Player.Dead -= OnDead;
            Qurre.Events.Player.RoleChange -= OnRoleChanging;
        }
        public void OnRoundStarted()
        {
            CreatingRole("RoundStart");
        }
        public void OnTeamRespawning(TeamRespawnEvent ev)
        {
            Timing.CallDelayed(1f, () =>
            {
                CreatingRole("TeamRespawn");
            });
        }
        public void OnDetonated()
        {
            Timing.CallDelayed(1f, () =>
            {
                CreatingRole("Detonate");
            });
        }
        public void CreatingRole(string NameOfEvent)
        {
            foreach (var list in CustomConfig.MakingRoles.Where(ev => ev.NameOfEvent_ForSpawn == NameOfEvent && Player.List.Count() >= ev.HowNeedPeople_ToSpawnRole))
            {
                if (list.Spawn_Chance_Role >= Random.Range(0, 100))
                {
                    var people_count = list.HowMuchPeople_CanBeSpawn;
                    while (people_count > 0)
                    {
                        Player player = GetRandomPlayer();
                        if (player.Team != Team.SCP && list.Who_CanBe.Contains(player.Role))
                        {
                            PlayerList.Contains(player);

                            player.ClearBroadcasts();
                            player.Broadcast(list.Broadcast_Role, 10);
                            player.SendConsoleMessage(list.Info_Console, "green");

                            player.Role = list.Class_Role;
                            player.RoleName = list.Name_Role;
                            player.RoleColor = list.Role_Color;
                            player.MaxHp = list.Hp_Role;
                            player.Hp = player.MaxHp;

                            foreach (var item in list.Items_Role)
                            {
                                player.AddItem(item);
                            }

                            Timing.CallDelayed(1f, () =>
                            {
                                if (list.Spawn_Room == RoomType.Surface)
                                {
                                    player.Position = list.RoomSurface_SpawnPosition;
                                }
                                else player.TeleportToRoom(list.Spawn_Room);
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
