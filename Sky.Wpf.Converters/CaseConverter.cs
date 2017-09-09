namespace Sky.Wpf.Converters
{
    using Sky.Wpf.Converters.Common;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Markup;

    /// <summary>
    /// string 转换 string
    /// 大小写转换
    /// </summary>
    /// <remarks>
    /// <para>
    /// </para>
    /// </remarks>
    /// <example>
    /// <code lang="xml">
    /// <![CDATA[
    /// <Label Content="{Binding Name, Converter={CaseConverter Upper}}"/>
    /// ]]>
    /// </code>
    /// </example>
    /// <example>
    /// <code lang="xml">
    /// <![CDATA[
    /// <Label Content="{Binding Name, Converter={CaseConverter SourceCasing=Upper, TargetCasing=Lower}}"/>
    /// ]]>
    /// </code>
    /// </example> 
    [ValueConversion(typeof(string), typeof(string))] 
    public class CaseConverter : IValueConverter
    {
        private CharacterCasing sourceCasing;
        private CharacterCasing targetCasing;

        

        /// <summary>
        /// Initializes a new instance of the CaseConverter class with the specified source and target casings.
        /// </summary>
        /// <param name="casing">
        /// The source and target casings for the converter (see <see cref="Casing"/>).
        /// </param>
        public CaseConverter(CharacterCasing casing)
        {
            this.Casing = casing;
        }

        /// <summary>
        /// Initializes a new instance of the CaseConverter class with the specified source and target casings.
        /// </summary>
        /// <param name="sourceCasing">
        /// The source casing for the converter (see <see cref="SourceCasing"/>).
        /// </param>
        /// <param name="targetCasing">
        /// The target casing for the converter (see <see cref="TargetCasing"/>).
        /// </param>
        public CaseConverter(CharacterCasing sourceCasing, CharacterCasing targetCasing)
        {
            this.SourceCasing = sourceCasing;
            this.TargetCasing = targetCasing;
        }

        /// <summary>
        /// Gets or sets the source casing for the converter.
        /// </summary> 
        [ConstructorArgument("sourceCasing")] 
        public CharacterCasing SourceCasing
        {
            get
            {
                return this.sourceCasing;
            }

            set
            {
                value.AssertEnumMember("value", CharacterCasing.Lower, CharacterCasing.Upper, CharacterCasing.Normal);
                this.sourceCasing = value;
            }
        }

        /// <summary>
        /// Gets or sets the target casing for the converter.
        /// </summary> 
        [ConstructorArgument("targetCasing")] 
        public CharacterCasing TargetCasing
        {
            get
            {
                return this.targetCasing;
            }

            set
            {
                value.AssertEnumMember("value", CharacterCasing.Lower, CharacterCasing.Upper, CharacterCasing.Normal);
                this.targetCasing = value;
            }
        }

        /// <summary>
        /// Sets both the source and target casings for the converter.
        /// </summary> 
        [ConstructorArgument("casing")] 
        public CharacterCasing Casing
        {
            set
            {
                value.AssertEnumMember("value", CharacterCasing.Lower, CharacterCasing.Upper, CharacterCasing.Normal);
                this.sourceCasing = value;
                this.targetCasing = value;
            }
        }

        /// <summary>
        /// Attempts to convert the specified value.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <param name="targetType">
        /// The type of the binding target property.
        /// </param>
        /// <param name="parameter">
        /// The converter parameter to use.
        /// </param>
        /// <param name="culture">
        /// The culture to use in the converter.
        /// </param>
        /// <returns>
        /// A converted value.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;

            if (str != null)
            {
                culture = culture ?? CultureInfo.CurrentCulture;

                switch (this.TargetCasing)
                {
                    case CharacterCasing.Lower:
                        return str.ToLower(culture);
                    case CharacterCasing.Upper:
                        return str.ToUpper(culture);
                    case CharacterCasing.Normal:
                        return str;
                }
            }

            return DependencyProperty.UnsetValue;
        }

        /// <summary>
        /// Attempts to convert the specified value back.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <param name="targetType">
        /// The type of the binding target property.
        /// </param>
        /// <param name="parameter">
        /// The converter parameter to use.
        /// </param>
        /// <param name="culture">
        /// The culture to use in the converter.
        /// </param>
        /// <returns>
        /// A converted value.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;

            if (str != null)
            {
                culture = culture ?? CultureInfo.CurrentCulture;

                switch (this.SourceCasing)
                {
                    case CharacterCasing.Lower:
                        return str.ToLower(culture);
                    case CharacterCasing.Upper:
                        return str.ToUpper(culture);
                    case CharacterCasing.Normal:
                        return str;
                }
            }

            return DependencyProperty.UnsetValue;
        }
    }
}
