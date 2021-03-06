﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XmlConfig;

namespace App.Shared.GameModules.Player.Appearance.AnimationEvent
{
    public class DropMagazine : IAnimationEventCallback
    {
        public void AnimationEventCallback(PlayerEntity player, string param, UnityEngine.AnimationEvent eventParam)
        {
            player.appearanceInterface.Appearance.DropMagazine();
        }
    }
}
