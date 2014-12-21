﻿namespace MapEverything.Converters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Reflection;

    using Fasterflect;

    public class GenericTypeConverterOld<TFrom, TTo> : TypeConverter
    {
        private Dictionary<string, PropertyMapConvert> fromToMapping = new Dictionary<string, PropertyMapConvert>();
        private Dictionary<string, PropertyMapConvert> toFromMapping = new Dictionary<string, PropertyMapConvert>();

        public GenericTypeConverterOld(ITypeMapper typeMapper) : this(typeMapper, CultureInfo.CurrentCulture)
        {
        }

        public GenericTypeConverterOld(ITypeMapper typeMapper, IFormatProvider formatProvider)
        {
            var fromType = typeof(TFrom);
            var toType = typeof(TTo);
            
            foreach (var fromProp in fromType.GetProperties())
            {
                var toProp = toType.GetProperty(fromProp.Name);
                if (toProp != null)
                {
                    var fromConverter = typeMapper.GetConverter(fromProp.PropertyType, toProp.PropertyType);
                    var toConverter = typeMapper.GetConverter(toProp.PropertyType, fromProp.PropertyType);
                    this.fromToMapping.Add(fromProp.Name, new PropertyMapConvert(fromProp, toProp, fromConverter));
                    this.toFromMapping.Add(toProp.Name, new PropertyMapConvert(toProp, fromProp, toConverter));
                }
            }

        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(TTo))
            {
                return true;
            }

            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(TTo))
            {
                return true;
            }

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is TTo)
            {
                var result = Activator.CreateInstance<TFrom>();

                foreach (var key in this.toFromMapping.Keys)
                {
                    this.toFromMapping[key].MapConvertProperty(value, result);
                }

                return result;
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(TTo) && value is TFrom)
            {
                var result = Activator.CreateInstance<TTo>();

                foreach (var key in this.fromToMapping.Keys)
                {
                    this.fromToMapping[key].MapConvertProperty(value, result);
                }

                return result;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        private class PropertyMapConvert
        {
            private readonly MemberGetter fromTypeMemberGetter;

            private readonly MemberSetter toTypeMemberSetter;

            private readonly Func<object, object> converter;

            public PropertyMapConvert(PropertyInfo fromPropertyInfo, PropertyInfo toPropertyInfo, Func<object, object> converter)
            {
                this.fromTypeMemberGetter = fromPropertyInfo.DelegateForGetPropertyValue();
                this.toTypeMemberSetter = toPropertyInfo.DelegateForSetPropertyValue();
                this.converter = converter;
            }

            public void MapConvertProperty(object fromObject, object toObject)
            {
                this.toTypeMemberSetter(toObject, this.converter(this.fromTypeMemberGetter(fromObject)));
            }
        }
    }
}
