using System;
using System.Collections.Generic;

using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;


public static class ScreenShotUtil
{
    public static void captureScreen(Screen window, string path, string filename)
    {
        captureScreen(window.Bounds, path, filename);
    }

    public static void captureScreen(Rectangle rect, string path, string filename)
    {
        if (Directory.Exists(path) == false)
            Directory.CreateDirectory(path);

        try
        {
            using (Bitmap bmp = new Bitmap(rect.Width, rect.Height))
            {
                using (Graphics gScreen = Graphics.FromImage(bmp))
                    gScreen.CopyFromScreen(rect.Location, Point.Empty, rect.Size);

                bmp.Save(path + "\\" + filename, System.Drawing.Imaging.ImageFormat.Png);
            }
        }
        catch (Exception ex)
        {

            Debug.warning("ScreenShotUtil::captureScreen error. reason:" + ex.Message +
                " path:" + path + "\\" + filename);
        }
    }
} // class
