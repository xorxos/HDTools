using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Redesign
{
    public class ResizingAdorner : Adorner
    {
        // Resizing adorner uses Thumbs for visual elements.   
        // The Thumbs have built-in mouse input handling. 
        Thumb topLeft, topRight, bottomLeft, bottomRight;

        double lastDragChange = 0;
        double lastTopLeftVerticleChange = 0;

        // To store and manage the adorner's visual children. 
        VisualCollection visualChildren;

        // Initialize the ResizingAdorner. 
        public ResizingAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            visualChildren = new VisualCollection(this);

            // Call a helper method to initialize the Thumbs 
            // with a customized cursors. 
            BuildAdornerCorner(ref topLeft, Cursors.SizeNWSE);
            BuildAdornerCorner(ref topRight, Cursors.SizeNESW);
            BuildAdornerCorner(ref bottomLeft, Cursors.SizeNESW);
            BuildAdornerCorner(ref bottomRight, Cursors.SizeNWSE);

            // Add handlers for resizing. 
            bottomLeft.DragDelta += new DragDeltaEventHandler(HandleBottomLeft);
            bottomLeft.DragCompleted += new DragCompletedEventHandler(Any_DragCompleted);
            bottomRight.DragDelta += new DragDeltaEventHandler(HandleBottomRight);
            topLeft.DragDelta += new DragDeltaEventHandler(HandleTopLeft);
            topLeft.DragCompleted += new DragCompletedEventHandler(Any_DragCompleted);
            topRight.DragDelta += new DragDeltaEventHandler(HandleTopRight);
            topRight.DragCompleted += new DragCompletedEventHandler(Any_DragCompleted);
        }

        private void Any_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            lastDragChange = 0;
            lastTopLeftVerticleChange = 0;
            
        }
        

        // Handler for resizing from the bottom-right. 
        void HandleBottomRight(object sender, DragDeltaEventArgs args)
        {
            FrameworkElement adornedElement = this.AdornedElement as FrameworkElement;
            Thumb bottomRightCorner = sender as Thumb;
            //setting new height and width after drag
            if (adornedElement != null && bottomRightCorner != null)
            {
                EnforceSize(adornedElement);

                double oldWidth = adornedElement.Width;
                double oldHeight = adornedElement.Height;

                double newWidth = Math.Max(adornedElement.Width + args.HorizontalChange, bottomRightCorner.DesiredSize.Width);
                double newHeight = Math.Max(args.VerticalChange + adornedElement.Height, bottomRightCorner.DesiredSize.Height);

                adornedElement.Width = newWidth;
                adornedElement.Height = newHeight;
            }
        }

        // Handler for resizing from the bottom-left. 
        void HandleBottomLeft(object sender, DragDeltaEventArgs e)
        {
            FrameworkElement adornedElement = this.AdornedElement as FrameworkElement;
            Thumb topRightCorner = sender as Thumb;
            //setting new height, width and canvas left after drag
            if (adornedElement != null && topRightCorner != null)
            {
                EnforceSize(adornedElement);

                double oldWidth = adornedElement.Width;
                double oldHeight = adornedElement.Height;

                
                double newWidth = Math.Max(adornedElement.Width - (e.HorizontalChange - lastDragChange), bottomLeft.DesiredSize.Width);
                double newHeight = Math.Max(adornedElement.Height + e.VerticalChange, bottomLeft.DesiredSize.Height);

                double oldLeft = Canvas.GetLeft(adornedElement);
                double newLeft = oldLeft - (newWidth - oldWidth);
                adornedElement.Width = newWidth;
                Canvas.SetLeft(adornedElement, newLeft);
                lastDragChange = e.HorizontalChange;
                adornedElement.Height = newHeight;
            }
        }

        // Handler for resizing from the top-right. 
        void HandleTopRight(object sender, DragDeltaEventArgs e)
        {
            FrameworkElement adornedElement = this.AdornedElement as FrameworkElement;
            Thumb topRightCorner = sender as Thumb;
            //setting new height, width and canvas top after drag
            if (adornedElement != null && topRightCorner != null)
            {
                EnforceSize(adornedElement);

                double oldWidth = adornedElement.Width;
                double oldHeight = adornedElement.Height;

                double newWidth = Math.Max(adornedElement.Width + e.HorizontalChange, topRightCorner.DesiredSize.Width);
                double newHeight = Math.Max(adornedElement.Height - (e.VerticalChange-lastDragChange), topRightCorner.DesiredSize.Height);
                adornedElement.Width = newWidth;

                double oldTop = Canvas.GetTop(adornedElement);
                double newTop = oldTop - (newHeight - oldHeight);
                adornedElement.Height = newHeight;
                lastDragChange = e.VerticalChange;
                Canvas.SetTop(adornedElement, newTop);
            }
        }

        // Handler for resizing from the top-left. 
        void HandleTopLeft(object sender, DragDeltaEventArgs e)
        {
            FrameworkElement adornedElement = this.AdornedElement as FrameworkElement;
            Thumb topLeftCorner = sender as Thumb;
            //setting new height, width and canvas top, left after drag
            if (adornedElement != null && topLeftCorner != null)
            {
                EnforceSize(adornedElement);

                double oldWidth = adornedElement.Width;
                double oldHeight = adornedElement.Height;

                double newWidth = Math.Max(adornedElement.Width - (e.HorizontalChange - lastDragChange), topLeftCorner.DesiredSize.Width);
                double newHeight = Math.Max(adornedElement.Height - (e.VerticalChange - lastTopLeftVerticleChange), topLeftCorner.DesiredSize.Height);

                double oldLeft = Canvas.GetLeft(adornedElement);
                double newLeft = oldLeft - (newWidth - oldWidth);
                adornedElement.Width = newWidth;
                Canvas.SetLeft(adornedElement, newLeft);

                double oldTop = Canvas.GetTop(adornedElement);
                double newTop = oldTop - (newHeight - oldHeight);
                adornedElement.Height = newHeight;

                lastDragChange = e.HorizontalChange;
                lastTopLeftVerticleChange = e.VerticalChange;
                Canvas.SetTop(adornedElement, newTop);
            }
        }

        // Arrange the Adorners. 
        protected override Size ArrangeOverride(Size finalSize)
        {
            // desiredWidth and desiredHeight are the width and height of the element that's being adorned.   
            // These will be used to place the ResizingAdorner at the corners of the adorned element.   
            double desiredWidth = (AdornedElement as FrameworkElement).ActualWidth;
            double desiredHeight = (AdornedElement as FrameworkElement).ActualHeight;
            // adornerWidth & adornerHeight are used for placement as well.
            double adornerWidth = (AdornedElement as FrameworkElement).Width;
            double adornerHeight = (AdornedElement as FrameworkElement).Height;
            //Arrange the thumbs:
            topLeft.Arrange(new Rect(-adornerWidth / 2, -adornerHeight / 2, adornerWidth, adornerHeight));
            topRight.Arrange(new Rect(desiredWidth - adornerWidth / 2, -adornerHeight / 2, adornerWidth, adornerHeight));
            bottomLeft.Arrange(new Rect(-adornerWidth / 2, desiredHeight - adornerHeight / 2, adornerWidth, adornerHeight));
            bottomRight.Arrange(new Rect(desiredWidth - adornerWidth / 2, desiredHeight - adornerHeight / 2, adornerWidth, adornerHeight));

            // Return the final size. 
            return finalSize;
        }

        // Helper method to instantiate the corner Thumbs, set the Cursor property,  
        // set some appearance properties, and add the elements to the visual tree. 
        void BuildAdornerCorner(ref Thumb circleThumb, Cursor customizedCursor)
        {
            if (circleThumb != null) return;

            circleThumb = new Thumb();

            // Set some arbitrary visual characteristics. 
            circleThumb.Cursor = customizedCursor;
            circleThumb.Height = circleThumb.Width = 10;
            circleThumb.BorderBrush = new SolidColorBrush(Colors.Gray);
            circleThumb.BorderThickness = new Thickness(1);
            circleThumb.Background = new SolidColorBrush(Colors.White);

            visualChildren.Add(circleThumb);
        }

        // This method ensures that the Widths and Heights are initialized.  Sizing to content produces 
        // Width and Height values of Double.NaN.  Because this Adorner explicitly resizes, the Width and Height 
        // need to be set first.  It also sets the maximum size of the adorned element. 
        void EnforceSize(FrameworkElement adornedElement)
        {
            if (adornedElement.Width.Equals(Double.NaN))
                adornedElement.Width = adornedElement.DesiredSize.Width;
            if (adornedElement.Height.Equals(Double.NaN))
                adornedElement.Height = adornedElement.DesiredSize.Height;

            FrameworkElement parent = adornedElement.Parent as FrameworkElement;
            if (parent != null)
            {
                adornedElement.MaxHeight = parent.ActualHeight;
                adornedElement.MaxWidth = parent.ActualWidth;
            }
        }
        // Override the VisualChildrenCount and GetVisualChild properties to interface with  
        // the adorner's visual collection. 
        protected override int VisualChildrenCount { get { return visualChildren.Count; } }
        protected override Visual GetVisualChild(int index) { return visualChildren[index]; }
    }
}
