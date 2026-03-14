using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

public class CarrierHeightItem
{
    public DateTime dateTime;
    public string[] unit = new string[5];
    public double[] value = new double[5];
    public bool result;
}

public class CarrierHeightDB
{
    static MySqlManager m_sqlManager = new MySqlManager("localhost", "root", "1q2w3e4r", "carrierheight");

    static string m_schema = "carrierheight";

    public static bool isExistTable(string schema, string table)
    {
        string isExistTableQuery = "SELECT EXISTS ( SELECT 1 FROM Information_schema.tables " +
            "WHERE table_schema = '" + schema + "' " +
            "AND table_name = '" + table + "'" + ") AS flag";

        DataTable dt = m_sqlManager.dataTable(isExistTableQuery);

        string text = dt.Rows[0]["flag"].ToString();

        if (text == "1")
            return true;

        return false;
    }

    private static void createTable(string yearText)
    {
        string query = "CREATE TABLE `" + m_schema + "`.`" + yearText + "` (" +
                    "datetime DATETIME NOT NULL, " +
                    "unit1 VARCHAR(45) NULL, " +
                    "unit2 VARCHAR(45) NULL, " +
                    "unit3 VARCHAR(45) NULL, " +
                    "unit4 VARCHAR(45) NULL, " +
                    "unit5 VARCHAR(45) NULL, " +
                    "value1 DOUBLE NULL, " +
                    "value2 DOUBLE NULL, " +
                    "value3 DOUBLE NULL, " +
                    "value4 DOUBLE NULL, " +
                    "value5 DOUBLE NULL, " +
                    "result INT NULL, " +
                    "PRIMARY KEY (datetime)) " +
                    "ENGINE = InnoDB " +
                    "DEFAULT CHARACTER SET = utf8;";

        bool result = m_sqlManager.executeNonQuery(query);
    }

    public static DataTable dataTable(string sql)
    {
        return m_sqlManager.dataTable(sql);
    }
}
