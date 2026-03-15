using Lib.UI.Generic.Icons;
using Lib.UI.Generic.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.UI.Generic.Icons
{
    public static class IconImages
    {
        public static Image ConvertIconToImage(Icon icon)
        {
            if (icon == null)
                return null;

            using (var bitmap = icon.ToBitmap())
            {
                return new Bitmap(bitmap);
            }
        }

        public static byte[] GetBytes(EnumIconsAll icon)
        {
            switch (icon)
            {
                case EnumIconsAll.Check: return Resources.Check; 
                case EnumIconsAll.DirectoryError: return Resources.DirectoryError; 
                case EnumIconsAll.DirectoryOK: return Resources.DirectoryOK; 
                case EnumIconsAll.DirectorySearch: return Resources.DirectorySearch; 
                case EnumIconsAll.Error: return Resources.Error; 
                case EnumIconsAll.EXIT: return Resources.EXIT; 
                case EnumIconsAll.FileSave: return Resources.FileSave; 
                case EnumIconsAll.FileSaveError: return Resources.FileSaveError; 
                case EnumIconsAll.FileSaveInfomation: return Resources.FileSaveInfomation; 
                case EnumIconsAll.FileSaveOK: return Resources.FileSaveOK; 
                case EnumIconsAll.FileSaveWarning: return Resources.FileSaveWarning; 
                case EnumIconsAll.Information: return Resources.Information;
                case EnumIconsAll.Lock: return Resources.Lock;
                case EnumIconsAll.LockError: return Resources.LockError; 
                case EnumIconsAll.LockInformation: return Resources.LockInformation; 
                case EnumIconsAll.LockOK: return Resources.LockOK; 
                case EnumIconsAll.LocKOpen: return Resources.LocKOpen; 
                case EnumIconsAll.LockSetting: return Resources.LockSetting; 
                case EnumIconsAll.LockWarning: return Resources.LockWarning; 
                case EnumIconsAll.LoginKey: return Resources.LoginKey; 
                case EnumIconsAll.LoginKeyAdd: return Resources.LoginKeyAdd; 
                case EnumIconsAll.LoginKeyError: return Resources.LoginKeyError; 
                case EnumIconsAll.OK: return Resources.OK; 
                case EnumIconsAll.Pause: return Resources.Pause; 
                case EnumIconsAll.Print: return Resources.Print; 
                case EnumIconsAll.PrintError: return Resources.PrintError; 
                case EnumIconsAll.PrintInformation: return Resources.PrintInformation; 
                case EnumIconsAll.PrintOK: return Resources.PrintOK; 
                case EnumIconsAll.PrintWarning: return Resources.PrintWarning; 
                case EnumIconsAll.Question: return Resources.Question; 
                case EnumIconsAll.QurestionWithInformation: return Resources.QurestionWithInformation; 
                case EnumIconsAll.SensorOFF: return Resources.SensorOFF; 
                case EnumIconsAll.SensorON: return Resources.SensorON; 
                case EnumIconsAll.Setting: return Resources.Setting; 
                case EnumIconsAll.SettingError: return Resources.SettingError; 
                case EnumIconsAll.SettingInformation: return Resources.SettingInformation; 
                case EnumIconsAll.SettingOK: return Resources.SettingOK; 
                case EnumIconsAll.SettingSearch: return Resources.SettingSearch; 
                case EnumIconsAll.SettingWarning: return Resources.SettingWarning; 
                case EnumIconsAll.StarProcessing: return Resources.StarProcessing; 
                case EnumIconsAll.Start: return Resources.Start; 
                case EnumIconsAll.Stop: return Resources.Stop; 
                case EnumIconsAll.Stopped: return Resources.Stopped; 
                case EnumIconsAll.StopSign: return Resources.StopSign; 
                case EnumIconsAll.TalkAbout: return Resources.TalkAbout; 
                case EnumIconsAll.Warning: return Resources.Warning; 
                default: throw new NotImplementedException();
            }
        }

        public static Icon GetIcon(EnumIconsAll icon)
        {
            byte[] imageBytes = GetBytes(icon);

            if (imageBytes == null || imageBytes.Length == 0)
                return null;

            using (var ms = new MemoryStream(imageBytes))
            {
                return new Icon(ms);
            }
        }

        public static Icon GetIcon(EnumDirectoryIcons icon)
        {
            switch (icon)
            {
                case EnumDirectoryIcons.DirectoryError: return GetIcon((EnumIconsAll)icon);
                case EnumDirectoryIcons.DirectoryOK: return GetIcon((EnumIconsAll)icon);
                case EnumDirectoryIcons.DirectorySearch: return GetIcon((EnumIconsAll)icon);
                default: throw new NotImplementedException();
            }
        }

        public static Icon GetIcon(EnumFileSaveIcons icon)
        {
            switch (icon)
            {
                case EnumFileSaveIcons.FileSave: return GetIcon((EnumIconsAll)icon);
                case EnumFileSaveIcons.FileSaveError: return GetIcon((EnumIconsAll)icon);
                case EnumFileSaveIcons.FileSaveInfomation: return GetIcon((EnumIconsAll)icon);
                case EnumFileSaveIcons.FileSaveOK: return GetIcon((EnumIconsAll)icon);
                case EnumFileSaveIcons.FileSaveWarning: return GetIcon((EnumIconsAll)icon);
                default: throw new NotImplementedException();
            }
        }

        public static Icon GetIcon(EnumLoginKeyIcons icon)
        {
            switch (icon)
            {
                case EnumLoginKeyIcons.LoginKey: return GetIcon((EnumIconsAll)icon);
                case EnumLoginKeyIcons.LoginKeyAdd: return GetIcon((EnumIconsAll)icon);
                case EnumLoginKeyIcons.LoginKeyError: return GetIcon((EnumIconsAll)icon);
                default: throw new NotImplementedException();
            }
        }

        public static Icon GetIcon(EnumPermissionIcons icon)
        {
            switch (icon)
            {
                case EnumPermissionIcons.Lock: return GetIcon((EnumIconsAll)icon);
                case EnumPermissionIcons.LockError: return GetIcon((EnumIconsAll)icon);
                case EnumPermissionIcons.LockInformation: return GetIcon((EnumIconsAll)icon);
                case EnumPermissionIcons.LockOK: return GetIcon((EnumIconsAll)icon);
                case EnumPermissionIcons.LocKOpen: return GetIcon((EnumIconsAll)icon);
                case EnumPermissionIcons.LockSetting: return null; //Image
                case EnumPermissionIcons.LockWarning: return GetIcon((EnumIconsAll)icon);
                default: throw new NotImplementedException();
            }
        }

        public static Icon GetIcon(EnumPrintIcons icon)
        {
            switch (icon)
            {
                case EnumPrintIcons.Print: return GetIcon((EnumIconsAll)icon);
                case EnumPrintIcons.PrintError: return GetIcon((EnumIconsAll)icon);
                case EnumPrintIcons.PrintInformation: return GetIcon((EnumIconsAll)icon);
                case EnumPrintIcons.PrintOK: return GetIcon((EnumIconsAll)icon);
                case EnumPrintIcons.PrintWarning: return GetIcon((EnumIconsAll)icon);
                default: throw new NotImplementedException();
            }
        }

        public static Icon GetIcon(EnumSensorIcons icon)
        {
            switch (icon)
            {
                case EnumSensorIcons.SensorOFF: return null; //Image
                case EnumSensorIcons.SensorON: return null; //Image
                default: throw new NotImplementedException();
            }
        }

        public static Icon GetIcon(EnumSettingIcons icon)
        {
            switch (icon)
            {
                case EnumSettingIcons.Setting: return GetIcon((EnumIconsAll)icon);
                case EnumSettingIcons.SettingError: return GetIcon((EnumIconsAll)icon);
                case EnumSettingIcons.SettingInformation: return GetIcon((EnumIconsAll)icon);
                case EnumSettingIcons.SettingOK: return GetIcon((EnumIconsAll)icon);
                case EnumSettingIcons.SettingSearch: return GetIcon((EnumIconsAll)icon);
                case EnumSettingIcons.SettingWarning: return GetIcon((EnumIconsAll)icon);
                default: throw new NotImplementedException();
            }
        }

        public static Icon GetIcon(EnumMessageBoxIcons icon)
        {
            switch (icon)
            {
                case EnumMessageBoxIcons.Check: return GetIcon((EnumIconsAll)icon);
                case EnumMessageBoxIcons.Error: return GetIcon((EnumIconsAll)icon);
                case EnumMessageBoxIcons.EXIT: return null; //Image
                case EnumMessageBoxIcons.Information: return GetIcon((EnumIconsAll)icon);
                case EnumMessageBoxIcons.OK: return null; //Image
                case EnumMessageBoxIcons.Question: return GetIcon((EnumIconsAll)icon);
                case EnumMessageBoxIcons.QurestionWithInformation: return GetIcon((EnumIconsAll)icon);
                case EnumMessageBoxIcons.StopSign: return GetIcon((EnumIconsAll)icon);
                case EnumMessageBoxIcons.TalkAbout: return GetIcon((EnumIconsAll)icon);
                case EnumMessageBoxIcons.Warning: return GetIcon((EnumIconsAll)icon);
                default: throw new NotImplementedException();
            }
        }

        public static Icon GetIcon(EnumProcessingIcons icon)
        {
            switch (icon)
            {
                case EnumProcessingIcons.Complete: return GetIcon((EnumIconsAll)icon);
                case EnumProcessingIcons.Error: return GetIcon((EnumIconsAll)icon);
                case EnumProcessingIcons.OK: return null; //Image
                case EnumProcessingIcons.Pause: return GetIcon((EnumIconsAll)icon);
                case EnumProcessingIcons.Processing: return GetIcon((EnumIconsAll)icon);
                case EnumProcessingIcons.Start: return GetIcon((EnumIconsAll)icon);
                case EnumProcessingIcons.Stop: return GetIcon((EnumIconsAll)icon);
                case EnumProcessingIcons.Stopped: return GetIcon((EnumIconsAll)icon);
                case EnumProcessingIcons.StopSign: return GetIcon((EnumIconsAll)icon);
                case EnumProcessingIcons.Warning: return GetIcon((EnumIconsAll)icon);
                default: throw new NotImplementedException();
            }
        }


        public static Image GetImage(EnumDirectoryIcons icon)
        {
            switch (icon)
            {
                case EnumDirectoryIcons.DirectoryError: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumDirectoryIcons.DirectoryOK: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumDirectoryIcons.DirectorySearch: return GetIcon((EnumIconsAll)icon).ToBitmap();
                default: throw new NotImplementedException();
            }
        }

        public static Image GetImage(EnumFileSaveIcons icon)
        {
            switch (icon)
            {
                case EnumFileSaveIcons.FileSave: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumFileSaveIcons.FileSaveError: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumFileSaveIcons.FileSaveInfomation: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumFileSaveIcons.FileSaveOK: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumFileSaveIcons.FileSaveWarning: return GetIcon((EnumIconsAll)icon).ToBitmap();
                default: throw new NotImplementedException();
            }
        }

        public static Image GetImage(EnumLoginKeyIcons icon)
        {
            switch (icon)
            {
                case EnumLoginKeyIcons.LoginKey: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumLoginKeyIcons.LoginKeyAdd: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumLoginKeyIcons.LoginKeyError: return GetIcon((EnumIconsAll)icon).ToBitmap();
                default: throw new NotImplementedException();
            }
        }

        public static Image GetImage(EnumPermissionIcons icon)
        {
            switch (icon)
            {
                case EnumPermissionIcons.Lock: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumPermissionIcons.LockError: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumPermissionIcons.LockInformation: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumPermissionIcons.LockOK: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumPermissionIcons.LocKOpen: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumPermissionIcons.LockSetting: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumPermissionIcons.LockWarning: return GetIcon((EnumIconsAll)icon).ToBitmap();
                default: throw new NotImplementedException();
            }
        }

        public static Image GetImage(EnumPrintIcons icon)
        {
            switch (icon)
            {
                case EnumPrintIcons.Print: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumPrintIcons.PrintError: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumPrintIcons.PrintInformation: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumPrintIcons.PrintOK: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumPrintIcons.PrintWarning: return GetIcon((EnumIconsAll)icon).ToBitmap();
                default: throw new NotImplementedException();
            }
        }

        public static Image GetImage(EnumSensorIcons icon)
        {
            switch (icon)
            {
                case EnumSensorIcons.SensorOFF: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumSensorIcons.SensorON: return GetIcon((EnumIconsAll)icon).ToBitmap();
                default: throw new NotImplementedException();
            }
        }

        public static Image GetImage(EnumSettingIcons icon)
        {
            switch (icon)
            {
                case EnumSettingIcons.Setting: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumSettingIcons.SettingError: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumSettingIcons.SettingInformation: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumSettingIcons.SettingOK: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumSettingIcons.SettingSearch: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumSettingIcons.SettingWarning: return GetIcon((EnumIconsAll)icon).ToBitmap();
                default: throw new NotImplementedException();
            }
        }

        public static Image GetImage(EnumMessageBoxIcons icon)
        {
            switch (icon)
            {
                case EnumMessageBoxIcons.Check: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumMessageBoxIcons.Error: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumMessageBoxIcons.EXIT: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumMessageBoxIcons.Information: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumMessageBoxIcons.OK: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumMessageBoxIcons.Question: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumMessageBoxIcons.QurestionWithInformation: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumMessageBoxIcons.StopSign: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumMessageBoxIcons.TalkAbout: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumMessageBoxIcons.Warning: return GetIcon((EnumIconsAll)icon).ToBitmap();
                default: throw new NotImplementedException();
            }
        }

        public static Image GetImage(EnumProcessingIcons icon)
        {
            switch (icon)
            {
                case EnumProcessingIcons.Complete: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumProcessingIcons.Error: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumProcessingIcons.OK: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumProcessingIcons.Pause: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumProcessingIcons.Processing: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumProcessingIcons.Start: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumProcessingIcons.Stop: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumProcessingIcons.Stopped: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumProcessingIcons.StopSign: return GetIcon((EnumIconsAll)icon).ToBitmap();
                case EnumProcessingIcons.Warning: return GetIcon((EnumIconsAll)icon).ToBitmap();
                default: throw new NotImplementedException();
            }
        }
    }
}
