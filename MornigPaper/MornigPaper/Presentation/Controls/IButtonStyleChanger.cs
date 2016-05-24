using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MornigPaper.Presentation.Controls
{
    /// <summary>
    /// Общий интерфейс для всех уровней взаимодействия с кнопкой.
    /// </summary>
    public interface IButtonStyleChanger
    {
        event ButtonClick ButtonClicked;

        ///// <summary>
        ///// Текст кнопки.
        ///// </summary>
        //string Text
        //{
        //    get;
        //    set;

        //}
        /// <summary>
        /// Кисть, которая раскрашивает кнопку, когда над ней находится курсор мыши.
        /// </summary>
        Brush MouseOverBrush
        {
            get;
            set;
        }
        /// <summary>
        /// Кисть, которая раскрашивает кнопку, когда она нажата.
        /// </summary>
        Brush IsPressedBrush
        {
            get;
            set;
        }
        /// <summary>
        /// Кисть для прямогугольника обводки.
        /// </summary>
        Brush BackBrush { get; set; }
        /// <summary>
        /// Кисть для прямоугольника заливки.
        /// </summary>
        Brush ShineBrush { get; set; }
        /// <summary>
        /// Радиус кривизны прямогугольников.
        /// То есть округлость кнопки.
        /// </summary>
        double Radius { get; set; }
        /// <summary>
        /// Кисть для написания текста кнопки при наведении и нажатии.
        /// </summary>
        Brush TextChangedBrush { get; set; }
        /// <summary>
        /// Кисть для написания текста кнопки.
        /// </summary>
        Brush TextDefaultBrush { get; set; }
        /// <summary>
        /// Задаёт высоту кнопок в элементе управления.
        /// </summary>
        double ButtonHeight { get; set; }

        /// <summary>
        /// Завершает инициализацию элемента управления, устанавливая стиль.
        /// После вызова этого метода стиль будет закреплён за кнопкой 
        /// и его дальнейшее изменение будет невозможным.
        /// </summary>
        void AddStyle();

        /// <summary>
        /// Добавление в элемент управления кнопки с надписью 
        /// для каждой строки в последовательности.
        /// </summary>
        void AddButtons(IEnumerable<string> buttonTexts);
    }

    public delegate void ButtonClick(ButtonClickedEventArgs e);
}
