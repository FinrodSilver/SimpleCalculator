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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //variables to hold the total values // Operations
        private double currentTotal = 0;
        private bool firstNumber = true;
        private enum Operation { Add, Subtract, Multiply, Divide, Equals, Start, LastOp };
        Operation currentOperation = Operation.Start;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Pressing any of the numbers
        private void numberPressed(object sender, RoutedEventArgs e)
        {
            int result;
            Button btn = (Button)sender;
            string value = btn.Content.ToString();

            //Special if for the period button
            if (value.Equals("."))
            {
                if (firstNumber)
                {
                    return;
                }
                if (!output.Text.Contains("."))
                {
                    output.Text += value;
                    firstNumber = false;
                }
                return;
            }

            // Numeric Buttons
            if (Int32.TryParse(value, out result))
            {
                if (firstNumber || output.Text.Equals("0"))
                {
                    output.Text = "";
                }
                output.Text += value;
                firstNumber = false;
            }
        }

        // Clear Button
        private void clear_Click(object sender, RoutedEventArgs e)
        {
            output.Text = "0";
            currentTotal = 0;
            firstNumber = true;
        }

        // Calculate Operations Method
        private void calculateTotal(Operation op)
        {
            double result;
            double newValue = Double.Parse(output.Text);

            if (op != Operation.LastOp)
            {
                currentOperation = op;
            }

            switch (currentOperation)
            {
                case Operation.Add:
                    result = currentTotal + newValue;
                    break;
                case Operation.Subtract:
                    if (currentTotal == 0)
                    {
                        result = newValue;
                    }
                    else
                    {
                        result = currentTotal - newValue;
                    }
                    break;
                case Operation.Multiply:
                    if (currentTotal == 0)
                    {
                        result = newValue;
                    }
                    else
                    {
                        result = currentTotal * newValue;
                    }
                    break;
                case Operation.Divide:
                    if (currentTotal == 0)
                    {
                        result = newValue;
                    }
                    else
                    {
                        result = currentTotal / newValue;
                    }
                    break;
                default:
                    return;
            }
            currentTotal = result;
            output.Text = result.ToString();
            firstNumber = true;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            calculateTotal(Operation.Add);
        }

        private void subtract_Click(object sender, RoutedEventArgs e)
        {
            calculateTotal(Operation.Subtract);
        }

        private void multiply_Click(object sender, RoutedEventArgs e)
        {
            calculateTotal(Operation.Multiply);
        }

        private void divide_Click(object sender, RoutedEventArgs e)
        {
            calculateTotal(Operation.Divide);
        }

        private void equals_Click(object sender, RoutedEventArgs e)
        {
            calculateTotal(Operation.LastOp);
        }
    }
}
