﻿using UnityEngine;

namespace Pixelsmao.UnityCommonSolution.Extensions
{
    public static class BehaviourExtensions
    {
        public static T Enable<T>(this T self) where T : Behaviour
        {
            self.enabled = true;
            return self;
        }
        public static T Disable<T>(this T self) where T : Behaviour
        {
            self.enabled = false;
            return self;
        }
    }
}