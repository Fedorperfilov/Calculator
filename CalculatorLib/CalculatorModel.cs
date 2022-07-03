using System;
using System.ComponentModel;
using System.Linq;

namespace CalculatorLib
{
    /// <summary>
    /// Родительский класс модели калькулятора.
    /// </summary>
    public abstract class CalculatorModel : EventArgs, INotifyPropertyChanged
    {
        protected string _CurrentOperator = "";
        /// <summary>
        /// Текущий оператор.
        /// </summary>
        public string CurrentOperator
        {
            get
            {
                return _CurrentOperator;
            }
        }

        protected string _CurrentResult = "";
        /// <summary>
        /// Текущий результат.
        /// </summary>
        public string CurrentResult
        {
            get
            {
                return _CurrentResult;
            }
        }
        
        protected string _CurrentOutput = "";
        /// <summary>
        /// Текущий итог.
        /// </summary>
        public string CurrentOutput
        {
            get
            {
                return _CurrentOutput;
            }
        }

        
        protected string _CurrentNum = "0";
        /// <summary>
        /// Текущее число.
        /// </summary>
        public string CurrentNum
        {
            get
            {
                return _CurrentNum;
            }
        }

        /// <summary>
        /// Текущий тип операции.
        /// </summary>
        public OperationType OperationType;

        /// <summary>
        /// Выбранная система счисления.
        /// </summary>
        public NumeralSystem CurrentNumeralSystem;

        /// <summary>
        /// Проведена ли операция.
        /// </summary>
        public bool OperationHandled = false;

        /// <summary>
        /// Обработчик операций.
        /// </summary>
        protected IOperationsHandler _OperationsHandler;

        /// <summary>
        /// Модель-родитель.
        /// </summary>
        public CalculatorModel Parent;

        /// <summary>
        /// Дочеряя модель.
        /// </summary>
        public CalculatorModel Child;

        /// <summary>
        /// Активная модель.
        /// </summary>
        public CalculatorModel ActiveModel;

        /// <summary>
        /// Аргументы для передачи данных.
        /// </summary>
        public class DataEventArgs : EventArgs { public dynamic Data; }
        
        /// <summary>
        /// Обработчик событий при изменении текущего числа.
        /// </summary>
        public event EventHandler<DataEventArgs> HandleNumChanged;

        /// <summary>
        /// Обработчик событий при изменении состояния кнопок.
        /// </summary>
        public event EventHandler<DataEventArgs> HandleKeysStateChanged;

        /// <summary>
        /// Обработчик событий при изменении значений для вывода.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public CalculatorModel()
        {
            Parent = this;
            ActiveModel = this;
            CurrentNumeralSystem = NumeralSystem.DECIMAL_WITH_POINT;
        }

        /// <summary>
        /// Вызов события при изменении текущего числа.
        /// </summary>
        protected void OnCurrentNumChanged()
        {
            HandleNumChanged?.Invoke(this, new DataEventArgs { Data = Parent.CurrentNum });
        }

        /// <summary>
        /// Вызов события при изменении состояния кнопок.
        /// </summary>
        /// <param name="keys">Кнопки.</param>
        public void OnKeysStateChanged(string[] keys)
        {
            HandleKeysStateChanged?.Invoke(this, new DataEventArgs { Data = keys });
        }
        
        /// <summary>
        /// Произвести операцию.
        /// </summary>
        protected void PerformOperation()
        {
            Parent.OperationHandled = false;
            string output = "";
            string result = "";
            try
            {
                
                result = _OperationsHandler.PerformOperation(Parent._CurrentResult, Parent._CurrentNum, Parent.OperationType, Parent.CurrentNumeralSystem);
                Parent._CurrentResult = result;
                output = result + _OperationsHandler.GetCurrentOperator(Parent.OperationType);
            }
            catch
            {

            }
            SaveCurrentNum("");
            SaveOutputNum(output);
            Parent.OperationHandled = true;
        }

