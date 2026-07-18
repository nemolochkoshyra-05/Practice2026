using System;
using System.Reflection;
using System.Linq;

namespace task07
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class DisplayNameAttribute : Attribute
    {
        public string DisplayName { get; }

        public DisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class VersionAttribute : Attribute
    {
        public int Major { get; }
        public int Minor { get; }

        public VersionAttribute(int major, int minor)
        {
            Major = major;
            Minor = minor;
        }
    }

    [DisplayName("Пример класса")]
    [Version(1, 0)]
    public class SampleClass
    {
        [DisplayName("Числовое свойство")]
        public int Number { get; set; }

        [DisplayName("Тестовый метод")]
        public void TestMethod()
        {
            Console.WriteLine("Работает тестовый метод.");
        }
    }

    public static class ReflectionHelper
    {
        public static void PrintTypeInfo(Type type)
        {
            Console.WriteLine($"--- Анализ типа: {type.Name} ---");

            var displayNameAttr = type.GetCustomAttribute<DisplayNameAttribute>();
            if (displayNameAttr != null)
                Console.WriteLine($"Отображаемое имя класса: {displayNameAttr.DisplayName}");

            var versionAttr = type.GetCustomAttribute<VersionAttribute>();
            if (versionAttr != null)
                Console.WriteLine($"Версия класса: {versionAttr.Major}.{versionAttr.Minor}");

            Console.WriteLine("\nСвойства с атрибутом DisplayName:");
            var propertiesWithAttrs = type.GetProperties()
                .Select(p => new { PropertyInfo = p, Attr = p.GetCustomAttribute<DisplayNameAttribute>() })
                .Where(x => x.Attr != null);

            foreach (var prop in propertiesWithAttrs)
                Console.WriteLine($"- {prop.PropertyInfo.Name}: {prop.Attr!.DisplayName}");

            Console.WriteLine("\nМетоды с атрибутом DisplayName:");
            var methodsWithAttrs = type.GetMethods()
                .Select(m => new { MethodInfo = m, Attr = m.GetCustomAttribute<DisplayNameAttribute>() })
                .Where(x => x.Attr != null);

            foreach (var method in methodsWithAttrs)
                Console.WriteLine($"- {method.MethodInfo.Name}: {method.Attr!.DisplayName}");
        }
    }
}