using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Pixelsmao.UnityCommonSolution.Extensions
{
    public static class StringExtensions
    {
        #region Type Conversion

        /// <summary>
        /// 字符型转换为整型
        /// </summary>
        public static int ToInt(this string self) => int.Parse(self);

        /// <summary>
        /// 尝试字符型转换为整型：指定字符无法转换为整型时返回值为-1
        /// </summary>
        public static bool TryToInt(this string self, out int int32) => int.TryParse(self, out int32);

        /// <summary>
        /// 字符型转换为浮点类型
        /// </summary>
        public static float ToFloat(this string self) => float.Parse(self);

        /// <summary>
        /// 尝试字符型转换为浮点类型：指定字符无法转换为整型时返回值为-1
        /// </summary>
        public static bool TryToFloat(this string self, out float floatResult) => float.TryParse(self, out floatResult);

        /// <summary>
        /// 将字符转换为UTF7字节数组
        /// </summary>
        public static byte[] ToUTF7Bytes(this string self) => Encoding.UTF7.GetBytes(self);

        /// <summary>
        /// 将字符转换为UTF7格式
        /// </summary>
        public static string ToUTF7(this string self) => BitConverter.ToString(ToUTF7Bytes(self));

        /// <summary>
        /// 将字符转换为UTF8字节数组
        /// </summary>
        public static byte[] ToUTF8Bytes(this string self) => Encoding.UTF8.GetBytes(self);

        /// <summary>
        /// 将字符转换为UTF8格式
        /// </summary>
        public static string ToUTF8(this string self) => BitConverter.ToString(ToUTF8Bytes(self));

        /// <summary>
        /// 将字符转换为UTF32字节数组
        /// </summary>
        public static byte[] ToUTF32Bytes(this string self) => Encoding.UTF32.GetBytes(self);

        /// <summary>
        /// 将字符转换为UTF32格式
        /// </summary>
        public static string ToUTF32(this string self) => BitConverter.ToString(ToUTF32Bytes(self));

        /// <summary>
        /// 将字符转换为Unicode字节数组
        /// </summary>
        public static byte[] ToUnicodeBytes(this string self) => Encoding.Unicode.GetBytes(self);

        /// <summary>
        /// 将字符转换为Unicode格式
        /// </summary>
        public static string ToUnicode(this string self) => BitConverter.ToString(ToUnicodeBytes(self));

        /// <summary>
        /// 将字符转换为BigEndianUnicode字节数组
        /// </summary>
        public static byte[] ToBigEndianUnicodeBytes(this string self) => Encoding.BigEndianUnicode.GetBytes(self);

        /// <summary>
        /// 将字符转换为BigEndianUnicode格式
        /// </summary>
        public static string ToBigEndianUnicode(this string self) =>
            BitConverter.ToString(ToBigEndianUnicodeBytes(self));

        /// <summary>
        /// 将字符转换为GBK字节数组
        /// </summary>
        public static byte[] ToGBKBytes(this string self) => Encoding.GetEncoding("GBK").GetBytes(self);

        /// <summary>
        /// 将字符转换为GBK格式
        /// </summary>
        public static string ToGBK(this string self) => BitConverter.ToString(ToGBKBytes(self));

        public static string ToHMSTime(this string totalSeconds)
        {
            if (int.TryParse(totalSeconds, out var totalSecondsInt))
            {
                return totalSecondsInt.ToHMSTime();
            }

            if (float.TryParse(totalSeconds, out var totalSecondsFloat))
            {
            }

            return string.Empty;
        }

        /// <summary>
        /// 转换为十六进制字节数组，不是十六进制字符串时返回null
        /// </summary>
        public static byte[] ToHexadecimalBytes(this string self)
        {
            if (!self.IsHexadecimal()) return null;
            if (self.Length % 2 != 0) return null;
            return ConvertHexStringToByteArray(self, out var hexadecimalBytes) ? hexadecimalBytes : null;
        }

        /// <summary>
        /// 转换为十六进制字节数组，不是十六进制字符串时返回null
        /// </summary>
        public static bool TryToHexadecimalBytes(this string self, out byte[] hexadecimalBytes)
        {
            hexadecimalBytes = null;
            if (!self.IsHexadecimal() || self.Length % 2 != 0) return false;
            return ConvertHexStringToByteArray(self, out hexadecimalBytes);
        }

        private static bool ConvertHexStringToByteArray(string hexString, out byte[] byteArray)
        {
            byteArray = new byte[hexString.Length / 2];
            for (var i = 0; i < hexString.Length; i += 2)
            {
                var hexPair = hexString.Substring(i, 2);
                if (!byte.TryParse(hexPair, NumberStyles.HexNumber, null, out var byteValue))
                {
                    return false;
                }

                byteArray[i / 2] = byteValue;
            }

            return true;
        }

        /// <summary>
        /// 将字符转换为Type类型
        /// </summary>
        public static Type ToType(this string typeName)
        {
            typeName ??= string.Empty;
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var type = assembly.GetType(typeName);
                if (type != null) return type;
            }

            return assemblies.SelectMany(assembly => assembly.GetTypes()).FirstOrDefault(type =>
                type.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 将Windows路径格式中'\'替换为'/'，适用于基于Linux、Unix系统的路径格式
        /// </summary>
        public static string ToUnixPlatformPath(this string self) => self.Replace("\\", "/");


        /// <summary>
        /// 将适用于Linux、Unix的路径格式中'\'替换为'/'，转换为Windows默认路径格式
        /// </summary>
        public static string ToWindowsPlatformPath(this string self) => self.Replace("/", "\\");

        #endregion

        #region Extraction Operation

        /// <summary>
        /// 是否包含字母
        /// </summary>
        public static bool ContainAlphabet(this string self) => IsMatchContainRegex(self, Regexs.AnyAlphabet);

        /// <summary>
        /// 提取字母
        /// </summary>
        public static string ExtractAlphabet(this string self) => ExtractContentRegex(self, Regexs.AnyAlphabet);

        /// <summary>
        /// 尝试提取字母
        /// </summary>
        public static bool TryExtractAlphabet(this string self, out string result)
        {
            return TryExtractContentRegex(self, Regexs.AnyAlphabet, out result);
        }

        /// <summary>
        /// 是否包含大写字母
        /// </summary>
        public static bool ContainUppercaseAlphabet(this string self)
        {
            return IsMatchContainRegex(self, Regexs.EnglishUppercaseAlphabet);
        }

        /// <summary>
        /// 提取大写字母
        /// </summary>
        public static string ExtractUppercaseAlphabet(this string self)
        {
            return ExtractContentRegex(self, Regexs.EnglishUppercaseAlphabet);
        }

        /// <summary>
        /// 尝试提取大写字母
        /// </summary>
        public static bool TryExtractUppercaseAlphabet(this string self, out string result)
        {
            return TryExtractContentRegex(self, Regexs.EnglishUppercaseAlphabet, out result);
        }


        /// <summary>
        /// 是否包含小写字母
        /// </summary>
        public static bool ContainLowercaseAlphabet(this string self)
        {
            return IsMatchContainRegex(self, Regexs.EnglishLowercaseAlphabet);
        }

        /// <summary>
        /// 提取小写字母
        /// </summary>
        public static string ExtractLowercaseAlphabet(this string self)
        {
            return ExtractContentRegex(self, Regexs.EnglishLowercaseAlphabet);
        }

        /// <summary>
        /// 提取小写字母
        /// </summary>
        public static bool TryExtractLowercaseAlphabet(this string self, out string result)
        {
            return TryExtractContentRegex(self, Regexs.EnglishLowercaseAlphabet, out result);
        }


        /// <summary>
        /// 是否包含数字
        /// </summary>
        public static bool ContainNumeric(this string self) => IsMatchContainRegex(self, Regexs.AnyNumeric);

        /// <summary>
        /// 提取数字
        /// </summary>
        public static string ExtractNumeric(this string self) => ExtractContentRegex(self, Regexs.AnyNumeric);

        /// <summary>
        /// 尝试提取数字
        /// </summary>
        public static bool TryExtractNumeric(this string self, out string result)
        {
            return TryExtractContentRegex(self, Regexs.AnyNumeric, out result);
        }

        public static string ExtractFloatNumeric(this string self)
        {
            return MatchingFirst(self, Regexs.FloatType);
        }

        public static string[] ExtractFloatNumerics(this string self)
        {
            return MatchingAll(self, Regexs.FloatType);
        }

        public static string ExtractInt(this string self)
        {
            return MatchingFirst(self, Regexs.IntType);
        }

        public static string ExtractIntFloat(this string self)
        {
            return MatchingFirst(self, Regexs.IntFloat);
        }

        public static string[] ExtractIntFloats(this string self)
        {
            return MatchingAll(self, Regexs.IntFloat);
        }

        /// <summary>
        /// 提取字母和数字
        /// </summary>
        public static string ExtractAlphabetAndNumeric(this string self)
        {
            return ExtractContentRegex(self, Regexs.AnyAlphabetNumeric);
        }


        //未测试API
        /// <summary>
        /// 提取字符
        /// </summary>
        public static string ExtractCharacters(this string self, SymbolType symbolType)
        {
            switch (symbolType)
            {
                case SymbolType.SpecificSymbol:
                    return MatchRegex(self, Regexs.SpecificSymbol);
                case SymbolType.BasicSpecificSymbol:
                    return MatchRegex(self, Regexs.BasicSpecificSymbol);
                case SymbolType.OtherSpecificSymbol:
                    return MatchRegex(self, Regexs.OtherSpecificSymbol);
                case SymbolType.AdvancedSpecificSymbol:
                    return MatchRegex(self, Regexs.AdvancedSpecificSymbol);
                case SymbolType.PatternSpecificSymbol:
                    return MatchRegex(self, Regexs.PatternSpecificSymbol);
                case SymbolType.Punctuation:
                    return MatchRegex(self, Regexs.Punctuation);
                case SymbolType.BasicPunctuation:
                    return MatchRegex(self, Regexs.BasicPunctuation);
                case SymbolType.AdvancedPunctuation:
                    return MatchRegex(self, Regexs.AdvancedPunctuation);
                case SymbolType.UncommonPunctuation:
                    return MatchRegex(self, Regexs.UncommonPunctuation);
                case SymbolType.EnglishPunctuation:
                    return MatchRegex(self, Regexs.EnglishPunctuation);
                case SymbolType.ChinesePunctuation:
                    return MatchRegex(self, Regexs.ChinesePunctuation);
                case SymbolType.SpecialPunctuation:
                    return MatchRegex(self, Regexs.SpecialPunctuation);
                case SymbolType.EnglishSpecialPunctuation:
                    return MatchRegex(self, Regexs.EnglishSpecialPunctuation);
                case SymbolType.ChineseSpecialPunctuation:
                    return MatchRegex(self, Regexs.ChineseSpecialPunctuation);
                case SymbolType.NumericNumericalOrder:
                    return MatchRegex(self, Regexs.NumericNumericalOrder);
                case SymbolType.CircularNumericNumericalOrder:
                    return MatchRegex(self, Regexs.CircularNumericNumericalOrder);
                case SymbolType.BracketedNumericNumericalOrder:
                    return MatchRegex(self, Regexs.BracketedNumericNumericalOrder);
                case SymbolType.DotNumericalOrder:
                    return MatchRegex(self, Regexs.DotNumericalOrder);
                case SymbolType.RomanNumericalOrder:
                    return MatchRegex(self, Regexs.RomanNumericalOrder);
                case SymbolType.PieNumericalOrder:
                    return MatchRegex(self, Regexs.PieNumericalOrder);
                case SymbolType.ChineseLowercaseNumericalOrder:
                    return MatchRegex(self, Regexs.ChineseLowercaseNumericalOrder);
                case SymbolType.ChineseUppercaseNumericalOrder:
                    return MatchRegex(self, Regexs.ChineseUppercaseNumeralsOrder);
                case SymbolType.BracketedLowercaseChineseNumericalOrder:
                    return MatchRegex(self, Regexs.BracketedLowercaseChineseNumeralsOrder);
                case SymbolType.MathematicalSymbol:
                    return MatchRegex(self, Regexs.MathematicalSymbol);
                case SymbolType.BasicMathematicalOperator:
                    return MatchRegex(self, Regexs.BasicMathematicalOperator);
                case SymbolType.AdvancedMathematicalOperator:
                    return MatchRegex(self, Regexs.AdvancedMathematicalOperator);
                case SymbolType.MathematicalUnit:
                    return MatchRegex(self, Regexs.MathematicalUnit);
                case SymbolType.GrecoLatinSymbol:
                    return MatchRegex(self, Regexs.GrecoLatinSymbol);
                case SymbolType.GreekAlphabet:
                    return MatchRegex(self, Regexs.GreekAlphabet);
                case SymbolType.LatinAlphabet:
                    return MatchRegex(self, Regexs.LatinAlphabet);
                case SymbolType.SpellingPhoneticNotationSymbol:
                    return MatchRegex(self, Regexs.SpellingPhoneticNotationSymbol);
                case SymbolType.SpellingSymbol:
                    return MatchRegex(self, Regexs.SpellingSymbol);
                case SymbolType.PhoneticNotationSymbol:
                    return MatchRegex(self, Regexs.PhoneticNotationSymbol);
                case SymbolType.ChineseCharacter:
                    return MatchRegex(self, Regexs.ChineseCharacter);
                case SymbolType.Chinese:
                    return MatchRegex(self, Regexs.Chinese);
                case SymbolType.ChineseNumeralsSymbol:
                    return MatchRegex(self, Regexs.RomanNumericalOrder);
                case SymbolType.ChineseLowercaseNumerals:
                    return MatchRegex(self, Regexs.ChineseLowercaseNumerals);
                case SymbolType.ChineseUppercaseNumerals:
                    return MatchRegex(self, Regexs.ChineseUppercaseNumerals);
                case SymbolType.ChineseRarelyUsedCharacters:
                    return MatchRegex(self, Regexs.ChineseRarelyUsedCharacters);
                case SymbolType.ChineseCharacterRadicals:
                    return MatchRegex(self, Regexs.ChineseCharacterRadicals);
                case SymbolType.ChineseBasicCharacterRadicals:
                    return MatchRegex(self, Regexs.ChineseBasicCharacterRadicals);
                case SymbolType.ChineseAdvancedCharacterRadicals:
                    return MatchRegex(self, Regexs.ChineseAdvancedCharacterRadicals);
                case SymbolType.English:
                    return MatchRegex(self, Regexs.English);
                case SymbolType.EnglishLowercaseAlphabet:
                    return MatchRegex(self, Regexs.EnglishLowercaseAlphabet);
                case SymbolType.EnglishUppercaseAlphabet:
                    return MatchRegex(self, Regexs.EnglishUppercaseAlphabet);
                case SymbolType.EnglishPhoneticAlphabet:
                    return MatchRegex(self, Regexs.EnglishPhoneticAlphabet);
                case SymbolType.EnglishVowelPhonetic:
                    return MatchRegex(self, Regexs.EnglishVowelPhonetic);
                case SymbolType.EnglishAuxiliaryPhonetic:
                    return MatchRegex(self, Regexs.EnglishAuxiliaryPhonetic);
                case SymbolType.Japanese:
                    return MatchRegex(self, Regexs.Japanese);
                case SymbolType.JapaneseHiragana:
                    return MatchRegex(self, Regexs.JapaneseHiragana);
                case SymbolType.JapaneseKatakana:
                    return MatchRegex(self, Regexs.JapaneseKatakana);
                case SymbolType.JapanesePunctuation:
                    return MatchRegex(self, Regexs.JapanesePunctuation);
                case SymbolType.Korean:
                    return MatchRegex(self, Regexs.Korean);
                case SymbolType.KoreanBasicSymbol:
                    return MatchRegex(self, Regexs.KoreanBasicSymbol);
                case SymbolType.KoreanAdvancedSymbol:
                    return MatchRegex(self, Regexs.KoreanAdvancedSymbol);
                case SymbolType.RussianAlphabet:
                    return MatchRegex(self, Regexs.RussianAlphabet);
                case SymbolType.RussianBasicAlphabet:
                    return MatchRegex(self, Regexs.RussianBasicAlphabet);
                case SymbolType.RussianAdvancedAlphabet:
                    return MatchRegex(self, Regexs.RussianAdvancedAlphabet);
                case SymbolType.Tabs:
                    return MatchRegex(self, Regexs.Tabs);
                case SymbolType.BasicTabs:
                    return MatchRegex(self, Regexs.BasicTabs);
                case SymbolType.ConventionalTabs:
                    return MatchRegex(self, Regexs.ConventionalTabs);
                case SymbolType.AdvancedTabs:
                    return MatchRegex(self, Regexs.AdvancedTabs);
                default:
                    throw new ArgumentOutOfRangeException(nameof(symbolType), symbolType, null);
            }
        }


        /// <summary>
        /// 提取Unicode中文字符(仅中文字符，不包含标点符号、空格、序号和偏旁部首)
        /// </summary>
        public static string ExtractChineseCharacter(this string self)
        {
            return ExtractContentRegex(self, Regexs.ChineseCharacter);
        }

        /// <summary>
        /// 尝试提取Unicode中文字符(仅中文字符，不包含标点符号、空格、序号和偏旁部首)
        /// </summary>
        public static bool TryExtractChineseCharacter(this string self, out string result)
        {
            return TryExtractContentRegex(self, Regexs.ChineseCharacter, out result);
        }

        /// <summary>
        /// 提取中文(包含Unicode中文字符、标点符号、数字序号、空格、偏旁部首和部分生僻字)
        /// </summary>
        public static string ExtractChinese(this string self)
        {
            return ExtractContentRegex(self, Regexs.Chinese);
        }

        //TODO 正则表达式整理完成

        #endregion

        #region Bool Judgment

        /// <summary>
        /// 是否为十六进制字符串
        /// </summary>
        public static bool IsHexadecimal(this string self)
        {
            return !string.IsNullOrEmpty(self) && Regex.IsMatch(self, Regexs.Hexadecimal);
        }

        /// <summary>
        /// 是有效的Window路径
        /// </summary>
        public static bool IsValidWindowsPath(string path)
        {
            return new Regex(Regexs.WindowsPathWithUNC).IsMatch(path);
        }

        /// <summary>
        /// 是否为标点符号
        /// </summary>
        public static bool IsPunctuation(this char c) => char.IsPunctuation(c);

        /// <summary>
        /// 是否包含中文字符(Unicode)
        /// </summary>
        public static bool ContainChineseCharacter(this string self) =>
            Regex.IsMatch(self, Regexs.ChineseCharacter);

        /// <summary>
        /// 是否是中文字符(Unicode)
        /// </summary>
        public static bool IsChineseCharacter(this char c) =>
            Regex.IsMatch(c.ToString(), Regexs.ChineseCharacter);

        #endregion

        #region Add Operation

        /// <summary>
        /// 添加缩进
        /// </summary>
        public static string AddIndent(this string self, int indentSize = 2)
        {
            if (string.IsNullOrEmpty(self)) return self;
            StringBuilder builder = new StringBuilder();
            return builder.AppendIndent(indentSize).Append(self).ToString();
        }

        /// <summary>
        /// 在每个字符之间添加空格
        /// </summary>
        public static string AddSpacesBetweenEachCharacter(this string self, int spacing = 1) =>
            string.IsNullOrEmpty(self) ? self : string.Join(new string(' ', spacing), self.ToCharArray());


        /// <summary>
        /// 在字符串前方添加当前时间戳
        /// </summary>
        public static string AddTimestamp(this string self)
        {
            return $"【{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}】{self}";
        }

        /// <summary>
        /// 在字符串前方添加当前时间戳
        /// </summary>
        /// <param name="self">字符串</param>
        /// <param name="format">时间戳格式化格式</param>
        /// <returns></returns>
        public static string AddTimestamp(this string self, string format)
        {
            return $"【{DateTime.Now.ToString(format)}】{self}";
        }

        #endregion

        #region Text Operation

        /// <summary>
        /// 在首行和句号(中文或英文)后换行并添加首行缩进
        /// </summary>
        /// TODO 若句号后跟空行则无法在准确位置添加
        public static string AutoPeriodWrapIndent(this string self, int indentSize = 2)
        {
            if (string.IsNullOrEmpty(self)) return self;
            var punctuationIndices = Enumerable.Range(0, self.Length)
                .Where(i => self[i] == '。' || self[i] == '.')
                .ToList();

            var builder = new StringBuilder();
            var previousIndex = 0;

            builder.AppendIndent(indentSize);
            foreach (var currentIndex in punctuationIndices)
            {
                // 复制当前索引之前的部分
                builder.Append(self.Substring(previousIndex, currentIndex - previousIndex));

                // 添加句号本身
                builder.Append(self[currentIndex]);

                // 添加换行符
                builder.Append('\n');

                // 添加缩进
                builder.AppendIndent(indentSize);

                // 更新上一个索引为当前索引之后的位置
                previousIndex = currentIndex + 1;
            }

            // 添加最后一个句号之后的内容
            if (previousIndex < self.Length)
            {
                builder.Append(self.Substring(previousIndex));
            }

            return builder.ToString();
        }

        public static StringBuilder AppendIndent(this StringBuilder builder, int indentSize = 2)
        {
            builder.Append("<color=#FFF-FFF00>");
            for (int j = 0; j < indentSize; j++)
            {
                builder.Append("　");
            }

            builder.Append("</color>");
            return builder;
        }

        /// <summary>
        /// 强制换行
        /// </summary>
        public static string ForceWrap(this string self, int maximumCharacterInLine)
        {
            if (string.IsNullOrEmpty(self)) return self;

            var builder = new StringBuilder();
            var previousIndex = 0;
            // 计算当前索引直到到达字符串末尾
            for (var currentIndex = maximumCharacterInLine;
                 currentIndex < self.Length;
                 currentIndex += maximumCharacterInLine)
            {
                // 如果当前位置超过字符串长度，则设置为字符串长度
                if (currentIndex > self.Length)
                    currentIndex = self.Length;

                // 添加从 previousIndex 到 currentIndex 的子字符串
                builder.Append(self.Substring(previousIndex, currentIndex - previousIndex));

                // 添加换行符
                builder.Append("\n");

                // 更新 previousIndex
                previousIndex = currentIndex;
            }

            // 添加最后一部分文本
            if (previousIndex < self.Length)
            {
                builder.Append(self.Substring(previousIndex));
            }

            return builder.ToString();
        }

        #endregion

        #region Remove Operation

        /// <summary>
        /// 删除空白字符(包括空格、制表符、换行符等)
        /// </summary>
        public static string RemoveBlank(this string self)
        {
            return string.IsNullOrEmpty(self) ? self : Regex.Replace(self, Regexs.AnyBlank, "");
        }

        /// <summary>
        /// 删除空格字符
        /// </summary>
        public static string RemoveSpace(this string self)
        {
            return string.IsNullOrEmpty(self) ? self : self.Replace(" ", "");
        }

        /// <summary>
        /// 删除数字
        /// </summary>
        public static string RemoveNumeric(this string self)
        {
            return string.IsNullOrEmpty(self) ? self : Regex.Replace(self, Regexs.AnyNumeric, "");
        }

        /// <summary>
        /// 删除字母(忽略大小写)
        /// </summary>
        public static string RemoveAlphabet(this string self)
        {
            return string.IsNullOrEmpty(self) ? self : Regex.Replace(self, Regexs.AnyAlphabet, "");
        }

        /// <summary>
        /// 删除大写字母
        /// </summary>
        public static string RemoveUppercaseAlphabet(this string self)
        {
            return string.IsNullOrEmpty(self) ? self : Regex.Replace(self, Regexs.AnyUpperAlphabet, "");
        }

        /// <summary>
        /// 删除小写字母
        /// </summary>
        public static string RemoveLowercaseAlphabet(this string self)
        {
            return string.IsNullOrEmpty(self) ? self : Regex.Replace(self, Regexs.AnyLowerAlphabet, "");
        }

        /// <summary>
        /// 删除字母和数字
        /// </summary>
        public static string RemoveAlphabetNumeric(this string self)
        {
            return string.IsNullOrEmpty(self) ? self : Regex.Replace(self, Regexs.AnyAlphabetNumeric, "");
        }

        /// <summary>
        /// 删除标点符号
        /// </summary>
        public static string RemovePunctuation(this string self)
        {
            return string.IsNullOrEmpty(self) ? self : Regex.Replace(self, Regexs.Punctuation, "");
        }

        #endregion

        private static string MatchRegex(string sourceString, string pattern)
        {
            var catchCharacters = new StringBuilder();
            foreach (Match match in new Regex(pattern).Matches(sourceString))
            {
                catchCharacters.Append(match.Value);
            }

            return catchCharacters.ToString();
        }

        /// <summary>
        /// 提取字符串中符合正则表达式的第一个结果
        /// </summary>
        private static string MatchingFirst(this string self, string pattern)
        {
            return new Regex(pattern).Match(self).Value;
        }

        /// <summary>
        /// 提取字符串中符合正则表达式的最后一个结果
        /// </summary>
        private static string MatchingLast(this string self, string pattern)
        {
            return new Regex(pattern).Match(self).Value;
        }

        /// <summary>
        /// 提取字符串中符合正则表达式的所有结果
        /// </summary>
        private static string[] MatchingAll(this string self, string pattern)
        {
            var matches = new Regex(pattern).Matches(self);
            var result = new string[matches.Count];
            for (var i = 0; i < matches.Count; i++)
            {
                result[i] = matches[i].Value;
            }

            return result;
        }

        /// <summary>
        /// 提取字符串中符合正则表达式的所有结果并合并成一个
        /// </summary>
        /// <param name="self"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private static string ExtractAllInOne(this string self, string pattern)
        {
            var resultBuilder = new StringBuilder();
            foreach (Match match in new Regex(pattern).Matches(self))
            {
                resultBuilder.Append(match.Value);
            }

            return resultBuilder.ToString();
        }


        private static string ExtractContentRegex(string sourceString, string pattern)
        {
            var resultBuilder = new StringBuilder();
            foreach (Match match in new Regex(pattern).Matches(sourceString))
            {
                resultBuilder.Append(match.Value);
            }

            return resultBuilder.ToString();
        }

        private static bool TryExtractContentRegex(string sourceString, string pattern, out string result)
        {
            result = string.Empty;
            if (!IsMatchContainRegex(sourceString, pattern)) return false;
            var resultBuilder = new StringBuilder();
            foreach (Match match in new Regex(pattern).Matches(sourceString))
            {
                resultBuilder.Append(match.Value);
            }

            result = resultBuilder.ToString();
            return true;
        }

        private static string[] ExtractContentsRegex(string sourceString, string pattern)
        {
            return null;
        }

        private static bool IsMatchContainRegex(string sourceString, string pattern)
        {
            return new Regex(pattern).IsMatch(sourceString);
        }
    }
}