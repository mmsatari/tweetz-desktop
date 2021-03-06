﻿// Copyright (c) 2013 Blue Onion Software - All rights reserved

using System;
using System.Globalization;
using System.Windows.Data;

namespace tweetz5.Utilities
{
    internal class ScreenNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "@" + value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}