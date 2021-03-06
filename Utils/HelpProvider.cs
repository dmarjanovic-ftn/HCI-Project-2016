﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using HCI_2016_Project.UserInterface;

namespace HCI_2016_Project.Utils
{
    public class HelpProvider
    {
        public static string GetHelpKey(DependencyObject obj)
        {
            return obj.GetValue(HelpKeyProperty) as string;
        }

        public static string GetHelpSection(DependencyObject obj)
        {
            return obj.GetValue(HelpSectionProperty) as string;
        }

        public static void SetHelpKey(DependencyObject obj, string value)
        {
            obj.SetValue(HelpKeyProperty, value);
        }

        public static void SetHelpSection(DependencyObject obj, string value)
        {
            obj.SetValue(HelpSectionProperty, value);
        }

        public static readonly DependencyProperty HelpKeyProperty =
            DependencyProperty.RegisterAttached("HelpKey", typeof(string), typeof(HelpProvider), new PropertyMetadata("index", HelpKey));

        public static readonly DependencyProperty HelpSectionProperty =
            DependencyProperty.RegisterAttached("HelpSection", typeof(string), typeof(HelpProvider), new PropertyMetadata("index", HelpSection));
        

        private static void HelpKey(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void HelpSection(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public static void ShowHelp(string key, string section, Window originator)
        {
            HelpWindow hw = new HelpWindow(key, section, originator);
            hw.Show();
        }
    }
}