        /// <summary>
        /// Получить результат.
        /// </summary>
        public void GetResult()
        {
            Serilog.Log.Information("Get result.");
            Parent.ActiveModel.PerformOperation();
            SaveCurrentNum(CurrentResult);
            Parent._CurrentResult = "";
            SaveOutputNum("");
        }

        /// <summary>
        /// Сохранить результат.
        /// </summary>
        /// <param name="currentOutput">Результат.</param>
        public void SaveOutputNum(string currentOutput)
        {
            Serilog.Log.Information("Save result.");
            ReduceNum(currentOutput);
            Parent._CurrentOutput = currentOutput;
            Parent.OnCurrentNumChanged();
            OnPropertyChanged(Parent, "CurrentOutput");
        }

        /// <summary>
        /// Сохранить текущее число.
        /// </summary>
        /// <param name="currentNum">Текущее число.</param>
        public virtual void SaveCurrentNum(string currentNum)
        {
            Serilog.Log.Information("Save current num.");
            if (Parent.OperationHandled)
            {
                Clear();
            }
            currentNum = ReduceNum(currentNum);
            if (Parent._CurrentNum == "0")
            {
                if (currentNum == "0")
                {
                    currentNum = "";
                }
                else if (currentNum != ",")
                {
                    Parent._CurrentNum = "";
                }
            }
            if (!CheckNum(currentNum))
            {
                return;
            }
            Parent._CurrentNum += currentNum;
            Parent.OnCurrentNumChanged();
            OnPropertyChanged(Parent, "CurrentNum");
        }

        /// <summary>
        /// Сократить число для вывода.
        /// </summary>
        /// <param name="num">Число.</param>
        /// <returns></returns>
        public string ReduceNum(string num)
        {
            if (num.Length > 12)
            {
                num = num.Remove(11);
            }
            return num;
        }

        /// <summary>
        /// Проверить число.
        /// </summary>
        /// <param name="currentNum">Текущее число.</param>
        /// <returns></returns>
        protected virtual bool CheckNum(string currentNum)
        {
            if (Parent._CurrentNum.Length == 12)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Очистить всё.
        /// </summary>
        public void Clear()
        {
            Serilog.Log.Information("Clear textbox.");
            Remove(Parent._CurrentNum.Length);
            if (Parent.OperationHandled)
            {
                Parent.OperationHandled = false;
                return;
            }
            Parent._CurrentOutput = "";
            OnPropertyChanged(Parent, "CurrentOutput");
        }

        /// <summary>
        /// Удалить определенное количество цифр.
        /// </summary>
        /// <param name="lenght">Количество цифр для удаления.</param>
        public void Remove(int lenght = 1)
        {
            Serilog.Log.Information("Remove one symbol.");
            Parent._CurrentNum = Parent._CurrentNum.Remove(Parent._CurrentNum.Length - lenght);
            if (Parent._CurrentNum == "")
            {
                _CurrentNum = "0";
            }
            OnCurrentNumChanged();
            OnPropertyChanged(Parent, "CurrentNum");
        }

        /// <summary>
        /// Сменить знак числа.
        /// </summary>
        public void ChangeSign()
        {
            Serilog.Log.Information("Change sign.");
            if (Parent._CurrentNum.First() == '-')
            {
                Parent._CurrentNum = Parent._CurrentNum.Remove(0, 1);
            }
            else
            {
                Parent._CurrentNum = "-" + Parent._CurrentNum;
            }
            OnPropertyChanged(Parent, "CurrentNum");
        }

        /// <summary>
        /// Вызов и обработка события изменения значения для вывода.
        /// </summary>
        /// <param name="o">Объект.</param>
        /// <param name="name">Название поля.</param>
        protected void OnPropertyChanged(object o, string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(o, new PropertyChangedEventArgs(name));
            }
        }
    }
}
