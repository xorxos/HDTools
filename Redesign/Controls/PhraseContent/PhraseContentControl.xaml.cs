using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Redesign.Controls.PhraseContent
{
    /// <summary>
    /// Interaction logic for PhraseContentControl.xaml
    /// </summary>
    public partial class PhraseContentControl : UserControl
    {
        public PhraseContentControl()
        {
            InitializeComponent();

            _FontFamily.ItemsSource = System.Windows.Media.Fonts.SystemFontFamilies;
            _FontSize.ItemsSource = FontSizes;
            _richTextBox.Focus();
        }

        public double[] FontSizes
        {
            get
            {
                return new double[] {
                    9.0,
                    10.0, 11.0, 12.0, 13.0, 14.0, 15.0,
                    16.0, 17.0, 18.0, 19.0, 20.0, 22.0, 24.0, 26.0, 28.0, 30.0,
                    32.0, 34.0, 36.0, 38.0, 40.0, 44.0, 48.0, 52.0, 56.0, 60.0, 64.0, 68.0, 72.0, 76.0,
                    80.0, 88.0, 96.0, 104.0, 112.0, 120.0, 128.0, 136.0, 144.0
                    };
            }
        }

        private void _richTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            UpdateVisualState();
        }

        private void FontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;
            _richTextBox.Focus();
            FontFamily editValue = (FontFamily)e.AddedItems[0];
            ApplyPropertyValueToSelectedText(TextElement.FontFamilyProperty, editValue);
        }

        private void FontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;
            _richTextBox.Focus();
            ApplyPropertyValueToSelectedText(TextElement.FontSizeProperty, e.AddedItems[0]);
            
        }

        void ApplyPropertyValueToSelectedText(DependencyProperty formattingProperty, object value)
        {
            //if (value == null)
            //    return;

            //_richTextBox.Selection.ApplyPropertyValue(formattingProperty, value);
            if ((_richTextBox == null) || (_richTextBox.Selection == null))
                return;

            SolidColorBrush solidColorBrush = value as SolidColorBrush;
            if ((solidColorBrush != null) && solidColorBrush.Color.Equals(Colors.Transparent))
            {
                _richTextBox.Selection.ApplyPropertyValue(formattingProperty, null);
            }
            else
            {
                _richTextBox.Selection.ApplyPropertyValue(formattingProperty, value);
            }
        }

        private void UpdateVisualState()
        {
            UpdateToggleButtonState();
            UpdateSelectionListType();
            UpdateSelectedFontFamily();
            UpdateFontColor();
            UpdateSelectedFontSize();
        }

        private void UpdateToggleButtonState()
        {
            UpdateItemCheckedState(_btnBold, TextElement.FontWeightProperty, FontWeights.Bold);
            UpdateItemCheckedState(_btnItalic, TextElement.FontStyleProperty, FontStyles.Italic);
            UpdateItemCheckedState(_btnUnderline, Inline.TextDecorationsProperty, TextDecorations.Underline);

            UpdateItemCheckedState(_btnAlignLeft, Paragraph.TextAlignmentProperty, TextAlignment.Left);
            UpdateItemCheckedState(_btnAlignCenter, Paragraph.TextAlignmentProperty, TextAlignment.Center);
            UpdateItemCheckedState(_btnAlignRight, Paragraph.TextAlignmentProperty, TextAlignment.Right);
            UpdateItemCheckedState(_btnAlignJustify, Paragraph.TextAlignmentProperty, TextAlignment.Right);
        }

        private void UpdateSelectionListType()
        {
            Paragraph startParagraph = _richTextBox.Selection.Start.Paragraph;
            Paragraph endParagraph = _richTextBox.Selection.End.Paragraph;
            if (startParagraph != null && endParagraph != null && (startParagraph.Parent is ListItem) && (endParagraph.Parent is ListItem) && object.ReferenceEquals(((ListItem)startParagraph.Parent).List, ((ListItem)endParagraph.Parent).List))
            {
                TextMarkerStyle markerStyle = ((ListItem)startParagraph.Parent).List.MarkerStyle;
                if (markerStyle == TextMarkerStyle.Disc) //bullets
                {
                    _btnBullets.IsChecked = true;
                }
                else if (markerStyle == TextMarkerStyle.Decimal) //numbers
                {
                    _btnNumbers.IsChecked = true;
                }
            }
            else
            {
                _btnBullets.IsChecked = false;
                _btnNumbers.IsChecked = false;
            }
        }

        private void UpdateSelectedFontFamily()
        {
            object value = _richTextBox.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
            FontFamily currentFontFamily = (FontFamily)((value == DependencyProperty.UnsetValue) ? null : value);
            if (currentFontFamily != null)
            {
                _FontFamily.SelectedItem = currentFontFamily;
            }
        }

        private void UpdateSelectedFontSize()
        {
            //object value = _richTextBox.Selection.GetPropertyValue(TextElement.FontSizeProperty);
            //_FontSize.SelectedValue = (value == DependencyProperty.UnsetValue) ? null : value;


            object value = DependencyProperty.UnsetValue;
            if ((_richTextBox != null) && (_richTextBox.Selection != null))
            {
                value = _richTextBox.Selection.GetPropertyValue(TextElement.FontSizeProperty);
            }

            if (value == DependencyProperty.UnsetValue)
                return;

            if (_FontSize != null)
            {
                _FontSize.SelectedValue = value;
            }
        }


        //private void _FontColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        //{
            
        //    Console.WriteLine(e.ToString());
        //    Color newColor = (Color)e.NewValue;
        //    Brush brush = new SolidColorBrush(newColor);
        //    ApplyPropertyValueToSelectedText(TextElement.ForegroundProperty, brush);
        //    _richTextBox.Focus();
            
        //}

        private void UpdateFontColor()
        {
            object value = DependencyProperty.UnsetValue;
            if ((_richTextBox != null) && (_richTextBox.Selection != null))
            {
                value = _richTextBox.Selection.GetPropertyValue(TextElement.ForegroundProperty);
            }

            if (value == DependencyProperty.UnsetValue)
                return;

            Color? currentColor = ((value == null)
                                    ? null
                                    : (Color?)((SolidColorBrush)value).Color);
            if (_FontColor != null)
            {
                _FontColor.SelectedColor = currentColor;
            }
        }

        private void UpdateFontBackgroundColor()
        {
            object value = DependencyProperty.UnsetValue;
            if ((_richTextBox != null) && (_richTextBox.Selection != null))
            {
                value = _richTextBox.Selection.GetPropertyValue(TextElement.BackgroundProperty);
            }

            if (value == DependencyProperty.UnsetValue)
                return;

            Color? currentColor = ((value == null)
                                    ? null
                                    : (Color?)((SolidColorBrush)value).Color);
            //if (_FontBackgroundColor != null)
            //{
            //    _FontBackgroundColor.SelectedColor = currentColor;
            //}
        }

        void UpdateItemCheckedState(ToggleButton button, DependencyProperty formattingProperty, object expectedValue)
        {
            //object currentValue = _richTextBox.Selection.GetPropertyValue(formattingProperty);
            //button.IsChecked = (currentValue == DependencyProperty.UnsetValue) ? false : currentValue != null && currentValue.Equals(expectedValue);
            


            object currentValue = DependencyProperty.UnsetValue;
            if ((_richTextBox != null) && (_richTextBox.Selection != null))
            {
                currentValue = _richTextBox.Selection.GetPropertyValue(formattingProperty);
            }

            if (currentValue == DependencyProperty.UnsetValue)
                return;

            if (button != null)
            {
                button.IsChecked = (currentValue == null)
                                    ? false
                                    : currentValue != null && currentValue.Equals(expectedValue);
            }
        }

        private void _btnBullets_Checked(object sender, RoutedEventArgs e)
        {
            _btnNumbers.IsChecked = false;
        }

        private void _btnNumbers_Checked(object sender, RoutedEventArgs e)
        {
            _btnBullets.IsChecked = false;
        }

        private void _btnAlignLeft_Checked(object sender, RoutedEventArgs e)
        {
            _btnAlignRight.IsChecked = false;
            _btnAlignCenter.IsChecked = false;
            _btnAlignJustify.IsChecked = false;
        }

        private void _btnAlignCenter_Checked(object sender, RoutedEventArgs e)
        {
            _btnAlignRight.IsChecked = false;
            _btnAlignLeft.IsChecked = false;
            _btnAlignJustify.IsChecked = false;
        }

        private void _btnAlignRight_Checked(object sender, RoutedEventArgs e)
        {
            _btnAlignLeft.IsChecked = false;
            _btnAlignCenter.IsChecked = false;
            _btnAlignJustify.IsChecked = false;
        }

        private void _btnAlignJustify_Checked(object sender, RoutedEventArgs e)
        {
            _btnAlignRight.IsChecked = false;
            _btnAlignCenter.IsChecked = false;
            _btnAlignLeft.IsChecked = false;
        }

        private void DropDownButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Clicked");
        }
        
    }
}
