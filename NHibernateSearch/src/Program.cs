using NHibernate.Search.Event;
using NHibernate.Search.Store;

namespace NHibernateSearch_Demo {
    using System;
    using System.Collections;
    using System.Diagnostics;
    using Entities;
    using Lucene.Net.QueryParsers;
    using Lucene.Net.Analysis;
    using NHibernate;
    using NHibernate.Cfg;
    using NHibernate.Search;
    using NHibernate.Search.Engine;
    using NHibernate.Search.Impl;    

    internal class Program {
        private static Configuration cfg;
        private static ISessionFactory sf;

        private static void Main() {
            Configure();
            FillDb();
            Simple();
            LuceneQueries();
            LuceneCriteria();

            Console.ReadKey(true);
        }

        private static void Configure()
        {
            cfg = new Configuration();
            //cfg.SetProperty("hibernate.search.default.directory_provider", typeof(RAMDirectoryProvider).AssemblyQualifiedName);
            cfg.SetProperty("hibernate.search.default.directory_provider", typeof(FSDirectoryProvider).AssemblyQualifiedName);
            cfg.SetProperty(NHibernate.Search.Environment.AnalyzerClass, typeof(StopAnalyzer).AssemblyQualifiedName);
            cfg.SetListener(NHibernate.Event.ListenerType.PostUpdate, new FullTextIndexEventListener());
            cfg.SetListener(NHibernate.Event.ListenerType.PostInsert, new FullTextIndexEventListener());
            cfg.SetListener(NHibernate.Event.ListenerType.PostDelete, new FullTextIndexEventListener());
            cfg.Configure();
            sf = cfg.BuildSessionFactory();
        }


        private static void LuceneQueries() {
            
            using (IFullTextSession s = Search.CreateFullTextSession(sf.OpenSession())) {
                
                    QueryParser qp = new QueryParser("id", new StopAnalyzer());

                    IQuery NHQuery = s.CreateFullTextQuery(qp.Parse("Summary:series"), typeof(Book));

                    IList result = NHQuery.List();

                    Debug.Assert(result.Count == 2);
            }
        }

        private static void LuceneCriteria() {

            using (IFullTextSession s = Search.CreateFullTextSession(sf.OpenSession())) {
                    
                    IList result = s.CreateCriteria(typeof(Book))
                        .Add(SearchRestrictions.Query("Summary:NHibernate or Name:NHibernate"))
                        .List();
                    
                    Debug.Assert(result.Count == 2);
            }
        }


        private static void Simple() {
            using (ISession s = sf.OpenSession()){
                IQuery q = s.CreateQuery("from Book");
                Debug.Assert(3 == q.List().Count);
            }
        }

        private static void FillDb() {
            using (IFullTextSession s = Search.CreateFullTextSession(sf.OpenSession())) {
                using(ITransaction tx = s.BeginTransaction()){
                    Book b1 = new Book(1,
                        "Eric Evans",
                        "Domain-Driven Design: Tackling Complexity in the Heart of Software",
                        "This book provides a broad framework for making design decisions and a technical vocabulary for discussing domain design. It is a synthesis of widely accepted best practices along with the author's own insights and experiences."
                        );

                    s.Save(b1);

                    Book b2 = new Book(2,
                        "Pierre Kuate",
                        "NHibernate in Action",
                        "In the classic style of Manning's 'In Action' series, NHibernate in Action introduces .NET developers to the NHibernate Object/Relational Mapping tool. As NHibernate is a port of Hibernate from Java to .NET.");
                    s.Save(b2);

                    Book b3 = new Book(3,
                        "John Doe",
                        "Foo book NHibernate",
                        "Foo series book");
                    s.Save(b3);

                    s.Flush();
                    tx.Commit();
                }
            }
        }
    }
}