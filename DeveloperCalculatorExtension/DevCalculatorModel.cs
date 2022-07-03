using CalculatorLib;
using System;

namespace DevCalculatorExtension
{
    /// <summary>
    /// Модель расширения 'Программист'.
    /// </summary>
    public class DevCalculatorModel : CalculatorModel
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public DevCalculatorModel()
        {
            _OperationsHandler = new DevOperationsHandler();
        }

        private string _CurrentBinary = "";
        /// <summary>
        /// Текущее число в двоичной системе для вывода в боковую панель.
        /// </summary>
        public string CurrentBinary
        {
            get
            {
                return _CurrentBinary;
            }
        }

        /// <summary>
        /// Вывести текущее число в двоичной системе в боковую панель.
        /// </summary>
        /// <param name="currentNum">Текущее число.</param>
        public void OutBinary(string currentNum)
        {
            try
            {
                _CurrentBinary = GetBinaryNumber(currentNum);
            }
            catch
            {

            }
            OnPropertyChanged(this, "CurrentBinary");
        }

        /// <summary>
        /// Получить число в двоичной системе счисления.
        /// </summary>
        /// <param name="number">Число.</param>
        /// <returns></returns>
        public string GetBinaryNumber(string number)
        {
            Serilog.Log.Information("Represent number in binary system.");
            if (number == "")
            {
                return "";
            }
            string resultNumber = "";
            long num = 0;
            if (Parent.CurrentNumeralSystem == NumeralSystem.HEXADECIMAL)
            {
                num = Convert.ToInt64(number, 16);
            }
            else if (Parent.CurrentNumeralSystem == NumeralSystem.DECIMAL)
            {
                num = Convert.ToInt32(number);
            }
            else if (Parent.CurrentNumeralSystem == NumeralSystem.BINARY)
            {
                return number;
            }
            string currentNumber = Convert.ToString(num, 2);
            for (int i = 0; i < currentNumber.Length; i++)
            {
                resultNumber += currentNumber[i];
            }
            return resultNumber;
        }

        /// <summary>
        /// Получить число в шестнадцатиричной системе счисления.
        /// </summary>
        /// <param name="number">Число.</param>
        /// <returns></returns>
        public string GetHexNumber(string number)
        {
            Serilog.Log.Information("Represent number in hexadecimal system.");
            if (number == "")
            {
                return "";
            }
            string resultNumber = "";
            if (Parent.CurrentNumeralSystem == NumeralSystem.HEXADECIMAL)
            {
                return number;
            }
            else if (Parent.CurrentNumeralSystem == NumeralSystem.DECIMAL)
            {
                resultNumber = Convert.ToInt64(number).ToString("X");
            }
            else if (Parent.CurrentNumeralSystem == NumeralSystem.BINARY)
            {
                resultNumber = Convert.ToInt64(number, 2).ToString("X");
            }
            return resultNumber;
        }

        /// <summary>
        /// Получить число в десятиричной системе счисления.
        /// </summary>
        /// <param name="number">Число.</param>
        /// <returns></returns>
        public string GetDecNumber(string number)
        {
            Serilog.Log.Information("Represent number in decimal system.");
            if (number == "")
            {
                return "";
            }
            string resultNumber = "";
            if (Parent.CurrentNumeralSystem == NumeralSystem.HEXADECIMAL)
            {
                resultNumber = Convert.ToInt32(Convert.ToInt64(number, 16)).ToString();
            }
            else if (Parent.CurrentNumeralSystem == NumeralSystem.DECIMAL)
            {
                return number;
            }
            else if (Parent.CurrentNumeralSystem == NumeralSystem.BINARY)
            {
                resultNumber = Convert.ToInt32(number, 2).ToString();
            }
            return resultNumber;
        }

        /// <summary>
        /// Получить остаток от деления.
        /// </summary>
        public void GetDivisionRemainder()
        {
            Serilog.Log.Information("Get remainder of the division.");
            Parent.OperationType = OperationType.DivisionAndRemainder;
            PerformOperation();
        }

        /// <summary>
        /// Побитовый сдвиг влево.
        /// </summary>
        public void LeftBitShift()
        {
            Serilog.Log.Information("Left bit shift.");
            Parent.OperationType = OperationType.LeftBitShift;
            PerformOperation();
        }

        /// <summary>
        /// Побитовый сдвиг вправо.
        /// </summary>
        public void RightBitShift()
        {
            Serilog.Log.Information("Right bit shift.");
            Parent.OperationType = OperationType.RightBitShift;
            PerformOperation();
        }
    }
}
