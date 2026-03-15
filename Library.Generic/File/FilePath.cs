using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Generic.File
{
    public static class FilePath
    {
        /// <summary>
        /// 실행된 Application의 Assembly 경로를 가져온다.
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationPath()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location).TrimEnd('\\');
        }

        /// <summary>
        /// 실행된 Application의 Assembly 경로를 기준으로 Database 폴더 내의 파일 경로를 가져온다.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFilePathInDatabaseFolder(string fileName)
        {
            return Path.Combine(GetDatabaseFolder(), fileName);
        }

        /// <summary>
        /// 실행된 Application의 Assembly 경로를 기준으로 Database 폴더 경로를 가져온다.
        /// </summary>
        /// <returns></returns>
        public static string GetDatabaseFolder()
        {
            return Path.Combine(GetApplicationPath(), "Database");
        }

        /// <summary>
        /// 실행된 Application의 Assembly 경로를 기준으로 Database 폴더 내 파일 경로를 가져온다.
        /// </summary>
        /// <returns></returns>
        public static string GetDatabaseFile(string fileName)
        {
            return Path.Combine(GetDatabaseFolder(), "fileName");
        }

        /// <summary>
        /// 실행된 Application의 Assembly 경로를 기준으로 지정된 폴더 경로를 가져온다.
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public static string GetFolder(string folderName)
        {
            return Path.Combine(GetApplicationPath(), folderName);
        }

        /// <summary>
        /// 실행된 Application의 Assembly 경로를 기준으로 지정된 폴더 내의 파일 경로를 가져온다.
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetFilePath(string folderName, string filename)
        {
            return Path.Combine(GetFolder(folderName), filename);
        }
    }
}
