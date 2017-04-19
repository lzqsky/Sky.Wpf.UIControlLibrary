﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Documents;
using System.Windows.Input;

namespace test1
{
    public class CustomPanel : Panel
    {

        /// <summary>
        /// RequiredHeight Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty RequiredHeightProperty = DependencyProperty.RegisterAttached("RequiredHeight", typeof(double), typeof(CustomPanel), new FrameworkPropertyMetadata((double)double.NaN, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(OnRequiredHeightPropertyChanged)));

        private static void OnRequiredHeightPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {

        }

        public static double GetRequiredHeight(DependencyObject d)
        {
            return (double)d.GetValue(RequiredHeightProperty);
        }

        public static void SetRequiredHeight(DependencyObject d, double value)
        {
            d.SetValue(RequiredHeightProperty, value);
        }

        private double m_ExtraSpace = 0;

        private double m_NormalSpace = 0;

        protected override Size MeasureOverride(Size availableSize)
        {
            //Measure the children...
            foreach (UIElement child in Children)
                child.Measure(availableSize);

            //Sort them depending on their desired size...
            var sortedChildren = Children.Cast<UIElement>().OrderBy<UIElement, double>(new Func<UIElement, double>(delegate (UIElement child)
            {
                return GetRequiredHeight(child);
            }));

            //Compute remaining space...
            double remainingSpace = availableSize.Height;
            m_NormalSpace = 0.0;
            int remainingChildren = Children.Count;
            foreach (UIElement child in sortedChildren)
            {
                m_NormalSpace = remainingSpace / remainingChildren;
                double height = GetRequiredHeight(child);
                if (height < m_NormalSpace) // if == there would be no point continuing as there would be no remaining space
                    remainingSpace -= height;
                else
                {
                    remainingSpace = 0;
                    break;
                }
                remainingChildren--;
            }

            //Dtermine the extra space to add to every child...
            m_ExtraSpace = remainingSpace / Children.Count;
            return Size.Empty;  //The panel should take all the available space...
        }

        protected override System.Windows.Size ArrangeOverride(System.Windows.Size finalSize)
        {
            double offset = 0.0;

            foreach (UIElement child in Children)
            {
                double height = GetRequiredHeight(child);
                double value = (double.IsNaN(height) ? m_NormalSpace : Math.Min(height, m_NormalSpace)) + m_ExtraSpace;
                child.Arrange(new Rect(0, offset, finalSize.Width, value));
                offset += value;
            }

            return finalSize;   //The final size is the available size...
        }
    }

    public class MyPanelParent:Panel
    {
        public MyPanelParent()
        {
            ItemHeight = 40;
            ItemWidth = 80;
        }
        public double ItemHeight { get; set; }
        public double ItemWidth { get; set; }
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach(UIElement child in InternalChildren)
            {
                Size s = GetChildSize(child);
                child.Measure(availableSize);
                if(child.DesiredSize.Width < ItemWidth)
                {

                }
                else if(child.DesiredSize.Width % ItemWidth != 0)
                {
                    child.Measure(new Size(s.Width, s.Height));
                }
            }
            return availableSize;     //base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double x = 0;
            foreach(UIElement child in InternalChildren)
            {
                Size s = GetChildSize(child);
                child.Arrange(new Rect(x, 0, s.Width,s.Height));
                x += ItemWidth + 10;
            }
            return finalSize;
        }

        private Size GetChildSize(UIElement child)
        {
            int width = (int)(child.DesiredSize.Width / ItemWidth);
            int height = (int)(child.DesiredSize.Height / ItemHeight);
            return new Size(width == 0 ? ItemWidth : width, height == 0 ? ItemHeight : height);
        }
    }

    public class MyPanel:Grid
    {
        //protected override Size MeasureOverride(Size availableSize)
        //{
        //    //Size size = new Size(100, 50);
        //    //foreach (UIElement child in InternalChildren)
        //    //{
        //    //    child.Measure(size);
        //    //}
        //    return availableSize;
        //}

        protected override Size ArrangeOverride(Size finalSize)
        {
            //double x = 50;
            //foreach (UIElement child in InternalChildren)
            //{
            //    child.Arrange(new Rect(new Point(x, 0), child.DesiredSize));
            //    x += child.DesiredSize.Width + 10;
            //}
            return base.ArrangeOverride(finalSize);
        }
    }
}
