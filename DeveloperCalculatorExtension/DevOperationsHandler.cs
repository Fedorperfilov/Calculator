using CalculatorLib;
using System;

namespace DevCalculatorExtension
{
    /// <summary>
    /// Обработчик операций расширения 'Разработчик' для калькулятора.
    /// </summary>
    public class DevOperationsHandler : IOperationsHandler
    {
        public string PerformOperation(string operandA, string operandB, OperationType operationType, NumeralSystem numeralSystem)
        {
            string result = "";

            switch (operationType)
            {
                case OperationType.DivisionAndRemainder:
                    if (numeralSystem == NumeralSystem.HEXADECIMAL)
                    {
                        result = (operandA != "" ? Convert.ToInt64(operandA, 16) % Convert.ToInt64(operandB, 16) : Convert.ToInt64(operandB, 16)).ToString("X");
                    }
                    else if (numeralSystem == NumeralSystem.DECIMAL)
                    {
                        result = (operandA != "" ? Convert.ToInt32(operandA) % Convert.ToInt32(operandB) : Convert.ToInt32(operandB)).ToString();
                    }
                    else if (numeralSystem == NumeralSystem.BINARY)
                    {
                        result = Convert.ToString((operandA != "" ? Convert.ToInt32(operandA, 2) % Convert.ToInt32(operandB, 2) : Convert.ToInt32(operandB, 2)), 2);
                    }
                    break;
                case OperationType.LeftBitShift:
                    if (numeralSystem == NumeralSystem.HEXADECIMAL)
                    {
                        result = (operandA != "" ? Convert.ToUInt64(operandA, 16) << Convert.ToInt32(operandB, 16) : Convert.ToUInt64(operandB, 16)).ToString("X");
                    }
                    else if (numeralSystem == NumeralSystem.DECIMAL)
                    {
                        result = (operandA != "" ? Convert.ToUInt32(operandA) << Convert.ToInt32(operandB) : Convert.ToUInt32(operandB)).ToString();
                    }
                    else if (numeralSystem == NumeralSystem.BINARY)
                    {
                        result = Convert.ToString((operandA != "" ? Convert.ToUInt32(operandA, 2) << Convert.ToInt32(operandB, 2) : Convert.ToUInt32(operandB, 2)), 2);
                    }
                    break;
                case OperationType.RightBitShift:
                    if (numeralSystem == NumeralSystem.HEXADECIMAL)
                    {
                        result = (operandA != "" ? Convert.ToUInt64(operandA, 16) >> Convert.ToInt32(operandB, 16) : Convert.ToUInt64(operandB, 16)).ToString("X");
                    }
                    else if (numeralSystem == NumeralSystem.DECIMAL)
                    {
                        result = (operandA != "" ? Convert.ToUInt32(operandA) >> Convert.ToInt32(operandB) : Convert.ToUInt32(operandB)).ToString();
                    }
                    else if (numeralSystem == NumeralSystem.BINARY)
                    {
                        result = Convert.ToString((operandA != "" ? Convert.ToUInt32(operandA, 2) >> Convert.ToInt32(operandB, 2) : Convert.ToUInt32(operandB, 2)), 2);
                    }
                    break;
            }

            return result;
        }

        public string GetCurrentOperator(OperationType operationType)
        {
            string result = " ";
            switch (operationType)
            {
                case OperationType.DivisionAndRemainder:
                    result += "%";
                    break;
                case OperationType.LeftBitShift:
                    result += "<<";
                    break;
                case OperationType.RightBitShift:
                    result += ">>";
                    break;
            }
            return result;
        }
    }
}
