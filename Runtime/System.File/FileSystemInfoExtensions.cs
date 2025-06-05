using System.IO;
namespace Pixelsmao.UnityCommonSolution.Extensions
{
    public static class FileSystemInfoExtensions
    {
        public static string FileNameWithoutExtension(this FileSystemInfo fileInfo) =>
            Path.GetFileNameWithoutExtension(fileInfo.Name);

        public static bool IsImageFile(this FileSystemInfo fileSystemInfo) => FileUtility.IsImageFile(fileSystemInfo);

        public static bool IsVideoFile(this FileSystemInfo fileSystemInfo) => FileUtility.IsVideoFile(fileSystemInfo );

        public static bool IsUnityVideoFile(this FileSystemInfo fileInfo) =>
            FileUtility.IsUnityVideoFile(fileInfo as FileInfo);

        public static bool IsTextFile(this FileSystemInfo fileSystemInfo) => FileUtility.IsTextFile(fileSystemInfo);

        /// <summary>
        /// 使用文件默认的程序打开文件
        /// </summary>
        public static void Open(this FileSystemInfo fileSystemInfo) => FileUtility.Open(fileSystemInfo);


        /// <summary>
        /// 在Windows资源管理器中选中文件
        /// </summary>
        public static void Select(this FileSystemInfo fileSystemInfo) => FileUtility.Select(fileSystemInfo);
    }
}