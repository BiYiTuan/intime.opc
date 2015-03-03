using Intime.OPC.Infrastructure.Mvvm.Utility;
using Intime.OPC.Infrastructure.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;

namespace Intime.OPC.Infrastructure.Print
{
    public class ReportUtility
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int WriteProfileString(string lpszSection, string lpszKeyName, string lpszString);

        [DllImport("user32.dll")]
        private static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);

        [DllImport("gdi32")]
        private static extern int AddFontResource(string lpFileName);

        [DllImport("gdi32")]
        private static extern int RemoveFontResource(string lpFileName);

        /// <summary>
        /// Install Code39 bar code font
        /// </summary>
        public static void InstallBarcodeFont()
        {
            const string FontName = "C39HrP48DmTt";

            InstalledFontCollection fonts = new InstalledFontCollection();
            if (fonts.Families.Contains(fontFamily => fontFamily.Name == FontName)) return;

            var fontNameWithExtension = string.Format("{0}.TTF", FontName);
            var fontRelativePath = string.Format(@"\Print\Fonts\{0}", fontNameWithExtension);
            var codeBase = Assembly.GetExecutingAssembly().GetName().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            var assemblyPath = Path.GetDirectoryName(path);
            var fontFilePath = string.Concat(assemblyPath, fontRelativePath);
            var winFontDir = System.Environment.GetEnvironmentVariable("WINDIR") + "\\fonts";
            var destinationFontPath = Path.Combine(winFontDir, fontNameWithExtension);

            try
            {
                if (File.Exists(destinationFontPath)) File.Delete(destinationFontPath);
                File.Copy(fontFilePath, destinationFontPath);

                AddFontResource(destinationFontPath);
                HandleWin32Exception();

                WriteProfileString("fonts", FontName + "(TrueType)", destinationFontPath);
                HandleWin32Exception();
            }
            catch (Exception exception)
            {
                MvvmUtility.ShowMessageAsync(string.Format(Resources.BarCodeFontInstallationErrorMessage, exception.Message),"错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static void HandleWin32Exception()
        {
            var error = Marshal.GetLastWin32Error();
            if (error != 0)
            {
                var errorMessage = new Win32Exception(error).Message;
                throw new ApplicationException(errorMessage);
            }
        }
    }
}
