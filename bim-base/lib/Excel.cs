//#define USE_EXCEL // Microsoft.Office.Interop.Excel 설치 필요

using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

#if USE_EXCEL
using ExcelExtract = Microsoft.Office.Interop.Excel;

public class Excel
{
    private static object lockObject = new object();

    static string m_path = "";

    static Excel()
    {
        m_path = pathUtil.savePath();
    }
    
    public static List<string[]> readExcelData(string path)
    {
        List<string[]> retData = new List<string[]>();

        FileInfo fileInfo = new FileInfo(path);

        if (fileInfo.Directory.Exists == false)
        {
            Debug.warning("Excel::readExcelData directory not found. path:" + path);
            return retData;
        }

        if (!fileInfo.Exists)
        {
            Debug.warning("Excel::readExcelData file not found. path:" + path);
            return retData;
        }

        ExcelExtract.Application excelApp = null;
        ExcelExtract.Workbook wb = null;
        ExcelExtract.Worksheet ws = null;

        try
        {
            excelApp = new ExcelExtract.Application();
            wb = excelApp.Workbooks.Open(path);
            ws = wb.Worksheets.get_Item(1) as ExcelExtract.Worksheet;

            ExcelExtract.Range rng = ws.UsedRange; 
            object[,] data = rng.Value;

            int rowCount = data.GetLength(0);
            int columnCount = data.GetLength(1);

            for (int r = 1; r <= rowCount; r++)
            {
                string[] retStr = new string[columnCount];

                for (int c = 1; c <= columnCount; c++)
                {
                    if (data[r, c] == null)
                    {
                        continue;
                    }

                    retStr[c - 1] = data[r, c].ToString();
                }

                retData.Add(retStr);
            }

            wb.Close(true);
            excelApp.Quit();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            releaseObject(ws);
            releaseObject(wb);
            releaseObject(excelApp);
        }

        return retData;
    }

    public static void save(String text, String filename = "")
    {
        lock (lockObject)
        {
            ExcelExtract.Application excelApp = new ExcelExtract.Application();
            ExcelExtract.Workbooks workBooks = excelApp.Workbooks;
            ExcelExtract.Workbook book = workBooks.Add("");
            ExcelExtract.Sheets workSheets = book.Worksheets;
            ExcelExtract.Worksheet sheet = (ExcelExtract.Worksheet)workSheets["sheet1"];

            excelApp.Visible = false;
            excelApp.DisplayAlerts = false;

            string[] splitText = text.Split('\n');

            for (int i = 0; i < splitText.Length; i++)
            {
                int cellNo = i + 1;
                string[] splitRowText = splitText[i].Split('\t');
                char charNo = 'A';

                for (int j = 0; j < splitRowText.Length; j++)
                {
                    string cell = charNo.ToString() + cellNo;
                    sheet.Range[cell].Value = splitRowText[j].ToString();
                    charNo++;
                }
            }

            sheet.Columns.AutoFit();

            string saveName = "";

            if (filename == "")
                saveName = saveExcelPath();
            else
                saveName = m_path + "\\" + filename + ".xlsx";

            try
            {
                book.SaveAs(saveName);
            }
            catch (Exception e)
            {
                string error = "saveName : " + saveName + " / message : " + e.Message + " / trace : " + e.StackTrace;
            }

            excelApp.Quit();
            releaseObject(sheet);
            releaseObject(workSheets);
            releaseObject(book);
            releaseObject(workBooks);
            releaseObject(excelApp);
            excelApp = null;
            GC.Collect();
        }
    }

    private static string saveExcelPath()
    {
        SaveFileDialog saveFile = new SaveFileDialog();
        saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "";
        saveFile.Title = "저장 위치를 지정해주세요";
        saveFile.DefaultExt = "xlsx";
        saveFile.Filter = "EXCEL(*.xlsx)|*.xlsx";
        saveFile.ShowDialog();

        return saveFile.FileName;
    }

    private static void releaseObject(object obj)
    {
        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            obj = null;
        }
        catch (Exception e)
        {
            obj = null;
            throw e;
        }
    }
} // class

#endif
