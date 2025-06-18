using System;
using System.Collections.Generic;

namespace Pixelsmao.UnityCommonSolution.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// 获取循环列表中的上一个合法索引
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="source">目标集合</param>
        /// <param name="currentIndex">当前索引位置</param>
        /// <returns>循环后的上一个索引</returns>
        /// <exception cref="ArgumentNullException">集合为null时抛出</exception>
        /// <exception cref="InvalidOperationException">空集合时抛出</exception>
        /// <exception cref="ArgumentOutOfRangeException">当前索引越界时抛出</exception>
        public static int GetCyclePreviousIndex<T>(this IList<T> source, int currentIndex)
        {
            // 参数合法性检查
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (source.Count == 0) throw new InvalidOperationException("Cannot cycle empty collection");
            if (currentIndex < 0 || currentIndex >= source.Count)
                throw new ArgumentOutOfRangeException(nameof(currentIndex),
                    $"Index must be in [0, {source.Count - 1}]");

            // 计算带负数保护的循环索引：(currentIndex - 1 + count) % count
            return (currentIndex - 1 + source.Count) % source.Count;
        }

        /// <summary>
        /// 获取循环列表中的下一个合法索引
        /// </summary>
        /// <typeparam name="T">集合元素类型</typeparam>
        /// <param name="source">目标集合</param>
        /// <param name="currentIndex">当前索引位置</param>
        /// <returns>循环后的下一个索引</returns>
        public static int GetCycleNextIndex<T>(this IList<T> source, int currentIndex)
        {
            // 相同的参数检查流程
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (source.Count == 0) throw new InvalidOperationException("Cannot cycle empty collection");
            if (currentIndex < 0 || currentIndex >= source.Count)
                throw new ArgumentOutOfRangeException(nameof(currentIndex),
                    $"Index must be in [0, {source.Count - 1}]");

            // 简化版循环索引：(currentIndex + 1) % count
            return (currentIndex + 1) % source.Count;
        }

        /// <summary>
        /// 获取当前索引在循环列表中的前一个元素
        /// </summary>
        /// <param name="source">目标集合</param>
        /// <param name="currentIndex">当前合法索引</param>
        public static T GetCyclePreviousElement<T>(this IList<T> source, int currentIndex)
        {
            // 复用已有的索引验证和计算逻辑
            var prevIndex = source.GetCyclePreviousIndex(currentIndex);
            return source[prevIndex];
        }

        /// <summary>
        /// 获取当前索引在循环列表中的下一个元素
        /// </summary>
        /// <param name="source"></param>
        /// <param name="currentIndex">当前合法索引</param>
        public static T GetCycleNextElement<T>(this IList<T> source, int currentIndex)
        {
            var nextIndex = source.GetCycleNextIndex(currentIndex);
            return source[nextIndex];
        }
        
        public static T GetCyclePreviousElement<T>(this IList<T> source, T currentItem)
        {
            var currentIndex = source.IndexOf(currentItem);
            return source.GetCyclePreviousElement(currentIndex);
        }

        public static T GetCycleNextElement<T>(this IList<T> source, T currentItem)
        {
            var currentIndex = source.IndexOf(currentItem);
            return source.GetCycleNextElement(currentIndex);
        }
    }
}