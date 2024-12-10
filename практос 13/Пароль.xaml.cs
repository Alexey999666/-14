using System;
using System.Collections.Generic;
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
   
    public partial class Pasword : Window
    {
        public Pasword()
        {
            InitializeComponent();
        }
        /// <summary>
        /// ставит курсор на поле ввода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Activated(object sender, EventArgs e)
        {
            pasword.Focus();
        }
        /// <summary>
        /// проверяет пароль
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbnPasword_Click(object sender, RoutedEventArgs e)
        {
   
            
                if (pasword.Password == "123")
                {
                    this.DialogResult = true;
                    this.Close();
                }
                if (pasword.Password != "123")
                {
                    MessageBox.Show("Неверный пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    pasword.Password = "";
                }
            
            
        }

        private void tbnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Close();
        }
    }
}
