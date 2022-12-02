using System;
using System.Collections.Generic;

namespace Library
{
    internal class Book
    {
        public uint Id { get; private set; }
        public uint AuthorId { get; private set; }
        public string Title { get; private set; }
        public int YearOfPublication { get; private set; }
        public Dictionary<uint, uint> CabinetAndShelfNumbers { get; private set; }
        public bool IsAvailable { get; private set; }

        public Book(uint id, uint authorId, string title, int yearOfPublication, Dictionary<uint, uint> cabinetAndShelfNumbers, bool isAvailable)
        {
            Id = id;
            AuthorId = authorId;
            Title = title;
            YearOfPublication = yearOfPublication;
            CabinetAndShelfNumbers = cabinetAndShelfNumbers;
            IsAvailable = isAvailable;
        }

        public void SetAvailable(bool value)
        {
            IsAvailable = value;
        }
    }
}