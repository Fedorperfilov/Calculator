using CalculatorLib;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace DevCalculatorExtension
{
    [Export(typeof(IExtension))]
    /// <summary>
    /// Логика взаимодействия для DevCalcWindow.xaml
    /// </summary>
    public partial class DevCalcWindow : UserControl, IExtension
    {
        /// <summary>
        /// Модель расширения.
        /// </summary>
        public readonly DevCalculatorModel Model;

        /// <summary>
        /// Клавиши, доступные 
        /// при выборе режима отображения числа 
        /// в шестнадцатиричной системе.
        /// </summary>
        private string _HexButtons = "AButton" + "," + "BButton" + "," + "CButton" + "," + "DButton" + "," + "EButton" + "," + "FButton";

        /// <summary>
        /// Клавиши, недоступные 
        /// при выборе режима отображения числа 
        /// в двоичной системе.
        /// </summary>
        private string _BinaryButtons = "SevenButton" + "," + "EightButton" + "," + "NineButton" + "," + "FourButton" + "," + "FiveButton" + "," + "SixButton" + "," + "TwoButton" + "," + "ThreeButton";

        /// <summary>
        /// Конструктор.
        /// </summary>
        public DevCalcWindow()
        {
            InitializeComponent();
            Model = new DevCalculatorModel();
            DataContext = Model;
            Model.HandleKeysStateChanged += (o, e) => { KeysStateChanged(e.Data); };
        }

        /// <summary>
        /// Получить модель.
        /// </summary>
        /// <returns></returns>
        public CalculatorModel GetModel()
        {
            return Model;
        }

        /// <summary>
        /// Событие при изменении состояния кнопок.
        /// </summary>
        /// <param name="keys"></param>
        private void KeysStateChanged(string[] keys)
        {
            foreach (string key in keys)
            {
                Button button = (Button)this.FindName(key);
                if (button.IsEnabled)
                {
                    button.IsEnabled = false;
                    continue;
                }
                button.IsEnabled = true;
            }
        }

        /// <summary>
        /// Сменить режим отображения чисел на шестнадцатиричный.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргументы события.</param>
        private void HexButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeCurrentOutputType(false, NumeralSystem.HEXADECIMAL, _HexButtons);
        }

        /// <summary>
        /// Сменить режим отображения чисел на десятиричный.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргументы события.</param>
        private void DecButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeCurrentOutputType(true, NumeralSystem.DECIMAL, "");
        }

        /// <summary>
        /// Сменить режим отображения чисел на двоичный.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргументы события.</param>
        private void BinaryButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeCurrentOutputType(true, NumeralSystem.BINARY, _BinaryButtons);
        }
        
        /// <summary>
        /// Сменить режим отображения чисел.
        /// </summary>
        /// <param name="inParentModel">В родительской модели.</param>
        /// <param name="numeralSystem">Система счисления.</param>
        /// <param name="currentKeys">Кнопки для изменения состояния.</param>
        private void ChangeCurrentOutputType(bool inParentModel, NumeralSystem numeralSystem, string currentKeys)
        {
            if (Model.Parent.CurrentNumeralSystem == numeralSystem)
            {
                return;
            }
            CalculatorModel model = inParentModel ? Model.Parent : Model;
            if (Model.Parent.CurrentNumeralSystem == NumeralSystem.BINARY)
            {
                Model.Parent.OnKeysStateChanged(_BinaryButtons.Split(','));
            }
            else if (Model.Parent.CurrentNumeralSystem == NumeralSystem.HEXADECIMAL)
            {
                Model.OnKeysStateChanged(_HexButtons.Split(','));
            }

            string currentResult = "";
            string currentNum = "";
            if (numeralSystem == NumeralSystem.BINARY)
            {
                currentResult = Model.GetBinaryNumber(Model.Parent.CurrentOutput);
                currentNum = Model.GetBinaryNumber(Model.Parent.CurrentNum);
            }
            else if (numeralSystem == NumeralSystem.HEXADECIMAL)
            {
                currentResult = Model.GetHexNumber(Model.Parent.CurrentOutput);
                currentNum = Model.GetHexNumber(Model.Parent.CurrentNum);
            }
            else if (numeralSystem == NumeralSystem.DECIMAL)
            {
                currentResult = Model.GetDecNumber(Model.Parent.CurrentOutput);
                currentNum = Model.GetDecNumber(Model.Parent.CurrentNum);
            }
            Model.Parent.CurrentNumeralSystem = numeralSystem;

            Model.Parent.Clear();
            Model.Parent.SaveCurrentNum(currentNum);
            Model.Parent.SaveOutputNum(currentResult);

            if (currentKeys.Length == 0)
            {
                return;
            }
            model.OnKeysStateChanged(currentKeys.Split(','));
        }

        /// <summary>
        /// Событие при нажатии кнопки LBitShift.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргументы события.</param>
        private void LBitButton_Click(object sender, RoutedEventArgs e)
        {
            Model.LeftBitShift();
        }

        /// <summary>
        /// Событие при нажатии кнопки RBitShift.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргументы события.</param>
        private void RBitButton_Click(object sender, RoutedEventArgs e)
        {
            Model.RightBitShift();
        }

        /// <summary>
        /// Событие при нажатии кнопки %.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргументы события.</param>
        private void DivisionRemainderButton_Click(object sender, RoutedEventArgs e)
        {
            Model.GetDivisionRemainder();
        }

        /// <summary>
        /// Событие при нажатии кнопки A.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргументы события.</param>
        private void AButton_Click(object sender, RoutedEventArgs e)
        {
            Model.SaveCurrentNum("A");
        }

        /// <summary>
        /// Событие при нажатии кнопки B.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргументы события.</param>
        private void BButton_Click(object sender, RoutedEventArgs e)
        {
            Model.SaveCurrentNum("B");
        }

        /// <summary>
        /// Событие при нажатии кнопки C.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргументы события.</param>
        private void CButton_Click(object sender, RoutedEventArgs e)
        {
            Model.SaveCurrentNum("C");
        }

        /// <summary>
        /// Событие при нажатии кнопки D.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргументы события.</param>
        private void DButton_Click(object sender, RoutedEventArgs e)
        {
            Model.SaveCurrentNum("D");
        }

        /// <summary>
        /// Событие при нажатии кнопки E.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргументы события.</param>
        private void EButton_Click(object sender, RoutedEventArgs e)
        {
            Model.SaveCurrentNum("E");
        }

        /// <summary>
        /// Событие при нажатии кнопки F.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Аргументы события.</param>
        private void FButton_Click(object sender, RoutedEventArgs e)
        {
            Model.SaveCurrentNum("F");
        }
    }
}
