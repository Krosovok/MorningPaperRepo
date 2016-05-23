using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;



namespace MornigPaper.Presentation.Controls
{
    public partial class ImageTreeViewItem : TreeViewItem
    {
        #region Data Member

        Uri _imageUrl = null;
        Image _image = null;
        TextBlock _textBlock = null;

        #endregion

        #region Constructor

        public ImageTreeViewItem(DataTemplate headerTemplate)
        {
            //CreateTreeViewItemTemplate();
            HeaderTemplate = headerTemplate;
        }

        #endregion

        #region Properties

        public Uri ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
                _image.Source = new BitmapImage(value);
            }
        }

        public string Text
        {
            get { return _textBlock.Text; }
            set { _textBlock.Text = value; }
        }

        #endregion

        #region Private Methods

       

        #endregion
    }
}
