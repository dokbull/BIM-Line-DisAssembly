using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.IO;

public class CFtp
{
    public string m_uri; // "ftp://sample.iptime.org/folder/abcd.txt"
    public string m_userID;
    public string m_password;

    public CFtp(string uri, string userID, string password)
    {
        m_uri = uri;
        m_userID = userID;
        m_password = password;
    }

    /// <summary>
    /// ftp 파일 목록 조회
    /// </summary>
    /// <param name="folder"></param>
    /// <returns></returns>
    public List<string> fileList(string folder)
    {
        try
        {
            string uri = m_uri + "/" + folder;

            FtpWebRequest request = WebRequest.Create(uri) as FtpWebRequest;

            request.Credentials = new NetworkCredential(m_userID, m_password);
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream());

            List<string> list = new List<string>();

            while (true)
            {
                string fn = sr.ReadLine();

                if (string.IsNullOrEmpty(fn))
                    break;

                list.Add(fn);
            }

            sr.Close();
            return list;
        }
        catch (Exception ex)
        {
            Debug.warning("CFtp::fileList error. uri:" + m_uri + " folder:" + folder +
                " reason:" + ex.Message);
            return null;
        }
    }

    public bool uploadFile(string filename, string uploadFileName)
    {
        try
        {
            string uri = m_uri + "/" + uploadFileName;
            FtpWebRequest request = WebRequest.Create(uri) as FtpWebRequest;

            request.Credentials = new NetworkCredential(m_userID, m_password);
            request.Method = WebRequestMethods.Ftp.UploadFile;

            byte[] data;
            using (StreamReader reader = new StreamReader(filename))
            {
                data = Encoding.UTF8.GetBytes(reader.ReadToEnd());
            }

            request.ContentLength = data.Length;
            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
            }

            using (FtpWebResponse resp = (FtpWebResponse)request.GetResponse())
            {
                return true;
            }
        }
        catch (Exception /*ex*/)
        {
            return false;
        }
    }

    public bool downloadFile(string ftpFileName, string saveFileName)
    {
        try
        {
            string uri = m_uri + "/" + ftpFileName;
            FtpWebRequest request = WebRequest.Create(uri) as FtpWebRequest;

            request.Credentials = new NetworkCredential(m_userID, m_password);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = request.GetResponse() as FtpWebResponse;

            Stream stream = response.GetResponseStream();
            FileStream fs = new FileStream(saveFileName, FileMode.Create, FileAccess.Write);

            byte[] buff = new byte[1024];

            while (true)
            {
                int byteCount = stream.Read(buff, 0, buff.Length);

                if (byteCount == 0)
                    break;

                fs.Write(buff, 0, byteCount);
            }

            fs.Close();
            stream.Close();
        }
        catch (Exception ex)
        {
            Debug.warning("CFtp::downloadFile error. uri:" + m_uri + " ftpFileName:" + ftpFileName +
                " saveFileName:" + saveFileName + " reason:" + ex.Message);
            return false;
        }

        return true;
    }

    /// <summary>
    /// ftp 파일 삭제
    /// </summary>
    /// <param name="filename">폴더명을 포함한 파일 이름 디렉토리간 구분은 "/" </param>
    /// <returns></returns>
    public bool deleteFile(string filename)
    {
        try
        {
            string uri = m_uri + "/" + filename;

            FtpWebRequest request = WebRequest.Create(uri) as FtpWebRequest;

            request.Credentials = new NetworkCredential(m_userID, m_password);
            request.Method = WebRequestMethods.Ftp.DeleteFile;

            FtpWebResponse response = request.GetResponse() as FtpWebResponse;
        }
        catch (Exception ex)
        {
            Debug.warning("CFtp::delete error. uri:" + m_uri + " filename:" + filename +
                " reason:" + ex.Message);
            return false;
        }

        return true;
    }
}
