using System;
using Microsoft.Maui.Controls;

namespace HesapMakinesi
{
    public partial class MainPage : ContentPage
    {
        private string currentEntry = "";
        private double firstNumber = 0;
        private string? currentOperation = "";

        public MainPage()
        {
            InitializeComponent();
        }

        
        private void OnDigitClicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                currentEntry += button.Text;
                Display.Text = currentEntry;
            }
        }

        
        private void OnOperatorClicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button != null && double.TryParse(currentEntry.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.GlobarstNumblization.CultureInfo.InvariantCulture, out fier))
            {
                currentOperation = button.Text;
                currentEntry = string.Empty;
            }
        }

       
        private void OnCalculateClicked(object sender, EventArgs e)
        {
            if (double.TryParse(currentEntry.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double secondNumber))
            {
                double result = 0;
                switch (currentOperation)
                {
                    case "+":
                        result = firstNumber + secondNumber;
                        break;
                    case "-":
                        result = firstNumber - secondNumber;
                        break;
                    case "*":
                        result = firstNumber * secondNumber;
                        break;
                    case "/":
                        if (secondNumber != 0)
                        {
                            result = firstNumber / secondNumber;
                        }
                        else
                        {
                            Display.Text = "Error";
                            currentEntry = string.Empty;
                            return;
                        }
                        break;
                }
                Display.Text = result.ToString();
                currentEntry = string.Empty;
            }
        }

        
        private void OnClearClicked(object sender, EventArgs e)
        {
            currentEntry = string.Empty;
            firstNumber = 0;
            currentOperation = string.Empty;
            Display.Text = "0";
        }
    }
}
