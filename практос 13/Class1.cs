using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace практос_13
{
    /// <summary>
    /// Класс для обмена значенями о размере таблицы между окнами
    /// </summary>
    internal static class Setting
    {
        /// <summary>
        /// Свойтсво, хранящее значение количества строк
        /// </summary>
        public static int Rows { get; set; }
        /// <summary>
        /// Свойтсво, хранящее значение количества столбцов
        /// </summary>
        public static int Columns { get; set; }
        /// <summary>
        /// Свойтсво, хранящее значение диапзона
        /// </summary>
        public static int Range { get; set; }
    }
}
