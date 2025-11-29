using System;
using System.Collections.Generic;
using MyApp.BLL;
using MyApp.DAL;

namespace MyApp.Presentation
{
    public static class Menu
    {
        private static EntityService? _service;
        private static EntityContext? _ctx;

        public static void MainMenu()
        {
            Console.WriteLine("=== Лабораторна 3.3 — Трирівнева архітектура (XML серіалізація) ===");

            var dbPath = "vectors_db.xml";
            _ctx = new EntityContext(dbPath);

         
            if (!System.IO.File.Exists(dbPath))
            {
                _ctx.CreateDatabase();
                Console.WriteLine("Створено початкову базу даних (3 вектори).");
            }

            _service = new EntityService(_ctx);

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1. Показати всі вектори");
                Console.WriteLine("2. Додати вектор");
                Console.WriteLine("3. Видалити вектор по індексу");
                Console.WriteLine("4. Знайти за кольором");
                Console.WriteLine("5. Збільшити вектор по індексу");
                Console.WriteLine("6. Зберегти поточну колекцію (перезапис)");
                Console.WriteLine("7. Завантажити та показати (читання з файлу)");
                Console.WriteLine("0. Вихід");
                Console.Write("Вибір: ");
                var key = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    switch (key)
                    {
                        case "1": ShowAll(); break;
                        case "2": AddVector(); break;
                        case "3": RemoveVector(); break;
                        case "4": FindByColor(); break;
                        case "5": IncreaseVector(); break;
                        case "6": SaveCurrent(); break;
                        case "7": LoadAndShow(); break;
                        case "0": return;
                        default: Console.WriteLine("Невідома команда"); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                }
            }
        }

        private static void ShowAll()
        {
            var all = _service!.GetAll();
            if (all.Count == 0) { Console.WriteLine("Колекція порожня."); return; }
            for (int i = 0; i < all.Count; i++) Console.WriteLine($"[{i}] {all[i]}");
        }

        private static void AddVector()
        {
            Console.Write("Колір лінії: ");
            var color = Console.ReadLine() ?? "black";
            Console.Write("EndX: ");
            if (!double.TryParse(Console.ReadLine(), out var x)) x = 0;
            Console.Write("EndY: ");
            if (!double.TryParse(Console.ReadLine(), out var y)) y = 0;
            var v = new MyApp.DAL.Vector(color, x, y);
            _service!.Add(v);
            Console.WriteLine("Додано вектор.");
        }

        private static void RemoveVector()
        {
            Console.Write("Індекс для видалення: ");
            if (!int.TryParse(Console.ReadLine(), out var idx)) { Console.WriteLine("Невірний індекс"); return; }
            _service!.RemoveAt(idx);
            Console.WriteLine("Виконано видалення (якщо індекс був коректний).");
        }

        private static void FindByColor()
        {
            Console.Write("Колір для пошуку: ");
            var color = Console.ReadLine() ?? "";
            var found = _service!.FindByColor(color);
            if (found.Count == 0) Console.WriteLine("Не знайдено.");
            else foreach (var f in found) Console.WriteLine(f);
        }

        private static void IncreaseVector()
        {
            Console.Write("Індекс вектора: ");
            if (!int.TryParse(Console.ReadLine(), out var idx)) { Console.WriteLine("Невірний індекс"); return; }
            Console.Write("На скільки збільшити довжину: ");
            if (!double.TryParse(Console.ReadLine(), out var delta)) { Console.WriteLine("Невірне число"); return; }
            var list = _service!.GetAll();
            if (idx < 0 || idx >= list.Count) { Console.WriteLine("Індекс поза межами."); return; }
            list[idx].Increase(delta);
            _service.SaveAll(list);
            Console.WriteLine("Вектор збільшено та збережено.");
        }

        private static void SaveCurrent()
        {
            var all = _service!.GetAll();
            _service.SaveAll(all);
            Console.WriteLine("Поточна колекція збережена (перезапис).");
        }

        private static void LoadAndShow()
        {
            var all = _service!.GetAll();
            Console.WriteLine($"Зчитано {all.Count} елементів:");
            foreach (var e in all) Console.WriteLine(e);
        }
    }
}

