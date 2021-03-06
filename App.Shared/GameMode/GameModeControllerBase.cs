﻿using App.Shared.GameMode;
using Core;
using Core.EntityComponent;
using Core.Room;
using Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using WeaponConfigNs;

namespace App.Shared
{
    /// <summary>
    /// Defines the <see cref="GameModeControllerBase" />
    /// </summary>
    public class GameModeControllerBase : ModuleLogicActivator<GameModeControllerBase>, ISessionMode, IWeaponProcessListener, IPickupHandler, IReservedBulletHandler, IWeaponSlotsLibrary
    {
        public IWeaponProcessListener ProcessListener { get; protected set; }

        public IPickupHandler PickupHandler { get; protected set; }

        public IReservedBulletHandler ReservedBulletHandler { get; protected set; }

        public IWeaponSlotsLibrary SlotLibary { get; protected set; }

        public virtual EWeaponSlotType GetSlotByIndex(int index)
        {
            return EWeaponSlotType.None;
        }

        public virtual void RecoverPlayerWeapon(PlayerEntity player)
        {
            player.WeaponController().InitBag(0);
        }

        public virtual void Initialize(Contexts contexts, int modeId)
        {
        }

        public virtual bool CanModeSwitchBag
        {
            get { return true; }
        }

        /// <summary>
        /// 当前模式Id
        /// </summary>
        public int ModeId { get; private set; }

        public List<PlayerWeaponBagData> FilterSortedWeaponBagDatas(Components.Player.PlayerInfoComponent playerInfoComponent)
        {
            if ((EGameMode)ModeId == EGameMode.Survival) return null;
            if (playerInfoComponent.WeaponBags == null ||
        playerInfoComponent.WeaponBags.Length == 0) return null;
            var valuableBagDatas = new List<PlayerWeaponBagData>(playerInfoComponent.WeaponBags);
            for (int i = valuableBagDatas.Count - 1; i >= 0; i--)
            {
                if (valuableBagDatas[i] == null || valuableBagDatas[i].weaponList.Count == 0 ||
                  valuableBagDatas[i].BagIndex > GlobalConst.WeaponBagMaxCount)
                {

                    valuableBagDatas.RemoveAt(i);
                    continue;
                }
           //     DebugUtil.LogInUnity("Server init bag item:"+valuableBagDatas[i].ToString(),DebugUtil.DebugColor.Blue);
            }
            valuableBagDatas.Sort(ModeUtil.RoomWeaponCompareCmd);
           
            return valuableBagDatas;
        }

        public List<PlayerWeaponBagData> FilterSortedWeaponBagDatas(PlayerEntity player)
        {
            if ((EGameMode)ModeId == EGameMode.Survival || !player.hasPlayerInfo) return null;
            return FilterSortedWeaponBagDatas(player.playerInfo);
        }
        public int GetUsableWeapnBagLength(PlayerEntity player)
        {
            if ((EGameMode)ModeId == EGameMode.Survival || !player.hasPlayerInfo) return 1;
            return GetUsableWeapnBagLength(player.playerInfo);
        }
        public int GetUsableWeapnBagLength(Components.Player.PlayerInfoComponent playerInfoComponent)
        {

            if ((EGameMode)ModeId == EGameMode.Survival || playerInfoComponent.WeaponBags == null ||
                playerInfoComponent.WeaponBags.Length == 0) return 1;
            int length = playerInfoComponent.WeaponBags.Length;
            foreach (var value in playerInfoComponent.WeaponBags)
            {
                if (value == null || value.weaponList.Count == 0 ||
             value.BagIndex > GlobalConst.WeaponBagMaxCount)
                    length -= 1;
            }
            return Math.Max(length, 1);
        }

        public void SetMode(int modeId)
        {
            ModeId = modeId;
        }

        public void OnPickup(IPlayerWeaponProcessor controller, EWeaponSlotType slot)
        {
            ProcessListener.OnPickup(controller, slot);
        }

        public void OnExpend(IPlayerWeaponProcessor controller, EWeaponSlotType slot)
        {
            ProcessListener.OnExpend(controller, slot);
        }

        public void OnDrop(IPlayerWeaponProcessor controller, EWeaponSlotType slot, EntityKey dropedWeapon)
        {
            ProcessListener.OnDrop(controller, slot, dropedWeapon);
        }

        public int SetReservedBullet(IPlayerWeaponProcessor controller, EWeaponSlotType slot, int count)
        {
            return ReservedBulletHandler.SetReservedBullet(controller, slot, count);
        }

        public int SetReservedBullet(IPlayerWeaponProcessor controller, EBulletCaliber caliber, int count)
        {
            return ReservedBulletHandler.SetReservedBullet(controller, caliber, count);
        }

        public int GetReservedBullet(IPlayerWeaponProcessor controller, EWeaponSlotType slot)
        {
            return ReservedBulletHandler.GetReservedBullet(controller, slot);
        }

        public int GetReservedBullet(IPlayerWeaponProcessor controller, EBulletCaliber caliber)
        {
            return ReservedBulletHandler.GetReservedBullet(controller, caliber);
        }

        public EWeaponSlotType GetWeaponSlotByIndex(int index)
        {
            return SlotLibary.GetWeaponSlotByIndex(index);
        }

        public bool IsSlotValid(EWeaponSlotType slot)
        {
            return SlotLibary.IsSlotValid(slot);
        }

        public EWeaponSlotType[] AvaliableSlots
        {
            get { return SlotLibary.AvaliableSlots; }
        }

        public void SendPickup(int entityId, int itemId, int category, int count)
        {
            PickupHandler.SendPickup(entityId, itemId, category, count);
        }

        public void SendAutoPickupWeapon(int entityId)
        {
            PickupHandler.SendAutoPickupWeapon(entityId);
        }

        public void AutoPickupWeapon(PlayerEntity player, int sceneEntityValue)
        {
            PickupHandler.AutoPickupWeapon(player, sceneEntityValue);
        }

        public void DoPickup(PlayerEntity player, int sceneEntityValue)
        {
            PickupHandler.DoPickup(player, sceneEntityValue);
        }

        public void Drop(PlayerEntity player, EWeaponSlotType slot)
        {
            PickupHandler.Drop(player, slot);
        }
    }
}
