﻿using App.Shared.Util;
using Assets.App.Shared.EntityFactory;
using Assets.Utils.Configuration;
using Core;
using Core.EntityComponent;
using Core.Utils;

using System;
using Utils.Singleton;

namespace App.Shared.GameModules.Weapon
{
    /// <summary>
    /// 4 location : ground body hand pacakge
    /// </summary>
    [WeaponSpecies(EWeaponSlotType.PrimeWeapon)]
    [WeaponSpecies(EWeaponSlotType.SecondaryWeapon)]
    [WeaponSpecies(EWeaponSlotType.PistolWeapon)]

    public class CommonWeaponAgent : WeaponBaseAgent
    {
        protected static readonly LoggerAdapter Logger = new LoggerAdapter(typeof(CommonWeaponAgent));

        protected int lastUsageId;

        //public IGrenadeCacheHelper GrenadeHelper
        //{
        //    get { return slotHelper as IGrenadeCacheHelper; }
        //}

        //protected GrenadeCacheData slotHelper;

        public CommonWeaponAgent(Func<EntityKey> in_holdExtractor, Func<EntityKey> in_emptyExtractor, EWeaponSlotType slot, GrenadeCacheHelper grenadeHelper) : base(in_holdExtractor, in_emptyExtractor, slot, grenadeHelper)
        {
        }

        //public  void SetHelper(GrenadeCacheData in_helper)
        //{
        //    slotHelper = in_helper;
        //}
        ///return needAutoSutff
        public override bool ExpendWeapon()
        {
            BaseComponent.Bullet -= 1;
            return false;
        }

        //选择下一个可装备的武器id
        public override int FindNextWeapon(bool autoStuff)
        {
            return -1;
        }

        /// <summary>
        /// weapon from body, hand to ground
        /// </summary>
        public override void ReleaseWeapon()
        {
            if (IsValid())
                Entity.SetDestroy();
        }

        /// <summary>
        /// release old weapon and create new one
        /// </summary>
        /// <param name="weaponSlotAgent"></param>
        /// <param name="Owner"></param>
        /// <param name="orient"></param>
        /// <param name="refreshParams"></param>
        /// <returns></returns>
        public override WeaponEntity ReplaceWeapon(EntityKey Owner, WeaponScanStruct orient, ref WeaponPartsRefreshStruct refreshParams)
        {
            refreshParams.lastWeaponKey = WeaponKey;
            ReleaseWeapon();
            var newEnity = WeaponEntityFactory.CreateEntity(orient);
            newEnity.SetRetain(Owner);
      //      DebugUtil.LogInUnity("add new weapon :{0}", DebugUtil.DebugColor.Blue, newEnity.entityKey.Value);
            if (orient.AvatarId < 1)
                orient.AvatarId = SingletonManager.Get<WeaponResourceConfigManager>().GetConfigById(orient.ConfigId).AvatorId;
            WeaponPartsStruct parts = orient.CreateParts();
            refreshParams.weaponInfo = orient;
            refreshParams.slot = handledSlot;
            refreshParams.oldParts = new WeaponPartsStruct();
            refreshParams.newParts = parts;
            refreshParams.armInPackage = true;
            return newEnity;
        }
    }
}
