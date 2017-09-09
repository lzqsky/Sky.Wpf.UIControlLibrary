namespace Sky.Wpf.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;


    /// <example>
    /// bool 转换 Visibility
    /// 设置IsReversed 是否反转
    /// 设置UseHidden  是否Hidden 或 Collapsed
    /// <code lang="xml">
    /// <![CDATA[
    /// <TextBox Visibility="{Binding ShowTheTextBox, Converter={BooleanToVisibilityConverter}}"/>
    /// ]]>
    /// </code>
    /// </example>
    /// <example>  
    /// <code lang="xml">
    /// <![CDATA[
    /// <TextBox Visibility="{Binding ShowTheTextBox, Converter={BooleanToVisibilityConverter UseHidden=true}}"/>
    /// ]]>
    /// </code>
    /// </example>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConverter : IValueConverter
    {
        private bool isReversed;
        private bool useHidden;
         
        public BooleanToVisibilityConverter()
        {
        }
        
       
        public BooleanToVisibilityConverter(bool isReversed, bool useHidden)
        {
            this.isReversed = isReversed;
            this.useHidden = useHidden;
        }

        /// <summary>
        /// 反转  如果是真转换成假
        /// </summary>
        public bool IsReversed
        {
            get { return this.isReversed; }
            set { this.isReversed = value; }
        }

        /// <summary>
        /// 如果是真 返回为Hidden  否则返回Collapsed
        /// </summary>
        public bool UseHidden
        {
            get { return this.useHidden; }
            set { this.useHidden = value; }
        }

       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = System.Convert.ToBoolean(value, CultureInfo.InvariantCulture);

            if (this.IsReversed)
            {
                val = !val;
            }

            if (val)
            {
                return Visibility.Visible;
            }
            
            return this.UseHidden ? Visibility.Hidden : Visibility.Collapsed;
        }

        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Visibility))
            {
                return DependencyProperty.UnsetValue;
            }

            var visibility = (Visibility)value;
            var result = visibility == Visibility.Visible;

            if (this.IsReversed)
            {
                result = !result;
            }

            return result;
        }
    }
}