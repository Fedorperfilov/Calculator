using CalculatorLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для BaseCalcWindow.xaml
    /// </summary>
    public partial class CalcWindow : Window
    {
        /// <summary>
        /// Контейнер для расширений.
        /// </summary>
        private CompositionContainer _Container;

        /// <summary>
        /// Импорт расширений в список.
        /// </summary>
        [ImportMany]
        private List<IExtension> _Extensions;

        /// <summary>
        /// Перечисление для прохода по списку.
        /// </summary>
        private int _ExtensionsEnumerator;

        /// <summary>
        /// Текущее расширение.
        /// </summary>
        private IExtension _CurrentExtension;

        /// <summary>
        /// Модель.
        /// </summary>
        private readonly BaseCalculatorModel _Model;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="model">Базовая модель</param>
        public CalcWindow(BaseCalculatorModel model)
        {
            InitializeComponent();
            _Model = model;
            DataContext = _Model;
            Init();
        }

        /// <summary>
        /// Действия при загрузке приложения.
        /// </summary>
        private async void Init()
        {
            await LoadPlugins();
        }

        /// <summary>
        /// Загрузка плагинов.
        /// </summary>
        /// <returns></returns>
        private Task LoadPlugins()
        {
            try
            {
                AggregateCatalog catalog = new AggregateCatalog();
                catalog.Catalogs.Add(new ApplicationCatalog());
                _Container = new CompositionContainer(catalog);
                _Container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Serilog.Log.Information(compositionException.ToString());
            }
            _ExtensionsEnumerator = 0;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Событие при нажатии кнопки стереть.
        /// </summary>
        /// <param name="sender">Объект-отправитель.</param>
        /// <param name="e">Аргументы события.</param>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            _Model.Clear();
        }

        /// <summary>
        /// Событие при загрузке основного окна.
        /// </summary>
        /// <param name="sender">Объект-отправитель.</param>
        /// <param name="e">Аргументы события.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentNumberBlock.Focus();
        }

        /// <summary>
        /// Событие при нажатии клавиши пользователем.
        /// </summary>
        /// <param name="sender">Объект-отправитель.</param>
        /// <param name="e">Аргументы события.</param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            KeyConverter converter = new KeyConverter();
            string output = converter.ConvertToString(e.Key);
            if (output.Contains("NumPad"))
            {
                output = output.Remove(0, 6);
            }

            if (e.Key == Key.OemComma || e.Key == Key.OemPeriod || e.Key == Key.Decimal)
            {
                SeparatorButton_Click(sender, e);
            }
            else if (e.Key == Key.OemPlus || e.Key == Key.Add)
            {
                PlusButton_Click(sender, e);
            }
            else if (e.Key == Key.OemMinus || e.Key == Key.Subtract)
            {
                MinusButton_Click(sender, e);
            }
            else if (e.Key == Key.Multiply)
            {
                MultiplyButton_Click(sender, e);
            }
            else if (e.Key == Key.Divide)
            {
                DivideButton_Click(sender, e);
            }
            else if (e.Key == Key.Back)
            {
                _Model.Remove();
            }
            else if (e.Key == Key.Enter)
            {
                _Model.GetResult();
            }

            int result = 0;
            if (int.TryParse(output, out result))
            {
                _Model.SaveCurrentNum(output);
            }
        }

        /// <summary>
        /// Событие при нажатии кнопки +.
        /// </summary>
        /// <param name="sender">Объект-отправитель.</param>
        /// <param name="e">Аргументы события.</param>
        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            _Model.Add();
        }

        /// <summary>
        /// Событие при нажатии кнопки -.
        /// </summary>
        /// <param name="sender">Объект-отправитель.</param>
        /// <param name="e">Аргументы события.</param>
        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            _Model.Substract();
        }

        /// <summary>
        /// Событие при нажатии кнопки *.
        /// </summary>
        /// <param name="sender">Объект-отправитель.</param>
        /// <param name="e">Аргументы события.</param>
        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            _Model.Multiply();
        }

        /// <summary>
        /// Событие при нажатии кнопки /.
        /// </summary>
        /// <param name="sender">Объект-отправитель.</param>
        /// <param name="e">Аргументы события.</param>
        private void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            _Model.Divide();
        }

        /// <summary>
        /// Событие при нажатии кнопки =.
        /// </summary>
        /// <param name="sender">Объект-отправитель.</param>
        /// <param name="e">Аргументы события.</param>
        private void ResultButton_Click(object sender, RoutedEventArgs e)
        {
            _Model.GetResult();
        }

        /// <summary>
        /// Событие при нажатии кнопки 7.
        /// </summary>
        /// <param name="sender">Объект-отправитель.</param>
        /// <param name="e">Аргументы события.</param>
        private void SevenButton_Click(object sender, RoutedEventArgs e)
        {
            _Model.SaveCurrentNum("7");
        }

        /// <summary>
        /// Событие при нажатии кнопки 8.
        /// </summary>
        /// <param name="sender">Объект-отправитель.</param>
        /// <param name="e">Аргументы события.</param>
        private void EightButton_Click(object sender, RoutedEventArgs e)
        {
            _Model.SaveCurrentNum("8");
        }

        /// <summary>
        /// Событие при нажатии кнопки 9.
        /// </summary>
        /// <param name="sender">Объект-отправитель.</param>
        /// <param name="e">Аргументы события.</param>
        private void NineButton_Click(object sender, RoutedEventArgs e)
        {
            _Model.SaveCurrentNum("9");
        }

        /// <summary>
        /// Событие при нажатии кнопки 10.
        /// </summary>
        /// <param name="sender">Объект-отправитель.</param>
        /// <param name="e">Аргументы события.</param>
        private void FourButton_Click(object sender, RoutedEventArgs e)
        {
            _Model.SaveCurrentNum("4");
        }

        /// <summary>
        /// Событие при нажатии кнопки 5.
        /// </summary>
        /// <param name="sender">Объект-отправитель.</param>
        /// <param name="e">Аргументы события.</param>
        private void FiveButton_Click(object sender, RoutedEventArgs e)
        {
            _Model.SaveCurrentNum("5");
        }

        /// <summary>
        /// Событие при нажатии кнопки 6.
        /// </summary>
        /// <param name="sender">Объект-отправитель.</param>
        /// <param name="e">Аргументы события.</param>
        private void SixButton_Click(object sender, RoutedEventArgs e)
        {
            _Model.SaveCurrentNum("6");
        }

        /// <summary>
        /// Событие при нажатии кнопки 1.
        /// </summary>
        /// <param name="sender">Объект-отправитель.</param>
        /// <param name="e">Аргументы события.</param>
        private void OneButton_Click(object sender, RoutedEventArgs e)
        {
            _Model.SaveCurrentNum("1");
        }

        /// <summary>
        /// Событие при нажатии кнопки 2.
        /// </summary>
        /// <param name="sender">Объект-отправитель.</param>
        /// <param name="e">Аргументы события.</param>
        private void TwoButton_Click(object sender, RoutedEventArgs e)
        {
            _Model.SaveCurrentNum("2");
        }

        /// <summary>
        /// Событие при нажатии кнопки 3.
        /// </summary>
        /// <param name="sender">Объект-отправитель.</param>
        /// <param name="e">Аргументы события.</param>
        private void ThreeButton_Click(object sender, RoutedEventArgs e)
        {
            _Model.SaveCurrentNum("3");
        }

        /// <summary>
        /// Событие при нажатии кнопки 0.
        /// </summary>
        /// <param name="sender">Объект-отправитель.</param>
        /// <param name="e">Аргументы события.</param>
        private void ZeroButton_Click(object sender, RoutedEventArgs e)
        {
            _Model.SaveCurrentNum("0");
        }

        /// <summary>
        /// Событие при нажатии кнопки ,.
        /// </summary>
        /// <param name="sender">Объект-отправитель.</param>
        /// <param name="e">Аргументы события.</param>
        private void SeparatorButton_Click(object sender, RoutedEventArgs e)
        {
            if (_Model.CurrentNumeralSystem != NumeralSystem.DECIMAL_WITH_POINT)
            {
                return;
            }
            _Model.SaveCurrentNum(",");
        }

        /// <summary>
        /// Событие при нажатии кнопки смены знака.
        /// </summary>
        /// <param name="sender">Объект-отправитель.</param>
        /// <param name="e">Аргументы события.</param>
        private void ChangeSignButton_Click(object sender, RoutedEventArgs e)
        {
            _Model.ChangeSign();
        }

        /// <summary>
        /// Соыбтие при нажатии кнопки Mode.
        /// </summary>
        /// <param name="sender">Объект-отправитель.</param>
        /// <param name="e">Аргументы события.</param>
        private void ModeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!SwitchExtension())
                {
                    return;
                }
                ExtensionGrid.Children.Add((UIElement)_CurrentExtension);
            }
            catch (Exception ex)
            {
                Serilog.Log.Information(String.Format("Exception thrown: {0}\nStackTrace: {1}.", ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// Событие при изменении состояния кнопок.
        /// </summary>
        /// <param name="keys">Кнопки.</param>
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
        /// Переключение дополнений.
        /// </summary>
        /// <returns></returns>
        private bool SwitchExtension()
        {
            if (_Extensions.Count == 0)
            {
                return false;
            }
            _Model.Clear();
            if (_CurrentExtension != null)
            {
                Serilog.Log.Information("Unload extension.");
                ExtensionGrid.Children.Remove((UIElement)_CurrentExtension);
                _CurrentExtension = null;
                SeparatorButton.IsEnabled = true;
                _Model.CurrentNumeralSystem = NumeralSystem.DECIMAL_WITH_POINT;
                _Model.Child.Parent = null;
                _Model.Child = null;
                _Model.ActiveModel = null;
                if (_Extensions.Count == _ExtensionsEnumerator)
                {
                    _ExtensionsEnumerator = 0;
                    return false;
                }
            }
            Serilog.Log.Information("Load extension.");
            _CurrentExtension = _Extensions[_ExtensionsEnumerator];
            _Model.Child = _CurrentExtension.GetModel();
            _Model.Child.Parent = _Model;
            if (_Model.Child is DevCalculatorExtension.DevCalculatorModel)
            {
                _Model.HandleNumChanged += (o, e) => { (_Model.Child as DevCalculatorExtension.DevCalculatorModel).OutBinary(e.Data); };
                _Model.CurrentNumeralSystem = NumeralSystem.DECIMAL;
                SeparatorButton.IsEnabled = false;
            }
            _Model.ActiveModel = _Model.Child;
            _Model.HandleKeysStateChanged += (o, e) => { KeysStateChanged(e.Data); };
            _ExtensionsEnumerator++;
            return true;
        }
    }
}
