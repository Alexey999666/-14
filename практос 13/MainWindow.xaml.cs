using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;
using LibMas;
using System.ComponentModel;

namespace практос_13
{
   
    public partial class MainWindow : Window
    {
        /// <summary>
        /// создаем два массива для работы
        /// </summary>
        bool[] B;
        int[,] A;
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            ///загружаем файл изначальных настроек
            LoadedConfig();
            ///вызывается при закрытии программы
            this.Closing += Windows_Close;
        }
        /// <summary>
        /// закрывает программу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Очистка всех полей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridA.ItemsSource != null || datagridB.ItemsSource != null)
            {
                dataGridA.ItemsSource = null;
                A = null;
                datagridB.ItemsSource = null;
                B = null;
                tbRow.Clear();
                tbColumn.Clear();
                tbRange.Clear();
            }
            else MessageBox.Show("Таблица пуста", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        /// <summary>
        /// Информация о задании
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInf_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("По массиву A(5,6)получить массив В(6), присвоив его j-элементу значение true, если все " +
                "элементы j - столбца массива А нулевые, и значение false иначе.", "Задание", MessageBoxButton.OK, MessageBoxImage.Question);
        }
        /// <summary>
        /// Информация о разработчике
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutor_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Андрианов Алексей, Вариант 14", "Автор", MessageBoxButton.OK, MessageBoxImage.Question);
        }
        
        /// <summary>
        /// метод для создания и заполнения матрицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFill_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbRow.Text, out int N) && N > 0 && int.TryParse(tbColumn.Text, out int M) && M>0 && int.TryParse(tbRange.Text, out int Range)&& Range >0)
            {
                A = new int[N, M];

                Random rand = new Random();
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                    {
                        A[i, j] = rand.Next(Range + 1);
                    }
                }               
                dataGridA.ItemsSource = VisualArray.ToDataTableA(A).DefaultView;
                tbSize.Text = $"({N};{M})";
            }
            else MessageBox.Show("Введите правильное значение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
       
        /// <summary>
        /// метод для проверки j-элементов на то являются ли все они нулевыми
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            if (A != null && cellEditEnding == cellEditEndingDone)
            {
                B = new bool[A.GetLength(1)];
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    bool elementB = true;
                    for (int i = 0; i < A.GetLength(0); i++)
                    {
                        if (A[i, j] != 0)
                        {
                            elementB = false;
                            break;
                        }
                    }
                    B[j] = elementB;
                }

                datagridB.ItemsSource = VisualArray.ToDataTableB(B).DefaultView;
            }
            else MessageBox.Show("Введите правильное значение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        /// <summary>
        /// флаги для проверки коректной работы ручного изменения 
        /// </summary>
        bool cellEditEnding = false;
        bool cellEditEndingDone = false;
        /// <summary>
        /// метод для ручного изменения значений матрицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            cellEditEnding = true;
            int indexColumn = e.Column.DisplayIndex;
            if (Int32.TryParse(((TextBox)e.EditingElement).Text, out int result))
            {
                cellEditEndingDone = true;
                int indexRow = e.Row.GetIndex();

                A[indexRow, indexColumn] = result;
            }

        }
        /// <summary>
        /// метод для открытия файла с сохраненой матрицей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            A = ArrayEditor.Open();
            dataGridA.ItemsSource = VisualArray.ToDataTableA(A).DefaultView;
        }
        /// <summary>
        /// метод для сохранения матрицы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
             ArrayEditor.Save(A);
        }
        /// <summary>
        /// вызывается при загрузке основного окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Windows_Louded(object sender, RoutedEventArgs e)
        {
            ///вызываем окно с паролем
            Pasword pasword = new Pasword();
            pasword.Owner = this;
            if (pasword.ShowDialog() != true)
            {
                this.Close();
            }
            ///настраеваем таймер
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 1);
            timer.IsEnabled = true;
        }
        /// <summary>
        /// метод таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            tbDate.Text = dateTime.ToString("dd.MM.yy");
            tbTime.Text = dateTime.ToString("HH:mm:ss");
            timer.Start();
        }
        /// <summary>
        /// для подврежденния выхода из программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Windows_Close(object sender, CancelEventArgs e)
        {
            MessageBoxResult exit = MessageBox.Show("Вы хотите выйти?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (exit == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// загружаем файл с изначальными настройками
        /// </summary>
        private void LoadedConfig()
        {
            if (File.Exists("config.ini"))
            {
                string[] sizeTable = File.ReadAllText("config.ini").Split(' ');

                if (int.TryParse(sizeTable[0], out int rows) && int.TryParse(sizeTable[1], out int column) && int.TryParse(sizeTable[2], out int range))
                {
                    tbRow.Text = rows.ToString();
                    tbColumn.Text = column.ToString();
                    tbRange.Text = range.ToString();
                }
            }
        }
        /// <summary>
        /// открыаваем окно с нстройками
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings setting = new Settings();
            setting.Owner = this;
            if (setting.ShowDialog() == true)
            {
                tbRow.Text = Setting.Rows.ToString();
                tbColumn.Text = Setting.Columns.ToString();
                tbRange.Text = Setting.Range.ToString();
            }
        }
    }
}
