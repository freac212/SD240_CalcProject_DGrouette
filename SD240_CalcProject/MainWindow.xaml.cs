using Jint;
using SD240_CalcProject.ConversionModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SD240_CalcProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    // Calc notes:
    // - textbox doesn't take regular input, just button presses.
    // - Keyboard keys are assigned to those button presses, ex: backspace and backspace button.

    public partial class MainWindow : Window
    {
        private static bool CanPlaceOperatorValue = false;

        public MainWindow()
        {
            InitializeComponent();
            InputBox.Text = "0";
        }

        private void Button_Click_0(object sender, RoutedEventArgs e)
        {
            AddTextToInput("0");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddTextToInput("1");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddTextToInput("2");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            AddTextToInput("3");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AddTextToInput("4");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            AddTextToInput("5");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            AddTextToInput("6");
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            AddTextToInput("7");
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            AddTextToInput("8");
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            AddTextToInput("9");
        }

        private void Button_Click_Backspace(object sender, RoutedEventArgs e)
        {
            if (InputBox.Text.Length > 0)
                InputBox.Text = InputBox.Text.Remove(InputBox.Text.Length - 1);
        }

        private void Button_Click_Clear(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void Button_Click_Plus(object sender, RoutedEventArgs e)
        {
            // Take what ever number is in the inputbox
            // place a "+" after it
            // add that stuff to the OperatorBox
            // Clear InputBox
            if (InputBoxIsNotEmpty(InputBox) || CanPlaceOperatorValue)
            {
                AddTextOperatorBox("+", InputBox.Text);
                CanPlaceOperatorValueSwitch();
                ClearInputBox();
            }
        }

        private void Button_Click_Equals(object sender, RoutedEventArgs e)
        {
            // Take whats in the inputbox
            // append it to the operators box text. 
            // Convert Operators to something the compiler can work with
            // Clear opBox
            // set inputBox to the value.

            var equation = OperationBox.Text + InputBox.Text;

            // Checking for infinite...
            if (equation.Contains("∞"))
            {
                ClearAll();
                return;
            }

            // handle broken operators
            // If there's an operator with nothing to the right of it, remove it.
            //var brokenOpsExp = @"(\+|\/|\*|\%|\-|\^)$|(\s\%\s)$";
            //MatchCollection matches = Regex.Matches(equation, brokenOpsExp);
            //foreach (Match m in matches)
            //{
            //}
            equation = equation.Trim('+', '/', '*', '%', '^', ' ');
            equation = equation.TrimEnd('-');


            // To convert:
            //  > 5 mod 3 -> 5 % 3
            equation = equation.Replace("Mod", "%");

            //  > "4^2 -> Math.pow(4,2)
            // match first, replace, match again, repeat till done.
            while (ContainsMatch(equation, out Match match))
            {
                equation = equation.Replace(match.Value, Math.Pow(double.Parse(match.Groups[2].Value), double.Parse(match.Groups[3].Value)).ToString());
            }

            //  > !(4) -> can perform these ops myself 8 x 8 -> 8 times
            if (equation.Contains("!"))
            {
                var expression = @"(\!\(\d+\))";
                MatchCollection mc = Regex.Matches(equation, expression);

                foreach (Match m in mc)
                {
                    var digitExp = @"(\d+)";
                    MatchCollection digitMatch = Regex.Matches(m.Value, digitExp);

                    var parsedVal = int.Parse(digitMatch[0].Value);
                    string factorialConverted = "(";

                    for (int i = 0; i < parsedVal; i++)
                    {
                        if (i == parsedVal - 1)
                            factorialConverted += parsedVal - i;
                        else
                            factorialConverted += parsedVal - i + "*";
                    }
                    factorialConverted += ")";

                    equation = equation.Replace(m.Value, factorialConverted);
                }
            }

            //  > "√(7) -> Math.sqrt(7)
            equation = equation.Replace("√", " Math.sqrt");

            var output = new Engine()
                .Execute(equation)
                .GetCompletionValue()
                .ToString();

            InputBox.Text = output;
            ClearOperationBox();

        }

        private void Button_Click_Minus(object sender, RoutedEventArgs e)
        {
            // Take what ever number is in the inputbox
            // place a "+" after it
            // add that stuff to the OperatorBox
            // Clear InputBox
            if (InputBoxIsNotEmpty(InputBox) || CanPlaceOperatorValue)
            {
                AddTextOperatorBox("-", InputBox.Text);
                CanPlaceOperatorValueSwitch();
                ClearInputBox();
            }
        }

        private void Button_Click_Multiply(object sender, RoutedEventArgs e)
        {
            if (InputBoxIsNotEmpty(InputBox) || CanPlaceOperatorValue)
            {
                AddTextOperatorBox("*", InputBox.Text);
                CanPlaceOperatorValueSwitch();
                ClearInputBox();
            }
        }


        private void Button_Click_Divide(object sender, RoutedEventArgs e)
        {
            if (InputBoxIsNotEmpty(InputBox) || CanPlaceOperatorValue)
            {
                AddTextOperatorBox("/", InputBox.Text);
                CanPlaceOperatorValueSwitch();
                ClearInputBox();
            }
        }


        private void Button_Click_Modulus(object sender, RoutedEventArgs e)
        {
            if (InputBoxIsNotEmpty(InputBox) || CanPlaceOperatorValue)
            {
                AddTextOperatorBox(" Mod ", InputBox.Text);
                CanPlaceOperatorValueSwitch();
                ClearInputBox();
            }
        }

        private void Button_Click_Factorial(object sender, RoutedEventArgs e)
        {
            // ++Q Handle this differently
            // Also factorials should max out at 3500... Because anything higher breaks stuff (I.e. that loop down their loops many many many times.)
            if (InputBoxIsNotEmpty(InputBox) && int.Parse(InputBox.Text) > 3500)
            {
                ClearInputBox();
                // ++Q display error
            }
            else if (InputBoxIsNotEmpty(InputBox))
            {
                AddTextOperatorBox("", $"!({InputBox.Text})");
                CanPlaceOperatorValueSwitch();
                ClearInputBox();
            }
        }

        private void Button_Click_Negate(object sender, RoutedEventArgs e)
        {
            // Check if negative exists, if so, remove it, else, add it.
            if (InputBox.Text.Contains("-"))
                InputBox.Text = InputBox.Text.TrimStart('-');
            else
                InputBox.Text = InputBox.Text.Insert(0, "-");
        }

        private void Button_Click_LeftParen(object sender, RoutedEventArgs e)
        {
            // If only a left paren has been placed, and equals is pressed, place the right in automatically.
            // Use a switch, left placed = true, 
        }

        private void Button_Click_RightParen(object sender, RoutedEventArgs e)
        {
            // Cannot be added unless a left paren has been placed. 
        }

        private void Button_Click_SquareRoot(object sender, RoutedEventArgs e)
        {
            if (InputBoxIsNotEmpty(InputBox) || CanPlaceOperatorValue)
            {
                AddTextOperatorBox("", $"√({InputBox.Text})");
                CanPlaceOperatorValueSwitch();
                ClearInputBox();
            }
        }

        private void Button_Click_Decimal(object sender, RoutedEventArgs e)
        {
            if (!InputBox.Text.Contains("."))
            {
                AddTextToInput(".");
            }
        }
        private void Button_Click_Exponent(object sender, RoutedEventArgs e)
        {
            if (InputBoxIsNotEmpty(InputBox) || CanPlaceOperatorValue)
            {
                AddTextOperatorBox("^", InputBox.Text);
                CanPlaceOperatorValueSwitch();
                ClearInputBox();
            }
        }


        // Methods
        private bool ContainsMatch(string equation, out Match equationOut)
        {
            var powerExp = @"((\d+)\^(\d+))";
            MatchCollection powerMatches = Regex.Matches(equation, powerExp);

            if (powerMatches.Count > 0)
            {
                equationOut = powerMatches[0];
                return true;
            }
            else
            {
                equationOut = null;
                return false;
            }
        }
        private void ClearAll()
        {
            ClearOperationBox();
            ClearInputBox();
            AddTextToInput("0");
        }
        private void CanPlaceOperatorValueSwitch()
        {
            CanPlaceOperatorValue = CanPlaceOperatorValue ? false : true;
        }
        private bool InputBoxIsNotEmpty(TextBox inputBox)
        {
            if (!String.IsNullOrEmpty(inputBox.Text) && !String.IsNullOrWhiteSpace(inputBox.Text))
                return true;
            else
                return false;
        }
        private void AddTextOperatorBox(string op, string text)
        {
            OperationBox.Text = OperationBox.Text + text + op;
        }
        private void AddTextToInput(string text)
        {
            // if the input box is not reached char cap, and is not just a single 0 -> append new char to end
            // if text count is only 1, and only contains a 0 -> replace the zero with whatever is incoming in text.
            // if input box only contains zeros(more than one) -> replace with whatever is incoming
            // Find a way to ensure no leading zeros
            // if inputbox only contains zeros, replace with incoming text

            if (CanInsertText(InputBox) && InputBox.Text != "0" || text == ".")
                InputBox.Text += text;
            else if (InputBox.Text.Count() == 1 && InputBox.Text == "0")
                InputBox.Text = text;
            else if (InputBox.Text.All(p => p == '0'))
                InputBox.Text = text;
        }
        private bool CanInsertText(TextBox inputBox)
        {
            if (inputBox.Text.Length >= 0 && inputBox.Text.Length < 32)
                return true;
            else
                return false;
        }
        private void ClearInputBox()
        {
            InputBox.Text = String.Empty;
        }

        private void ClearOperationBox()
        {
            OperationBox.Text = String.Empty;
        }

        private void Button_Click_ConvertionWindow(object sender, RoutedEventArgs e)
        {
            if (ConversionBox.Visibility == Visibility.Visible)
            {
                ConversionBox.Visibility = Visibility.Hidden;
                Application.Current.MainWindow.Width = 357;
            }
            else
            {
                ConversionBox.Visibility = Visibility.Visible;
                ProgrammerConversions("4", "101203", "123123", "7");
                Application.Current.MainWindow.Width = 820;
            }
        }


        public void SetActiveOutput(object sender, RoutedEventArgs e)
        {
            var x = (TextBox)e.Source;
            Enum.TryParse(x.Name, out CurrentActiveOutput);
        }


        private void Button_Click_IntiateConversions(object sender, RoutedEventArgs e)
        {
            // Determine which conversion we're on,
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

                                // Set the values
                                TextOutput1.Text = prog.Integer;
                                TextOutput2.Text = prog.Hexadecimal;
                                TextOutput3.Text = prog.Octal;
                                TextOutput4.Text = prog.Binary;
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

                                // Set the values
                                TextOutput1.Text = prog.Integer;
                                TextOutput2.Text = prog.Hexadecimal;
                                TextOutput3.Text = prog.Octal;
                                TextOutput4.Text = prog.Binary;
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

                                // Set the values
                                TextOutput1.Text = prog.Integer;
                                TextOutput2.Text = prog.Hexadecimal;
                                TextOutput3.Text = prog.Octal;
                                TextOutput4.Text = prog.Binary;
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

                                // Set the values
                                TextOutput1.Text = prog.Integer;
                                TextOutput2.Text = prog.Hexadecimal;
                                TextOutput3.Text = prog.Octal;
                                TextOutput4.Text = prog.Binary;
                            }
                            catch
                            {
                                TextOutput4.Text = "NaN";
                            }
                            break;

                        default:
                            TextOutput1.Text = "NaN";
                            TextOutput2.Text = "NaN";
                            TextOutput3.Text = "NaN";
                            TextOutput4.Text = "NaN";
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

                                // Set the values
                                TextOutput1.Text = percent.DecimalVal;
                                TextOutput2.Text = percent.PercentVal;
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

                                // Set the values
                                TextOutput1.Text = percent.DecimalVal;
                                TextOutput2.Text = percent.PercentVal;
                            }
                            catch
                            {
                                TextOutput1.Text = "NaN";
                            }
                            break;

                        default:
                            TextOutput1.Text = "NaN";
                            TextOutput2.Text = "NaN";
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

                                // Set the values
                                TextOutput1.Text = weight.Milligrams;
                                TextOutput2.Text = weight.Grams;
                                TextOutput3.Text = weight.Kilograms;
                                TextOutput4.Text = weight.Ounces;
                                TextOutput5.Text = weight.Pounds;
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

                                // Set the values
                                TextOutput1.Text = weight.Milligrams;
                                TextOutput2.Text = weight.Grams;
                                TextOutput3.Text = weight.Kilograms;
                                TextOutput4.Text = weight.Ounces;
                                TextOutput5.Text = weight.Pounds;
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

                                // Set the values
                                TextOutput1.Text = weight.Milligrams;
                                TextOutput2.Text = weight.Grams;
                                TextOutput3.Text = weight.Kilograms;
                                TextOutput4.Text = weight.Ounces;
                                TextOutput5.Text = weight.Pounds;
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

                                // Set the values
                                TextOutput1.Text = weight.Milligrams;
                                TextOutput2.Text = weight.Grams;
                                TextOutput3.Text = weight.Kilograms;
                                TextOutput4.Text = weight.Ounces;
                                TextOutput5.Text = weight.Pounds;
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

                                // Set the values
                                TextOutput1.Text = weight.Milligrams;
                                TextOutput2.Text = weight.Grams;
                                TextOutput3.Text = weight.Kilograms;
                                TextOutput4.Text = weight.Ounces;
                                TextOutput5.Text = weight.Pounds;
                            }
                            catch
                            {
                                TextOutput5.Text = "NaN";
                            }
                            break;

                        default:
                            TextOutput1.Text = "NaN";
                            TextOutput2.Text = "NaN";
                            TextOutput3.Text = "NaN";
                            TextOutput4.Text = "NaN";
                            TextOutput5.Text = "NaN";
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

                                // Set the values
                                TextOutput1.Text = temp.Celsius;
                                TextOutput2.Text = temp.Fahrenheit;
                                TextOutput3.Text = temp.Kelvin;
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

                                // Set the values
                                TextOutput1.Text = temp.Celsius;
                                TextOutput2.Text = temp.Fahrenheit;
                                TextOutput3.Text = temp.Kelvin;
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

                                // Set the values
                                TextOutput1.Text = temp.Celsius;
                                TextOutput2.Text = temp.Fahrenheit;
                                TextOutput3.Text = temp.Kelvin;
                            }
                            catch
                            {
                                TextOutput3.Text = "NaN";
                            }
                            break;
                        default:
                            TextOutput1.Text = "NaN";
                            TextOutput2.Text = "NaN";
                            TextOutput3.Text = "NaN";
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

                                // Set the values
                                TextOutput1.Text = length.Millimeter;
                                TextOutput2.Text = length.Centimeter;
                                TextOutput3.Text = length.Meter;
                                TextOutput4.Text = length.Kilometer;
                                TextOutput5.Text = length.Inches;
                                TextOutput6.Text = length.Feet;

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

                                // Set the values
                                TextOutput1.Text = length.Millimeter;
                                TextOutput2.Text = length.Centimeter;
                                TextOutput3.Text = length.Meter;
                                TextOutput4.Text = length.Kilometer;
                                TextOutput5.Text = length.Inches;
                                TextOutput6.Text = length.Feet;

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

                                // Set the values
                                TextOutput1.Text = length.Millimeter;
                                TextOutput2.Text = length.Centimeter;
                                TextOutput3.Text = length.Meter;
                                TextOutput4.Text = length.Kilometer;
                                TextOutput5.Text = length.Inches;
                                TextOutput6.Text = length.Feet;

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

                                // Set the values
                                TextOutput1.Text = length.Millimeter;
                                TextOutput2.Text = length.Centimeter;
                                TextOutput3.Text = length.Meter;
                                TextOutput4.Text = length.Kilometer;
                                TextOutput5.Text = length.Inches;
                                TextOutput6.Text = length.Feet;

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

                                // Set the values
                                TextOutput1.Text = length.Millimeter;
                                TextOutput2.Text = length.Centimeter;
                                TextOutput3.Text = length.Meter;
                                TextOutput4.Text = length.Kilometer;
                                TextOutput5.Text = length.Inches;
                                TextOutput6.Text = length.Feet;

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

                                // Set the values
                                TextOutput1.Text = length.Millimeter;
                                TextOutput2.Text = length.Centimeter;
                                TextOutput3.Text = length.Meter;
                                TextOutput4.Text = length.Kilometer;
                                TextOutput5.Text = length.Inches;
                                TextOutput6.Text = length.Feet;

                            }
                            catch
                            {
                                TextOutput6.Text = "NaN";
                            }
                            break;

                        default:
                            TextOutput1.Text = "NaN";
                            TextOutput2.Text = "NaN";
                            TextOutput3.Text = "NaN";
                            TextOutput4.Text = "NaN";
                            TextOutput5.Text = "NaN";
                            TextOutput6.Text = "NaN";
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

                                // Set the values
                                TextOutput1.Text = data.Bytes;
                                TextOutput2.Text = data.Kilobytes;
                                TextOutput3.Text = data.Megabytes;
                                TextOutput4.Text = data.Gigabytes;
                                TextOutput5.Text = data.Terabytes;

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

                                // Set the values
                                TextOutput1.Text = data.Bytes;
                                TextOutput2.Text = data.Kilobytes;
                                TextOutput3.Text = data.Megabytes;
                                TextOutput4.Text = data.Gigabytes;
                                TextOutput5.Text = data.Terabytes;

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

                                // Set the values
                                TextOutput1.Text = data.Bytes;
                                TextOutput2.Text = data.Kilobytes;
                                TextOutput3.Text = data.Megabytes;
                                TextOutput4.Text = data.Gigabytes;
                                TextOutput5.Text = data.Terabytes;

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

                                // Set the values
                                TextOutput1.Text = data.Bytes;
                                TextOutput2.Text = data.Kilobytes;
                                TextOutput3.Text = data.Megabytes;
                                TextOutput4.Text = data.Gigabytes;
                                TextOutput5.Text = data.Terabytes;

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

                                // Set the values
                                TextOutput1.Text = data.Bytes;
                                TextOutput2.Text = data.Kilobytes;
                                TextOutput3.Text = data.Megabytes;
                                TextOutput4.Text = data.Gigabytes;
                                TextOutput5.Text = data.Terabytes;
                            }
                            catch
                            {
                                TextOutput5.Text = "NaN";
                            }
                            break;
                        default:
                            TextOutput1.Text = "NaN";
                            TextOutput2.Text = "NaN";
                            TextOutput3.Text = "NaN";
                            TextOutput4.Text = "NaN";
                            TextOutput5.Text = "NaN";
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

                                // Set the values
                                TextOutput1.Text = time.Seconds;
                                TextOutput2.Text = time.Minutes;
                                TextOutput3.Text = time.Hours;
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

                                // Set the values
                                TextOutput1.Text = time.Seconds;
                                TextOutput2.Text = time.Minutes;
                                TextOutput3.Text = time.Hours;
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

                                // Set the values
                                TextOutput1.Text = time.Seconds;
                                TextOutput2.Text = time.Minutes;
                                TextOutput3.Text = time.Hours;
                            }
                            catch
                            {
                                TextOutput3.Text = "NaN";
                            }
                            break;

                        default:
                            TextOutput1.Text = "NaN";
                            TextOutput2.Text = "NaN";
                            TextOutput3.Text = "NaN";
                            break;
                    }
                    break;

                default:
                    TextOutput1.Text = "NaN";
                    TextOutput2.Text = "NaN";
                    TextOutput3.Text = "NaN";
                    TextOutput4.Text = "NaN";
                    TextOutput5.Text = "NaN";
                    TextOutput6.Text = "NaN";
                    break;
            }
        }

        private void Button_Click_ProgrammerConversions(object sender, RoutedEventArgs e)
        {
            ProgrammerConversions(InputBox.Text, String.Empty, String.Empty, String.Empty);
        }

        private void Button_Click_PercentConversions(object sender, RoutedEventArgs e)
        {
            PercentConversions(InputBox.Text, String.Empty);
        }

        private void Button_Click_WeightConversions(object sender, RoutedEventArgs e)
        {
            WeightConversions("", "", "", "", "");
        }

        private void Button_Click_TemperatureConversions(object sender, RoutedEventArgs e)
        {
            TemperatureConversions("", "", "");
        }

        private void Button_Click_LengthConversions(object sender, RoutedEventArgs e)
        {
            LengthConversions("", "", "", "", "", "");
        }

        private void Button_Click_FileSizeConversions(object sender, RoutedEventArgs e)
        {
            DataConversions("", "", "", "", "");
        }

        private void Button_Click_TimeConversions(object sender, RoutedEventArgs e)
        {
            TimeConversions("", "", "");
        }

        private void ProgrammerConversions(string val1, string val2, string val3, string val4)
        {
            CurrentConversionType = ConversionTypes.Programmer;
            HideClearAllGroups();
            SetGroup(Groups.Group1, "Integer", val1);
            SetGroup(Groups.Group2, "Hexadecimal", val2);
            SetGroup(Groups.Group3, "Octal", val3);
            SetGroup(Groups.Group4, "Binary", val4);
        }

        private void PercentConversions(string val1, string val2)
        {
            CurrentConversionType = ConversionTypes.Percent;
            HideClearAllGroups();
            SetGroup(Groups.Group1, "Decimal", val1);
            SetGroup(Groups.Group2, "Percent", val2);
        }

        private void WeightConversions(string val1, string val2, string val3, string val4, string val5)
        {
            CurrentConversionType = ConversionTypes.Weight;
            HideClearAllGroups();
            SetGroup(Groups.Group1, "MilliGrams", val1);
            SetGroup(Groups.Group2, "Grams", val2);
            SetGroup(Groups.Group3, "KiloGrams", val3);
            SetGroup(Groups.Group4, "Ounces", val4);
            SetGroup(Groups.Group5, "Pounds", val5);
        }

        private void TemperatureConversions(string val1, string val2, string val3)
        {
            CurrentConversionType = ConversionTypes.Temperature;
            HideClearAllGroups();
            SetGroup(Groups.Group1, "Celcius", val1);
            SetGroup(Groups.Group2, "Fahrenheit", val2);
            SetGroup(Groups.Group3, "Kelvin", val3);
        }

        private void LengthConversions(string val1, string val2, string val3, string val4, string val5, string val6)
        {
            CurrentConversionType = ConversionTypes.Length;
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
            CurrentConversionType = ConversionTypes.Data;
            HideClearAllGroups();
            SetGroup(Groups.Group1, "Bytes", val1);
            SetGroup(Groups.Group2, "KiloBytes", val2);
            SetGroup(Groups.Group3, "MegaBytes", val3);
            SetGroup(Groups.Group4, "GigaBytes", val4);
            SetGroup(Groups.Group5, "TeraBytes", val5);
        }

        private void TimeConversions(string val1, string val2, string val3)
        {
            CurrentConversionType = ConversionTypes.Time;
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

        private ConversionTypes CurrentConversionType = ConversionTypes.Programmer;
        private ActiveOutput CurrentActiveOutput = ActiveOutput.TextOutput1;

        private enum Groups
        {
            Group1,
            Group2,
            Group3,
            Group4,
            Group5,
            Group6
        }

        private enum ConversionTypes
        {
            Programmer,
            Percent,
            Weight,
            Temperature,
            Length,
            Data,
            Time
        }

        private enum ActiveOutput
        {
            TextOutput1,
            TextOutput2,
            TextOutput3,
            TextOutput4,
            TextOutput5,
            TextOutput6
        }

    }
}
