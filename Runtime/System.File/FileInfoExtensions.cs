using System.IO;

namespace Pixelsmao.UnityCommonSolution.Extensions
{
    public static class FileInfoExtensions
    {
        public static string FileNameWithoutExtension(this FileInfo fileInfo) =>
            Path.GetFileNameWithoutExtension(fileInfo.Name);

        public static bool IsImageFile(this FileInfo fileInfo) => FileUtility.IsImageFile(fileInfo);
        
        public static bool IsVideoFile(this FileInfo fileInfo) => FileUtility.IsVideoFile(fileInfo);

        public static bool IsUnityVideoFile(this FileInfo fileInfo) => FileUtility.IsUnityVideoFile(fileInfo);

        public static bool IsTextFile(this FileInfo fileInfo) => FileUtility.IsTextFile(fileInfo);

        /// <summary>
        /// 使用文件默认的程序打开文件
        /// </summary>
        public static void Open(this FileInfo fileInfo) => FileUtility.Open(fileInfo);


        /// <summary>
        /// 在Windows资源管理器中选中文件
        /// </summary>
        public static void Select(this FileInfo fileInfo) => FileUtility.Select(fileInfo);
    }
}