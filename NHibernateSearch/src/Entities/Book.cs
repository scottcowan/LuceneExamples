namespace NHibernateSearch_Demo.Entities {
    using NHibernate.Search;
    using NHibernate.Search.Attributes;

    [Indexed]
    public class Book {
        private string author;
        private int id;
        private string name;
        private string summary;

        public Book() {
        }

        public Book(int id,string author, string name, string summary) {
            this.id = id;
            this.author = author;
            this.name = name;
            this.summary = summary;
        }

        [DocumentId]
        public virtual int Id {
            get { return id; }
            set { id = value; }
        }

        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual string Author {
            get { return author; }
            set { author = value; }
        }

        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual string Summary {
            get { return summary; }
            set { summary = value; }
        }

        [Field(Index.Tokenized, Store = Store.Yes)]
        public virtual string Name {
            get { return name; }
            set { name = value; }
        }
    }
}