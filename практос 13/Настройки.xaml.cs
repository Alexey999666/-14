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
using System.Windows.Shapes;

namespace практос_13
{
    /// <summary>
    /// Логика взаимодействия для Настройки.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }
        /// <summary>
        /// сохраняет файл с изначальнными настройками
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSavest_Click(object sender, RoutedEventArgs e)
        {
            
                if (int.TryParse(tbRowst.Text, out int Rowst) && Rowst > 0&& int.TryParse(tbColumnst.Text, out int Columnst)&& Columnst > 0 && int.TryParse(tbRangest.Text, out int Rangest)&& Rangest>0)
                {
                    Setting.Rows = Rowst;
                    Setting.Columns = Columnst;
                    Setting.Range = Rangest;
                    File.WriteAllText("config.ini", $"{Rowst} {Columnst} {Rangest}");
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Введите корректные значения.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            
        }
    }
}
