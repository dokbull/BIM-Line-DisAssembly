using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient; ///// MySql.Data 를 리퍼런스 설정 해 주어야 함 (Mysql Connector 다운로드하여야 함)

public class MsSqlServerManager
{
    object lockObject = new object();

    string m_address;
    string m_id;
    string m_password;

    string m_connName;
    string m_scheme;

    public MsSqlServerManager(string address, string id, string password, string scheme)
    {
        m_id = id;
        m_password = password;
        m_address = address;
        m_scheme = scheme;

        m_connName =
            "Server=" + m_address + ";" +
            "DataBase=" + m_scheme + ";" +
            "Uid=" + m_id + ";" +
            "Pwd=" + m_password + ";";
    }

    public virtual bool connTest()
    {
        SqlConnection conn = new SqlConnection(m_connName);

        try
        {
            conn.Open();
            conn.Close();
        }
        catch (Exception /*e*/)
        {
            conn.Close();
            return false;
        }
        
        return true;
    }

    public bool executeNonQuery(string str)
    {
        lock (lockObject)
        {
            int ret = 0;

            using (SqlConnection conn = new SqlConnection(m_connName))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(str, conn);
                    ret = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Debug.debug("MsSqlServerManager::executeNonQuery error query:" + str + " error:" + e.Message);
                    return false;
                }
            }

            return true;
        }
    }
    public DataTable dataTable(string sql)
    {
        // Debug.debug("MsSqlServerManager::dataTable sql:" + sql);

        DataTable dt = new DataTable();

        using (SqlConnection conn = new SqlConnection(m_connName))
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            try
            {
                adapter.Fill(dt);
            }
            catch (Exception e)
            {
                Debug.warning("MsSqlServerManager::dataTable exception message:" + e.Message);
                return null;
            }
        }

        return dt;
    }
}