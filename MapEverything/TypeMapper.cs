﻿namespace MapEverything
{
    using System;
    using System.Collections.Concurrent;
    using System.ComponentModel;
    using System.Data.SqlTypes;
    using System.Globalization;
    using System.Linq;

    using MapEverything.Converters;

    public class TypeMapper : ITypeMapper
    {
        protected readonly Type[] ConvertTypes =
            {
                null,               // TypeCode.Empty = 0
                typeof(object),     // TypeCode.Object = 1
                typeof(DBNull),     // TypeCode.DBNull = 2
                typeof(bool),       // TypeCode.Boolean = 3
                typeof(char),       // TypeCode.Char = 4
                typeof(sbyte),      // TypeCode.SByte = 5
                typeof(byte),       // TypeCode.Byte = 6
                typeof(short),      // TypeCode.Int16 = 7
                typeof(ushort),     // TypeCode.UInt16 = 8
                typeof(int),        // TypeCode.Int32 = 9
                typeof(uint),       // TypeCode.UInt32 = 10
                typeof(long),       // TypeCode.Int64 = 11
                typeof(ulong),      // TypeCode.UInt64 = 12
                typeof(float),      // TypeCode.Single = 13
                typeof(double),     // TypeCode.Double = 14
                typeof(decimal),    // TypeCode.Decimal = 15
                typeof(DateTime),   // TypeCode.DateTime = 16
                typeof(object),     // 17 is missing
                typeof(string)      // TypeCode.String = 18
            };

        private ConcurrentDictionary<Type, TypeConverter> typeConverters;

        public TypeMapper()
        {
            this.typeConverters = new ConcurrentDictionary<Type, TypeConverter>();
            this.AddTypeConverter(typeof(Guid), new GuidTypeConverter());
            this.AddTypeConverter(typeof(SqlDateTime), new SqlDateTimeTypeConverter());
        }

        public TTo Convert<TFrom, TTo>(TFrom value)
        {
            return (TTo)this.Convert(value, typeof(TTo));
        }

        public TTo Convert<TFrom, TTo>(TFrom value, IFormatProvider formatProvider)
        {
            return (TTo)this.Convert(value, typeof(TTo), formatProvider);
        }

        public TTo Convert<TFrom, TTo>(TFrom value, Converter<TFrom, TTo> converter)
        {
            return converter(value);
        }

        public Converter<TFrom, TTo> GetConverter<TFrom, TTo>()
        {
            return this.GetConverter<TFrom, TTo>(CultureInfo.CurrentCulture);
        }

        public Converter<TFrom, TTo> GetConverter<TFrom, TTo>(IFormatProvider formatProvider)
        {
            var converter = this.GetConverter(typeof(TFrom), typeof(TTo), formatProvider);
            return value => (TTo)converter(value);
        }

        public object Convert(object value, Type toType)
        {
            return this.Convert(value, toType, CultureInfo.CurrentCulture);
        }

        public object Convert(object value, Func<object, object> converter)
        {
            return converter(value);
        }

        public virtual object Convert(object value, Type toType, IFormatProvider formatProvider)
        {
            if (value == null)
            {
                return this.GetDefaultValue(toType);
            }

            var converter = this.GetConverter(value.GetType(), toType, formatProvider);

            return converter(value);
        }

        public Func<object, object> GetConverter(Type fromType, Type toType)
        {
            return this.GetConverter(fromType, toType, CultureInfo.CurrentCulture);
        }

        public virtual Func<object, object> GetConverter(Type fromType, Type toType, IFormatProvider formatProvider)
        {
            if (toType == this.ConvertTypes[(int)TypeCode.String])
            {
                return value => this.ConvertToString(value, formatProvider);
            }

            var toConverter = this.GetTypeConverter(toType);
            if (toConverter.CanConvertFrom(fromType))
            {
                this.AddTypeConverter(toType, toConverter);
                return value => toConverter.ConvertFrom(null, (CultureInfo)formatProvider, value);
            }

            var fromConverter = this.GetTypeConverter(fromType);
            if (fromConverter.CanConvertTo(toType))
            {
                this.AddTypeConverter(fromType, fromConverter);
                return value => fromConverter.ConvertTo(null, (CultureInfo)formatProvider, value, toType);
            }

            return value => System.Convert.ChangeType(value, toType, formatProvider);
        }

        public void AddTypeConverter<T>(TypeConverter typeConverter)
        {
            this.AddTypeConverter(typeof(T), typeConverter);
        }

        protected TypeConverter GetTypeConverter(Type type)
        {
            TypeConverter typeConverter;
            if (this.typeConverters.TryGetValue(type, out typeConverter))
            {
                return typeConverter;
            }

            return TypeDescriptor.GetConverter(type);
        }

        protected virtual object GetDefaultValue(Type t)
        {
            if (t.IsValueType)
            {
                return Activator.CreateInstance(t);
            }

            return null;
        }

        protected virtual string ConvertToString(object value, IFormatProvider formatProvider)
        {
            var ic = value as IConvertible;
            if (ic != null)
            {
                return ic.ToString(formatProvider);
            }

            return value.ToString();
        }

        private void AddTypeConverter(Type keyType, TypeConverter typeConverter)
        {
            this.typeConverters[keyType] = typeConverter;
        }
    }
}
