using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MySqlConnector;
using System.Security.Policy;
using System.Threading.Tasks;

public class MySqlManager
{
    object lockObject = new object();

    string m_address;
    string m_id;
    string m_password;

    string m_scheme;

    string m_connName = "";
    MySqlConnection m_connection = null;

    public MySqlManager(string address, string id, string password, string scheme, int port = 3306)
    {
        m_id = id;
        m_password = password;
        m_address = address;
        m_scheme = scheme;

        m_connName =
            "Server=" + m_address + ";" +
            "DataBase=" + m_scheme + ";" +
            "Port = " + port + ";" +
            "Uid=" + m_id + ";" +
            "Pwd=" + m_password + ";";
    }

    public bool connTest()
    {
        MySqlConnection conn = new MySqlConnection(m_connName);

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

    public bool connTestWithoutScheme(string address, string id, string password, int port = 3306)
    {
        string connStr =
            "Server=" + address + ";" +
            "Port = " + port + ";" +
            "Uid=" + id + ";" +
            "Pwd=" + password + ";";

        MySqlConnection conn = new MySqlConnection(connStr);

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

    public bool executeNonQuery(string str, bool useConnection = false)
    {
        lock (lockObject)
        {
            if (useConnection == true)
            {
                if (m_connection == null)
                    m_connection = new MySqlConnection(m_connName);

                if (m_connection.State != ConnectionState.Open)
                    m_connection.Open();

                try
                {
                    MySqlCommand cmd = new MySqlCommand(str, m_connection);
                    int dbRet = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Debug.debug("MySqlManager::executeNonQuery error query:" + str + " error:" + e.Message);
                    return false;
                }

                return true;
            }

            int ret = 0;

            using (MySqlConnection conn = new MySqlConnection(m_connName))
            {
                try
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    ret = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Debug.debug("MySqlManager::executeNonQuery error query:" + str + " error:" + e.Message);
                    return false;
                }
            }

            return true;
        }
    }

    public async Task<bool> executeNonQueryAsync(string str, bool useConnection = false)
    {
        using (MySqlConnection conn = new MySqlConnection(m_connName))
        {
            try
            {
                await conn.OpenAsync();

                MySqlCommand cmd = new MySqlCommand(str, conn);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                Debug.debug("MySqlManager::ExecuteNonQueryAsync error query:" + str + " error:" + e.Message);
                return false;
            }
        }

        return true;
    }

    public bool executeNonQueryWithScheme(string str, string scheme, int port = 3306)
    {
        string tempConnName =
            "Server=" + m_address + ";" +
            "DataBase=" + scheme + ";" +
            "Port = " + port + ";" +
            "Uid=" + m_id + ";" +
            "Pwd=" + m_password + ";";

        lock (lockObject)
        {
            int ret = 0;

            using (MySqlConnection conn = new MySqlConnection(tempConnName))
            {
                try
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(str, conn);
                    ret = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Debug.debug("MySqlManager::executeNonQuery error query:" + str + " error:" + e.Message);
                    return false;
                }
            }

            return true;
        }
    }

    public DataTable dataTable(string sql)
    {
        Debug.debug("MySqlManager::dataTable sql:" + sql);

        DataTable dt = new DataTable();

        using (MySqlConnection conn = new MySqlConnection(m_connName))
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);

            try
            {
                adapter.Fill(dt);
            }
            catch (Exception /*e*/)
            {
                return null;
            }
        }

        return dt;
    }

    public DataTable dataTableWithScheme(string sql, string scheme)
    {
        string tempConnName =
            "Server=" + m_address + ";" +
            "DataBase=" + scheme + ";" +
            "Port = " + 3306 + ";" +
            "Uid=" + m_id + ";" +
            "Pwd=" + m_password + ";";

        DataTable dt = new DataTable();

        using (MySqlConnection conn = new MySqlConnection(tempConnName))
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);

            try
            {
                adapter.Fill(dt);
            }
            catch (Exception /*e*/)
            {
                return null;
            }
        }

        return dt;
    }
}