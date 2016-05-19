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
    /// Логика взаимодействия для RoundButton.xaml
    /// </summary>
    public partial class RoundButton : UserControl
    {
        private static Style s;
        private Button basis;

        public RoundButton()
        {
            InitializeComponent();
            //this.Resources.Add(typeof(Button), GetButtonStyle());
            //foreach (var item in this.Resources)
            //{
            //    string s;
            //}



            this.gridDesu.Resources.Add(typeof(Button), GetButtonStyle());
            this.gridDesu.Children.Add(basis = new Button()
            {
                Name = "MyButton",
                Content = "MY P_I_N_K BUTTON!!!"//,
                //Height =  this.Height,
                //Width = this.Width

            });

            //object o = this.gridDesu.FindName("BackRect");

            FrameworkElementFactory f = ((basis.Style.Setters.Last() as Setter).Value as ControlTemplate).VisualTree.FirstChild;
            foreach (Trigger t in ((basis.Style.Setters.Last() as Setter).Value as ControlTemplate).Triggers)
            {

            }

            

            //this.SizeChanged += UserControl1_SizeChanged;
        }

        public string Text
        {
            get
            {
                if (this.basis == null)
                    throw new ClassNotConstructedException();
                return this.basis.Content as string;
            }
            set
            {
                if (this.basis == null)
                    throw new ClassNotConstructedException();
                this.basis.Content = value;
            }

        }

        private Style GetButtonStyle()
        {
            if (s != null)
                return s;

            Style style = new Style(typeof(Button));

            Setter foreground = new Setter(Button.ForegroundProperty,
                Brushes.Gold);
            style.Setters.Add(foreground);

            Setter margin = new Setter(Button.MarginProperty, new Thickness(1d));
            style.Setters.Add(margin);

            Setter template = GetTemplate();
            style.Setters.Add(template);

            style.Triggers.Add(GetTrigger(Button.IsMouseOverProperty));
            style.Triggers.Add(GetTrigger(Button.IsPressedProperty));

            return s = style;
        }

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

        private Trigger GetTrigger(DependencyProperty prop)
        {
            return GetTrigger(prop, 
                new Setter(Button.ForegroundProperty, 
                    new SolidColorBrush(Colors.Black)));
        }

        private Setter GetTemplate()
        {
            ControlTemplate template = new ControlTemplate(typeof(Button));

            template.VisualTree = GetVisualTree();

            // Works:

            //template.Triggers.Add(GetTrigger(Button.IsMouseOverProperty,
            //    new Setter(Button.ForegroundProperty,
            //        new SolidColorBrush(Colors.Red))));
            
            // Another way:

            //Trigger t = new Trigger()
            //    {
            //        Property = Button.IsMouseOverProperty,
            //        Value = true
            //    };
            //t.Setters.Add(new Setter(Rectangle.FillProperty,
            //        new RadialGradientBrush(
            //            Colors.Purple,
            //            Colors.MediumPurple),
            //            "BackRect"));


            template.Triggers.Add(GetTrigger(Button.IsMouseOverProperty,
                new Setter(Rectangle.FillProperty,
                    new RadialGradientBrush(
                        Colors.Purple,
                        Colors.MediumPurple),
                        "BackRect"/**/)));


            template.Triggers.Add(GetTrigger(Button.IsPressedProperty,
                new Setter(Rectangle.FillProperty,
                    new RadialGradientBrush(
                        Colors.Black,
                        Colors.Purple),
                        "BackRect"/**/)));

            return new Setter(Button.TemplateProperty, template);
        }

        private FrameworkElementFactory GetVisualTree()
        {
            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(Grid));

            factory.AppendChild(BackRect());
            factory.AppendChild(ShineRect());

            FrameworkElementFactory presenter = new FrameworkElementFactory(typeof(ContentPresenter));
            presenter.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);
            presenter.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            factory.AppendChild(presenter);

            return factory;
        }

        private FrameworkElementFactory BackRect()
        {
            FrameworkElementFactory rect = new FrameworkElementFactory(typeof(Rectangle));
            //rect.SetValue(Rectangle.NameProperty, "BackRect");
            //rect.SetValue(Rectangle.VerticalAlignmentProperty, VerticalAlignment.Top);
            rect.SetValue(Rectangle.OpacityProperty, 1d);
            rect.SetValue(Rectangle.RadiusXProperty, 9d);
            rect.SetValue(Rectangle.RadiusYProperty, 9d);
            rect.SetValue(Rectangle.StrokeThicknessProperty, 0.37);
            //rect.SetValue(Rectangle.HeightProperty, 15d);            
            rect.SetValue(Rectangle.StrokeProperty, new LinearGradientBrush(
                    Colors.Violet,
                    Colors.DeepPink,
                    new Point(0, 0),
                    new Point(0, 1)));
            rect.SetBinding(Rectangle.FillProperty, new Binding());

            rect.Name = "BackRect";

            return rect;
        }

        private FrameworkElementFactory ShineRect()
        {
            FrameworkElementFactory rect = new FrameworkElementFactory(typeof(Rectangle));
            rect.SetValue(Rectangle.NameProperty, "ShineRect");
            //rect.SetValue(Rectangle.VerticalAlignmentProperty, VerticalAlignment.Top);
            rect.SetValue(Rectangle.OpacityProperty, 1d);
            rect.SetValue(Rectangle.RadiusXProperty, 6d);
            rect.SetValue(Rectangle.RadiusYProperty, 6d);
            rect.SetValue(Rectangle.StrokeThicknessProperty, 0.37);
            rect.SetValue(Rectangle.StrokeProperty, Brushes.Transparent);
            //rect.SetValue(Rectangle.HeightProperty, 15d);            
            //rect.SetValue(Rectangle.HeightProperty, this.Height);
            //rect.SetBinding(Rectangle.HeightProperty, new Binding("Height"));
            //rect.SetBinding(Rectangle.HeightProperty, new Binding());
            rect.SetValue(Rectangle.FillProperty, new LinearGradientBrush(
                    Colors.Pink,
                    Colors.Orchid,
                    new Point(0, 0),
                    new Point(0, 1)));



            return rect;
        }
        
        private void UserControl1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.gridDesu.Height    = this.Height;
            this.gridDesu.Width     = this.Width; 
        }


    }
}
