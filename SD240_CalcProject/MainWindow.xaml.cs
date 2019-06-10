using Jint;
using SD240_CalcProject.Controllers;
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
    public partial class MainWindow : Window
    {
        CalculatorController CalcController { get; set; }
        ConversionsController ConversionsController { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            CalcController = new CalculatorController(InputBox, OperationBox);
            ConversionsController = new ConversionsController()
            {
                TextLabel1 = TextLabel1,
                TextLabel2 = TextLabel2,
                TextLabel3 = TextLabel3,
                TextLabel4 = TextLabel4,
                TextLabel5 = TextLabel5,
                TextLabel6 = TextLabel6,
                TextOutput1 = TextOutput1,
                TextOutput2 = TextOutput2,
                TextOutput3 = TextOutput3,
                TextOutput4 = TextOutput4,
                TextOutput5 = TextOutput5,
                TextOutput6 = TextOutput6,
                Group1 = Group1,
                Group2 = Group2,
                Group3 = Group3,
                Group4 = Group4,
                Group5 = Group5,
                Group6 = Group6,
            };
            CalcController.InitializeCalculator();
        }

        // Calculator Buttons/ Actions
        private void Button_Click_0(object sender, RoutedEventArgs e)
        {
            CalcController.AddTextToInput("0");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CalcController.AddTextToInput("1");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CalcController.AddTextToInput("2");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            CalcController.AddTextToInput("3");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            CalcController.AddTextToInput("4");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            CalcController.AddTextToInput("5");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            CalcController.AddTextToInput("6");
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            CalcController.AddTextToInput("7");
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            CalcController.AddTextToInput("8");
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            CalcController.AddTextToInput("9");
        }

        private void Button_Click_Backspace(object sender, RoutedEventArgs e)
        {
            if (InputBox.Text.Length > 0)
                CalcController.OverwriteInput(
                    InputBox.Text.Remove(InputBox.Text.Length - 1)
                    );
        }

        private void Button_Click_Clear(object sender, RoutedEventArgs e)
        {
            CalcController.ClearAll();
        }

        private void Button_Click_Plus(object sender, RoutedEventArgs e)
        {
            // Take what ever number is in the inputbox
            // place a "+" after it
            // add that stuff to the OperatorBox
            // Clear InputBox
            if (CalcController.CanPlaceOperator())
            {
                CalcController.AddTextOperatorBox("+", InputBox.Text);
                CalcController.ClearInputBox();
            }
        }

        private void Button_Click_Minus(object sender, RoutedEventArgs e)
        {
            if (CalcController.CanPlaceOperator())
            {
                CalcController.AddTextOperatorBox("-", InputBox.Text);
                CalcController.ClearInputBox();
            }
        }

        private void Button_Click_Multiply(object sender, RoutedEventArgs e)
        {
            if (CalcController.CanPlaceOperator())
            {
                CalcController.AddTextOperatorBox("*", InputBox.Text);
                CalcController.ClearInputBox();
            }
        }

        private void Button_Click_Divide(object sender, RoutedEventArgs e)
        {
            if (CalcController.CanPlaceOperator())
            {
                CalcController.AddTextOperatorBox("/", InputBox.Text);
                CalcController.ClearInputBox();
            }
        }

        private void Button_Click_Modulus(object sender, RoutedEventArgs e)
        {
            if (CalcController.CanPlaceOperator())
            {
                CalcController.AddTextOperatorBox(" Mod ", InputBox.Text);
                CalcController.ClearInputBox();
            }
        }

        private void Button_Click_Factorial(object sender, RoutedEventArgs e)
        {
            // Also factorial max out at 3500... Because anything higher breaks stuff (I.e. that loop down their loops many many many times. Also overflows so)
            if (CalcController.InputBoxIsNotEmpty(InputBox) && int.Parse(InputBox.Text) > 3500)
            {
                CalcController.OverwriteInput("NaN");
            }
            else if (CalcController.CanPlaceOperator())
            {
                CalcController.AddTextOperatorBox("", $"!({InputBox.Text})");
                CalcController.ClearInputBox();
            }
        }

        private void Button_Click_SquareRoot(object sender, RoutedEventArgs e)
        {
            if (CalcController.CanPlaceOperator())
            {
                CalcController.AddTextOperatorBox("", $"√({InputBox.Text})");
                CalcController.ClearInputBox();
            }
        }

        private void Button_Click_Exponent(object sender, RoutedEventArgs e)
        {
            if (CalcController.CanPlaceOperator())
            {
                CalcController.AddTextOperatorBox("^", InputBox.Text);
                CalcController.ClearInputBox();
            }
        }

        private void Button_Click_Decimal(object sender, RoutedEventArgs e)
        {
            if (!InputBox.Text.Contains("."))
            {
                CalcController.AddTextToInput(".");
            }
        }

        private void Button_Click_Negate(object sender, RoutedEventArgs e)
        {
            // Check if negative exists, if so, remove it, else, add it.
            if (InputBox.Text.Contains("-"))
                InputBox.Text = InputBox.Text.TrimStart('-');
            else
                CalcController.AddTextToInput("-", 0);
        }

        private void Button_Click_Equals(object sender, RoutedEventArgs e)
        {
            var equation = OperationBox.Text + InputBox.Text;

            var result = CalcController.CalculateResult(equation);

            if (result != null)
            {
                CalcController.OverwriteInput(result);
                CalcController.ClearOperationBox();
            }
            else
            {
                CalcController.ClearAll();
            }
        }


        // Convertions Buttons/ Actions
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
                ConversionsController.ProgrammerConversions(InputBox.Text);
                ConversionsController.ConvertActiveInput();
                Application.Current.MainWindow.Width = 820;
            }
        }

        public void SetActiveOutput(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)e.Source;
            Enum.TryParse(textBox.Name, out ConversionsController.CurrentActiveOutput);
        }

        private void Button_Click_IntiateConversions(object sender, RoutedEventArgs e)
        {
            ConversionsController.ConvertActiveInput();
        }

        private void Button_Click_ProgrammerConversions(object sender, RoutedEventArgs e)
        {
            ConversionsController.ProgrammerConversions(InputBox.Text);
            ConversionsController.ConvertActiveInput();
        }

        private void Button_Click_PercentConversions(object sender, RoutedEventArgs e)
        {
            ConversionsController.PercentConversions(InputBox.Text);
            ConversionsController.ConvertActiveInput();
        }

        private void Button_Click_WeightConversions(object sender, RoutedEventArgs e)
        {
            ConversionsController.WeightConversions(InputBox.Text);
            ConversionsController.ConvertActiveInput();
        }

        private void Button_Click_TemperatureConversions(object sender, RoutedEventArgs e)
        {
            ConversionsController.TemperatureConversions(InputBox.Text);
            ConversionsController.ConvertActiveInput();
        }

        private void Button_Click_LengthConversions(object sender, RoutedEventArgs e)
        {
            ConversionsController.LengthConversions(InputBox.Text);
            ConversionsController.ConvertActiveInput();
        }

        private void Button_Click_FileSizeConversions(object sender, RoutedEventArgs e)
        {
            ConversionsController.DataConversions(InputBox.Text);
            ConversionsController.ConvertActiveInput();
        }

        private void Button_Click_TimeConversions(object sender, RoutedEventArgs e)
        {
            ConversionsController.TimeConversions(InputBox.Text);
            ConversionsController.ConvertActiveInput();
        }

    }
}
