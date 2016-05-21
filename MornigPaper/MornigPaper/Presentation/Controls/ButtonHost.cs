using System.Windows.Forms.Integration;
using System.Windows.Media;

namespace MornigPaper.Presentation.Controls
{
    public partial class ButtonHost : ElementHost, IButtonStyleChanger
    {
        public ButtonHost()
        {
            InitializeComponent();
            //this.Child = new RoundButton();
        }

        /// <summary>
        /// Устанавливает дочерний элкмент.
        /// Должен быть RoundButton.
        /// </summary>
        public new IButtonStyleChanger Child
        {
            get { return base.Child as RoundButton; }
            set { base.Child = value as RoundButton; }
        }
        /// <summary>
        /// Текст кнопки.
        /// </summary>
        public override string Text
        {
            get
            {
                if (Child == null)
                    return string.Empty;
                return Child.Text as string;
            }
            set
            {
                if (Child == null)
                    return;
                Child.Text = value;
            }

        }
        /// <summary>
        /// Кисть, которая раскрашивает кнопку, когда над ней находится курсор мыши.
        /// </summary>
        public Brush MouseOverBrush
        {
            get
            {
                return Child.MouseOverBrush;
            }
            set
            {
                Child.MouseOverBrush = value;
            }
        }
        /// <summary>
        /// Кисть, которая раскрашивает кнопку, когда она нажата.
        /// </summary>
        public Brush IsPressedBrush
        {
            get
            {
                return Child.IsPressedBrush;
            }
            set
            {
                Child.IsPressedBrush = value;
            }
        }
        /// <summary>
        /// Кисть для прямогугольника обводки.
        /// </summary>
        public Brush BackBrush
        {
            get
            {
                return Child.BackBrush;
            }
            set
            {
                Child.BackBrush = value;
            }
        }
        /// <summary>
        /// Кисть для прямоугольника заливки.
        /// </summary>
        public Brush ShineBrush
        {
            get
            {
                return Child.ShineBrush;
            }
            set
            {
                Child.ShineBrush = value;
            }
        }
        /// <summary>
        /// Радиус кривизны прямогугольников.
        /// То есть округлость кнопки.
        /// </summary>
        public double Radius
        {
            get
            {
                return Child.Radius;
            }
            set
            {
                Child.Radius = value;
            }
        }
        /// <summary>
        /// Кисть для написания текста кнопки при наведении и нажатии.
        /// </summary>
        public Brush TextChangedBrush
        {
            get
            {
                return Child.TextChangedBrush;
            }
            set
            {
                Child.TextChangedBrush = value;
            }
        }
        /// <summary>
        /// Кисть для написания текста кнопки.
        /// </summary>
        public Brush TextDefaultBrush
        {
            get
            {
                return Child.TextDefaultBrush;
            }
            set
            {
                Child.TextDefaultBrush = value;
            }
        }

        /// <summary>
        /// Завершает инициализацию элемента управления, устанавливая стиль.
        /// После вызова этого метода стиль будет закреплён за кнопкой 
        /// и его дальнейшее изменение будет невозможным.
        /// </summary>
        public void AddStyle()
        {
            Child.AddStyle();
        }
    }
}
