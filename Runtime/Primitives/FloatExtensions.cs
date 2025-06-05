using System;
using UnityEngine;


namespace Pixelsmao.UnityCommonSolution.Extensions
{
    public static class FloatExtensions
    {
        /// <summary>
        /// 与目标值是否相等(约等于)
        /// </summary>
        /// <param name="self">自己</param>
        /// <param name="target">目标值</param>
        /// <param name="nearAccuracy">逼近准确度(精度误差)</param>
        public static bool EqualApproximately(this float self, float target, float nearAccuracy)
        {
            return Mathf.Abs(self - target) <= nearAccuracy;
        }

        /// <summary>
        /// 是否大于或约等于目标值
        /// </summary>
        /// <param name="self">自己</param>
        /// <param name="target">目标值</param>
        /// <param name="nearAccuracy">逼近准确度(精度误差)</param>
        public static bool EqualApproximatelyOrGreater(this float self, float target, float nearAccuracy)
        {
            return self > target || EqualApproximately(self, target, nearAccuracy);
        }

        /// <summary>
        /// 是否小于或约等于目标值
        /// </summary>
        /// <param name="self">自己</param>
        /// <param name="target">目标值</param>
        /// <param name="nearAccuracy">逼近准确度(精度误差)</param>
        public static bool EqualApproximatelyOrLess(this float self, float target, float nearAccuracy)
        {
            return self < target || EqualApproximately(self, target, nearAccuracy);
        }

        /// <summary>
        /// 限制一个值在指定的最小值和最大值之间
        /// </summary>
        /// <param name="self">自己</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        public static float Clamp(this float self, float min, float max)
        {
            if (self < min) return min;
            return self > max ? max : self;
        }

        /// <summary>
        /// 将一个范围内的值映射到另一个范围内。
        /// </summary>
        /// <param name="self">自己</param>
        /// <param name="fromSource">源起始值</param>
        /// <param name="toSource">源终点值</param>
        /// <param name="fromTarget">目标起始值</param>
        /// <param name="toTarget">目标终点值</param>
        /// <returns></returns>
        public static float Map(this float self, float fromSource, float toSource, float fromTarget, float toTarget)
        {
            return (self - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
        }

        /// <summary>
        /// 四舍五入到指定位数。
        /// </summary>
        /// <param name="self">自己</param>
        /// <param name="decimalPlaces">保留小数点位数</param>
        /// <returns></returns>
        public static float RoundToDecimalPlaces(this float self, int decimalPlaces)
        {
            var power = Mathf.Pow(10, decimalPlaces);
            return Mathf.Round(self * power) / power;
        }

        /// <summary>
        /// 获在目标范围位置的0-1归一化值，当大于、小于极值时返回极值(0或1)，在范围中时不返回极值(0或1)
        /// </summary>
        /// <param name="self">自己</param>
        /// <param name="minValue">范围最大值</param>
        /// <param name="maxValue">范围最小值</param>
        public static float Normalize(this float self, float minValue, float maxValue)
        {
            if (maxValue <= minValue) throw new ArgumentException("Max value must be greater than min value.");
            if (self > maxValue) return 1;
            if (self < minValue) return 1;
            return (self - minValue) / (maxValue - minValue);
        }

        /// <summary>
        /// 获在目标范围位置的0-1归一化值，当满足大于、小于或接近极值(最大值和最小值)的误差范围时返回归一化极值(0或1)。
        /// </summary>
        /// <param name="self">自己</param>
        /// <param name="minValue">范围最大值</param>
        /// <param name="maxValue">范围最小值</param>
        /// <param name="nearAccuracy">逼近准确度(精度误差)</param>
        public static float NormalizeExtremum(this float self, float minValue, float maxValue, float nearAccuracy)
        {
            if (maxValue <= minValue) throw new ArgumentException("Max value must be greater than min value.");
            if (self.EqualApproximatelyOrGreater(maxValue, nearAccuracy)) return 1;
            if (self.EqualApproximatelyOrLess(minValue, nearAccuracy)) return 0;
            return (self - minValue) / (maxValue - minValue);
        }

        public static string ToHMSTime(this float totalSeconds, int decimalPlaces = 2)
        {
            if (decimalPlaces < 0 || decimalPlaces > 3)
                throw new System.ArgumentException("decimalPlaces must be between 0 and 3");
            var totalSecInt = Mathf.FloorToInt(totalSeconds);
            var fraction = totalSeconds - totalSecInt;
            var multiplier = (int)Mathf.Pow(10, decimalPlaces);
            var decimals = Mathf.RoundToInt(fraction * multiplier);

            // 处理四舍五入后的进位
            if (decimals >= multiplier)
            {
                decimals -= multiplier;
                totalSecInt += 1;
            }

            // 重新计算时分秒
            var hours = totalSecInt / 3600;
            var remaining = totalSecInt % 3600;
            var minutes = remaining / 60;
            var seconds = remaining % 60;

            // 格式字符串
            if (decimalPlaces <= 0) return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
            var decimalPart = decimals.ToString().PadLeft(decimalPlaces, '0');
            return $"{hours:D2}:{minutes:D2}:{seconds:D2}.{decimalPart}";
        }
    }
}