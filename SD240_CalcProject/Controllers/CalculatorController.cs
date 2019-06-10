using Jint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SD240_CalcProject.Controllers
{
    class CalculatorController
    {
        private TextBox InputBox { get; set; }
        private TextBox OperationBox { get; set; }


        public CalculatorController(TextBox inputBox, TextBox operationBox)
        {
            InputBox = inputBox;
            OperationBox = operationBox;
        }

        public bool ContainsMatch(string equation, out Match equationOut)
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

        public void InitializeCalculator()
        {
            InputBox.Text = "0";
        }

        public void ClearAll()
        {
            ClearOperationBox();
            ClearInputBox();
            AddTextToInput("0");
        }
        public bool InputBoxIsNotEmpty(TextBox inputBox)
        {
            return !String.IsNullOrWhiteSpace(inputBox.Text) ? true : false;
        }
        public void AddTextOperatorBox(string op, string text)
        {
            OperationBox.Text = OperationBox.Text + text + op;
        }
        public void AddTextToInput(string text)
        {
            // if the input box is not reached char cap, and is not just a single 0 -> append new char to end
            // if text count is only 1, and only contains a 0 -> replace the zero with whatever is incoming in text.
            // if input box only contains zeros(more than one) -> replace with whatever is incoming
            // Find a way to ensure no leading zeros
            // if inputbox only contains zeros, replace with incoming text
            if (CanInsertText(InputBox))
            {
                if (InputBox.Text != "0" || text == ".")
                    InputBox.Text += text;
                else if (InputBox.Text.Count() == 1 && InputBox.Text == "0")
                    InputBox.Text = text;
                else if (InputBox.Text.All(p => p == '0'))
                    InputBox.Text = text;
            }
        }
        public void AddTextToInput(string text, int index)
        {
            // if the input box is not reached char cap, and is not just a single 0 -> append new char to end
            // if text count is only 1, and only contains a 0 -> replace the zero with whatever is incoming in text.
            // if input box only contains zeros(more than one) -> replace with whatever is incoming
            // Find a way to ensure no leading zeros
            // if inputbox only contains zeros, replace with incoming text
            if (CanInsertText(InputBox))
            {
                if (InputBox.Text != "0" || text == ".")
                    InputBox.Text = InputBox.Text.Insert(index, "text");
                else if (InputBox.Text.Count() == 1 && InputBox.Text == "0")
                    InputBox.Text = text;
                else if (InputBox.Text.All(p => p == '0'))
                    InputBox.Text = text;
            }
        }
        public bool CanInsertText(TextBox inputBox)
        {
            if (inputBox.Text.Length >= 0 && inputBox.Text.Length < 20)
                return true;
            else
                return false;
        }

        internal string CalculateResult(string equation)
        {
            // Take whats in the inputbox
            // append it to the operators box text. 
            // Convert Operators to something the compiler can work with
            // Clear opBox
            // set inputBox to the value.

            // Checking for infinite and other edgecases
            if (equation.Contains("∞") || equation.Contains("NaN") || equation == "-" || equation.Contains("undefined"))
            {
                return null;
            }

            // handle broken operators
            // If there's an operator with nothing to the right of it, remove it.
            equation = equation.Trim('+', '/', '*', '%', '^', ' ');
            equation = equation.TrimEnd('-');

            // Converting operators to things the parsing language (jint) can understand, but would otherwise look bad in the GUI:
            //  > 5 mod 3 -> 5 % 3
            equation = equation.Replace("Mod", "%");

            //  > "4^2 -> Math.pow(4,2)
            // Use Math.Pow until their are no more viable matches left. 
            while (ContainsMatch(equation, out Match match))
            {
                equation = equation
                    .Replace(
                    match.Value,
                        Math.Pow(
                            double.Parse(match.Groups[2].Value),
                            double.Parse(match.Groups[3].Value
                        )).ToString());
            }

            //  > !(4) -> 8 x 8 -> 8 times
            // Matches !(anynumber) converts to number times itself, decrementing, till 0.
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

            return output;
        }

        public bool CanPlaceOperator()
        {
            return InputBoxIsNotEmpty(InputBox) ? true : false;
        }

        public void ClearInputBox()
        {
            InputBox.Text = String.Empty;
        }
        public void ClearOperationBox()
        {
            OperationBox.Text = String.Empty;
        }

        internal void OverwriteInput(string output)
        {
            InputBox.Text = output;
        }
    }
}
