namespace MyFirstWebAPI.Models
{
    public class Book
    {
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required int Pages { get; set; }

        public required int Id { get; set; }


        //public void InfoBook()
        //{
        //    Console.WriteLine($"Id: {Id}, Название: {Title} , Автор: {Author}, Страниц: {Pages}"); НЕ НУЖЕН ДЛЯ API
        //}
    }
}
