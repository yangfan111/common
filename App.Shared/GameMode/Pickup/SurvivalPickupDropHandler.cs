﻿namespace App.Shared.GameMode
{
    /// <summary>
    /// Defines the <see cref="PickupDropHandlerAdapter" />
    /// </summary>

    /// <summary>
    /// Defines the <see cref="SurvivalPickupDropHandler" />
    /// </summary>
    public class SurvivalPickupDropHandler : PickupDropHandlerAdapter
    {
 
        public SurvivalPickupDropHandler(Contexts context) : base(context)
        {
        }

          //使用服务器操作
            //var player = _playerContext.GetEntityWithEntityKey(new Core.EntityComponent.EntityKey(playerEntityId, (short)EEntityType.Player));
            //player.WeaponController().HeldWeaponAgent.SetFlagWaitDestroy();
           
           // var weaponAchive = player.WeaponController();
           //var curWeapon = weaponAchive.GetSlotWeaponInfo(_contexts, slot);
           // if (curWeapon.ConfigId > 0)
           // {
           //     var dropPos = player.GetHandWeaponPosition();
           //     var playerTrans = player.characterContoller.Value.transform;
           //     var forward = playerTrans.forward;
           //     var pos = dropPos + forward * _runtimeGameConfig.WeaponDropOffset;
           //     RaycastHit hhit;
           //     SceneObjectEntity sceneObjectEntity;
           //     if(Physics.Raycast(dropPos, forward, out hhit, _runtimeGameConfig.WeaponDropOffset, UnityLayerManager.GetLayerMask(EUnityLayerName.Default) | UnityLayerManager.GetLayerMask(EUnityLayerName.Terrain)))
           //     {
           //         RaycastHit vhit;
           //         if(Physics.Raycast(hhit.point, Vector3.down, out vhit, 100, UnityLayerManager.GetLayerMask(EUnityLayerName.Default) | UnityLayerManager.GetLayerMask(EUnityLayerName.Terrain)))
           //         {
           //             sceneObjectEntity = _sceneObjectEntityFactory.CreateWeaponEntity(curWeapon, vhit.point) as SceneObjectEntity;
           //         }
           //         else
           //         {
           //             sceneObjectEntity = _sceneObjectEntityFactory.CreateWeaponEntity(curWeapon, playerTrans.position) as SceneObjectEntity;
           //         }
           //     }
           //     else
           //     {
           //         RaycastHit vhit;
           //         if(Physics.Raycast(pos, Vector3.down, out vhit, 100, UnityLayerManager.GetLayerMask(EUnityLayerName.Default) | UnityLayerManager.GetLayerMask(EUnityLayerName.Terrain)))
           //         {
           //             sceneObjectEntity = _sceneObjectEntityFactory.CreateWeaponEntity(curWeapon, vhit.point) as SceneObjectEntity;
           //         }
           //         else
           //         {
           //             sceneObjectEntity = _sceneObjectEntityFactory.CreateWeaponEntity(curWeapon, playerTrans.position) as SceneObjectEntity;
           //         }
           //     }
           //     player.WeaponController().DropSlotWeapon(_contexts, slot);
           // }

    }
}
