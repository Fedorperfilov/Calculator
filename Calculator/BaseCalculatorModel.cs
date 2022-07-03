using CalculatorLib;

namespace Calculator
{
    /// <summary>
    /// Базовая модель калькулятора.
    /// </summary>
    public class BaseCalculatorModel : CalculatorModel
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public BaseCalculatorModel()
        {
            _OperationsHandler = new BaseOperationsHandler();
        }

        /// <summary>
        /// Проверка числа.
        /// </summary>
        /// <param name="currentNum">Число.</param>
        /// <returns></returns>
        protected override bool CheckNum(string currentNum)
        {
            if (!base.CheckNum(currentNum))
            {
                return false;
            }
            if (currentNum == "," && _CurrentNum.Contains(","))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Сложение.
        /// </summary>
        public void Add()
        {
            OperationType = OperationType.Addition;
            PerformOperation();
        }

        /// <summary>
        /// Деление.
        /// </summary>
        public void Divide()
        {
            OperationType = OperationType.Division;
            PerformOperation();
        }

        /// <summary>
        /// Вычитание.
        /// </summary>
        public void Substract()
        {
            OperationType = OperationType.Subtraction;
            PerformOperation();
        }

        /// <summary>
        /// Умножение.
        /// </summary>
        public void Multiply()
        {
            OperationType = OperationType.Multiplication;
            PerformOperation();
        }
    }
}
