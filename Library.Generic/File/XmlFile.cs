using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Generic.File
{
    public static class XmlFile
    {
        private const int CRYPTO_KEY_LENGTH = 16;

        /// <summary>
        /// XML 파일을 읽어온다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="obj">public 접근 Class 객체</param>
        /// <exception cref="FileNotFoundException"></exception>
        public static void LoadXmlFile<T>(string filePath, out T obj)
        {
            System.IO.FileStream fs = null;

            try
            {
                string dirPath = Path.GetDirectoryName(filePath);
                if (Directory.Exists(dirPath) == false)
                {
                    Directory.CreateDirectory(dirPath);
                }

                if (System.IO.File.Exists(filePath) == false)
                {
                    obj = default(T);
                    throw new FileNotFoundException("File not found", filePath);
                }
                else
                {
                    fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open);
                    System.Xml.Serialization.XmlSerializer sz = new System.Xml.Serialization.XmlSerializer(typeof(T));
                    obj = (T)sz.Deserialize(fs);
                }

            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        /// <summary>
        /// 암호화된 XML 파일을 읽어온다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="key">알고리즘 상 문자열 길이 = 16개</param>
        /// <param name="obj">public 접근 Class 객체</param>
        /// <exception cref="FileNotFoundException"></exception>
        public static void LoadCryptoXmlFile<T>(string filePath, string key, out T obj)
        {
            System.IO.FileStream fs = null;

            try
            {
                string dirPath = Path.GetDirectoryName(filePath);
                if (Directory.Exists(dirPath) == false)
                {
                    Directory.CreateDirectory(dirPath);
                }

                if (System.IO.File.Exists(filePath) == false)
                {
                    obj = default(T);
                    throw new FileNotFoundException("File not found", filePath);
                }
                else
                {
                    fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open);
                    using (Aes aes = Aes.Create())
                    {
                        key = key.PadLeft(CRYPTO_KEY_LENGTH, 'x');
                        key = key.Substring(0, CRYPTO_KEY_LENGTH);

                        byte[] crytoKey = Encoding.UTF8.GetBytes(key);
                        byte[] crytoIV = Encoding.UTF8.GetBytes(key);
                        aes.Key = crytoKey;
                        aes.IV = crytoIV;
                        using (CryptoStream cs = new CryptoStream(fs, aes.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            System.Xml.Serialization.XmlSerializer sz = new System.Xml.Serialization.XmlSerializer(typeof(T));
                            obj = (T)sz.Deserialize(cs);
                        }
                    }
                }

            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        /// <summary>
        /// XML 파일을 저장한다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="obj"></param>
        public static void SaveXmlFile<T>(string filePath, T obj)
        {
            System.IO.FileStream fs = null;

            try
            {
                string dirPath = Path.GetDirectoryName(filePath);
                if (Directory.Exists(dirPath) == false)
                {
                    Directory.CreateDirectory(dirPath);
                }

                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create);
                System.Xml.Serialization.XmlSerializer sz = new System.Xml.Serialization.XmlSerializer(typeof(T));
                sz.Serialize(fs, obj);

            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

        /// <summary>
        /// 암호화된 XML 파일을 저장한다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <param name="key">알고리즘 상 문자열 길이 = 16개</param>
        /// <param name="obj">public 접근 Class 객체</param>
        public static void SaveCryptoXmlFile<T>(string filePath, string key, T obj)
        {
            System.IO.FileStream fs = null;

            try
            {
                string dirPath = Path.GetDirectoryName(filePath);
                if (Directory.Exists(dirPath) == false)
                {
                    Directory.CreateDirectory(dirPath);
                }

                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create);
                using (Aes aes = Aes.Create())
                {
                    key = key.PadLeft(CRYPTO_KEY_LENGTH, 'x');
                    key = key.Substring(0, CRYPTO_KEY_LENGTH);

                    byte[] crytoKey = Encoding.UTF8.GetBytes(key);
                    byte[] crytoIV = Encoding.UTF8.GetBytes(key);
                    aes.Key = crytoKey;
                    aes.IV = crytoIV;
                    using (CryptoStream cs = new CryptoStream(fs, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        System.Xml.Serialization.XmlSerializer sz = new System.Xml.Serialization.XmlSerializer(typeof(T));
                        sz.Serialize(cs, obj);
                    }
                }

            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }

    }
}
