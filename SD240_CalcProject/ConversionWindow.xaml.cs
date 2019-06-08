using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SD240_CalcProject
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ConversionWindow : Window
    {
        public ConversionWindow(string intialValue)
        {
            if (String.IsNullOrEmpty(intialValue))
                intialValue = "0";

            InitializeComponent();
            TextLabel1.Text = "Integer";
            TextOutput1.Text = intialValue;

        }

        private void Button_Click_ProgrammerConversions(object sender, RoutedEventArgs e)
        {
            ProgrammerConversions("345", "123123", "123190");
        }

        private void Button_Click_PercentConversions(object sender, RoutedEventArgs e)
        {

            PercentConversions("0.04", "");
        }

        private void Button_Click_WeightConversions(object sender, RoutedEventArgs e)
        {
            WeightConversions("","","","","");
        }

        private void Button_Click_TemperatureConversions(object sender, RoutedEventArgs e)
        {
            TemperatureConversions("","","");
        }

        private void Button_Click_LengthConversions(object sender, RoutedEventArgs e)
        {
            LengthConversions("", "", "", "", "", "");
        }

        private void Button_Click_FileSizeConversions(object sender, RoutedEventArgs e)
        {
            DataConversions("","","","","");
        }

        private void Button_Click_TimeConversions(object sender, RoutedEventArgs e)
        {
            TimeConversions("","","");
        }

        private void ProgrammerConversions(string val1, string val2, string val3)
        {
            HideClearAllGroups();
            SetGroup(Groups.Group1, "Integer", val1);
            SetGroup(Groups.Group2, "Octal", val2);
            SetGroup(Groups.Group3, "Binary", val3);
        }

        private void PercentConversions(string val1, string val2)
        {
            HideClearAllGroups();
            SetGroup(Groups.Group1, "Decimal", val1);
            SetGroup(Groups.Group2, "Percent", val2);
        }

        private void WeightConversions(string val1, string val2, string val3, string val4, string val5)
        {
            HideClearAllGroups();
            SetGroup(Groups.Group1, "MilliGrams", val1);
            SetGroup(Groups.Group2, "Grams", val2);
            SetGroup(Groups.Group3, "KiloGrams", val3);
            SetGroup(Groups.Group4, "Ounces", val4);
            SetGroup(Groups.Group5, "Pounds", val5);
        }

        private void TemperatureConversions(string val1, string val2, string val3)
        {
            HideClearAllGroups();
            SetGroup(Groups.Group1, "Celcius", val1);
            SetGroup(Groups.Group2, "Fahrenheit", val2);
            SetGroup(Groups.Group3, "Kelvin", val3);
        }

        private void LengthConversions(string val1, string val2, string val3, string val4, string val5, string val6)
        {
            HideClearAllGroups();
            SetGroup(Groups.Group1, "MilliMeters", val1);
            SetGroup(Groups.Group2, "CentiMeters", val2);
            SetGroup(Groups.Group3, "Meters", val3);
            SetGroup(Groups.Group4, "KiloMeters", val4);
            SetGroup(Groups.Group5, "Inches", val5);
            SetGroup(Groups.Group6, "Feet", val6);
        }
        private void DataConversions(string val1, string val2, string val3, string val4, string val5)
        {
            HideClearAllGroups();
            SetGroup(Groups.Group1, "Bytes", val1);
            SetGroup(Groups.Group2, "KiloBytes", val2);
            SetGroup(Groups.Group3, "MegaBytes", val3);
            SetGroup(Groups.Group4, "GigaBytes", val4);
            SetGroup(Groups.Group5, "TeraBytes", val5);
        }

        private void TimeConversions(string val1, string val2, string val3)
        {
            HideClearAllGroups();
            SetGroup(Groups.Group1, "Seconds", val1);
            SetGroup(Groups.Group2, "Minutes", val2);
            SetGroup(Groups.Group3, "Hours", val3);
        }

        private void HideClearAllGroups()
        {
            TextLabel1.Text = null;
            TextOutput1.Text = null;

            TextLabel2.Text = null;
            TextOutput2.Text = null;

            TextLabel3.Text = null;
            TextOutput3.Text = null;

            TextLabel4.Text = null;
            TextOutput4.Text = null;

            TextLabel5.Text = null;
            TextOutput5.Text = null;

            TextLabel6.Text = null;
            TextOutput6.Text = null;

            Group1.Visibility = Visibility.Hidden;
            Group2.Visibility = Visibility.Hidden;
            Group3.Visibility = Visibility.Hidden;
            Group4.Visibility = Visibility.Hidden;
            Group5.Visibility = Visibility.Hidden;
            Group6.Visibility = Visibility.Hidden;
        }

        private void SetGroup(Groups group, string label, string val)
        {
            switch (group)
            {
                case Groups.Group1:
                    TextLabel1.Text = label;
                    TextOutput1.Text = val;
                    if (Group1.Visibility == Visibility.Hidden)
                        Group1.Visibility = Visibility.Visible;
                    
                    break;

                case Groups.Group2:
                    TextLabel2.Text = label;
                    TextOutput2.Text = val;
                    if (Group2.Visibility == Visibility.Hidden)
                        Group2.Visibility = Visibility.Visible;

                    break;

                case Groups.Group3:
                    TextLabel3.Text = label;
                    TextOutput3.Text = val;
                    if (Group3.Visibility == Visibility.Hidden)
                        Group3.Visibility = Visibility.Visible;

                    break;
                case Groups.Group4:
                    TextLabel4.Text = label;
                    TextOutput4.Text = val;
                    if (Group4.Visibility == Visibility.Hidden)
                        Group4.Visibility = Visibility.Visible;

                    break;
                case Groups.Group5:
                    TextLabel5.Text = label;
                    TextOutput5.Text = val;
                    if (Group5.Visibility == Visibility.Hidden)
                        Group5.Visibility = Visibility.Visible;

                    break;
                case Groups.Group6:
                    TextLabel6.Text = label;
                    TextOutput6.Text = val;
                    if (Group6.Visibility == Visibility.Hidden)
                        Group6.Visibility = Visibility.Visible;

                    break;
                default:
                    new Exception("Error - out of place enum val");
                    break;
            }
        }

        private enum Groups
        {
            Group1,
            Group2,
            Group3,
            Group4,
            Group5,
            Group6
        }
    }
}
