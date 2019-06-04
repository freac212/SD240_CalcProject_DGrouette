using Jint;
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
            ClearInputBox();
            CanPlaceOperatorValue = false;
            ClearOperationBox();
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
            if(equation.Contains("∞"))
            {
                ClearOperationBox();
                ClearInputBox();
                return;
            }

            // handle broken operators
            // If there's an operator with nothing to the right of it, remove it.
            var brokenOpsExp = @"(\+|\/|\*|\%|\-)$|(\s\%\s)$";
            MatchCollection matches = Regex.Matches(equation, brokenOpsExp);
            foreach (Match m in matches)
            {
                equation = equation.Replace(m.Value, "");
            }

            // To convert:
            //  > 5 mod 3 -> 5 % 3
            equation = equation.Replace("Mod", "%");

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

        private void CanPlaceOperatorValueSwitch()
        {
            CanPlaceOperatorValue = CanPlaceOperatorValue ? false : true;
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

        // Methods
        private bool InputBoxIsNotEmpty(TextBox inputBox)
        {
            if (!String.IsNullOrEmpty(InputBox.Text) && !String.IsNullOrWhiteSpace(InputBox.Text))
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
            if (CanInsertText(InputBox))
                InputBox.Text += text;
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
            InputBox.Text = "";
        }

        private void ClearOperationBox()
        {
            OperationBox.Text = "";
        }

        private void Button_Click_Negate(object sender, RoutedEventArgs e)
        {

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
    }
}
