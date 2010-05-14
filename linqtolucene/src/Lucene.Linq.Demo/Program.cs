using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using Lucene.Linq;
using System.Data.Linq;
using Lucene.Linq.Expressions;
using System.Diagnostics;
using Lucene.Net.Search;
using Lucene.Net.Index;
using Lucene.Linq;
namespace Lucene.Linq.Demo
{
  class Program
  {
    static NorthwindIndexContext index = null;


    static void Main(string[] args)
    {

        var path = @"C:\temp\index";
        try {
            System.IO.Directory.Delete(path, true);
        } catch (Exception ex) {
            Console.WriteLine(ex);
        }
        System.IO.Directory.CreateDirectory(path);
        Console.WriteLine("Northwind Db index:" + path);

        index = new NorthwindIndexContext(path,new NorthwindDataContext());
        
        // add all the customers/orders to the index
        //index.Write(); // uncomment this line to write both Orders and Customers
        index.Write<Customer>();
                

      // *** Uncomment any Demo below to view its output ***
      SimpleDemo();
      //VarialbeDemo();
      //ProjectionDemo();
      //OrDemo();
      //AndDemo();
      //GroupDemo();
      //DefaultFieldsDemo();
      //WildCardDemo();
      //SingleTermPrefixDemo();
      //MultiTermPrefixDemo();
      //FuzzyDemo();
      //ProximityDemo();
      //ExclusiveRangeDemo();
      //InclusiveRangeDemo();
      //RequireTermDemo();
      //BoostTermDemo();
      //NativeSyntaxDemo();
      
      //NestedDemo();
      //JoinDemo();

      System.Console.WriteLine("Press any key to continue...");
      System.Console.ReadLine();
    }

    static void SimpleDemo()
    {
      var query = from c in index.Customers
                  where c.ContactTitle == "Owner"
                  select c;

      Console.WriteLine("Simple Query:");
      ObjectDumper.Write(query);
      Console.WriteLine("Query Output: {0}", query.ToString());
      Console.WriteLine();

      var results = query.ToList();
      Console.WriteLine("SimpleDemo returned {0} results",results.Count);  
    }

    static void VarialbeDemo()
    {
      string name = "Maria";
      var query = from c in index.Customers
                  where c.ContactName == name
                  select c;

      Console.WriteLine("Variable Query:");
      ObjectDumper.Write(query);
      Console.WriteLine("Query Output: {0}", query.ToString());
      Console.WriteLine();
    }

    static void ProjectionDemo()
    {
      var query = from c in index.Customers
                  where c.ContactName == "maria"
                  select new { Name = c.ContactName, Id = c.CustomerID, Company = c.CompanyName };

      Console.WriteLine("Projection Query:");
      ObjectDumper.Write(query);
      Console.WriteLine("Query Output: {0}", query.ToString());
      Console.WriteLine();
    }

    static void OrDemo()
    {
        var query = from c in index.Customers
                  where (c.ContactName == "maria") || (c.CompanyName == "Ernst Handel")
                  select new { Name = c.ContactName, Id = c.CustomerID, Company = c.CompanyName };

      Console.WriteLine("Or Query:");
      ObjectDumper.Write(query);
      Console.WriteLine("Query Output: {0}", query.ToString());
      Console.WriteLine();
    }

    static void AndDemo()
    {
        var query = from c in index.Customers
                  where (c.ContactName == "maria") && (c.CompanyName == "Alfreds Futterkiste")
                  select new { Name = c.ContactName, Id = c.CustomerID, Company = c.CompanyName };

      Console.WriteLine("And Query:");
      ObjectDumper.Write(query);
      Console.WriteLine("Query Output: {0}", query.ToString());
      Console.WriteLine();
    }

    static void GroupDemo()
    {
        var query = from c in index.Customers
                  where (c.ContactName == "maria") || (c.ContactName == "Aria Cruz" && c.CompanyName == "Familia Arquibaldo")
                  select new { Name = c.ContactName, Id = c.CustomerID, Company = c.CompanyName };

      Console.WriteLine("Group Query:");
      ObjectDumper.Write(query);
      Console.WriteLine("Query Output: {0}", query.ToString());
      Console.WriteLine();
    }

    static void DefaultFieldsDemo()
    {
        var query = from c in index.Customers
                  where (c.Match("Folies"))
                  select new { Name = c.ContactName, Id = c.CustomerID, Company = c.CompanyName };

      Console.WriteLine("Default Fields Query:");
      ObjectDumper.Write(query);
      Console.WriteLine("Query Output: {0}", query.ToString());
      Console.WriteLine();
    }

    static void WildCardDemo()
    {
        var query = from c in index.Customers
                  where c.ContactName == "ma?ia"
                  select new { Name = c.ContactName, Id = c.CustomerID, Company = c.CompanyName };

      Console.WriteLine("Wildcard Query:");
      ObjectDumper.Write(query);
      Console.WriteLine("Query Output: {0}", query.ToString());
      Console.WriteLine();
    }

