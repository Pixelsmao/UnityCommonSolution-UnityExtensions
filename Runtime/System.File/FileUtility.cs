using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Pixelsmao.UnityCommonSolution.Extensions
{
    public static class FileUtility
    {
        private static readonly HashSet<string> imageExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg", ".jpeg", ".png", ".bmp", ".tiff", ".gif", ".svg", ".raw", ".webp"
        };

        private static readonly HashSet<string> videoExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".mkv", ".wmv", ".avi", ".mpeg", ".mpg", ".rmvb", ".flv", ".mp4", ".mov"
        };

        private static readonly HashSet<string> unityVideoExtensions =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                ".mkv", ".wmv", ".avi", ".webm", ".mp4", ".mov"
            };

        private static readonly HashSet<string> textExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".txt", ".xml", ".json", ".xlsx", ".xls", ".csv", ".ini", ".config"
        };


        public static bool IsImageFile(FileInfo fileInfo)
        {
            return imageExtensions.Contains(fileInfo.Extension.ToLower());
        }

        public static bool IsVideoFile(FileInfo fileInfo)
        {
            return videoExtensions.Contains(fileInfo.Extension.ToLower());
        }

        public static bool IsUnityVideoFile(FileInfo fileInfo)
        {
            return unityVideoExtensions.Contains(fileInfo.Extension.ToLower());
        }

        public static bool IsTextFile(FileInfo fileInfo)
        {
            return textExtensions.Contains(fileInfo.Extension.ToLower());
        }

        public static bool IsImageFile(FileSystemInfo fileSystemInfo)
        {
            return imageExtensions.Contains(fileSystemInfo.Extension.ToLower());
        }

        public static bool IsVideoFile(FileSystemInfo fileSystemInfo)
        {
            return videoExtensions.Contains(fileSystemInfo.Extension.ToLower());
        }

        public static bool IsUnityVideoFile(FileSystemInfo fileSystemInfo)
        {
            return unityVideoExtensions.Contains(fileSystemInfo.Extension.ToLower());
        }

        public static bool IsTextFile(FileSystemInfo fileSystemInfo)
        {
            return textExtensions.Contains(fileSystemInfo.Extension.ToLower());
        }

        /// <summary>
        /// 使用文件默认的程序打开文件
        /// </summary>
        public static void Open(FileInfo fileInfo) => Process.Start(fileInfo.FullName);


        /// <summary>
        /// 在Windows资源管理器中选中文件
        /// </summary>
        public static void Select(FileInfo fileInfo) =>
            Process.Start("explorer.exe", "/select," + fileInfo.FullName);

        /// <summary>
        /// 使用文件默认的程序打开文件
        /// </summary>
        public static void Open(FileSystemInfo fileSystemInfo) => Process.Start(fileSystemInfo.FullName);


        /// <summary>
        /// 在Windows资源管理器中选中文件
        /// </summary>
        public static void Select(FileSystemInfo fileSystemInfo) =>
            Process.Start("explorer.exe", "/select," + fileSystemInfo.FullName);
    }
}