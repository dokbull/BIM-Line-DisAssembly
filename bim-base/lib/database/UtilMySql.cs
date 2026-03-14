using Etier.IconHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class UtilMySql
{
    /// <summary>
    /// SQL TABLE 의 COLUMN 을 List<string> 으로 반환
    /// </summary>
    /// <param name="sqlManager"></param>
    /// <param name="tableName">테이블 이름</param>
    /// <param name="list">컬럼 list</param>
    /// <returns></returns>
    public static bool columnList(MySqlManager sqlManager, string tableName, List<string> list)
    {
        list.Clear();

        string query = "SELECT * FROM `" + tableName + "` LIMIT 1";

        DataTable dt = sqlManager.dataTable(query);

        if (dt == null)
            return false;

        foreach (DataColumn col in dt.Columns)
        {
            list.Add(col.ColumnName);
        }

        return true;
    }
}//class
