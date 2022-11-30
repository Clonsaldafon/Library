using System;

namespace Library
{
    internal class Book
    {
        public uint Id { get; private set; }
        public uint AuthorId { get; private set; }
        public uint CabinetId { get; private set; }
        public uint ShelfNumber { get; private set; }
        public string Title { get; private set; }
        public int YearOfPublication { get; private set; }
        public bool IsAvailable { get; private set; }

        public Book(uint id, uint authorId, uint cabinetId, uint shelfNumber, string title, int yearOfPublication, bool isAvailable)
        {
            Id = id;
            AuthorId = authorId;
            CabinetId = cabinetId;
            ShelfNumber = shelfNumber;
            Title = title;
            YearOfPublication = yearOfPublication;
            IsAvailable = isAvailable;
        }

        public void SetAvailable(bool value)
        {
            IsAvailable = value;
        }
    }
}