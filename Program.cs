using System;
using System.Globalization;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cultureInfo = new CultureInfo("ru-RU", false);

            Book[] books = new Book[]
            {
                new Book(1, 1, 1, 1, "Один день Ивана Денисовича", 1959, true),
                new Book(2, 1, 1, 1, "Раковый Корпус", 1963, true),
                new Book(3, 1, 1, 1, "В круге первом", 1955, true),
                new Book(4, 2, 1, 2, "Отцы и дети", 1860, true),
                new Book(5, 2, 1, 2, "Муму", 1852, true),
                new Book(6, 2, 1, 2, "Ася", 1857, true),
                new Book(7, 3, 2, 1, "Камера обскура", 1932, true),
                new Book(8, 3, 2, 1, "Защита Лужина", 1929, true),
                new Book(9, 3, 2, 1, "Король, дама, валет", 1928, true),
                new Book(10, 4, 2, 2, "Собачье сердце", 1925, true),
                new Book(11, 4, 2, 2, "Мастер и Маргарита", 1928, true),
                new Book(12, 4, 2, 2, "Белая гвардия", 1923, true),
                new Book(13, 5, 3, 1, "Капитанская дочка", 1836, true),
                new Book(14, 5, 3, 1, "Евгений Онегин", 1833, true),
                new Book(15, 5, 3, 1, "Пиковая дама", 1833, true),
                new Book(16, 6, 3, 2, "Окаянные дни", 1918, true),
                new Book(17, 6, 3, 2, "Легкое дыхание", 1916, true),
                new Book(18, 6, 3, 2, "Холодная осень", 1944, true),
                new Book(19, 7, 4, 1, "Ревизор", 1835, true),
                new Book(20, 7, 4, 1, "Тарас Бульба", 1835, true),
                new Book(21, 7, 4, 1, "Шинель", 1841, true),
                new Book(22, 8, 4, 2, "Палата №6", 1892, true),
                new Book(23, 8, 4, 2, "Человек в футляре", 1865, true),
                new Book(24, 8, 4, 2, "О любви", 1898, true),
                new Book(25, 9, 5, 1, "Преступление и наказание", 1865, true),
                new Book(26, 9, 5, 1, "Идиот", 1867, true),
                new Book(27, 9, 5, 1, "Братья Карамазовы", 1878, true),
                new Book(28, 10, 5, 2, "Где любовь, там и Бог", 1885, true),
                new Book(29, 10, 5, 2, "Смерть Ивана Ильича", 1882, true),
                new Book(30, 10, 5, 2, "Кавказский пленник", 1872, true)
            };

            Author[] authors = new Author[]
            {
                new Author(1, "Александр Исаевич Солженицын"),
                new Author(2, "Иван Сергеевич Тургенев"),
                new Author(3, "Владимир Владимирович Набоков"),
                new Author(4, "Михаил Афанасьевич Булгаков"),
                new Author(5, "Александр Сергеевич Пушкин"),
                new Author(6, "Иван Алексеевич Бунин"),
                new Author(7, "Николай Васильевич Гоголь"),
                new Author(8, "Антон Павлович Чехов"),
                new Author(9, "Федор Михайлович Достоевский"),
                new Author(10, "Лев Николаевич Толстой")
            };

            Cabinet[] cabinets = new Cabinet[]
            {
                new Cabinet(1, new uint[] { 1, 2 }),
                new Cabinet(2, new uint[] { 1, 2 }),
                new Cabinet(3, new uint[] { 1, 2 }),
                new Cabinet(4, new uint[] { 1, 2 }),
                new Cabinet(5, new uint[] { 1, 2 })
            };

            Reader[] readers = new Reader[]
            {
                new Reader(1, "Безмельницин Антон"),
                new Reader(2, "Бекасов Михаил"),
                new Reader(3, "Бойко Степан"),
                new Reader(4, "Устьянцев Евгений"),
                new Reader(5, "Устьянцев Сергей"),
                new Reader(6, "Святых Иван"),
                new Reader(7, "Киров Иван"),
                new Reader(8, "Калентьев Дмитрий"),
                new Reader(9, "Клещев Сергей"),
                new Reader(10, "Кожурков Георгий"),
                new Reader(11, "Колпаков Артем"),
                new Reader(12, "Парфенов Никита"),
                new Reader(13, "Перерва Владислав"),
                new Reader(14, "Разин Максим"),
                new Reader(15, "Фазылов Владислав"),
                new Reader(16, "Харинов Дмитрий")
            };

            Library[] library = new Library[]
            {
                new Library(1, 7, DateTime.Parse("11.04.2021", cultureInfo), DateTime.Parse("13.05.2021", cultureInfo)),
                new Library(3, 24, DateTime.Parse("07.03.2021", cultureInfo), DateTime.Parse("10.05.2021", cultureInfo)),
                new Library(2, 13, DateTime.Parse("10.12.2021", cultureInfo), DateTime.Parse("03.01.2022", cultureInfo)),
                new Library(2, 2, DateTime.Parse("14.06.2021", cultureInfo), DateTime.Parse("21.10.2021", cultureInfo)),
                new Library(7, 7, DateTime.Parse("10.12.2021", cultureInfo), DateTime.Parse("03.01.2022", cultureInfo)),
                new Library(8, 7, DateTime.Parse("10.05.2021", cultureInfo), DateTime.Parse("10.12.2021", cultureInfo)),
                new Library(9, 1, DateTime.Parse("07.03.2021", cultureInfo), DateTime.Parse("10.05.2021", cultureInfo)),
                new Library(4, 16, DateTime.Parse("07.03.2021", cultureInfo), DateTime.Parse("13.05.2021", cultureInfo)),
                new Library(10, 2, DateTime.Parse("21.10.2021", cultureInfo), DateTime.Parse("03.01.2022", cultureInfo)),
                new Library(13, 5, DateTime.Parse("14.06.2021", cultureInfo), DateTime.Parse("21.10.2021", cultureInfo))
            };
        }
    }
}