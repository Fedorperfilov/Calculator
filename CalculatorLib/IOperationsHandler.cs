namespace CalculatorLib
{
    /// <summary>
    /// Интерфейс обработчика операций.
    /// </summary>
    public interface IOperationsHandler
    {
        /// <summary>
        /// Произвести операцию.
        /// </summary>
        /// <param name="operandA">Операнд 1.</param>
        /// <param name="operandB">Операнд 2.</param>
        /// <param name="operationType">Тип операции.</param>
        /// <param name="numeralSystem">Система счисления.</param>
        /// <returns></returns>
        string PerformOperation(string operandA, string operandB, OperationType operationType, NumeralSystem numeralSystem);

        /// <summary>
        /// Получить текущий оператор.
        /// </summary>
        /// <param name="operationType">Тип операции.</param>
        /// <returns></returns>
        string GetCurrentOperator(OperationType operationType);
    }
}
