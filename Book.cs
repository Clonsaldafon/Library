using System;

namespace Library
{
    internal class Book
    {
        public uint Id { get; set; }
        public uint AuthorId { get; set; }
        public uint CabinetId { get; set; }
        public uint ShelfNumber { get; set; }
        public string Title { get; set; }
        public int YearOfPublication { get; set; }

        public Book(uint id, uint authorId, uint cabinetId, uint shelfNumber, string title, int yearOfPublication)
        {
            Id = id;
            AuthorId = authorId;
            CabinetId = cabinetId;
            ShelfNumber = shelfNumber;
            Title = title;
            YearOfPublication = yearOfPublication;
        }
    }
}