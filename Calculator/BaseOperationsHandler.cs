using CalculatorLib;
using System;

namespace Calculator
{
    /// <summary>
    /// Обработчик базовых операций калькулятора.
    /// </summary>
    public class BaseOperationsHandler : IOperationsHandler
    {
        public string PerformOperation(string operandA, string operandB, OperationType operationType, NumeralSystem numeralSystem)
        {
            string result = "";

            switch (operationType)
            {
                case OperationType.Addition:
                    if (numeralSystem == NumeralSystem.HEXADECIMAL)
                    {
                        result = (operandA != "" ? Convert.ToInt64(operandA, 16) + Convert.ToInt64(operandB, 16) : Convert.ToInt64(operandB, 16)).ToString("X");
                    }
                    else if (numeralSystem == NumeralSystem.DECIMAL)
                    {
                        result = (operandA != "" ? Convert.ToInt32(operandA) + Convert.ToInt32(operandB) : Convert.ToInt32(operandB)).ToString();
                    }
                    else if (numeralSystem == NumeralSystem.BINARY)
                    {
                        result = Convert.ToString((operandA != "" ? Convert.ToInt32(operandA, 2) + Convert.ToInt32(operandB, 2) : Convert.ToInt32(operandB, 2)), 2);
                    }
                    else if (numeralSystem == NumeralSystem.DECIMAL_WITH_POINT)
                    {
                        result = (operandA != "" ? double.Parse(operandA) + double.Parse(operandB) : double.Parse(operandB)).ToString();
                    }
                    break;
                case OperationType.Division:
                    if (operandB == "0")
                    {
                        throw new Exception("Division by zero.");
                    }
                    if (numeralSystem == NumeralSystem.HEXADECIMAL)
                    {
                        result = (operandA != "" ? Convert.ToInt64(operandA, 16) / Convert.ToInt64(operandB, 16) : Convert.ToInt64(operandB, 16)).ToString("X");
                    }
                    else if (numeralSystem == NumeralSystem.DECIMAL)
                    {
                        result = (operandA != "" ? Convert.ToInt32(operandA) / Convert.ToInt32(operandB) : Convert.ToInt32(operandB)).ToString();
                    }
                    else if (numeralSystem == NumeralSystem.BINARY)
                    {
                        result = Convert.ToString((operandA != "" ? Convert.ToInt32(operandA, 2) / Convert.ToInt32(operandB, 2) : Convert.ToInt32(operandB, 2)), 2);
                    }
                    else if (numeralSystem == NumeralSystem.DECIMAL_WITH_POINT)
                    {
                        result = (operandA != "" ? double.Parse(operandA) / double.Parse(operandB) : double.Parse(operandB)).ToString();
                    }
                    break;
                case OperationType.Subtraction:
                    if (numeralSystem == NumeralSystem.HEXADECIMAL)
                    {
                        result = (operandA != "" ? Convert.ToInt64(operandA, 16) - Convert.ToInt64(operandB, 16) : Convert.ToInt64(operandB, 16)).ToString("X");
                    }
                    else if (numeralSystem == NumeralSystem.DECIMAL)
                    {
                        result = (operandA != "" ? Convert.ToInt32(operandA) - Convert.ToInt32(operandB) : Convert.ToInt32(operandB)).ToString();
                    }
                    else if (numeralSystem == NumeralSystem.BINARY)
                    {
                        result = Convert.ToString((operandA != "" ? Convert.ToInt32(operandA, 2) - Convert.ToInt32(operandB, 2) : Convert.ToInt32(operandB, 2)), 2);
                    }
                    else if (numeralSystem == NumeralSystem.DECIMAL_WITH_POINT)
                    {
                        result = (operandA != "" ? double.Parse(operandA) - double.Parse(operandB) : double.Parse(operandB)).ToString();
                    }
                    break;
                case OperationType.Multiplication:
                    if (numeralSystem == NumeralSystem.HEXADECIMAL)
                    {
                        result = (operandA != "" ? Convert.ToInt64(operandA, 16) * Convert.ToInt64(operandB, 16) : Convert.ToInt64(operandB, 16)).ToString("X");
                    }
                    else if (numeralSystem == NumeralSystem.DECIMAL)
                    {
                        result = (operandA != "" ? Convert.ToInt32(operandA) * Convert.ToInt32(operandB) : Convert.ToInt32(operandB)).ToString();
                    }
                    else if (numeralSystem == NumeralSystem.BINARY)
                    {
                        result = Convert.ToString((operandA != "" ? Convert.ToInt32(operandA, 2) * Convert.ToInt32(operandB, 2) : Convert.ToInt32(operandB, 2)), 2);
                    }
                    else if (numeralSystem == NumeralSystem.DECIMAL_WITH_POINT)
                    {
                        result = (operandA != "" ? double.Parse(operandA) * double.Parse(operandB) : double.Parse(operandB)).ToString();
                    }
                    break;
            }

            return result;
        }

        public string GetCurrentOperator(OperationType operationType)
        {
            string currentOperator = " ";
            switch (operationType)
            {
                case OperationType.Addition:
                    currentOperator += "+";
                    break;
                case OperationType.Subtraction:
                    currentOperator += "-";
                    break;
                case OperationType.Division:
                    currentOperator += "/";
                    break;
                case OperationType.Multiplication:
                    currentOperator += "*";
                    break;
            }
            return currentOperator;
        }
    }
}
