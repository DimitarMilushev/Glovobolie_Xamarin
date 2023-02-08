using GlovobolieApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace GlovobolieApp.Helpers
{
    public class StatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color statusColor;
            switch((OrderStatus)value)
            {
                case OrderStatus.Requested:
                    statusColor = (Color)Application.Current.Resources["BackgroundPrimary"];
                    break;
                case OrderStatus.InProgress:
                    statusColor = (Color)Application.Current.Resources["Secondary"];
                    break;
                case OrderStatus.Delivered:
                    statusColor = (Color)Application.Current.Resources["BackgroundSecondary"];
                    break;
                default:
                    statusColor = (Color)Application.Current.Resources["BackgroundPrimary"];
                    break;
            }
            return statusColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
