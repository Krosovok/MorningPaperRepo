using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MornigPaper.Presentation.Controls
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        Dictionary<string, List<string>> ii;

        public UserControl1()
        {
            InitializeComponent();

            DataTemplate dt = GetHeaderTemplate();

            Style style = new Style() {
                TargetType = typeof(TreeViewItem)
            };
            style.Setters.Add(new Setter() {
                Property = TreeViewItem.HeaderTemplateProperty,
                Value = dt
            });

            this.Resources.Add(typeof(TreeViewItem), style);


            TreeViewItem treeItem = new TreeViewItem()
            {
                Header = "M",
                IsExpanded = true
            };

            //treeItem = new ImageTreeViewItem(dt)
            //{
            //    //ImageUrl = new Uri(@"C:\Users\Slava\Pictures\M.png"),
            //    Header = "M",
                
            //    ItemsSource = new string[] { "M" } 
            //    //Text = "Mythic"
            //};

            treeItem.Items.Add(new TreeViewItem()
            {
                Header = "R"
            });
            //{ 
            //    Header = "R",//Text = "Rare",  = new Uri(@"C:\Users\Slava\Pictures\R.png") 
            //    ItemsSource = new string[] { "R" } 
            //});
            treeItem.Items.Add(new TreeViewItem() {
                Header = "U"
            }); 
            treeItem.Items.Add(new TreeViewItem()
            {
                Header = "C"
            });
            //{
            //    Header = "U",//Text = "Uncommon", ImageUrl = new Uri(@"C:\Users\Slava\Pictures\U.png") 
            //    ItemsSource = new string[] { "U" } 
            //});
            //treeItem.Items.Add(new ImageTreeViewItem(dt) 
            //{
            //    Header = "C",//Text = "Common", ImageUrl = new Uri(@"C:\Users\Slava\Pictures\C.png") 
                
            //    ItemsSource = new string[] { "C" } 
            //});

            ii = new Dictionary<string, List<string>>();
            ii.Add("M", new List<string>(new string[] { "C", "U", "R" }));


            tvMain.Items.Add(treeItem);
        }

        private void FillTree(Dictionary<string, List<string>> ii)
        {
            tvMain.ItemTemplate = GetHeaderTemplate();
            tvMain.ItemContainerGenerator.StatusChanged +=
                new EventHandler(ItemContainerGenerator_StatusChanged);

            foreach (KeyValuePair<string, List<string>> i in ii)
            {
                tvMain.Items.Add(i);
            }
        }

        void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
        {
            if (tvMain.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                foreach (KeyValuePair<string, List<string>> i in ii)
                {
                    TreeViewItem item =
                (TreeViewItem)tvMain.ItemContainerGenerator.ContainerFromItem(i.Key);
                    if (item == null) continue;
                    item.IsExpanded = true;
                    if (item.Items.Count == 0)
                    {

                        foreach (string s in i.Value)
                        {
                            item.Items.Add(s);
                        }
                    }
                }
            }
        }

        private DataTemplate GetHeaderTemplate()
        {
            //create the data template
            DataTemplate dataTemplate = new DataTemplate();

            //create stack pane;
            FrameworkElementFactory stackPanel = new FrameworkElementFactory(typeof(StackPanel));
            stackPanel.Name = "parentStackpanel";
            stackPanel.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

            // Create check box
            FrameworkElementFactory checkBox = new FrameworkElementFactory(typeof(CheckBox));
            checkBox.Name = "chk";
            checkBox.SetValue(CheckBox.NameProperty, "chk");
            checkBox.SetValue(CheckBox.TagProperty, new Binding());
            checkBox.SetValue(CheckBox.MarginProperty, new Thickness(2));
            stackPanel.AppendChild(checkBox);

            // Create Image
            FrameworkElementFactory image = new FrameworkElementFactory(typeof(Image));
            image.SetValue(Image.MarginProperty, new Thickness(2));
            image.SetBinding(Image.SourceProperty, new Binding() { Converter = new CustomImagePathConverter() });
            stackPanel.AppendChild(image);

            // create text
            FrameworkElementFactory label = new FrameworkElementFactory(typeof(TextBlock));
            label.SetBinding(TextBlock.TextProperty, new Binding());
            label.SetValue(TextBlock.ToolTipProperty, new Binding());
            stackPanel.AppendChild(label);


            //set the visual tree of the data template
            dataTemplate.VisualTree = stackPanel;

            return dataTemplate;
        }

        public List<CheckBox> GetSelectedCheckBoxes(ItemCollection items)
        {
            List<CheckBox> list = new List<CheckBox>();
            foreach (TreeViewItem item in items)
            {
                UIElement elemnt = GetChildControl(item, "chk");
                if (elemnt != null)
                {
                    CheckBox chk = (CheckBox)elemnt;
                    if (chk.IsChecked.HasValue && chk.IsChecked.Value)
                    {
                        list.Add(chk);
                    }
                }

                List<CheckBox> l = GetSelectedCheckBoxes(item.Items);
                list.AddRange(l);
            }

            return list;
        }

        private UIElement GetChildControl(DependencyObject parentObject, string childName)
        {

            UIElement element = null;

            if (parentObject != null)
            {
                int totalChild = VisualTreeHelper.GetChildrenCount(parentObject);
                for (int i = 0; i < totalChild; i++)
                {
                    DependencyObject childObject = VisualTreeHelper.GetChild(parentObject, i);

                    if (childObject is FrameworkElement &&
                ((FrameworkElement)childObject).Name == childName)
                    {
                        element = childObject as UIElement;
                        break;
                    }

                    // get its child
                    element = GetChildControl(childObject, childName);
                    if (element != null) break;
                }
            }

            return element;
        }

        public class CustomImagePathConverter : IValueConverter
        {
            #region IValueConverter Members

            public object Convert(object value, Type targetType, object parameter,
                                            System.Globalization.CultureInfo culture)
            {
                return "../Images/" + GetImageName(value.ToString());
            }

            public object ConvertBack(object value, Type targetType, object parameter,
                                            System.Globalization.CultureInfo culture)
            {
                return "";
            }

            #endregion

            private string GetImageName(string text)
            {
                string name = "";
                name = text.ToLower() + ".png";
                return name;
            }
        }

        //private void CreateTreeViewItemTemplate()
        //{
        //    StackPanel stack = new StackPanel();
        //    stack.Orientation = Orientation.Horizontal;

        //    _image = new Image();
        //    _image.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
        //    _image.VerticalAlignment = System.Windows.VerticalAlignment.Center;
        //    _image.Width = 16;
        //    _image.Height = 16;
        //    _image.Margin = new Thickness(2);

        //    stack.Children.Add(_image);

        //    _textBlock = new TextBlock();
        //    _textBlock.Margin = new Thickness(2);
        //    _textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;

        //    stack.Children.Add(_textBlock);

        //    Header = stack;
        //}

        //private TreeViewItem GetTreeView(string text, string imagePath)
        //{
        //    TreeViewItem item = new TreeViewItem();

        //    item.IsExpanded = true;

        //    // create stack panel
        //    StackPanel stack = new StackPanel();
        //    stack.Orientation = Orientation.Horizontal;

        //    // create Image
        //    Image image = new Image();
        //    image.Source = new BitmapImage
        //        (new Uri("pack://application:,,/Images/" + imagePath));

        //    // Label
        //    Label lbl = new Label();
        //    lbl.Content = text;


        //    // Add into stack
        //    stack.Children.Add(image);
        //    stack.Children.Add(lbl);

        //    // assign stack to header
        //    item.Header = stack;
        //    return item;
        //}
    }
}
