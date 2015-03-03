//===================================================================================
//OPCApp_Main
// 
//===================================================================================
// OPC项目主程序
// 作者：赵晓玉
// 创建日期：2014-2-5
//===================================================================================
// 修改记录
//
//===================================================================================

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Intime.OPC.Infrastructure
{
    public class ObjectToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}