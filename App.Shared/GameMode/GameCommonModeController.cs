﻿
using Core;

namespace App.Shared.GameMode
{
    /// <summary>
    /// Defines the <see cref="GameCommonModeController" />
    /// </summary>
    public class GameCommonModeController : GameModeControllerBase
    {
        protected  ServerWeaponInitHandler serverInitHandler;

        public override void Initialize(Contexts contexs,int modeId)
        {
            SetMode(modeId);
            serverInitHandler = new ServerWeaponInitHandler();
            ProcessListener = new NormalProcessListener();
            PickupHandler = new CommonPickupDropHandler(contexs, ModeId);
            ReservedBulletHandler = new LocalReservedBulletHandler();
            SlotLibary = WeaponSlotsLibrary.Allocate(EWeaponSlotsGroupType.Group);

        }
      
        public override EWeaponSlotType GetSlotByIndex(int index)
        {
            if (index < 0 && index >= (int)EWeaponSlotType.Length)
            {
                return EWeaponSlotType.None;
            }
            switch (index)
            {
                case 2:
                    return EWeaponSlotType.None;
                default:
                    return (EWeaponSlotType)index;
            }
        }

        public override void RecoverPlayerWeapon(PlayerEntity player)
        {
            base.RecoverPlayerWeapon(player);
            serverInitHandler.RecoverPlayerWeapon(player, FilterSortedWeaponBagDatas(player));
        }
    }
}
