﻿using System.Collections.Generic;
using App.Shared.Components.Common;
using App.Shared.Components.Player;
using App.Shared.GameModules.Player;
using Core.Animation;
using Core.Components;
using Core.EntityComponent;
using Core.HitBox;
using Core.Utils;
using System.Text;
using UnityEngine;
using Utils.Singleton;

namespace App.Shared.GameModules.Bullet
{
    public class ClientPlayerHitBoxContext : IHitBoxContext
    {
        private static LoggerAdapter _logger = new LoggerAdapter(typeof(ClientPlayerHitBoxContext));
        private PlayerContext _playerContext;
        private AnimatorPoseReplayer _poseReplayer;
      
        public ClientPlayerHitBoxContext(PlayerContext playerContext)
        {
            _playerContext = playerContext;
            _poseReplayer = new AnimatorPoseReplayer();
        }

        public HitBoxComponent GetHitBoxComponent(EntityKey entityKey)
        {
            var entity = _playerContext.GetEntityWithEntityKey(entityKey);
            if ( entity!= null && entity.hasPosition && entity.hasHitBox)
            {
                return entity.hitBox;
            }

            return null;
        }

        public void UpdateHitBox(IGameEntity gameEntity)
        {
            var position = gameEntity.Position.Value;
            var rotation = gameEntity.GetComponent<OrientationComponent>().RotationYaw;
            var hitBoxComponent = GetHitBoxComponent(gameEntity.EntityKey);

            if (hitBoxComponent != null)
            {
                hitBoxComponent.HitBoxGameObject.transform.position = position;
                hitBoxComponent.HitBoxGameObject.transform.rotation = rotation;

                var playerEntity = GetPlayerEntity(gameEntity);
				
                playerEntity.thirdPersonAnimator.UnityAnimator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
                var provider = SingletonManager.Get<HitBoxTransformProviderCache>().GetProvider(playerEntity.thirdPersonModel.Value);
                provider.Update(position, rotation);
                HitBoxGameObjectUpdater.Update(hitBoxComponent.HitBoxGameObject.transform, provider);
                playerEntity.thirdPersonAnimator.UnityAnimator.cullingMode = AnimatorCullingMode.CullUpdateTransforms;

                if (_logger.IsDebugEnabled)
                {
                    _logger.DebugFormat("client animator {0}", playerEntity.networkAnimator.ToStringExt());
                    StringBuilder s = new StringBuilder();
                    hitBoxComponent.HitBoxGameObject.transform.Recursively(t => s.Append("[n " + t.name + ", p " + t.position.ToStringExt() + ", r " + t.rotation.ToStringExt() + "]"));
                    _logger.DebugFormat("hitbox pos {0}, rot {1}, transforms {2}, ", position, rotation, s);
                }
            }
        }

      
      
        private PlayerEntity GetPlayerEntity(IGameEntity gameEntity)
        {
            var playerEntity = _playerContext.GetEntityWithEntityKey(gameEntity.EntityKey);
            return playerEntity;
        }
    }
}