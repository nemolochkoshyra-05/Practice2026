using System;
using System.Linq;
using Xunit;
using task05;

namespace task05tests
{
    public class TestClass
    {
        public int PublicField;
        private string _privateField;
        public int Property { get; set; }

        public void Method() { }

        public int MethodWithArgs(string arg1, int arg2) { return 0; }
    }

    [Serializable]
    public class AttributedClass { }

    public class ClassAnalyzerTests
    {
        [Fact]
        public void GetPublicMethods_ReturnsCorrectMethods()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var methods = analyzer.GetPublicMethods();

            Assert.Contains("Method", methods);
        }

        [Fact]
        public void GetAllFields_IncludesPrivateFields()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var fields = analyzer.GetAllFields();

            Assert.Contains("_privateField", fields);
            Assert.Contains("PublicField", fields);
        }

        [Fact]
        public void GetProperties_ReturnsPropertyNames()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var properties = analyzer.GetProperties();

            Assert.Contains("Property", properties);
        }

        [Fact]
        public void HasAttribute_ReturnsTrueIfAttributeExists()
        {
            var analyzer = new ClassAnalyzer(typeof(AttributedClass));
            Assert.True(analyzer.HasAttribute<SerializableAttribute>());
        }

        [Fact]
        public void HasAttribute_ReturnsFalseIfAttributeDoesNotExist()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            Assert.False(analyzer.HasAttribute<SerializableAttribute>());
        }

        [Fact]
        public void GetMethodParams_ReturnsReturnTypeAndParamNames()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var methodParams = analyzer.GetMethodParams("MethodWithArgs").ToList();

            Assert.Contains("Int32", methodParams);
            Assert.Contains("arg1", methodParams);
            Assert.Contains("arg2", methodParams);
        }
    }
}