namespace CalculatorLib
{
    /// <summary>
    /// Интерфейс для дополнений.
    /// </summary>
    public interface IExtension
    {
        /// <summary>
        /// Получить модель дополнения.
        /// </summary>
        /// <returns></returns>
        CalculatorModel GetModel();
    }
}
