using System;
using System.Collections.Generic;
using System.Data;
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
        }

        private void Button_Click_Plus(object sender, RoutedEventArgs e)
        {
            // Take what ever number is in the inputbox
            // place a "+" after it
            // add that stuff to the OperatorBox
            // Clear InputBox
            if (InputBoxIsNotEmpty(InputBox))
            {
                AddTextOperatorBox("+", InputBox.Text);
                ClearInputBox();
            }
        }

        private void Button_Click_Equals(object sender, RoutedEventArgs e)
        {
            // Take whats in the inputbox
            // append it to the operators box text. 
            // Compute it
            // Clear opBox
            // set inputBox to the value.
            if (InputBoxIsNotEmpty(InputBox))
            {
                var equation = OperationBox.Text + InputBox.Text;

                DataTable dt = new DataTable();
                var v = dt.Compute(equation, "");
                InputBox.Text = v.ToString();
                ClearOperationBox();
            }
        }
        private void Button_Click_Minus(object sender, RoutedEventArgs e)
        {
            // Take what ever number is in the inputbox
            // place a "+" after it
            // add that stuff to the OperatorBox
            // Clear InputBox
            if (InputBoxIsNotEmpty(InputBox))
            {
                AddTextOperatorBox("-", InputBox.Text);
                ClearInputBox();
            }
        }

        private void Button_Click_Multiply(object sender, RoutedEventArgs e)
        {
            // Take what ever number is in the inputbox
            // place a "+" after it
            // add that stuff to the OperatorBox
            // Clear InputBox
            if (InputBoxIsNotEmpty(InputBox))
            {
                AddTextOperatorBox("*", InputBox.Text);
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
                InputBox.Text = InputBox.Text + text;
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
    }
}
