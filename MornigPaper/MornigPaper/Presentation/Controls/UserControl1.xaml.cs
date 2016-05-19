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
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private static Style s;

        public UserControl1()
        {
            InitializeComponent();
            //this.Resources.Add(typeof(Button), GetButtonStyle());
            //foreach (var item in this.Resources)
            //{
            //    string s;
            //}



            this.gridDesu.Resources.Add(typeof(Button), GetButtonStyle());
            this.gridDesu.Children.Add(new Button()
            {
                Name = "MyButton",
                Content = "MY P_I_N_K BUTTON!!!"//,
                //Height =  this.Height,
                //Width = this.Width

            });

            this.SizeChanged += UserControl1_SizeChanged;
        }

        private void UserControl1_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.gridDesu.Height    = this.Height;
            this.gridDesu.Width     = this.Width; 
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


            return s = style;
        }

        private Setter GetTemplate()
        {
            ControlTemplate template = new ControlTemplate(typeof(Button));

            template.VisualTree = GetVisualTree();

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

        //private Grid GetGrid()
        //{
        //    Grid g = new Grid();

        //    g.Children.Add(GetBackRect());
        //    g.Children.Add(ShineRect());
        //    g.Children.Add(new ContentPresenter()
        //    {
        //        VerticalAlignment = VerticalAlignment.Center,
        //        HorizontalAlignment = HorizontalAlignment.Center
        //    });

        //    return g;
        //}

        private FrameworkElementFactory BackRect()
        {
            FrameworkElementFactory rect = new FrameworkElementFactory(typeof(Rectangle));
            rect.SetValue(Rectangle.NameProperty, "BackRect");
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
        

    }
}
