using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

public class CFileManager
{
    string m_path = "";
    string m_header = "";

    public CFileManager(string path)
    {
        m_path = path;
    }

    public void setHeader(string header)
    {
        m_header = header;
    }

    public static void makeBackupFile(string filename, bool deleteSrcFile = true)
    {
        if (File.Exists(filename))
        {
            FileInfo fi = new FileInfo(filename);

            string backupPath = fi.DirectoryName;
            backupPath += "\\backup\\";

            if (Directory.Exists(backupPath) == false)
                Directory.CreateDirectory(backupPath);

            string nowString = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            string backupFileName = backupPath + "BAK_" + nowString + "_" + fi.Name;

            if (File.Exists(backupFileName) == false)
                File.Copy(filename, backupFileName);
            
            if (deleteSrcFile)
                File.Delete(filename);
        }
    }

    public List<string> readAll(bool UTF8 = false)
    {
        if (File.Exists(m_path) == false)
            return null;

        List<string> result = new List<string>();

        if (UTF8)
        {
            using (StreamReader reader = new StreamReader(m_path, System.Text.Encoding.UTF8))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }

        }
        else
        {
            using (StreamReader reader = new StreamReader(m_path, System.Text.Encoding.Default))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }
        }

        return result;
    }

    public List<string[]> readAll(params char[] separator)
    {
        if (File.Exists(m_path) == false)
            return null;

        List<string[]> result = new List<string[]>();

        using (StreamReader reader = new StreamReader(m_path, System.Text.Encoding.Default))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] splitStr = line.Split(separator);
                result.Add(splitStr);
            }
        }

        return result;
    }

    public List<string[]> readAllUTF8(params char[] separator)
    {
        if (File.Exists(m_path) == false)
            return null;

        List<string[]> result = new List<string[]>();

        using (StreamReader reader = new StreamReader(m_path, System.Text.Encoding.UTF8))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] splitStr = line.Split(separator);
                result.Add(splitStr);
            }
        }

        return result;
    }

    public bool write(string text, bool continueWrite = true)
    {
        bool writeHeader = false;

        FileInfo fi = new FileInfo(m_path);

        if (Directory.Exists(fi.Directory.FullName) == false)
            Directory.CreateDirectory(fi.Directory.FullName);

        if (File.Exists(m_path) == false)
            writeHeader = true;

        using (StreamWriter writer = new StreamWriter(m_path, continueWrite, System.Text.Encoding.UTF8))
        {
            if (writeHeader && m_header != "")
                writer.WriteLine(m_header);

            writer.WriteLine(text);
        }

        return true;
    }

    public bool writeAll(List<string[]> list, bool continueWrite = true)
    {
        List<string> textList = new List<string>();
        string text = "";

        for (int i = 0; i < list.Count; i++)
        {
            text = "";
            for (int j = 0; j < list[i].Length; j++)
            {
                text += list[i][j] + ",";
            }

            textList.Add(text);
        }

        return writeAll(textList, continueWrite);
    }
    public bool writeAll(List<string> list, bool continueWrite = true)
    {
        bool writeHeader = false;

        FileInfo fi = new FileInfo(m_path);

        if (Directory.Exists(fi.Directory.FullName) == false)
            Directory.CreateDirectory(fi.Directory.FullName);

        if (File.Exists(m_path) == false)
            writeHeader = true;

        using (StreamWriter writer = new StreamWriter(m_path, continueWrite, System.Text.Encoding.UTF8))
        {
            if (writeHeader && m_header != "")
                writer.WriteLine(m_header);

            foreach (string text in list)
            {
                writer.WriteLine(text);
            }
        }

        return true;
    }

}
