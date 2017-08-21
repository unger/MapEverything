﻿namespace SafeMapper.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;

    using NUnit.Framework;

    using SafeMapper.Tests.Model;
    using SafeMapper.Tests.Model.Enums;
    using SafeMapper.Tests.Model.Person;

    public class TestData
    {

        /************************************************************************/
        /*                                                                      
        /*   Misc                                                              
        /*                                                                      
        /************************************************************************/

        public static TestCaseData[] NonGenericCollectionTestData =
            {
                // Collections
                new TestCaseData((IList<int>)new List<int> { 1, 2, 3 }, typeof(IList<int>), typeof(int[])).Returns(new[] { 1, 2, 3 }),
                new TestCaseData((IList<string>)new List<string> { "1", "2", "3" }, typeof(IList<string>), typeof(HashSet<string>)).Returns(new HashSet<string> { "1", "2", "3" }),
                new TestCaseData((ISet<string>)new HashSet<string> { "1", "2", "3" }, typeof(ISet<string>), typeof(List<int>)).Returns(new List<int> { 1, 2, 3 }),
                new TestCaseData((IEnumerable<string>)new List<string> { "1", "2", "3" }, typeof(IEnumerable<string>), typeof(List<int>)).Returns(new List<int> { 1, 2, 3 }),
                new TestCaseData(new List<string> { "1", "2", "3" }, typeof(List<string>), typeof(IEnumerable<int>)).Returns(new List<int> { 1, 2, 3 }),
            };

        public static TestCaseData[] NonGenericTestData =
            {
                new TestCaseData(new Person { Name = "Magnus", Age = 37 }, typeof(Person), typeof(string)).Returns("Magnus 37"),

                new TestCaseData(DateTime.MinValue, typeof(DateTime), typeof(SqlDateTime)).Returns(SqlDateTime.MinValue),
                new TestCaseData(DateTime.MaxValue, typeof(DateTime), typeof(SqlDateTime)).Returns(SqlDateTime.MaxValue),
                new TestCaseData(SqlDateTime.MinValue, typeof(SqlDateTime), typeof(DateTime)).Returns((DateTime)SqlDateTime.MinValue),
                new TestCaseData(SqlDateTime.MaxValue, typeof(SqlDateTime), typeof(DateTime)).Returns((DateTime)SqlDateTime.MaxValue),

                new TestCaseData("true", typeof(string), typeof(bool)).Returns(true),
                new TestCaseData("True", typeof(string), typeof(bool)).Returns(true),
                new TestCaseData("false", typeof(string), typeof(bool)).Returns(false),
                new TestCaseData("False", typeof(string), typeof(bool)).Returns(false),

                new TestCaseData(false, typeof(bool), typeof(string)).Returns("False"),
                new TestCaseData(true, typeof(bool), typeof(string)).Returns("True"),

                new TestCaseData(false, typeof(bool?), typeof(string)).Returns("False"),
                new TestCaseData(true, typeof(bool?), typeof(string)).Returns("True"),
                new TestCaseData(null, typeof(bool?), typeof(string)).Returns(null),

                new TestCaseData(Int32Enum.Undefined, typeof(Int32Enum), typeof(int)).Returns(0),
                new TestCaseData(Int32Enum.Value1, typeof(Int32Enum), typeof(int)).Returns(1),
                new TestCaseData(Int32Enum.Value2, typeof(Int32Enum), typeof(int)).Returns(2),
                new TestCaseData(Int32Enum.Value3, typeof(Int32Enum), typeof(int)).Returns(3),
                
                new TestCaseData(Int32Enum.Undefined, typeof(Int32Enum), typeof(string)).Returns("Undefined"),
                new TestCaseData(Int32Enum.Value1, typeof(Int32Enum), typeof(string)).Returns("Value1"),
                new TestCaseData(Int32Enum.Value2, typeof(Int32Enum), typeof(string)).Returns("Value2"),
                new TestCaseData(Int32Enum.Value3, typeof(Int32Enum), typeof(string)).Returns("Value3"),
                
                new TestCaseData(null, typeof(string), typeof(Int32Enum)).Returns(Int32Enum.Undefined),
                new TestCaseData(string.Empty, typeof(string), typeof(Int32Enum)).Returns(Int32Enum.Undefined),
                new TestCaseData("Undefined", typeof(string), typeof(Int32Enum)).Returns(Int32Enum.Undefined),
                new TestCaseData("Value1", typeof(string), typeof(Int32Enum)).Returns(Int32Enum.Value1),
                new TestCaseData("Value2", typeof(string), typeof(Int32Enum)).Returns(Int32Enum.Value2),
                new TestCaseData("Value3", typeof(string), typeof(Int32Enum)).Returns(Int32Enum.Value3),
                
                new TestCaseData(-1, typeof(int), typeof(Int32Enum)).Returns(Int32Enum.Undefined),
                new TestCaseData(0, typeof(int), typeof(Int32Enum)).Returns(Int32Enum.Undefined),
                new TestCaseData(1, typeof(int), typeof(Int32Enum)).Returns(Int32Enum.Value1),
                new TestCaseData(2, typeof(int), typeof(Int32Enum)).Returns(Int32Enum.Value2),
                new TestCaseData(3, typeof(int), typeof(Int32Enum)).Returns(Int32Enum.Value3),

                new TestCaseData(Int32Enum.Undefined, typeof(Int32Enum), typeof(AnotherEnum)).Returns(AnotherEnum.Undefined),
                new TestCaseData(Int32Enum.Value1, typeof(Int32Enum), typeof(AnotherEnum)).Returns(AnotherEnum.Value1),
                new TestCaseData(Int32Enum.Value2, typeof(Int32Enum), typeof(AnotherEnum)).Returns(AnotherEnum.Value2),
                new TestCaseData(Int32Enum.Value3, typeof(Int32Enum), typeof(AnotherEnum)).Returns(AnotherEnum.Undefined),

                new TestCaseData((short)-1, typeof(short), typeof(Int32Enum)).Returns(Int32Enum.Undefined),
                new TestCaseData((short)0, typeof(short), typeof(Int32Enum)).Returns(Int32Enum.Undefined),
                new TestCaseData((short)1, typeof(short), typeof(Int32Enum)).Returns(Int32Enum.Value1),

                new TestCaseData((long)-1, typeof(long), typeof(Int32Enum)).Returns(Int32Enum.Undefined),
                new TestCaseData((long)0, typeof(long), typeof(Int32Enum)).Returns(Int32Enum.Undefined),
                new TestCaseData((long)1, typeof(long), typeof(Int32Enum)).Returns(Int32Enum.Value1),
                new TestCaseData(long.MaxValue, typeof(long), typeof(Int32Enum)).Returns(Int32Enum.Undefined),

                new TestCaseData(DateTime.MinValue, typeof(DateTime), typeof(Int32Enum)).Returns(Int32Enum.Undefined),

                // Primitive numeric conversions
                // byte
                new TestCaseData((byte)1, typeof(byte), typeof(byte)).Returns((byte)1),
                new TestCaseData((byte)1, typeof(byte), typeof(sbyte)).Returns((sbyte)1),
                new TestCaseData((byte)1, typeof(byte), typeof(short)).Returns((short)1),
                new TestCaseData((byte)1, typeof(byte), typeof(ushort)).Returns((ushort)1),
                new TestCaseData((byte)1, typeof(byte), typeof(int)).Returns((int)1),
                new TestCaseData((byte)1, typeof(byte), typeof(uint)).Returns((uint)1),
                new TestCaseData((byte)1, typeof(byte), typeof(long)).Returns((long)1),
                new TestCaseData((byte)1, typeof(byte), typeof(ulong)).Returns((ulong)1),
                new TestCaseData((byte)1, typeof(byte), typeof(decimal)).Returns(1m),
                new TestCaseData((byte)1, typeof(byte), typeof(float)).Returns((float)1),
                new TestCaseData((byte)1, typeof(byte), typeof(double)).Returns(1d),
                new TestCaseData((byte)0, typeof(byte), typeof(bool)).Returns(false),
                new TestCaseData((byte)1, typeof(byte), typeof(bool)).Returns(true),
                new TestCaseData((byte)65, typeof(byte), typeof(char)).Returns('A'),

                // sbyte
                new TestCaseData((sbyte)1, typeof(sbyte), typeof(byte)).Returns((byte)1),
                new TestCaseData((sbyte)1, typeof(sbyte), typeof(sbyte)).Returns((sbyte)1),
                new TestCaseData((sbyte)1, typeof(sbyte), typeof(short)).Returns((short)1),
                new TestCaseData((sbyte)1, typeof(sbyte), typeof(ushort)).Returns((ushort)1),
                new TestCaseData((sbyte)1, typeof(sbyte), typeof(int)).Returns((int)1),
                new TestCaseData((sbyte)1, typeof(sbyte), typeof(uint)).Returns((uint)1),
                new TestCaseData((sbyte)1, typeof(sbyte), typeof(long)).Returns((long)1),
                new TestCaseData((sbyte)1, typeof(sbyte), typeof(ulong)).Returns((ulong)1),
                new TestCaseData((sbyte)1, typeof(sbyte), typeof(decimal)).Returns(1m),
                new TestCaseData((sbyte)1, typeof(sbyte), typeof(float)).Returns((float)1),
                new TestCaseData((sbyte)1, typeof(sbyte), typeof(double)).Returns(1d),
                new TestCaseData((sbyte)0, typeof(sbyte), typeof(bool)).Returns(false),
                new TestCaseData((sbyte)1, typeof(sbyte), typeof(bool)).Returns(true),
                new TestCaseData((sbyte)65, typeof(sbyte), typeof(char)).Returns('A'),
                
                // short
                new TestCaseData((short)1, typeof(short), typeof(byte)).Returns((byte)1),
                new TestCaseData((short)1, typeof(short), typeof(sbyte)).Returns((sbyte)1),
                new TestCaseData((short)1, typeof(short), typeof(short)).Returns((short)1),
                new TestCaseData((short)1, typeof(short), typeof(ushort)).Returns((ushort)1),
                new TestCaseData((short)1, typeof(short), typeof(int)).Returns((int)1),
                new TestCaseData((short)1, typeof(short), typeof(uint)).Returns((uint)1),
                new TestCaseData((short)1, typeof(short), typeof(long)).Returns((long)1),
                new TestCaseData((short)1, typeof(short), typeof(ulong)).Returns((ulong)1),
                new TestCaseData((short)1, typeof(short), typeof(decimal)).Returns(1m),
                new TestCaseData((short)1, typeof(short), typeof(float)).Returns((float)1),
                new TestCaseData((short)1, typeof(short), typeof(double)).Returns(1d),
                new TestCaseData((short)0, typeof(short), typeof(bool)).Returns(false),
                new TestCaseData((short)1, typeof(short), typeof(bool)).Returns(true),
                new TestCaseData((short)65, typeof(short), typeof(char)).Returns('A'),

                // ushort
                new TestCaseData((ushort)1, typeof(ushort), typeof(byte)).Returns((byte)1),
                new TestCaseData((ushort)1, typeof(ushort), typeof(sbyte)).Returns((sbyte)1),
                new TestCaseData((ushort)1, typeof(ushort), typeof(short)).Returns((short)1),
                new TestCaseData((ushort)1, typeof(ushort), typeof(ushort)).Returns((ushort)1),
                new TestCaseData((ushort)1, typeof(ushort), typeof(int)).Returns((int)1),
                new TestCaseData((ushort)1, typeof(ushort), typeof(uint)).Returns((uint)1),
                new TestCaseData((ushort)1, typeof(ushort), typeof(long)).Returns((long)1),
                new TestCaseData((ushort)1, typeof(ushort), typeof(ulong)).Returns((ulong)1),
                new TestCaseData((ushort)1, typeof(ushort), typeof(decimal)).Returns(1m),
                new TestCaseData((ushort)1, typeof(ushort), typeof(float)).Returns((float)1),
                new TestCaseData((ushort)1, typeof(ushort), typeof(double)).Returns(1d),
                new TestCaseData((ushort)0, typeof(ushort), typeof(bool)).Returns(false),
                new TestCaseData((ushort)1, typeof(ushort), typeof(bool)).Returns(true),
                new TestCaseData((ushort)65, typeof(ushort), typeof(char)).Returns('A'),

                // int
                new TestCaseData((int)1, typeof(int), typeof(byte)).Returns((byte)1),
                new TestCaseData((int)1, typeof(int), typeof(sbyte)).Returns((sbyte)1),
                new TestCaseData((int)1, typeof(int), typeof(short)).Returns((short)1),
                new TestCaseData((int)1, typeof(int), typeof(ushort)).Returns((ushort)1),
                new TestCaseData((int)1, typeof(int), typeof(int)).Returns((int)1),
                new TestCaseData((int)1, typeof(int), typeof(uint)).Returns((uint)1),
                new TestCaseData((int)1, typeof(int), typeof(long)).Returns((long)1),
                new TestCaseData((int)1, typeof(int), typeof(ulong)).Returns((ulong)1),
                new TestCaseData((int)1, typeof(int), typeof(decimal)).Returns(1m),
                new TestCaseData((int)1, typeof(int), typeof(float)).Returns((float)1),
                new TestCaseData((int)1, typeof(int), typeof(double)).Returns(1d),
                new TestCaseData((int)0, typeof(int), typeof(bool)).Returns(false),
                new TestCaseData((int)1, typeof(int), typeof(bool)).Returns(true),
                new TestCaseData((int)65, typeof(int), typeof(char)).Returns('A'),
                
                // uint
                new TestCaseData((uint)1, typeof(uint), typeof(byte)).Returns((byte)1),
                new TestCaseData((uint)1, typeof(uint), typeof(sbyte)).Returns((sbyte)1),
                new TestCaseData((uint)1, typeof(uint), typeof(short)).Returns((short)1),
                new TestCaseData((uint)1, typeof(uint), typeof(ushort)).Returns((ushort)1),
                new TestCaseData((uint)1, typeof(uint), typeof(int)).Returns((int)1),
                new TestCaseData((uint)1, typeof(uint), typeof(uint)).Returns((uint)1),
                new TestCaseData((uint)1, typeof(uint), typeof(long)).Returns((long)1),
                new TestCaseData((uint)1, typeof(uint), typeof(ulong)).Returns((ulong)1),
                new TestCaseData((uint)1, typeof(uint), typeof(decimal)).Returns(1m),
                new TestCaseData((uint)1, typeof(uint), typeof(float)).Returns((float)1),
                new TestCaseData((uint)1, typeof(uint), typeof(double)).Returns(1d),
                new TestCaseData((uint)0, typeof(uint), typeof(bool)).Returns(false),
                new TestCaseData((uint)1, typeof(uint), typeof(bool)).Returns(true),
                new TestCaseData((uint)65, typeof(uint), typeof(char)).Returns('A'),

                // long
                new TestCaseData((long)1, typeof(long), typeof(byte)).Returns((byte)1),
                new TestCaseData((long)1, typeof(long), typeof(sbyte)).Returns((sbyte)1),
                new TestCaseData((long)1, typeof(long), typeof(short)).Returns((short)1),
                new TestCaseData((long)1, typeof(long), typeof(ushort)).Returns((ushort)1),
                new TestCaseData((long)1, typeof(long), typeof(int)).Returns((int)1),
                new TestCaseData((long)1, typeof(long), typeof(uint)).Returns((uint)1),
                new TestCaseData((long)1, typeof(long), typeof(long)).Returns((long)1),
                new TestCaseData((long)1, typeof(long), typeof(ulong)).Returns((ulong)1),
                new TestCaseData((long)1, typeof(long), typeof(decimal)).Returns(1m),
                new TestCaseData((long)1, typeof(long), typeof(float)).Returns((float)1),
                new TestCaseData((long)1, typeof(long), typeof(double)).Returns(1d),
                new TestCaseData((long)0, typeof(long), typeof(bool)).Returns(false),
                new TestCaseData((long)1, typeof(long), typeof(bool)).Returns(true),
                new TestCaseData((long)65, typeof(long), typeof(char)).Returns('A'),

                // ulong
                new TestCaseData((ulong)1, typeof(ulong), typeof(byte)).Returns((byte)1),
                new TestCaseData((ulong)1, typeof(ulong), typeof(sbyte)).Returns((sbyte)1),
                new TestCaseData((ulong)1, typeof(ulong), typeof(short)).Returns((short)1),
                new TestCaseData((ulong)1, typeof(ulong), typeof(ushort)).Returns((ushort)1),
                new TestCaseData((ulong)1, typeof(ulong), typeof(int)).Returns((int)1),
                new TestCaseData((ulong)1, typeof(ulong), typeof(uint)).Returns((uint)1),
                new TestCaseData((ulong)1, typeof(ulong), typeof(long)).Returns((long)1),
                new TestCaseData((ulong)1, typeof(ulong), typeof(ulong)).Returns((ulong)1),
                new TestCaseData((ulong)1, typeof(ulong), typeof(decimal)).Returns(1m),
                new TestCaseData((ulong)1, typeof(ulong), typeof(float)).Returns((float)1),
                new TestCaseData((ulong)1, typeof(ulong), typeof(double)).Returns(1d),
                new TestCaseData((ulong)0, typeof(ulong), typeof(bool)).Returns(false),
                new TestCaseData((ulong)1, typeof(ulong), typeof(bool)).Returns(true),
                new TestCaseData((ulong)65, typeof(ulong), typeof(char)).Returns('A'),
                
                // decimal
                new TestCaseData((decimal)1, typeof(decimal), typeof(byte)).Returns((byte)1),
                new TestCaseData((decimal)1, typeof(decimal), typeof(sbyte)).Returns((sbyte)1),
                new TestCaseData((decimal)1, typeof(decimal), typeof(short)).Returns((short)1),
                new TestCaseData((decimal)1, typeof(decimal), typeof(ushort)).Returns((ushort)1),
                new TestCaseData((decimal)1, typeof(decimal), typeof(int)).Returns((int)1),
                new TestCaseData((decimal)1, typeof(decimal), typeof(uint)).Returns((uint)1),
                new TestCaseData((decimal)1, typeof(decimal), typeof(long)).Returns((long)1),
                new TestCaseData((decimal)1, typeof(decimal), typeof(ulong)).Returns((ulong)1),
                new TestCaseData((decimal)1, typeof(decimal), typeof(decimal)).Returns(1m),
                new TestCaseData((decimal)1, typeof(decimal), typeof(float)).Returns((float)1),
                new TestCaseData((decimal)1, typeof(decimal), typeof(double)).Returns(1d),
                new TestCaseData((decimal)0, typeof(decimal), typeof(bool)).Returns(false),
                new TestCaseData((decimal)1, typeof(decimal), typeof(bool)).Returns(true),
                new TestCaseData((decimal)65, typeof(decimal), typeof(char)).Returns('A'),
                
                // float
                new TestCaseData((float)1, typeof(float), typeof(byte)).Returns((byte)1),
                new TestCaseData((float)1, typeof(float), typeof(sbyte)).Returns((sbyte)1),
                new TestCaseData((float)1, typeof(float), typeof(short)).Returns((short)1),
                new TestCaseData((float)1, typeof(float), typeof(ushort)).Returns((ushort)1),
                new TestCaseData((float)1, typeof(float), typeof(int)).Returns((int)1),
                new TestCaseData((float)1, typeof(float), typeof(uint)).Returns((uint)1),
                new TestCaseData((float)1, typeof(float), typeof(long)).Returns((long)1),
                new TestCaseData((float)1, typeof(float), typeof(ulong)).Returns((ulong)1),
                new TestCaseData((float)1, typeof(float), typeof(decimal)).Returns(1m),
                new TestCaseData((float)1, typeof(float), typeof(float)).Returns((float)1),
                new TestCaseData((float)1, typeof(float), typeof(double)).Returns(1d),
                new TestCaseData((float)0, typeof(float), typeof(bool)).Returns(false),
                new TestCaseData((float)1, typeof(float), typeof(bool)).Returns(true),
                new TestCaseData((float)65, typeof(float), typeof(char)).Returns('A'),

                // double
                new TestCaseData((double)1, typeof(double), typeof(byte)).Returns((byte)1),
                new TestCaseData((double)1, typeof(double), typeof(sbyte)).Returns((sbyte)1),
                new TestCaseData((double)1, typeof(double), typeof(short)).Returns((short)1),
                new TestCaseData((double)1, typeof(double), typeof(ushort)).Returns((ushort)1),
                new TestCaseData((double)1, typeof(double), typeof(int)).Returns((int)1),
                new TestCaseData((double)1, typeof(double), typeof(uint)).Returns((uint)1),
                new TestCaseData((double)1, typeof(double), typeof(long)).Returns((long)1),
                new TestCaseData((double)1, typeof(double), typeof(ulong)).Returns((ulong)1),
                new TestCaseData((double)1, typeof(double), typeof(decimal)).Returns(1m),
                new TestCaseData((double)1, typeof(double), typeof(float)).Returns((float)1),
                new TestCaseData((double)1, typeof(double), typeof(double)).Returns(1d),
                new TestCaseData((double)0, typeof(double), typeof(bool)).Returns(false),
                new TestCaseData((double)1, typeof(double), typeof(bool)).Returns(true),
                new TestCaseData((double)65, typeof(double), typeof(char)).Returns('A'),
                
                // bool true
                new TestCaseData(true, typeof(bool), typeof(byte)).Returns((byte)1),
                new TestCaseData(true, typeof(bool), typeof(sbyte)).Returns((sbyte)1),
                new TestCaseData(true, typeof(bool), typeof(short)).Returns((short)1),
                new TestCaseData(true, typeof(bool), typeof(ushort)).Returns((ushort)1),
                new TestCaseData(true, typeof(bool), typeof(int)).Returns((int)1),
                new TestCaseData(true, typeof(bool), typeof(uint)).Returns((uint)1),
                new TestCaseData(true, typeof(bool), typeof(long)).Returns((long)1),
                new TestCaseData(true, typeof(bool), typeof(ulong)).Returns((ulong)1),
                new TestCaseData(true, typeof(bool), typeof(decimal)).Returns(1m),
                new TestCaseData(true, typeof(bool), typeof(float)).Returns((float)1),
                new TestCaseData(true, typeof(bool), typeof(double)).Returns(1d),
                new TestCaseData(true, typeof(bool), typeof(bool)).Returns(true),
                new TestCaseData(true, typeof(bool), typeof(char)).Returns('1'),
                
                // bool false
                new TestCaseData(false, typeof(bool), typeof(byte)).Returns((byte)0),
                new TestCaseData(false, typeof(bool), typeof(sbyte)).Returns((sbyte)0),
                new TestCaseData(false, typeof(bool), typeof(short)).Returns((short)0),
                new TestCaseData(false, typeof(bool), typeof(ushort)).Returns((ushort)0),
                new TestCaseData(false, typeof(bool), typeof(int)).Returns((int)0),
                new TestCaseData(false, typeof(bool), typeof(uint)).Returns((uint)0),
                new TestCaseData(false, typeof(bool), typeof(long)).Returns((long)0),
                new TestCaseData(false, typeof(bool), typeof(ulong)).Returns((ulong)0),
                new TestCaseData(false, typeof(bool), typeof(decimal)).Returns(0m),
                new TestCaseData(false, typeof(bool), typeof(float)).Returns((float)0),
                new TestCaseData(false, typeof(bool), typeof(double)).Returns(0d),
                new TestCaseData(false, typeof(bool), typeof(bool)).Returns(false),
                new TestCaseData(false, typeof(bool), typeof(char)).Returns('0'),
                
                // char
                new TestCaseData('A', typeof(char), typeof(byte)).Returns((byte)65),
                new TestCaseData('A', typeof(char), typeof(sbyte)).Returns((sbyte)65),
                new TestCaseData('A', typeof(char), typeof(short)).Returns((short)65),
                new TestCaseData('A', typeof(char), typeof(ushort)).Returns((ushort)65),
                new TestCaseData('A', typeof(char), typeof(int)).Returns((int)65),
                new TestCaseData('A', typeof(char), typeof(uint)).Returns((uint)65),
                new TestCaseData('A', typeof(char), typeof(long)).Returns((long)65),
                new TestCaseData('A', typeof(char), typeof(ulong)).Returns((ulong)65),
                new TestCaseData('A', typeof(char), typeof(decimal)).Returns(65m),
                new TestCaseData('A', typeof(char), typeof(float)).Returns((float)65),
                new TestCaseData('A', typeof(char), typeof(double)).Returns(65d),
                new TestCaseData('A', typeof(char), typeof(bool)).Returns(true),
                new TestCaseData('0', typeof(char), typeof(bool)).Returns(false),
                new TestCaseData('1', typeof(char), typeof(bool)).Returns(true),
                new TestCaseData('A', typeof(char), typeof(char)).Returns('A'),

                // Other
                new TestCaseData("SafeMapper", typeof(string), typeof(char[])).Returns(
                    new[] { 'S', 'a', 'f', 'e', 'M', 'a', 'p', 'p', 'e', 'r' }),
                new TestCaseData(
                    new[] { 'S', 'a', 'f', 'e', 'M', 'a', 'p', 'p', 'e', 'r' },
                    typeof(char[]),
                    typeof(string)).Returns("SafeMapper"),
            };


        /************************************************************************/
        /*                                                                      
        /*   String                                                              
        /*                                                                      
        /************************************************************************/

        public static TestCaseData[] StringToStringData =
            {
                new TestCaseData(string.Empty).Returns(string.Empty),
                new TestCaseData(null).Returns(null),
                new TestCaseData("foo").Returns("foo"),
            };

        public static TestCaseData[] StringToIntData =
            {
                new TestCaseData(string.Empty).Returns(0),
                new TestCaseData(null).Returns(0),
                new TestCaseData("0").Returns(0),
                new TestCaseData("10").Returns(10),
                new TestCaseData("10.0").Returns(0),
                new TestCaseData("10,0").Returns(0),
                new TestCaseData("10.5").Returns(0),
                new TestCaseData("10,5").Returns(0),
                new TestCaseData("-10").Returns(-10),
                new TestCaseData("1 000").Returns(0),
                new TestCaseData("1,000").Returns(0),
                new TestCaseData("1.000").Returns(0),
                new TestCaseData("1 000.00").Returns(0),
                new TestCaseData("2147483647").Returns(2147483647),
                new TestCaseData("2147483648").Returns(0),
                new TestCaseData("-2147483648").Returns(-2147483648),
                new TestCaseData("-2147483649").Returns(0),
                new TestCaseData("abc").Returns(0),
                new TestCaseData("123a").Returns(0),
            };

        public static TestCaseData[] StringToNullableIntData =
            {
                new TestCaseData(string.Empty).Returns(null),
                new TestCaseData(null).Returns(null),
                new TestCaseData("0").Returns(0),
                new TestCaseData("10").Returns(10),
                new TestCaseData("10.0").Returns(null),
                new TestCaseData("10,0").Returns(null),
                new TestCaseData("10.5").Returns(null),
                new TestCaseData("10,5").Returns(null),
                new TestCaseData("-10").Returns(-10),
                new TestCaseData("1 000").Returns(null),
                new TestCaseData("1,000").Returns(null),
                new TestCaseData("1.000").Returns(null),
                new TestCaseData("1 000.00").Returns(null),
                new TestCaseData("2147483647").Returns(2147483647),
                new TestCaseData("2147483648").Returns(null),
                new TestCaseData("-2147483648").Returns(-2147483648),
                new TestCaseData("-2147483649").Returns(null),
                new TestCaseData("abc").Returns(null),
                new TestCaseData("123a").Returns(null),
            };

        public static TestCaseData[] StringToGuidData =
            {
                new TestCaseData(string.Empty).Returns(Guid.Empty),
                new TestCaseData(null).Returns(Guid.Empty),
                new TestCaseData("00000000-0000-0000-0000-000000000000").Returns(Guid.Empty),
                new TestCaseData("0cb6c00f-fc44-484f-8ddd-823709b74601").Returns(new Guid("0cb6c00f-fc44-484f-8ddd-823709b74601")),
                new TestCaseData("0cb6c00ffc44484f8ddd823709b74601").Returns(new Guid("0cb6c00f-fc44-484f-8ddd-823709b74601")),
                new TestCaseData("abc").Returns(Guid.Empty),
                new TestCaseData("123").Returns(Guid.Empty),
            };

        public static TestCaseData[] StringToDateTimeData =
            {
                new TestCaseData(string.Empty).Returns(DateTime.MinValue),
                new TestCaseData(null).Returns(DateTime.MinValue),
                new TestCaseData("1977-03-04").Returns(new DateTime(1977, 03, 04)),
                new TestCaseData("1977-03-04 13:37").Returns(new DateTime(1977, 03, 04, 13, 37, 00)),
            };

        /************************************************************************/
        /*                                                                      
        /*   From Decimal                                                              
        /*                                                                      
        /************************************************************************/

        public static TestCaseData[] DecimalToStringData =
            {
                new TestCaseData(decimal.MaxValue).Returns("79228162514264337593543950335"),
                new TestCaseData(decimal.MinValue).Returns("-79228162514264337593543950335"),
                new TestCaseData(0m).Returns("0"),
                new TestCaseData(123.5m).Returns("123.5"),
                new TestCaseData(123.49m).Returns("123.49"),
                new TestCaseData(1.00m).Returns("1.00"),
                new TestCaseData(1000m).Returns("1000"),
                new TestCaseData(1000000m).Returns("1000000"),
            };

        public static TestCaseData[] DecimalToByteData =
            {
                new TestCaseData(decimal.MaxValue).Returns(0),
                new TestCaseData(decimal.MinValue).Returns(0),
                new TestCaseData((decimal)byte.MaxValue).Returns(byte.MaxValue),
                new TestCaseData((decimal)byte.MinValue).Returns(byte.MinValue),
                new TestCaseData(123.5m).Returns(123),
                new TestCaseData(123.49m).Returns(123),
            };

        public static TestCaseData[] DecimalToNullableByteData =
            {
                new TestCaseData(decimal.MaxValue).Returns(null),
                new TestCaseData(decimal.MinValue).Returns(null),
                new TestCaseData((decimal)byte.MaxValue).Returns(byte.MaxValue),
                new TestCaseData((decimal)byte.MinValue).Returns(byte.MinValue),
                new TestCaseData(123.5m).Returns(123),
                new TestCaseData(123.49m).Returns(123),
            };

        public static TestCaseData[] DecimalToSByteData =
            {
                new TestCaseData(decimal.MaxValue).Returns(0),
                new TestCaseData(decimal.MinValue).Returns(0),
                new TestCaseData((decimal)sbyte.MaxValue).Returns(sbyte.MaxValue),
                new TestCaseData((decimal)sbyte.MinValue).Returns(sbyte.MinValue),
                new TestCaseData(123.5m).Returns(123),
                new TestCaseData(123.49m).Returns(123),
            };

        public static TestCaseData[] DecimalToNullableSByteData =
            {
                new TestCaseData(decimal.MaxValue).Returns(null),
                new TestCaseData(decimal.MinValue).Returns(null),
                new TestCaseData((decimal)sbyte.MaxValue).Returns(sbyte.MaxValue),
                new TestCaseData((decimal)sbyte.MinValue).Returns(sbyte.MinValue),
                new TestCaseData(123.5m).Returns(123),
                new TestCaseData(123.49m).Returns(123),
            };

        public static TestCaseData[] DecimalToInt16Data =
            {
                new TestCaseData(decimal.MaxValue).Returns(0),
                new TestCaseData(decimal.MinValue).Returns(0),
                new TestCaseData((decimal)short.MaxValue).Returns(short.MaxValue),
                new TestCaseData((decimal)short.MinValue).Returns(short.MinValue),
                new TestCaseData(123.5m).Returns(123),
                new TestCaseData(123.49m).Returns(123),
            };

        public static TestCaseData[] DecimalToNullableInt16Data =
            {
                new TestCaseData(decimal.MaxValue).Returns(null),
                new TestCaseData(decimal.MinValue).Returns(null),
                new TestCaseData((decimal)short.MaxValue).Returns(short.MaxValue),
                new TestCaseData((decimal)short.MinValue).Returns(short.MinValue),
                new TestCaseData(123.5m).Returns(123),
                new TestCaseData(123.49m).Returns(123),
            };

        public static TestCaseData[] DecimalToUInt16Data =
            {
                new TestCaseData(decimal.MaxValue).Returns(0),
                new TestCaseData(decimal.MinValue).Returns(0),
                new TestCaseData((decimal)ushort.MaxValue).Returns(ushort.MaxValue),
                new TestCaseData((decimal)ushort.MinValue).Returns(ushort.MinValue),
                new TestCaseData(123.5m).Returns(123),
                new TestCaseData(123.49m).Returns(123),
            };

        public static TestCaseData[] DecimalToNullableUInt16Data =
            {
                new TestCaseData(decimal.MaxValue).Returns(null),
                new TestCaseData(decimal.MinValue).Returns(null),
                new TestCaseData((decimal)ushort.MaxValue).Returns(ushort.MaxValue),
                new TestCaseData((decimal)ushort.MinValue).Returns(ushort.MinValue),
                new TestCaseData(123.5m).Returns(123),
                new TestCaseData(123.49m).Returns(123),
            };

        public static TestCaseData[] DecimalToInt32Data =
            {
                new TestCaseData(decimal.MaxValue).Returns(0),
                new TestCaseData(decimal.MinValue).Returns(0),
                new TestCaseData((decimal)int.MaxValue).Returns(int.MaxValue),
                new TestCaseData((decimal)int.MinValue).Returns(int.MinValue),
                new TestCaseData(123.5m).Returns(123),
                new TestCaseData(123.49m).Returns(123),
                new TestCaseData(-123.5m).Returns(-123),
            };

        public static TestCaseData[] DecimalToNullableInt32Data =
            {
                new TestCaseData(decimal.MaxValue).Returns(null),
                new TestCaseData(decimal.MinValue).Returns(null),
                new TestCaseData((decimal)int.MaxValue).Returns(int.MaxValue),
                new TestCaseData((decimal)int.MinValue).Returns(int.MinValue),
                new TestCaseData(123.5m).Returns(123),
                new TestCaseData(123.49m).Returns(123),
                new TestCaseData(-123.5m).Returns(-123),
            };

        public static TestCaseData[] DecimalToUInt32Data =
            {
                new TestCaseData(decimal.MaxValue).Returns(0),
                new TestCaseData(decimal.MinValue).Returns(0),
                new TestCaseData((decimal)uint.MaxValue).Returns(uint.MaxValue),
                new TestCaseData((decimal)uint.MinValue).Returns(uint.MinValue),
                new TestCaseData(123.5m).Returns(123),
                new TestCaseData(123.49m).Returns(123),
            };

        public static TestCaseData[] DecimalToNullableUInt32Data =
            {
                new TestCaseData(decimal.MaxValue).Returns(null),
                new TestCaseData(decimal.MinValue).Returns(null),
                new TestCaseData((decimal)uint.MaxValue).Returns(uint.MaxValue),
                new TestCaseData((decimal)uint.MinValue).Returns(uint.MinValue),
                new TestCaseData(123.5m).Returns(123),
                new TestCaseData(123.49m).Returns(123),
            };

        public static TestCaseData[] DecimalToInt64Data =
            {
                new TestCaseData(decimal.MaxValue).Returns(0),
                new TestCaseData(decimal.MinValue).Returns(0),
                new TestCaseData((decimal)long.MaxValue).Returns(long.MaxValue),
                new TestCaseData((decimal)long.MinValue).Returns(long.MinValue),
                new TestCaseData(123.5m).Returns(123),
                new TestCaseData(123.49m).Returns(123),
            };

        public static TestCaseData[] DecimalToNullableInt64Data =
            {
                new TestCaseData(decimal.MaxValue).Returns(null),
                new TestCaseData(decimal.MinValue).Returns(null),
                new TestCaseData((decimal)long.MaxValue).Returns(long.MaxValue),
                new TestCaseData((decimal)long.MinValue).Returns(long.MinValue),
                new TestCaseData(123.5m).Returns(123),
                new TestCaseData(123.49m).Returns(123),
            };

        public static TestCaseData[] DecimalToUInt64Data =
            {
                new TestCaseData(decimal.MaxValue).Returns(0),
                new TestCaseData(decimal.MinValue).Returns(0),
                new TestCaseData((decimal)ulong.MaxValue).Returns(ulong.MaxValue),
                new TestCaseData((decimal)ulong.MinValue).Returns(ulong.MinValue),
                new TestCaseData(123.5m).Returns(123),
                new TestCaseData(123.49m).Returns(123),
            };

        public static TestCaseData[] DecimalToNullableUInt64Data =
            {
                new TestCaseData(decimal.MaxValue).Returns(null),
                new TestCaseData(decimal.MinValue).Returns(null),
                new TestCaseData((decimal)ulong.MaxValue).Returns(ulong.MaxValue),
                new TestCaseData((decimal)ulong.MinValue).Returns(ulong.MinValue),
                new TestCaseData(123.5m).Returns(123),
                new TestCaseData(123.49m).Returns(123),
            };

        public static TestCaseData[] DecimalToSingleData =
            {
                new TestCaseData(decimal.MaxValue).Returns(7.9228162514264338E+28f),
                new TestCaseData(decimal.MinValue).Returns(-7.9228162514264338E+28f),
                new TestCaseData(123.5m).Returns(123.5f),
                new TestCaseData(123.49m).Returns(123.49f),
                new TestCaseData(-123.5m).Returns(-123.5f),
            };

        public static TestCaseData[] DecimalToNullableSingleData =
            {
                new TestCaseData(decimal.MaxValue).Returns(7.9228162514264338E+28f),
                new TestCaseData(decimal.MinValue).Returns(-7.9228162514264338E+28f),
                new TestCaseData(123.5m).Returns(123.5f),
                new TestCaseData(123.49m).Returns(123.49f),
                new TestCaseData(-123.5m).Returns(-123.5f),
            };

        public static TestCaseData[] DecimalToDoubleData =
            {
                new TestCaseData(decimal.MaxValue).Returns(7.9228162514264338E+28d),
                new TestCaseData(decimal.MinValue).Returns(-7.9228162514264338E+28d),
                new TestCaseData(123.5m).Returns(123.5d),
                new TestCaseData(123.49m).Returns(123.49d),
                new TestCaseData(-123.5m).Returns(-123.5d),
            };

        public static TestCaseData[] DecimalToNullableDoubleData =
            {
                new TestCaseData(decimal.MaxValue).Returns(7.9228162514264338E+28d),
                new TestCaseData(decimal.MinValue).Returns(-7.9228162514264338E+28d),
                new TestCaseData(123.5m).Returns(123.5d),
                new TestCaseData(123.49m).Returns(123.49d),
                new TestCaseData(-123.5m).Returns(-123.5d),
            };

        public static TestCaseData[] DecimalToDecimalData =
            {
                new TestCaseData(0m).Returns(0m),
                new TestCaseData(1.000m).Returns(1.000m),
                new TestCaseData(1000m).Returns(1000m),
                new TestCaseData(decimal.MaxValue).Returns(decimal.MaxValue),
                new TestCaseData(decimal.MinValue).Returns(decimal.MinValue),
            };

        public static TestCaseData[] DecimalToNullableDecimalData =
            {
                new TestCaseData(0m).Returns(0m),
                new TestCaseData(1.000m).Returns(1.000m),
                new TestCaseData(1000m).Returns(1000m),
                new TestCaseData(decimal.MaxValue).Returns(decimal.MaxValue),
                new TestCaseData(decimal.MinValue).Returns(decimal.MinValue),
            };

        public static TestCaseData[] DecimalToBooleanData =
            {
                new TestCaseData(decimal.MaxValue).Returns(true),
                new TestCaseData(decimal.MinValue).Returns(true),
                new TestCaseData(0m).Returns(false),
            };

        public static TestCaseData[] DecimalToNullableBooleanData =
            {
                new TestCaseData(decimal.MaxValue).Returns(true),
                new TestCaseData(decimal.MinValue).Returns(true),
                new TestCaseData(0m).Returns(false),
            };

        public static TestCaseData[] DecimalToCharData =
            {
                new TestCaseData(decimal.MaxValue).Returns((char)0),
                new TestCaseData(decimal.MinValue).Returns((char)0),
                new TestCaseData(65m).Returns('A'),
            };

        public static TestCaseData[] DecimalToNullableCharData =
            {
                new TestCaseData(decimal.MaxValue).Returns(null),
                new TestCaseData(decimal.MinValue).Returns(null),
                new TestCaseData(65m).Returns('A'),
            };

        /************************************************************************/
        /*                                                                      
        /*   To Decimal                                                              
        /*                                                                      
        /************************************************************************/

        public static TestCaseData[] StringToDecimalData =
            {
                new TestCaseData(string.Empty).Returns(0m),
                new TestCaseData(null).Returns(0m),
                new TestCaseData("0").Returns(0m),
                new TestCaseData("79228162514264337593543950335").Returns(decimal.MaxValue),
                new TestCaseData("79228162514264337593543950336").Returns(0m),
                new TestCaseData("-79228162514264337593543950335").Returns(decimal.MinValue),
                new TestCaseData("-79228162514264337593543950336").Returns(0m),
                new TestCaseData("abc").Returns(0m),
                new TestCaseData("123a").Returns(0m),
                new TestCaseData("123m").Returns(0m),
            };

        public static TestCaseData[] StringToNullableDecimalData =
            {
                new TestCaseData(string.Empty).Returns(null),
                new TestCaseData(null).Returns(null),
                new TestCaseData("0").Returns(0m),
                new TestCaseData("79228162514264337593543950335").Returns(decimal.MaxValue),
                new TestCaseData("79228162514264337593543950336").Returns(null),
                new TestCaseData("-79228162514264337593543950335").Returns(decimal.MinValue),
                new TestCaseData("-79228162514264337593543950336").Returns(null),
                new TestCaseData("abc").Returns(null),
                new TestCaseData("123a").Returns(null),
                new TestCaseData("123m").Returns(null),
            };

        // Use with decimalseperator = . and thousandseperator = [space]
        public static TestCaseData[] StringToDecimalWithFormatData =
            {
                new TestCaseData(string.Empty).Returns(0m),
                new TestCaseData(null).Returns(0m),
                new TestCaseData("0").Returns(0m),
                new TestCaseData("10").Returns(10m),
                new TestCaseData("10.0").Returns(10.0m),
                new TestCaseData("10,0").Returns(0m),
                new TestCaseData("10.5").Returns(10.5m),
                new TestCaseData("10,5").Returns(0m),
                new TestCaseData("-10").Returns(-10m),
                new TestCaseData("1000").Returns(1000m),
                new TestCaseData("1 000").Returns(1000m),
                new TestCaseData("1 000 000").Returns(1000000m),
                new TestCaseData("1,000").Returns(0m),
                new TestCaseData("1.000").Returns(1.000m),
                new TestCaseData("1 000.00").Returns(1000.00m),
                new TestCaseData("79228162514264337593543950335").Returns(decimal.MaxValue),
                new TestCaseData("79228162514264337593543950336").Returns(0m),
                new TestCaseData("-79228162514264337593543950335").Returns(decimal.MinValue),
                new TestCaseData("-79228162514264337593543950336").Returns(0m),
                new TestCaseData("abc").Returns(0m),
                new TestCaseData("123a").Returns(0m),
                new TestCaseData("123m").Returns(0m),
            };

        // Use with decimalseperator = . and thousandseperator = [space]
        public static TestCaseData[] StringToNullableDecimalWithFormatData =
            {
                new TestCaseData(string.Empty).Returns(null),
                new TestCaseData(null).Returns(null),
                new TestCaseData("0").Returns(0m),
                new TestCaseData("10").Returns(10m),
                new TestCaseData("10.0").Returns(10.0m),
                new TestCaseData("10,0").Returns(null),
                new TestCaseData("10.5").Returns(10.5m),
                new TestCaseData("10,5").Returns(null),
                new TestCaseData("-10").Returns(-10m),
                new TestCaseData("1000").Returns(1000m),
                new TestCaseData("1 000").Returns(1000m),
                new TestCaseData("1 000 000").Returns(1000000m),
                new TestCaseData("1,000").Returns(null),
                new TestCaseData("1.000").Returns(1.000m),
                new TestCaseData("1 000.00").Returns(1000.00m),
                new TestCaseData("79228162514264337593543950335").Returns(decimal.MaxValue),
                new TestCaseData("79228162514264337593543950336").Returns(null),
                new TestCaseData("-79228162514264337593543950335").Returns(decimal.MinValue),
                new TestCaseData("-79228162514264337593543950336").Returns(null),
                new TestCaseData("abc").Returns(null),
                new TestCaseData("123a").Returns(null),
                new TestCaseData("123m").Returns(null),
            };

        public static TestCaseData[] ByteToDecimalData =
            {
                new TestCaseData(byte.MaxValue).Returns((decimal)byte.MaxValue),
                new TestCaseData(byte.MinValue).Returns((decimal)byte.MinValue),
            };

        public static TestCaseData[] SByteToDecimalData =
            {
                new TestCaseData(sbyte.MaxValue).Returns((decimal)sbyte.MaxValue),
                new TestCaseData(sbyte.MinValue).Returns((decimal)sbyte.MinValue),
            };

        public static TestCaseData[] Int16ToDecimalData =
            {
                new TestCaseData(short.MaxValue).Returns((decimal)short.MaxValue),
                new TestCaseData(short.MinValue).Returns((decimal)short.MinValue),
            };

        public static TestCaseData[] UInt16ToDecimalData =
            {
                new TestCaseData(ushort.MaxValue).Returns((decimal)ushort.MaxValue),
                new TestCaseData(ushort.MinValue).Returns((decimal)ushort.MinValue),
            };

        public static TestCaseData[] Int32ToDecimalData =
            {
                new TestCaseData(int.MaxValue).Returns((decimal)int.MaxValue),
                new TestCaseData(int.MinValue).Returns((decimal)int.MinValue),
            };

        public static TestCaseData[] UInt32ToDecimalData =
            {
                new TestCaseData(uint.MaxValue).Returns((decimal)uint.MaxValue),
                new TestCaseData(uint.MinValue).Returns((decimal)uint.MinValue),
            };

        public static TestCaseData[] Int64ToDecimalData =
            {
                new TestCaseData(long.MaxValue).Returns((decimal)long.MaxValue),
                new TestCaseData(long.MinValue).Returns((decimal)long.MinValue),
            };

        public static TestCaseData[] UInt64ToDecimalData =
            {
                new TestCaseData(ulong.MaxValue).Returns((decimal)ulong.MaxValue),
                new TestCaseData(ulong.MinValue).Returns((decimal)ulong.MinValue),
            };

        public static TestCaseData[] DoubleToDecimalData =
            {
                new TestCaseData(double.MaxValue).Returns(0m),
                new TestCaseData(double.MinValue).Returns(0m),
                new TestCaseData((double)decimal.MaxValue).Returns(decimal.MaxValue),
                new TestCaseData((double)decimal.MinValue).Returns(decimal.MinValue),
                new TestCaseData(0.0d).Returns(0.0m),
                new TestCaseData(123.5d).Returns(123.5m),
                new TestCaseData(123.49d).Returns(123.49m),
            };

        public static TestCaseData[] DoubleToNullableDecimalData =
            {
                new TestCaseData(double.MaxValue).Returns(null),
                new TestCaseData(double.MinValue).Returns(null),
                new TestCaseData((double)decimal.MaxValue).Returns(decimal.MaxValue),
                new TestCaseData((double)decimal.MinValue).Returns(decimal.MinValue),
                new TestCaseData(0.0d).Returns(0.0m),
                new TestCaseData(123.5d).Returns(123.5m),
                new TestCaseData(123.49d).Returns(123.49m),
            };

        public static TestCaseData[] SingleToDecimalData =
            {
                new TestCaseData(float.MaxValue).Returns(0m),
                new TestCaseData(float.MinValue).Returns(0m),
                new TestCaseData((float)decimal.MaxValue).Returns(decimal.MaxValue),
                new TestCaseData((float)decimal.MinValue).Returns(decimal.MinValue),
                new TestCaseData(0.0f).Returns(0.0m),
                new TestCaseData(123.5f).Returns(123.5m),
                new TestCaseData(123.49f).Returns(123.49m),
            };

        public static TestCaseData[] SingleToNullableDecimalData =
            {
                new TestCaseData(float.MaxValue).Returns(null),
                new TestCaseData(float.MinValue).Returns(null),
                new TestCaseData((float)decimal.MaxValue).Returns(decimal.MaxValue),
                new TestCaseData((float)decimal.MinValue).Returns(decimal.MinValue),
                new TestCaseData(0.0f).Returns(0.0m),
                new TestCaseData(123.5f).Returns(123.5m),
                new TestCaseData(123.49f).Returns(123.49m),
            };

        public static TestCaseData[] BooleanToDecimalData =
            {
                new TestCaseData(true).Returns(1m),
                new TestCaseData(false).Returns(0m),
            };

        public static TestCaseData[] CharToDecimalData =
            {
                new TestCaseData(char.MaxValue).Returns((decimal)char.MaxValue),
                new TestCaseData(char.MinValue).Returns((decimal)char.MinValue),
                new TestCaseData('A').Returns(65m),
            };

        /************************************************************************/
        /*                                                                      
        /*   Long                                                              
        /*                                                                      
        /************************************************************************/

        public static TestCaseData[] LongToIntData =
            {
                new TestCaseData(0).Returns(0),
                new TestCaseData(int.MaxValue).Returns(int.MaxValue),
                new TestCaseData(int.MinValue).Returns(int.MinValue),
                new TestCaseData(long.MaxValue).Returns(0),
                new TestCaseData(long.MinValue).Returns(0),
            };

        /************************************************************************/
        /*                                                                      
        /*   Int                                                              
        /*                                                                      
        /************************************************************************/
        
        public static TestCaseData[] IntToLongData =
            {
                new TestCaseData(0).Returns(0),
                new TestCaseData(int.MaxValue).Returns(int.MaxValue),
                new TestCaseData(int.MinValue).Returns(int.MinValue),
            };

        public static TestCaseData[] IntToIntData =
            {
                new TestCaseData(0).Returns(0),
                new TestCaseData(int.MaxValue).Returns(int.MaxValue),
                new TestCaseData(int.MinValue).Returns(int.MinValue),
            };

        public static TestCaseData[] IntToStringData =
            {
                new TestCaseData(0).Returns("0"),
                new TestCaseData(int.MaxValue).Returns(int.MaxValue.ToString()),
                new TestCaseData(int.MinValue).Returns(int.MinValue.ToString()),
            };

        /************************************************************************/
        /*                                                                      
        /*   Guid                                                              
        /*                                                                      
        /************************************************************************/

        public static TestCaseData[] GuidToStringData =
            {
                new TestCaseData(Guid.Empty).Returns("00000000-0000-0000-0000-000000000000"),
                new TestCaseData(new Guid("0cb6c00f-fc44-484f-8ddd-823709b74601")).Returns("0cb6c00f-fc44-484f-8ddd-823709b74601"),
            };

        /************************************************************************/
        /*                                                                      
        /*   DateTime                                                              
        /*                                                                      
        /************************************************************************/

        public static TestCaseData[] DateTimeToStringData =
            {
                new TestCaseData(new DateTime(1977, 03, 04)).Returns(new DateTime(1977, 03, 04).ToString()),
                new TestCaseData(new DateTime(1977, 03, 04, 13, 37, 00)).Returns(new DateTime(1977, 03, 04, 13, 37, 0).ToString()),
            };

        public static TestCaseData[] DateTimeToSqlDateTimeData =
            {
                new TestCaseData(DateTime.MinValue).Returns(SqlDateTime.MinValue),
                new TestCaseData(DateTime.MaxValue).Returns(SqlDateTime.MaxValue),
            };

        /************************************************************************/
        /*                                                                      
        /*   SqlDateTime                                                              
        /*                                                                      
        /************************************************************************/

        public static TestCaseData[] SqlDateTimeToDateTimeData =
            {
                new TestCaseData(SqlDateTime.MinValue).Returns((DateTime)SqlDateTime.MinValue),
                new TestCaseData(SqlDateTime.MaxValue).Returns((DateTime)SqlDateTime.MaxValue),
            };




    }
}
