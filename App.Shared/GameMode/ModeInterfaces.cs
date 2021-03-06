﻿using Core;
using Core.EntityComponent;
using WeaponConfigNs;

namespace App.Shared.GameMode
{
    /// <summary>
    /// Defines the <see cref="IWeaponInitHandler" />
    /// </summary>
    public interface IWeaponInitHandler
    {
        void RecoverPlayerWeapon(object player);

        bool CanModeSwitchBag { get; }

        int ModeId { get; }
    }

    /// <summary>
    /// Defines the <see cref="IWeaponProcessListener" />
    /// </summary>
    public interface IWeaponProcessListener
    {
        void OnPickup(IPlayerWeaponProcessor controller, EWeaponSlotType slot);

        void OnExpend(IPlayerWeaponProcessor controller, EWeaponSlotType slot);

        void OnDrop(IPlayerWeaponProcessor controller, EWeaponSlotType slot, EntityKey dropedWeapon);
    }

    /// <summary>
    /// Defines the <see cref="IWeaponSlotsLibrary" />
    /// </summary>
    public interface IWeaponSlotsLibrary
    {
        EWeaponSlotType GetWeaponSlotByIndex(int index);

        bool IsSlotValid(EWeaponSlotType slot);

        EWeaponSlotType[] AvaliableSlots { get; }
    }

    /// <summary>
    /// Defines the <see cref="IPickupHandler" />
    /// </summary>
    public interface IPickupHandler
    {
        void SendPickup(int entityId , int itemId, int category, int count);

        void SendAutoPickupWeapon(int entityId);

        void AutoPickupWeapon(PlayerEntity player, int sceneEntity);

        void DoPickup(PlayerEntity player, int sceneEntity);

        void Drop(PlayerEntity player, EWeaponSlotType slot);
    }

    /// <summary>
    /// Defines the <see cref="IReservedBulletHandler" />
    /// </summary>
    public interface IReservedBulletHandler
    {
        int SetReservedBullet(IPlayerWeaponProcessor controller, EWeaponSlotType slot, int count);

        int SetReservedBullet(IPlayerWeaponProcessor controller, EBulletCaliber caliber, int count);

        int GetReservedBullet(IPlayerWeaponProcessor controller, EWeaponSlotType slot);

        int GetReservedBullet(IPlayerWeaponProcessor controller, EBulletCaliber caliber);
    }
   
}
