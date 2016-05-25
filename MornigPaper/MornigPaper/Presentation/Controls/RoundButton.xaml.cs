using MornigPaper.Exceptions;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MornigPaper.Presentation.Controls
{
    /// <summary>
    /// WPF элемент, представляющий собой округлую кнопку.
    /// </summary>
    public partial class RoundButtons : UserControl, IButtonStyleChanger
    {
        //private Button basis;
        private double buttonHeight;

        /// <summary>
        /// Создаёт экземпляр элемента управления с кнопкой. 
        /// В конструкторе не устанавливается стиль.
        /// Для завершения инициализации нужно вызвать метод AddStyle(), который задаст стиль. 
        /// До его вызова можно установить все свойства, связанные со стилем.
        /// </summary>
        public RoundButtons()
        {
            InitializeComponent();
            //this.gridDesu.Children.Add(basis = new Button()
            //{
            //    Name = "MyButton",
            //    Content = "MY P_I_N_K BUTTON!!!"
            //});
            

            MouseOverBrush = new RadialGradientBrush(
                        Colors.Purple,
                        Colors.MediumPurple);
            IsPressedBrush = new RadialGradientBrush(
                        Colors.Black,
                        Colors.Purple);
            BackBrush = new LinearGradientBrush(
                    Colors.Violet,
                    Colors.DeepPink,
                    new Point(0, 0),
                    new Point(0, 1));
            ShineBrush = new LinearGradientBrush(
                    Colors.Pink,
                    Colors.Orchid,
                    new Point(0, 0),
                    new Point(0, 1));
            Radius = 6d;
            TextDefaultBrush = Brushes.Black;
            TextChangedBrush = new SolidColorBrush(Colors.Gold);
        }

        public event ButtonClick ButtonClicked;

        //public string Text
        //{
        //    get
        //    {
        //        if (this.basis == null)
        //            throw new ClassNotConstructedException();
        //        return this.basis.Content as string;
        //    }
        //    set
        //    {
        //        if (this.basis == null)
        //            throw new ClassNotConstructedException();
        //        this.basis.Content = value;
        //    }

        //}
        public Brush MouseOverBrush
        {
            get;
            set;
        }
        public Brush IsPressedBrush
        {
            get;
            set;
        }
        public Brush BackBrush { get; set; }
        public Brush ShineBrush { get; set; }
        public double Radius { get; set; }
        public Brush TextChangedBrush { get; set; }
        public Brush TextDefaultBrush { get; set; }
        public double ButtonHeight
        {
            get
            {
                return buttonHeight;
            }
            set
            {
                buttonHeight = value;
                foreach (Button elem in this.Buttons.Children.OfType<Button>())
                {
                    elem.Height = buttonHeight;
                }
            }
        }

        public void AddStyle()
        {
            this.gridDesu.Resources.Add(typeof(Button), GetButtonStyle());
        }

        public void AddButtons(IEnumerable<string> buttonTexts)
        {
            foreach (string text in buttonTexts)
            {
                Button b = new Button()
                {
                    Content = text,
                    //Name = text,
                    Height = buttonHeight
                };
                b.Click += ButtonClickHandler;
                this.Buttons.Children.Add(b);
            }
        }

        /// <summary>
        /// Метод конструирует стиль, исходя из заданых свойств или их значений по умолчанию.
        /// </summary>
        private Style GetButtonStyle()
        {
            Style style = new Style(typeof(Button));

            Setter foreground = new Setter(Button.ForegroundProperty,
                TextDefaultBrush);
            style.Setters.Add(foreground);

            Setter margin = new Setter(Button.MarginProperty, new Thickness(1d));
            style.Setters.Add(margin);

            Setter template = GetTemplate();
            style.Setters.Add(template);

            style.Triggers.Add(GetTrigger(Button.IsMouseOverProperty));
            style.Triggers.Add(GetTrigger(Button.IsPressedProperty));

            return style;
        }

        /// <summary>
        /// Конструирует простой триггер, 
        /// который реагирует на установление данного свойства в true 
        /// используя данный объект типа Setter.
        /// </summary>
        private Trigger GetTrigger(DependencyProperty prop, Setter setter)
        {
            Trigger t = new Trigger()
            {
                Property = prop,
                Value = true
            };
            t.Setters.Add(setter);
            return t;
        }

        /// <summary>
        /// Конструирует простой триггер, 
        /// который реагирует на установление данного свойства в true 
        /// устанавливая свойство Foreground в значение TextChangedBrush.
        /// </summary>
        private Trigger GetTrigger(DependencyProperty prop)
        {
            return GetTrigger(prop, 
                new Setter(Button.ForegroundProperty, 
                    TextChangedBrush));
        }

        /// <summary>
        /// Конструирует шаблон кнопки с корневым узлом и двумя триггерами.
        /// </summary>
        private Setter GetTemplate()
        {
            ControlTemplate template = new ControlTemplate(typeof(Button));

            template.VisualTree = GetVisualTree();

            template.Triggers.Add(GetTrigger(Button.IsMouseOverProperty,
                new Setter(Rectangle.FillProperty,
                    MouseOverBrush,
                        "ShineRect")));


            template.Triggers.Add(GetTrigger(Button.IsPressedProperty,
                new Setter(Rectangle.FillProperty,
                    IsPressedBrush,
                        "ShineRect")));

            return new Setter(Button.TemplateProperty, template);
        }

        /// <summary>
        /// Конструирует корневой узел шаблона из двух прямоугольников.
        /// </summary>
        /// <returns></returns>
        private FrameworkElementFactory GetVisualTree()
        {
            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(Grid));

            factory.AppendChild(ShineRect());
            factory.AppendChild(BackRect());

            FrameworkElementFactory presenter = new FrameworkElementFactory(typeof(ContentPresenter));
            presenter.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);
            presenter.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            factory.AppendChild(presenter);

            return factory;
        }

        /// <summary>
        /// Консруирует прямоугольник обводки.
        /// </summary>
        private FrameworkElementFactory BackRect()
        {
            FrameworkElementFactory rect = new FrameworkElementFactory(typeof(Rectangle));
            rect.SetValue(Rectangle.OpacityProperty, 1d);
            rect.SetValue(Rectangle.RadiusXProperty, Radius + 3);
            rect.SetValue(Rectangle.RadiusYProperty, Radius + 3);
            rect.SetValue(Rectangle.StrokeThicknessProperty, 0.37); 
            rect.SetValue(Rectangle.StrokeProperty, BackBrush);
            rect.SetBinding(Rectangle.FillProperty, new Binding());

            rect.Name = "BackRect";

            return rect;
        }

        /// <summary>
        /// Конструирует прямогуольник заливки.
        /// </summary>
        private FrameworkElementFactory ShineRect()
        {
            FrameworkElementFactory rect = new FrameworkElementFactory(typeof(Rectangle));
            rect.SetValue(Rectangle.OpacityProperty, 1d);
            rect.SetValue(Rectangle.RadiusXProperty, Radius);
            rect.SetValue(Rectangle.RadiusYProperty, Radius);
            rect.SetValue(Rectangle.StrokeThicknessProperty, 0.37);
            rect.SetValue(Rectangle.StrokeProperty, Brushes.Transparent);
            rect.SetValue(Rectangle.FillProperty, ShineBrush);

            rect.Name = "ShineRect";

            return rect;
        }
        
        //private void UserControl1_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    this.gridDesu.Height    = this.Height;
        //    this.gridDesu.Width     = this.Width; 
        //}


        private void ButtonClickHandler(object sender, RoutedEventArgs e)
        {
            OnButtonClicked((sender as Button).Content as string);
        }

        private void OnButtonClicked(string data)
        {
            if (ButtonClicked != null)
            {
                ButtonClicked(new ButtonClickedEventArgs(data));
            }
        }


    }
}
