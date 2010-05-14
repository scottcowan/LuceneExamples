using System.Diagnostics;
using System.Reflection;
using log4net.Config;
using Microsoft.Practices.ServiceLocation;
using NHibernate.SolrNet.Impl;
using NHibernate.Util;
using SolrNet;
using SolrNet.Impl;
using SolrNet.Impl.DocumentPropertyVisitors;
using SolrNet.Mapping;

namespace NHibernate.SolrNet.Demo
{
    public class Runner{

        public void Go()
        {
            Setup();
            Insert();

        }

        public void Insert()
        {
            using (var session = sessionFactory.OpenSession()) {
                session.Save(new Entity {
                                            Id = "abcd",
                                            Description = "Testing NH-Solr integration",
                                            Tags = new[] {"cat1", "aoe"},
                                        });
                session.Flush();
            }
            using (var session = cfgHelper.OpenSession(sessionFactory)) {
                var entities = session.CreateSolrQuery("solr").List<Entity>();
                Debug.Assert(1 == entities.Count);
                Debug.Assert(2 == entities[0].Tags.Count);
            }
        }

        public void DoesntLeakMem() {
            using (var session = cfgHelper.OpenSession(sessionFactory)) {
                session.FlushMode = FlushMode.Never;
                session.Save(new Entity {
                                            Id = "abcd",
                                            Description = "Testing NH-Solr integration",
                                            Tags = new[] { "cat1", "aoe" },
                                        });
            }
            var listener = cfg.EventListeners.PostInsertEventListeners[0];
            var addField = typeof (SolrNetListener<Entity>).GetField("entitiesToAdd", BindingFlags.NonPublic | BindingFlags.Instance);
            var addDict = (WeakHashtable)addField.GetValue(listener);
            Debug.Assert(0 == addDict.Count);
        }

        private Configuration SetupNHibernate() {
            var cfg = new Configuration();
            cfg.Configure();
            return cfg;
        }

        private void SetupSolr() {
            Startup.InitContainer();

            Startup.Container.Remove<IReadOnlyMappingManager>();
            var mapper = new MappingManager();
            mapper.Add(typeof (Entity).GetProperty("Description"), "name");
            mapper.Add(typeof (Entity).GetProperty("Id"), "id");
            mapper.Add(typeof (Entity).GetProperty("Tags"), "cat");
            Startup.Container.Register<IReadOnlyMappingManager>(c => mapper);

            Startup.Container.Remove<ISolrDocumentPropertyVisitor>();
            var propertyVisitor = new DefaultDocumentVisitor(mapper, Startup.Container.GetInstance<ISolrFieldParser>());
            Startup.Container.Register<ISolrDocumentPropertyVisitor>(c => propertyVisitor);

            Startup.Init<Entity>("http://localhost:8983/solr");
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Entity>>();
            solr.Delete(SolrQuery.All).Commit();
        }

        public void Setup() {
            BasicConfigurator.Configure();
            SetupSolr();

            cfg = SetupNHibernate();

            cfgHelper = new CfgHelper();
            cfgHelper.Configure(cfg, true);
            sessionFactory = cfg.BuildSessionFactory();
        }

        private Configuration cfg;
        private CfgHelper cfgHelper;
        private ISessionFactory sessionFactory;

    }
}