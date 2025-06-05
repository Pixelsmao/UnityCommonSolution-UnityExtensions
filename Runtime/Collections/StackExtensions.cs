using System;
using System.Collections.Generic;

namespace Pixelsmao.UnityCommonSolution.Extensions
{
    public static class StackExtensions
    {
        public static Stack<T> ForEach<T>(this Stack<T> self, Action<T> action)
        {
            foreach (var item in self)
            {
                action(item);
            }
            return self;
        }
    }
}