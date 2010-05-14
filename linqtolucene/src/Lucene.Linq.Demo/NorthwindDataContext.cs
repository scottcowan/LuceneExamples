using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Lucene.Linq.Demo
{
  [System.Data.Linq.Mapping.DatabaseAttribute(Name = "Northwind")]
  public partial class NorthwindDataContext : System.Data.Linq.DataContext
  {
    #region Fields
    private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
    #endregion

    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertCustomer(Customer instance);
    partial void UpdateCustomer(Customer instance);
    partial void DeleteCustomer(Customer instance);
    partial void InsertOrder(Order instance);
    partial void UpdateOrder(Order instance);
    partial void DeleteOrder(Order instance);
    #endregion

    #region Constructors

    static NorthwindDataContext()
    {
    }

    public NorthwindDataContext(string connection) :
      base(connection, mappingSource)
    {
      OnCreated();
    }

    public NorthwindDataContext(System.Data.IDbConnection connection) :
      base(connection, mappingSource)
    {
      OnCreated();
    }

    public NorthwindDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
      base(connection, mappingSource)
    {
      OnCreated();
    }

    public NorthwindDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) :
      base(connection, mappingSource)
    {
      OnCreated();
    }

    public NorthwindDataContext() :
      base(global::Lucene.Linq.Demo.Properties.Settings.Default.NorthwindConnectionString, mappingSource)
    {
      OnCreated();
    }

    #endregion

    #region Properties

    public System.Data.Linq.Table<Customer> Customers
    {
      get
      {
        return this.GetTable<Customer>();
      }
    }


    public System.Data.Linq.Table<Order> Orders
    {
      get
      {
        return this.GetTable<Order>();
      }
    }

    #endregion
  }
}
