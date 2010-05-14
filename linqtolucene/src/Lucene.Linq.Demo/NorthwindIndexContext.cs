using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Lucene.Linq.Expressions;
using Lucene.Net.Store;

namespace Lucene.Linq.Demo
{
    
  public partial class NorthwindIndexContext : DatabaseIndexSet<NorthwindDataContext>
  {
    #region Fields

    #endregion

    #region Extension Methods
    partial void OnCreated();
    #endregion

    #region Constructors

    public NorthwindIndexContext(NorthwindDataContext database)
        : base(database) {
        OnCreated();
    }

    public NorthwindIndexContext(DirectoryInfo directory, NorthwindDataContext database)
      : base(directory,database) 
    {
      OnCreated();
    }

    public NorthwindIndexContext(string path, NorthwindDataContext database)
        : base(path, database)
    {
        OnCreated();
    }


    #endregion

    #region Properties

    public IIndex<Customer> Customers
    {
      get 
      {
           return this.Get<Customer>();        
      }
    }

    public IIndex<Order> Orders
    {
      get
      {
            return this.Get<Order>();
      }
    }

    #endregion

    #region Methods

    

    #endregion

  }
}
