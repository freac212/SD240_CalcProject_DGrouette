using SD240_CalcProject.ConversionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SD240_CalcProject.Controllers
{
    class ConversionsController
    {
        public TextBlock TextLabel1 { get; set; }
        public TextBlock TextLabel2 { get; set; }
        public TextBlock TextLabel3 { get; set; }
        public TextBlock TextLabel4 { get; set; }
        public TextBlock TextLabel5 { get; set; }
        public TextBlock TextLabel6 { get; set; }
        public TextBox TextOutput1 { get; set; }
        public TextBox TextOutput2 { get; set; }
        public TextBox TextOutput3 { get; set; }
        public TextBox TextOutput4 { get; set; }
        public TextBox TextOutput5 { get; set; }
        public TextBox TextOutput6 { get; set; }
        public Grid Group1 { get; set; }
        public Grid Group2 { get; set; }
        public Grid Group3 { get; set; }
        public Grid Group4 { get; set; }
        public Grid Group5 { get; set; }
        public Grid Group6 { get; set; }


        public ConversionTypes CurrentConversionType = ConversionTypes.Programmer;
        public ActiveOutput CurrentActiveOutput = ActiveOutput.TextOutput1;

        public enum Groups
        {
            Group1,
            Group2,
            Group3,
            Group4,
            Group5,
            Group6
        }

        public enum ConversionTypes
        {
            Programmer,
            Percent,
            Weight,
            Temperature,
            Length,
            Data,
            Time
        }

        public enum ActiveOutput
        {
            TextOutput1,
            TextOutput2,
            TextOutput3,
            TextOutput4,
            TextOutput5,
            TextOutput6
        }


        public void ProgrammerConversions(string val1, string val2 = "", string val3 = "", string val4 = "")
        {
            CurrentConversionType = ConversionTypes.Programmer;
            HideClearAllGroups();
            CurrentActiveOutput = ActiveOutput.TextOutput1;
            SetGroup(Groups.Group1, "Integer", val1);
            SetGroup(Groups.Group2, "Hexadecimal", val2);
            SetGroup(Groups.Group3, "Octal", val3);
            SetGroup(Groups.Group4, "Binary", val4);
        }

        public void PercentConversions(string val1, string val2 = "")
        {
            CurrentConversionType = ConversionTypes.Percent;
            HideClearAllGroups();
            CurrentActiveOutput = ActiveOutput.TextOutput1;
            SetGroup(Groups.Group1, "Decimal", val1);
            SetGroup(Groups.Group2, "Percent", val2);
        }

        public void WeightConversions(string val1, string val2 = "", string val3 = "", string val4 = "", string val5 = "")
        {
            CurrentConversionType = ConversionTypes.Weight;
            HideClearAllGroups();
            CurrentActiveOutput = ActiveOutput.TextOutput1;
            SetGroup(Groups.Group1, "MilliGrams", val1);
            SetGroup(Groups.Group2, "Grams", val2);
            SetGroup(Groups.Group3, "KiloGrams", val3);
            SetGroup(Groups.Group4, "Ounces", val4);
            SetGroup(Groups.Group5, "Pounds", val5);
        }

        public void TemperatureConversions(string val1, string val2 = "", string val3 = "")
        {
            CurrentConversionType = ConversionTypes.Temperature;
            HideClearAllGroups();
            CurrentActiveOutput = ActiveOutput.TextOutput1;
            SetGroup(Groups.Group1, "Celcius", val1);
            SetGroup(Groups.Group2, "Fahrenheit", val2);
            SetGroup(Groups.Group3, "Kelvin", val3);
        }

        public void LengthConversions(string val1, string val2 = "", string val3 = "", string val4 = "", string val5 = "", string val6 = "")
        {
            CurrentConversionType = ConversionTypes.Length;
            HideClearAllGroups();
            CurrentActiveOutput = ActiveOutput.TextOutput1;
            SetGroup(Groups.Group1, "MilliMeters", val1);
            SetGroup(Groups.Group2, "CentiMeters", val2);
            SetGroup(Groups.Group3, "Meters", val3);
            SetGroup(Groups.Group4, "KiloMeters", val4);
            SetGroup(Groups.Group5, "Inches", val5);
            SetGroup(Groups.Group6, "Feet", val6);
        }

        public void DataConversions(string val1, string val2 = "", string val3 = "", string val4 = "", string val5 = "")
        {
            CurrentConversionType = ConversionTypes.Data;
            HideClearAllGroups();
            CurrentActiveOutput = ActiveOutput.TextOutput1;
            SetGroup(Groups.Group1, "Bytes", val1);
            SetGroup(Groups.Group2, "KiloBytes", val2);
            SetGroup(Groups.Group3, "MegaBytes", val3);
            SetGroup(Groups.Group4, "GigaBytes", val4);
            SetGroup(Groups.Group5, "TeraBytes", val5);
        }

        public void TimeConversions(string val1, string val2 = "", string val3 = "")
        {
            CurrentConversionType = ConversionTypes.Time;
            HideClearAllGroups();
            CurrentActiveOutput = ActiveOutput.TextOutput1;
            SetGroup(Groups.Group1, "Seconds", val1);
            SetGroup(Groups.Group2, "Minutes", val2);
            SetGroup(Groups.Group3, "Hours", val3);
        }

        public void HideClearAllGroups()
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

        public void SetGroup(Groups group, string label, string val)
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

        public void ConvertActiveInput()
        {
            switch (CurrentConversionType)
            {
                // == PROGRAMER ==
                case ConversionTypes.Programmer:
                    var prog = new Programmer();
                    // get input value
                    switch (CurrentActiveOutput)
                    {
                        case ActiveOutput.TextOutput1:
                            prog.Integer = TextOutput1.Text;
                            // get other values
                            try
                            {
                                prog.Hexadecimal = int.Parse(prog.Integer).ToString("X");
                                prog.Octal = Convert.ToString(int.Parse(prog.Integer), 8);
                                prog.Binary = Convert.ToString(int.Parse(prog.Integer), 2);

                                SetOutputValues(prog.Integer, prog.Hexadecimal, prog.Octal, prog.Binary);
                            }
                            catch
                            {
                                TextOutput1.Text = "NaN";
                            }


                            break;

                        case ActiveOutput.TextOutput2:
                            prog.Hexadecimal = TextOutput2.Text;
                            // get other values
                            try
                            {
                                prog.Integer = Convert.ToInt32(prog.Hexadecimal, 16).ToString();
                                prog.Octal = Convert.ToString(int.Parse(prog.Integer), 8);
                                prog.Binary = Convert.ToString(int.Parse(prog.Integer), 2);

                                SetOutputValues(prog.Integer, prog.Hexadecimal, prog.Octal, prog.Binary);
                            }
                            catch
                            {
                                TextOutput2.Text = "NaN";
                            }
                            break;

                        case ActiveOutput.TextOutput3:
                            prog.Octal = TextOutput3.Text;
                            // get other values
                            try
                            {
                                var intVal = Convert.ToInt32(prog.Octal, 8);
                                prog.Integer = intVal.ToString();
                                prog.Hexadecimal = intVal.ToString("X");
                                prog.Binary = Convert.ToString(intVal, 2);

                                SetOutputValues(prog.Integer, prog.Hexadecimal, prog.Octal, prog.Binary);
                            }
                            catch
                            {
                                TextOutput3.Text = "NaN";
                            }
                            break;

                        case ActiveOutput.TextOutput4:
                            prog.Binary = TextOutput4.Text;
                            // get other values
                            try
                            {
                                var intVal = Convert.ToInt32(prog.Binary, 2);
                                prog.Integer = intVal.ToString();
                                prog.Hexadecimal = intVal.ToString("X");
                                prog.Octal = Convert.ToString(intVal, 8);

                                SetOutputValues(prog.Integer, prog.Hexadecimal, prog.Octal, prog.Binary);
                            }
                            catch
                            {
                                TextOutput4.Text = "NaN";
                            }
                            break;

                        default:
                            DisplayErrors(4);
                            break;
                    }
                    break;

                // == PERCENT ==
                case ConversionTypes.Percent:
                    var percent = new Percent();
                    switch (CurrentActiveOutput)
                    {
                        case ActiveOutput.TextOutput1:
                            percent.DecimalVal = TextOutput1.Text;
                            // get other values
                            try
                            {
                                percent.PercentVal = (decimal.Parse(percent.DecimalVal) * 100).ToString();
                                SetOutputValues(percent.DecimalVal, percent.PercentVal);
                            }
                            catch
                            {
                                TextOutput1.Text = "NaN";
                            }
                            break;

                        case ActiveOutput.TextOutput2:
                            percent.PercentVal = TextOutput2.Text;
                            // get other values
                            try
                            {
                                percent.DecimalVal = (decimal.Parse(percent.PercentVal) / 100).ToString();

                                SetOutputValues(percent.DecimalVal, percent.PercentVal);
                            }
                            catch
                            {
                                TextOutput1.Text = "NaN";
                            }
                            break;

                        default:
                            DisplayErrors(2);
                            break;
                    }
                    break;

                // == WEIGHT ==
                case ConversionTypes.Weight:
                    var weight = new Weight();
                    switch (CurrentActiveOutput)
                    {
                        case ActiveOutput.TextOutput1:
                            weight.Milligrams = TextOutput1.Text;
                            // get other values
                            try
                            {
                                double milligrams = double.Parse(weight.Milligrams);
                                weight.Grams = (milligrams * 0.001).ToString();
                                weight.Kilograms = (milligrams * 0.000001).ToString();
                                weight.Ounces = (milligrams * 0.000035).ToString();
                                weight.Pounds = (milligrams * 0.000002).ToString();

                                SetOutputValues(weight.Milligrams, weight.Grams, weight.Kilograms, weight.Ounces, weight.Pounds);
                            }
                            catch
                            {
                                TextOutput1.Text = "NaN";
                            }
                            break;

                        case ActiveOutput.TextOutput2:
                            weight.Grams = TextOutput2.Text;
                            // get other values
                            try
                            {
                                double grams = double.Parse(weight.Grams);
                                weight.Milligrams = (grams * 1000).ToString();
                                weight.Kilograms = (grams / 1000).ToString();
                                weight.Ounces = (grams * 0.035274).ToString();
                                weight.Pounds = (grams * 0.002205).ToString();

                                SetOutputValues(weight.Milligrams, weight.Grams, weight.Kilograms, weight.Ounces, weight.Pounds);
                            }
                            catch
                            {
                                TextOutput2.Text = "NaN";
                            }
                            break;

                        case ActiveOutput.TextOutput3:
                            weight.Kilograms = TextOutput3.Text;
                            // get other values
                            try
                            {
                                double kilograms = double.Parse(weight.Kilograms);
                                weight.Milligrams = (kilograms * 1000000).ToString();
                                weight.Grams = (kilograms * 1000).ToString();
                                weight.Ounces = (kilograms * 35.27396).ToString();
                                weight.Pounds = (kilograms * 2.204623).ToString();

                                SetOutputValues(weight.Milligrams, weight.Grams, weight.Kilograms, weight.Ounces, weight.Pounds);
                            }
                            catch
                            {
                                TextOutput3.Text = "NaN";
                            }
                            break;

                        case ActiveOutput.TextOutput4:
                            weight.Ounces = TextOutput4.Text;
                            // get other values
                            try
                            {
                                double ounces = double.Parse(weight.Ounces);
                                weight.Milligrams = (ounces * 28349.52).ToString();
                                weight.Grams = (ounces * 28.34952).ToString();
                                weight.Kilograms = (ounces * 0.02835).ToString();
                                weight.Pounds = (ounces * 0.0625).ToString();

                                SetOutputValues(weight.Milligrams, weight.Grams, weight.Kilograms, weight.Ounces, weight.Pounds);
                            }
                            catch
                            {
                                TextOutput4.Text = "NaN";
                            }
                            break;
                        case ActiveOutput.TextOutput5:
                            weight.Pounds = TextOutput5.Text;
                            // get other values
                            try
                            {
                                double pounds = double.Parse(weight.Pounds);
                                weight.Milligrams = (pounds * 453592.4).ToString();
                                weight.Grams = (pounds * 453.5924).ToString();
                                weight.Kilograms = (pounds * 0.4535924).ToString();
                                weight.Ounces = (pounds * 16).ToString();

                                SetOutputValues(weight.Milligrams, weight.Grams, weight.Kilograms, weight.Ounces, weight.Pounds);
                            }
                            catch
                            {
                                TextOutput5.Text = "NaN";
                            }
                            break;

                        default:
                            DisplayErrors(5);
                            break;
                    }
                    break;

                // == TEMPERATURE ==
                case ConversionTypes.Temperature:
                    var temp = new Temperature();
                    switch (CurrentActiveOutput)
                    {
                        case ActiveOutput.TextOutput1:
                            temp.Celsius = TextOutput1.Text;
                            // get other values
                            try
                            {
                                double celsius = double.Parse(temp.Celsius);
                                temp.Fahrenheit = ((celsius * 9 / 5) + 32).ToString();
                                temp.Kelvin = (celsius + 273.15).ToString();

                                SetOutputValues(temp.Celsius, temp.Fahrenheit, temp.Kelvin);
                            }
                            catch
                            {
                                TextOutput1.Text = "NaN";
                            }
                            break;
                        case ActiveOutput.TextOutput2:
                            temp.Fahrenheit = TextOutput2.Text;
                            // get other values
                            try
                            {
                                double fahrenheit = double.Parse(temp.Fahrenheit);
                                temp.Celsius = ((fahrenheit - 32) * 5 / 9).ToString();
                                temp.Kelvin = ((fahrenheit - 32) * 5 / 9 + 273.15).ToString();

                                SetOutputValues(temp.Celsius, temp.Fahrenheit, temp.Kelvin);
                            }
                            catch
                            {
                                TextOutput2.Text = "NaN";
                            }
                            break;
                        case ActiveOutput.TextOutput3:
                            temp.Kelvin = TextOutput3.Text;
                            // get other values
                            try
                            {
                                double kelvin = double.Parse(temp.Kelvin);
                                temp.Celsius = (kelvin - 273.15).ToString();
                                temp.Fahrenheit = ((kelvin - 273.15) * 9 / 5 + 32).ToString();

                                SetOutputValues(temp.Celsius, temp.Fahrenheit, temp.Kelvin);
                            }
                            catch
                            {
                                TextOutput3.Text = "NaN";
                            }
                            break;
                        default:
                            DisplayErrors(3);
                            break;
                    }
                    break;

                // == LENGTH ==
                case ConversionTypes.Length:
                    var length = new Length();
                    switch (CurrentActiveOutput)
                    {
                        case ActiveOutput.TextOutput1:
                            length.Millimeter = TextOutput1.Text;
                            // get other values
                            try
                            { // mm cm m km inches feet
                                double millimeter = double.Parse(length.Millimeter);
                                length.Centimeter = (millimeter * 0.1).ToString();
                                length.Meter = (millimeter * 0.001).ToString();
                                length.Kilometer = (millimeter * 0.000001).ToString();
                                length.Inches = (millimeter * 0.0393701).ToString();
                                length.Feet = (millimeter * 0.00328084).ToString();

                                SetOutputValues(length.Millimeter, length.Centimeter, length.Meter, length.Kilometer, length.Inches, length.Feet);
                            }
                            catch
                            {
                                TextOutput1.Text = "NaN";
                            }
                            break;

                        case ActiveOutput.TextOutput2:
                            length.Centimeter = TextOutput2.Text;
                            // get other values
                            try
                            {
                                double centimeter = double.Parse(length.Centimeter);
                                length.Millimeter = (centimeter * 10).ToString();
                                length.Meter = (centimeter * 0.01).ToString();
                                length.Kilometer = (centimeter * 0.00001).ToString();
                                length.Inches = (centimeter * 0.393701).ToString();
                                length.Feet = (centimeter * 0.0328084).ToString();

                                SetOutputValues(length.Millimeter, length.Centimeter, length.Meter, length.Kilometer, length.Inches, length.Feet);
                            }
                            catch
                            {
                                TextOutput2.Text = "NaN";
                            }
                            break;

                        case ActiveOutput.TextOutput3:
                            length.Meter = TextOutput3.Text;
                            // get other values
                            try
                            {
                                double meter = double.Parse(length.Meter);
                                length.Millimeter = (meter * 1000).ToString();
                                length.Centimeter = (meter * 100).ToString();
                                length.Kilometer = (meter * 0.001).ToString();
                                length.Inches = (meter * 39.3701).ToString();
                                length.Feet = (meter * 3.28084).ToString();

                                SetOutputValues(length.Millimeter, length.Centimeter, length.Meter, length.Kilometer, length.Inches, length.Feet);
                            }
                            catch
                            {
                                TextOutput3.Text = "NaN";
                            }
                            break;

                        case ActiveOutput.TextOutput4:
                            length.Kilometer = TextOutput4.Text;
                            // get other values
                            try
                            {
                                double kilometer = double.Parse(length.Kilometer);
                                length.Millimeter = (kilometer * 1000000).ToString();
                                length.Centimeter = (kilometer * 100000).ToString();
                                length.Meter = (kilometer * 1000).ToString();
                                length.Inches = (kilometer * 39370.1).ToString();
                                length.Feet = (kilometer * 3280.84).ToString();

                                SetOutputValues(length.Millimeter, length.Centimeter, length.Meter, length.Kilometer, length.Inches, length.Feet);
                            }
                            catch
                            {
                                TextOutput4.Text = "NaN";
                            }
                            break;
                        case ActiveOutput.TextOutput5:
                            length.Inches = TextOutput5.Text;
                            // get other values
                            try
                            {
                                double inches = double.Parse(length.Inches);
                                length.Millimeter = (inches * 25.4).ToString();
                                length.Centimeter = (inches * 2.54).ToString();
                                length.Meter = (inches * 0.0254).ToString();
                                length.Kilometer = (inches * 0.0000254).ToString();
                                length.Feet = (inches * 0.0833333).ToString();

                                SetOutputValues(length.Millimeter, length.Centimeter, length.Meter, length.Kilometer, length.Inches, length.Feet);
                            }
                            catch
                            {
                                TextOutput5.Text = "NaN";
                            }
                            break;
                        case ActiveOutput.TextOutput6:
                            length.Feet = TextOutput6.Text;
                            // get other values
                            try
                            {
                                double feet = double.Parse(length.Feet);
                                length.Millimeter = (feet * 304.8).ToString();
                                length.Centimeter = (feet * 30.48).ToString();
                                length.Meter = (feet * 0.3048).ToString();
                                length.Kilometer = (feet * 0.0003048).ToString();
                                length.Inches = (feet * 12).ToString();

                                SetOutputValues(length.Millimeter, length.Centimeter, length.Meter, length.Kilometer, length.Inches, length.Feet);
                            }
                            catch
                            {
                                TextOutput6.Text = "NaN";
                            }
                            break;

                        default:
                            DisplayErrors(6);
                            break;
                    }
                    break;

                // == DATA ==
                case ConversionTypes.Data:
                    var data = new Data();
                    switch (CurrentActiveOutput)
                    {
                        case ActiveOutput.TextOutput1:
                            data.Bytes = TextOutput1.Text;
                            // get other values
                            try
                            {
                                double bytes = double.Parse(data.Bytes);
                                data.Kilobytes = (bytes * 0.001).ToString();
                                data.Megabytes = (bytes * 0.000001).ToString();
                                data.Gigabytes = (bytes * 0.000000001).ToString();
                                data.Terabytes = (bytes * 0.000000000001).ToString();

                                SetOutputValues(data.Bytes, data.Kilobytes, data.Megabytes, data.Gigabytes, data.Terabytes);
                            }
                            catch
                            {
                                TextOutput1.Text = "NaN";
                            }
                            break;
                        case ActiveOutput.TextOutput2:
                            data.Kilobytes = TextOutput2.Text;
                            // get other values
                            try
                            {
                                double kilobytes = double.Parse(data.Kilobytes);
                                data.Bytes = (kilobytes * 1000).ToString();
                                data.Megabytes = (kilobytes * 0.001).ToString();
                                data.Gigabytes = (kilobytes * 0.000001).ToString();
                                data.Terabytes = (kilobytes * 0.000000001).ToString();

                                SetOutputValues(data.Bytes, data.Kilobytes, data.Megabytes, data.Gigabytes, data.Terabytes);
                            }
                            catch
                            {
                                TextOutput2.Text = "NaN";
                            }
                            break;
                        case ActiveOutput.TextOutput3:
                            data.Megabytes = TextOutput3.Text;
                            // get other values
                            try
                            {
                                double megabytes = double.Parse(data.Megabytes);
                                data.Bytes = (megabytes * 1000000).ToString();
                                data.Kilobytes = (megabytes * 1000).ToString();
                                data.Gigabytes = (megabytes * 0.001).ToString();
                                data.Terabytes = (megabytes * 0.000001).ToString();

                                SetOutputValues(data.Bytes, data.Kilobytes, data.Megabytes, data.Gigabytes, data.Terabytes);
                            }
                            catch
                            {
                                TextOutput3.Text = "NaN";
                            }
                            break;
                        case ActiveOutput.TextOutput4:
                            data.Gigabytes = TextOutput4.Text;
                            // get other values
                            try
                            {
                                double gigabytes = double.Parse(data.Gigabytes);
                                data.Bytes = (gigabytes * 1000000000).ToString();
                                data.Kilobytes = (gigabytes * 1000000).ToString();
                                data.Megabytes = (gigabytes * 1000).ToString();
                                data.Terabytes = (gigabytes * 0.001).ToString();

                                SetOutputValues(data.Bytes, data.Kilobytes, data.Megabytes, data.Gigabytes, data.Terabytes);
                            }
                            catch
                            {
                                TextOutput4.Text = "NaN";
                            }
                            break;
                        case ActiveOutput.TextOutput5:
                            data.Terabytes = TextOutput5.Text;
                            // get other values
                            try
                            {
                                double terabytes = double.Parse(data.Terabytes);
                                data.Bytes = (terabytes * 1000000000000).ToString();
                                data.Kilobytes = (terabytes * 1000000000).ToString();
                                data.Megabytes = (terabytes * 1000000).ToString();
                                data.Gigabytes = (terabytes * 1000).ToString();

                                SetOutputValues(data.Bytes, data.Kilobytes, data.Megabytes, data.Gigabytes, data.Terabytes);
                            }
                            catch
                            {
                                TextOutput5.Text = "NaN";
                            }
                            break;
                        default:
                            DisplayErrors(5);
                            break;
                    }
                    break;

                // == TIME ==
                case ConversionTypes.Time:
                    var time = new Time();
                    switch (CurrentActiveOutput)
                    {
                        case ActiveOutput.TextOutput1:
                            time.Seconds = TextOutput1.Text;
                            // get other values
                            try
                            {
                                double seconds = double.Parse(time.Seconds);
                                time.Minutes = (seconds * 0.0166667).ToString();
                                time.Hours = (seconds * 0.000277778).ToString();

                                SetOutputValues(time.Seconds, time.Minutes, time.Hours);
                            }
                            catch
                            {
                                TextOutput1.Text = "NaN";
                            }
                            break;

                        case ActiveOutput.TextOutput2:
                            time.Minutes = TextOutput2.Text;
                            // get other values
                            try
                            {
                                double minutes = double.Parse(time.Minutes);
                                time.Seconds = (minutes * 60).ToString();
                                time.Hours = (minutes * 0.0166667).ToString();

                                SetOutputValues(time.Seconds, time.Minutes, time.Hours);
                            }
                            catch
                            {
                                TextOutput2.Text = "NaN";
                            }
                            break;

                        case ActiveOutput.TextOutput3:
                            time.Hours = TextOutput3.Text;
                            // get other values
                            try
                            {
                                double hours = double.Parse(time.Hours);
                                time.Seconds = (hours * 3600).ToString();
                                time.Minutes = (hours * 60).ToString();

                                SetOutputValues(time.Seconds, time.Minutes, time.Hours);
                            }
                            catch
                            {
                                TextOutput3.Text = "NaN";
                            }
                            break;

                        default:
                            DisplayErrors(3);
                            break;
                    }
                    break;

                default:
                    DisplayErrors(6);
                    break;
            }
        }

        private void SetOutputValues(string out1 = null, string out2 = null, string out3 = null, string out4 = null, string out5 = null, string out6 = null)
        {
            if(out1 != null)
            {
                TextOutput1.Text = out1;
            }
            if (out2 != null)
            {
                TextOutput2.Text = out2;
            }
            if (out3 != null)
            {
                TextOutput3.Text = out3;
            }
            if (out4 != null)
            {
                TextOutput4.Text = out4;
            }
            if (out5 != null)
            {
                TextOutput5.Text = out5;
            }
            if (out6 != null)
            {
                TextOutput6.Text = out6;
            }
        }

        private void DisplayErrors(int errorOutputCount, string errorMessage = "NaN") {
            if(errorOutputCount == 1)
            {
                TextOutput1.Text = errorMessage;
            }
            else if(errorOutputCount == 2)
            {
                TextOutput1.Text = errorMessage;
                TextOutput2.Text = errorMessage;
            }
            else if (errorOutputCount == 3)
            {
                TextOutput1.Text = errorMessage;
                TextOutput2.Text = errorMessage;
                TextOutput3.Text = errorMessage;
            }
            else if (errorOutputCount == 4)
            {
                TextOutput1.Text = errorMessage;
                TextOutput2.Text = errorMessage;
                TextOutput3.Text = errorMessage;
                TextOutput4.Text = errorMessage;
            }
            else if (errorOutputCount == 5)
            {
                TextOutput1.Text = errorMessage;
                TextOutput2.Text = errorMessage;
                TextOutput3.Text = errorMessage;
                TextOutput4.Text = errorMessage;
                TextOutput5.Text = errorMessage;
            }
            else if (errorOutputCount == 6)
            {
                TextOutput1.Text = errorMessage;
                TextOutput2.Text = errorMessage;
                TextOutput3.Text = errorMessage;
                TextOutput4.Text = errorMessage;
                TextOutput5.Text = errorMessage;
                TextOutput6.Text = errorMessage;
            } else
            {
                // all outputs anyway!
                TextOutput1.Text = errorMessage;
                TextOutput2.Text = errorMessage;
                TextOutput3.Text = errorMessage;
                TextOutput4.Text = errorMessage;
                TextOutput5.Text = errorMessage;
                TextOutput6.Text = errorMessage;
            }
        }
    }
}