    static void SingleTermPrefixDemo()
    {
        var query = from c in index.Customers
                  where c.ContactName.StartsWith("mar")
                  select new { Name = c.ContactName, Id = c.CustomerID, Company = c.CompanyName };

      Console.WriteLine("Prefix Query:");
      ObjectDumper.Write(query);
      Console.WriteLine("Query Output: {0}", query.ToString());
      Console.WriteLine();
    }

    static void MultiTermPrefixDemo()
    {
        var query = from c in index.Customers
                  where c.ContactName.StartsWith("+Maria And")
                  select new { Name = c.ContactName, Id = c.CustomerID, Company = c.CompanyName };

      Console.WriteLine("Multi-Term Prefix Query:");
      ObjectDumper.Write(query);
      Console.WriteLine("Query Output: {0}", query.ToString());
      Console.WriteLine();
    }

    static void FuzzyDemo()
    {
        var query = from c in index.Customers
                  where c.ContactName.Like("Maria")
                  select new { Name = c.ContactName, Id = c.CustomerID, Company = c.CompanyName };

      Console.WriteLine("Fuzzy Query:");
      ObjectDumper.Write(query);
      Console.WriteLine("Query Output: {0}", query.ToString());
      Console.WriteLine("Query Output: {0}", query.Count());
      Console.WriteLine();
    }

    static void ProximityDemo()
    {
        var query = from c in index.Customers
                  where c.ContactName.Like("Maria An", 1)
                  select new { Name = c.ContactName, Id = c.CustomerID, Company = c.CompanyName };

      Console.WriteLine("Proximity Query:");
      ObjectDumper.Write(query);
      Console.WriteLine("Query Output: {0}", query.ToString());
      Console.WriteLine();
    }

    static void ExclusiveRangeDemo()
    {
        var query = from c in index.Customers
                  where c.ContactName.Between("Annette", "Aria")
                  select new { Name = c.ContactName, Id = c.CustomerID, Company = c.CompanyName };

      Console.WriteLine("Exclusive Range Demo:");
      ObjectDumper.Write(query);
      Console.WriteLine("Query Output: {0}", query.ToString());
      Console.WriteLine();
    }

    static void InclusiveRangeDemo()
    {
        var query = from c in index.Customers
                  where c.ContactName.Include("Annette", "Aria")
                  select new { Name = c.ContactName, Id = c.CustomerID, Company = c.CompanyName };

      Console.WriteLine("Inlcusive Range Demo:");
      ObjectDumper.Write(query);
      Console.WriteLine("Query Output: {0}", query.ToString());
      Console.WriteLine();
    }

    static void RequireTermDemo()
    {
        var query = from c in index.Customers
                  where c.ContactName.Match("maria".Require(), "Anders")
                  select new { Name = c.ContactName, Id = c.CustomerID, Company = c.CompanyName };

      Console.WriteLine("Inlcusive Range Demo:");
      ObjectDumper.Write(query);
      Console.WriteLine("Query Output: {0}", query.ToString());
      Console.WriteLine();
    }

    static void BoostTermDemo()
    {
        var query = from c in index.Customers
                  where c.ContactName.Match("maria".Boost(3), "Art")
                  select new { Name = c.ContactName, Id = c.CustomerID, Company = c.CompanyName };

      Console.WriteLine("Inlcusive Range Demo:");
      ObjectDumper.Write(query);
      Console.WriteLine("Query Output: {0}", query.ToString());
      Console.WriteLine();
    }

    static void NativeSyntaxDemo()
    {
        var query = from c in index.Customers
                  where c.Search("((+la^4 OR en) OR (WeirdCustomName:mar* AND !CompanyName:Victuailles))")
                  select new { Name = c.ContactName, Id = c.CustomerID, Company = c.CompanyName };

      Console.WriteLine("Native Syntax Range Demo:");
      ObjectDumper.Write(query);
      Console.WriteLine("Query Output: {0}", query.ToString());
      Console.WriteLine();
    }

    static void NestedDemo()
    {
        var query = from c in index.Customers
                  where c.ContactName == "maria"
                  select new
                  {
                    Name = c.ContactName,
                    Orders = from o in index.Orders
                             where o.CustomerID == c.CustomerID
                             select o
                  };

      Console.WriteLine("Nested Demo:");

      foreach (var item in query)
      {
        Console.WriteLine(item.Name);
        foreach (var order in item.Orders)
        {
          Console.WriteLine("\tOrder:{0}", order.ShipAddress);
        }
      }

      //ObjectDumper.Write(query, 4);
      Console.WriteLine("Query Output: {0}", query.ToString());
      Console.WriteLine();
    }

    static void JoinDemo()
    {
        var query = from c in index.Customers
                    from o in index.Orders
                  select new
                  {
                    Name = c.ContactName,
                    Address = o.ShipAddress
                  };

      Console.WriteLine("Join Demo:");
      ObjectDumper.Write(query);
      Console.WriteLine("Query Output: {0}", query.ToString());
      Console.WriteLine();
    }
  }
}
