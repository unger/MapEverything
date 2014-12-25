﻿namespace MapEverything.Profiler
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using AutoMapper;

    using FastMapper;

    using MapEverything.Profiler.AutoMapperHelpers;
    using MapEverything.Tests.Model;
    using MapEverything.Tests.Model.Person;
    using MapEverything.Utils;

    using TB.ComponentModel;

    public class ProfileConversion : ProfileBase
    {
        protected override void Execute(int iterations)
        {
            var formatProvider = CultureInfo.CurrentCulture;

            var stringIntArray = new string[iterations];
            var stringInvalidArray = new string[iterations];
            var stringDecimalArray = new string[iterations];
            var stringGuidArray = new string[iterations];
            var stringDateTimeArray = new string[iterations];
            var guidArray = new Guid[iterations];
            var intArray = new int[iterations];
            var decimalArray = new decimal[iterations];
            var dateTimeArray = new DateTime[iterations];
            var customerArray = new Customer[iterations];
            var personArray = new Person[iterations];

            for (int i = 0; i < iterations; i++)
            {
                stringIntArray[i] = i.ToString(formatProvider);
                stringInvalidArray[i] = System.Web.Security.Membership.GeneratePassword((i % 10) + 1, i % 5);
                stringDecimalArray[i] = (i * 0.9m).ToString(formatProvider);
                stringGuidArray[i] = Guid.NewGuid().ToString();
                intArray[i] = i;
                decimalArray[i] = i * 0.9m;
                guidArray[i] = Guid.NewGuid();
                stringDateTimeArray[i] = DateTime.Now.ToString(formatProvider);
                dateTimeArray[i] = DateTime.Now;
                personArray[i] = new Person
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Name " + i,
                    Age = i % 85,
                    Length = 1.70m + ((i % 20) / 100m)
                };
                customerArray[i] = CustomerFactory.CreateTestCustomer();
            }
            
            /*
            ProfileConvert<string, int>(stringIntArray, formatProvider, i => int.Parse(stringIntArray[i], formatProvider));

            //ProfileConvert<string, int>(stringInvalidArray, formatProvider, i => int.Parse(stringInvalidArray[i], formatProvider));

            ProfileConvert<string, decimal>(stringDecimalArray, formatProvider, i => StringParser.TryParseDecimal(stringDecimalArray[i], formatProvider));

            //ProfileConvert<string, decimal>(stringInvalidArray, formatProvider, i => StringParser.TryParseDecimal(stringInvalidArray[i], formatProvider));

            ProfileConvert<string, Guid>(stringGuidArray, formatProvider, i => new Guid(stringGuidArray[i]));

            //ProfileConvert<string, Guid>(stringInvalidArray, formatProvider, i => new Guid(stringInvalidArray[i]));

            ProfileConvert<string, DateTime>(stringDateTimeArray, formatProvider, i => Convert.ToDateTime(stringDateTimeArray[i]));

            //ProfileConvert<string, DateTime>(stringInvalidArray, formatProvider, i => Convert.ToDateTime(stringInvalidArray[i]));

            ProfileConvert<int, string>(intArray, formatProvider, i => intArray[i].ToString(formatProvider));
            
            ProfileConvert<decimal, string>(decimalArray, formatProvider, i => decimalArray[i].ToString(formatProvider));
            
            ProfileConvert<Guid, string>(guidArray, CultureInfo.CurrentCulture, i => guidArray[i].ToString());

            ProfileConvert<DateTime, string>(dateTimeArray, CultureInfo.CurrentCulture, i => dateTimeArray[i].ToString());*/

            this.ProfileConvert<Customer, CustomerDto>(customerArray, CultureInfo.CurrentCulture, null);

            this.ProfileConvert<Person, PersonDto>(personArray, CultureInfo.CurrentCulture, null);
        }

        private void ProfileConvert<TSource, TDestination>(TSource[] input, CultureInfo formatProvider, Action<int> compareFunc)
        {
            var typeMapper = new TypeMapper();
            var typeMapperConverter = typeMapper.GetConverter(typeof(TSource), typeof(TDestination), formatProvider);

            if (typeof(TDestination) != typeof(string))
            {
                if (typeof(TDestination) == typeof(DateTime) && typeof(TSource) == typeof(string))
                {
                    Mapper.CreateMap(typeof(TSource), typeof(TDestination)).ConvertUsing(typeof(AutoMapperDateTimeTypeConverter));
                }
                else
                {
                    Mapper.CreateMap<TSource, TDestination>();
                }
            }

            Console.WriteLine("Profiling convert from {0} to {1}, {2} iterations", typeof(TSource).Name, typeof(TDestination).Name, input.Length);

            if (compareFunc != null)
            {
                this.AddResult(this.Profile("Native", input.Length, compareFunc));
            }

            this.AddResult(
                this.Profile(
                    "TypeMapper",
                    input.Length,
                    i => typeMapper.Convert(input[i], typeof(TDestination), formatProvider)));

            this.AddResult(this.Profile("TypeMapper delegate", input.Length, i => typeMapper.Convert(input[i], typeMapperConverter)));
            /*
            this.AddResult(
                this.Profile(
                    "SimpleTypeConverter",
                    input.Length,
                    i => SimpleTypeConverter.ConvertTo(input[i], typeof(TDestination), formatProvider)));


            this.AddResult(
                this.Profile(
                    "UniversalTypeConverter",
                    input.Length,
                    i => UniversalTypeConverter.Convert(input[i], typeof(TDestination), formatProvider)));

            */
            this.AddResult(
                this.Profile(
                    "FastMapper",
                    input.Length,
                    i => TypeAdapter.Adapt<TSource, TDestination>(input[i])));


            this.AddResult(this.Profile("AutoMapper", input.Length, i => Mapper.Map<TSource, TDestination>(input[i])));

        }
    }
}
