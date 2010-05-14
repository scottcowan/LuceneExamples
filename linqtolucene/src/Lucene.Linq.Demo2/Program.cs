using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Linq.Mapping;
using Lucene.Linq;
using System.Diagnostics;
using Lucene.Linq.Expressions;
namespace Lucene.Linq.Demo2 {

    class CountryComparer : IEqualityComparer<Country> {

        #region IEqualityComparer<Country> Members

        public bool Equals(Country x, Country y) {
            if (x.Name.Equals(y.Name, StringComparison.CurrentCultureIgnoreCase)) {
                return true;
            } else {
                return false;
            }
        }

        public int GetHashCode(Country obj) {
            return obj.GetHashCode();
        }

        #endregion
    }

    class Program {



        static IIndex<Country> countryIndex = null;
        static List<Country> countryList = null;



        static void Main(string[] args) {

            try {

                /*
                 * 
                 * index objects with attributes
                 * 
                 * Still need a small adapter for Linq to XML though :<
                 * 
                 */
                countryList = new List<Country>();
                countryIndex = new Index<Country>();

                countryList.AddRange(new CountryXmlReader().Execute());
                countryIndex.Add(countryList);

                Console.WriteLine("Added {0}", countryIndex.Count);
                Console.WriteLine("======================================");

               SkipDemo();
                PagingDemo();

                

            } catch (Exception ex) {
                Console.WriteLine(ex);
            } finally {
                Console.WriteLine("[enter] to exit");
                Console.ReadLine();
            }


        }


        static void PagingDemo() {
            Console.WriteLine("=========================================");
            Console.WriteLine("===========PAGING DEMO ==================");

            var query = (from c in countryIndex
                         where c.TwoLetterCode.StartsWith("A")
                         select c);

            

            // To Paged List demos count, skip and take
            var results = query.ToPagedList(1, 5);

            
            Console.WriteLine("Page {0}/{1}, Page Size : {2}",
                results.PageNumber,
                results.PageCount,
                results.PageSize);
            Console.WriteLine("Count: {0}, Total Count : {1}", results.Count, results.TotalItemCount);
            Console.WriteLine("Page Count : {0}", results.PageCount);

            foreach (var a in results)
                Console.WriteLine(a.Name);

        }




        static void SkipDemo() {

            Console.WriteLine("=========================================");
            Console.WriteLine("===========SKIP DEMO ==================");

            var query = (from c in countryIndex
                         where c.TwoLetterCode.StartsWith("B")
                         select c);

            var queryList = (from c in countryList
                         where c.TwoLetterCode.StartsWith("B")
                         select c);

            Console.WriteLine("Total list count :{0}", queryList.Count());
            Console.WriteLine("Total query count :{0}", query.Count());

            IEnumerable<Country> results = query.Skip(3).Skip(3).Skip(3).Take(5);

            IEnumerable<Country> resultsList = queryList.Skip(3).Skip(3).Skip(3).Take(5);

            Console.WriteLine("Query count after skipping 9 and taking 5 :{0}", results.Count());
            Console.WriteLine("List count after skipping 9 and taking 5 :{0}", resultsList.Count());

            // check each index result is in the list result
            foreach (var c in results) {
                Debug.Assert(resultsList.Contains(c,new CountryComparer()),"!!!ERROR, results don't match");
            }

            Console.WriteLine("======== INDEX ");
            foreach (var a in results)
                Console.WriteLine(a.Name);

            Console.WriteLine("======== LIST ");
            foreach (var a in resultsList)
                Console.WriteLine(a.Name);

        }

    }
}
