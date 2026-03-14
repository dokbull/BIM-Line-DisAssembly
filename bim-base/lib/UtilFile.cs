using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

public static class UtilFile
{
    /// <summary>
    /// 디렉토리를 체크 하며, 디렉토리가 없다면 디렉토리 생성
    /// </summary>
    /// <param name="m_path">디렉토리 PATH</param>
    public static void dirCheck(string m_path)
    {
        if (Directory.Exists(m_path) == false)
            Directory.CreateDirectory(m_path);
    }

    /// <summary>
    /// 파일 목록 가져오기
    /// </summary>
    /// <param name="list"></param>
    /// <param name="m_path">경로</param>
    /// <param name="extenstion">확장자 구분은 "," 단위로 한다. 
    /// ini 파일만 적용한다면 "ini"
    /// ini 및 exe 를 적용한다면 "ini,exe"
    /// 기본 옵션은 "*" 로 모두 가져오게 되어있음
    /// </param>
    public static void fileList(List<FileInfo> list, string m_path, string extenstion = "*")
    {
        list.Clear();

        string[] fileList = Directory.GetFiles(m_path);

        List<string> extenstionList = new List<string>();

        if (extenstion != "*")
        {
            string[] split = extenstion.Split(',');

            foreach (string ext in split)
            {
                extenstionList.Add(ext);
            }
        }

        foreach (string filename in fileList)
        {
            FileInfo info = new FileInfo(filename);

            bool search = false;

            if (extenstion != "*")
            {
                foreach (string ext in extenstionList)
                {
                    if (ext == info.Extension)
                    {
                        search = true;
                        break;
                    }
                }

                if (search == false)
                    continue;
            }

            list.Add(info);
        }
    }

    public static void copyDirectory(string sourceDirName, string destDirName, bool copySubDirs)
    {
        DirectoryInfo dir = new DirectoryInfo(sourceDirName);
        DirectoryInfo[] dirs = dir.GetDirectories();

        // If the source directory does not exist, throw an exception.
        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException(
                "Source directory does not exist or could not be found: "
                + sourceDirName);
        }

        // If the destination directory does not exist, create it.
        if (!Directory.Exists(destDirName))
        {
            Directory.CreateDirectory(destDirName);

        }


        // Get the file contents of the directory to copy.
        FileInfo[] files = dir.GetFiles();

        foreach (FileInfo file in files)
        {
            // Create the path to the new copy of the file.
            string temppath = Path.Combine(destDirName, file.Name);

            // Copy the file.
            file.CopyTo(temppath, false);
        }

        // If copySubDirs is true, copy the subdirectories.
        if (copySubDirs)
        {

            foreach (DirectoryInfo subdir in dirs)
            {
                // Create the subdirectory.
                string temppath = Path.Combine(destDirName, subdir.Name);

                // Copy the subdirectories.
                copyDirectory(subdir.FullName, temppath, copySubDirs);
            }
        }
    }

    public static void deleteAll(string strPath)
    {
        foreach (string Folder in Directory.GetDirectories(strPath))
            deleteAll(Folder); //재귀함수 호출

        foreach (string file in Directory.GetFiles(strPath))
        {
            FileInfo fi = new FileInfo(file);
            fi.Delete();
        }

        Directory.Delete(strPath);
    }
}
