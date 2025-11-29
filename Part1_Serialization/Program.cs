using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Part1_Serialization
{
    public class Vector
    {
        public string LineColor { get; set; } = "black";
        public double EndX { get; set; }
        public double EndY { get; set; }

        public Vector() { }

        public Vector(string color, double x, double y)
        {
            LineColor = color;
            EndX = x;
            EndY = y;
        }

        public double Length()
        {
            return Math.Sqrt(EndX * EndX + EndY * EndY);
        }

        public void Increase(double delta)
        {
            var len = Length();
            if (len == 0) return;
            var factor = (len + delta) / len;
            EndX *= factor;
            EndY *= factor;
        }

        public override string ToString()
            => $"Color: {LineColor}, End: ({EndX:F2}, {EndY:F2}), Length: {Length():F2}";
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Частина 1: Демонстрація XML-серіалізації ===");

           
            Vector[] arr = new Vector[]
            {
                new Vector("red", 3, 4),
                new Vector("green", -1, 2),
                new Vector("blue", 0, -5)
            };

            string filePath = "vectors_part1.xml";

           
            var xs = new XmlSerializer(typeof(List<Vector>));
            var arrAsList = new List<Vector>(arr);
            using (var fs = File.Create(filePath))
            {
                xs.Serialize(fs, arrAsList);
            }
            Console.WriteLine($"Серіалізовано масив (3 елемента) у файл '{filePath}'.");

           
            List<Vector> loadedList;
            using (var fs = File.OpenRead(filePath))
            {
                loadedList = (List<Vector>)xs.Deserialize(fs);
            }
            Vector[] arr2 = loadedList.ToArray();
            Console.WriteLine("Десеріалізовано у новий масив. Вміст:");
            foreach (var v in arr2) Console.WriteLine(v);

      
            var list = new List<Vector>
            {
                new Vector("cyan", 1, 1),
                new Vector("magenta", 2, -1),
                new Vector("yellow", -3, 0)
            };
            string listPath = "vectors_list_part1.xml";
            using (var fs = File.Create(listPath))
            {
                xs.Serialize(fs, list);
            }
            Console.WriteLine($"Серіалізовано колекцію List у '{listPath}'. Тепер читаємо назад:");
            using (var fs = File.OpenRead(listPath))
            {
                var loaded = (List<Vector>)xs.Deserialize(fs);
                foreach (var v in loaded) Console.WriteLine(v);
            }

            Console.WriteLine("\nПорівняй: масив та колекція серіалізуються однаково (XML зберігає всі поля).");
            Console.WriteLine("=== Кінець Частини 1 ===");
        }
    }
}

