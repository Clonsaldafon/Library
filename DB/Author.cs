namespace Library.DB
{
    internal class Author
    {
        public uint Id { get; private set; }
        public string FullName { get; private set; }

        public Author(uint id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }
    }
}