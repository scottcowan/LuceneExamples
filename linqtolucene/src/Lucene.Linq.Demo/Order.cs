using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;
using Lucene.Linq.Mapping;

namespace Lucene.Linq.Demo
{
  [Document]
  [Table(Name = "dbo.Orders")]
  public partial class Order : IIndexable, INotifyPropertyChanging, INotifyPropertyChanged
  {
    #region Fields
    private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
    private int _OrderID;
    private string _CustomerID;
    private System.Nullable<int> _EmployeeID;
    private System.Nullable<System.DateTime> _OrderDate;
    private System.Nullable<System.DateTime> _RequiredDate;
    private System.Nullable<System.DateTime> _ShippedDate;
    private System.Nullable<int> _ShipVia;
    private System.Nullable<decimal> _Freight;
    private string _ShipName;
    private string _ShipAddress;
    private string _ShipCity;
    private string _ShipRegion;
    private string _ShipPostalCode;
    private string _ShipCountry;
    private EntityRef<Customer> _Customer;
    #endregion

    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate();
    partial void OnCreated();
    partial void OnOrderIDChanging(int value);
    partial void OnOrderIDChanged();
    partial void OnCustomerIDChanging(string value);
    partial void OnCustomerIDChanged();
    partial void OnEmployeeIDChanging(System.Nullable<int> value);
    partial void OnEmployeeIDChanged();
    partial void OnOrderDateChanging(System.Nullable<System.DateTime> value);
    partial void OnOrderDateChanged();
    partial void OnRequiredDateChanging(System.Nullable<System.DateTime> value);
    partial void OnRequiredDateChanged();
    partial void OnShippedDateChanging(System.Nullable<System.DateTime> value);
    partial void OnShippedDateChanged();
    partial void OnShipViaChanging(System.Nullable<int> value);
    partial void OnShipViaChanged();
    partial void OnFreightChanging(System.Nullable<decimal> value);
    partial void OnFreightChanged();
    partial void OnShipNameChanging(string value);
    partial void OnShipNameChanged();
    partial void OnShipAddressChanging(string value);
    partial void OnShipAddressChanged();
    partial void OnShipCityChanging(string value);
    partial void OnShipCityChanged();
    partial void OnShipRegionChanging(string value);
    partial void OnShipRegionChanged();
    partial void OnShipPostalCodeChanging(string value);
    partial void OnShipPostalCodeChanged();
    partial void OnShipCountryChanging(string value);
    partial void OnShipCountryChanged();
    #endregion

    #region Constructors

    public Order()
    {
      OnCreated();
      this._Customer = default(EntityRef<Customer>);
    }

    #endregion

    #region Properties

    [Field(FieldIndex.Tokenized, FieldStore.Yes)]
    [Column(Storage = "_OrderID", AutoSync = AutoSync.OnInsert, DbType = "Int NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
    public int OrderID
    {
      get
      {
        return this._OrderID;
      }
      set
      {
        if ((this._OrderID != value))
        {
          this.OnOrderIDChanging(value);
          this.SendPropertyChanging();
          this._OrderID = value;
          this.SendPropertyChanged("OrderID");
          this.OnOrderIDChanged();
        }
      }
    }

    [Field(FieldIndex.Tokenized, FieldStore.Yes)]
    [Column(Storage = "_CustomerID", DbType = "NChar(5)")]
    public string CustomerID
    {
      get
      {
        return this._CustomerID;
      }
      set
      {
        if ((this._CustomerID != value))
        {
          if (this._Customer.HasLoadedOrAssignedValue)
          {
            throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
          }
          this.OnCustomerIDChanging(value);
          this.SendPropertyChanging();
          this._CustomerID = value;
          this.SendPropertyChanged("CustomerID");
          this.OnCustomerIDChanged();
        }
      }
    }

    [Column(Storage = "_EmployeeID", DbType = "Int")]
    public System.Nullable<int> EmployeeID
    {
      get
      {
        return this._EmployeeID;
      }
      set
      {
        if ((this._EmployeeID != value))
        {
          this.OnEmployeeIDChanging(value);
          this.SendPropertyChanging();
          this._EmployeeID = value;
          this.SendPropertyChanged("EmployeeID");
          this.OnEmployeeIDChanged();
        }
      }
    }

    [Column(Storage = "_OrderDate", DbType = "DateTime")]
    public System.Nullable<System.DateTime> OrderDate
    {
      get
      {
        return this._OrderDate;
      }
      set
      {
        if ((this._OrderDate != value))
        {
          this.OnOrderDateChanging(value);
          this.SendPropertyChanging();
          this._OrderDate = value;
          this.SendPropertyChanged("OrderDate");
          this.OnOrderDateChanged();
        }
      }
    }

    [Column(Storage = "_RequiredDate", DbType = "DateTime")]
    public System.Nullable<System.DateTime> RequiredDate
    {
      get
      {
        return this._RequiredDate;
      }
      set
      {
        if ((this._RequiredDate != value))
        {
          this.OnRequiredDateChanging(value);
          this.SendPropertyChanging();
          this._RequiredDate = value;
          this.SendPropertyChanged("RequiredDate");
          this.OnRequiredDateChanged();
        }
      }
    }

    [Column(Storage = "_ShippedDate", DbType = "DateTime")]
    public System.Nullable<System.DateTime> ShippedDate
    {
      get
      {
        return this._ShippedDate;
      }
      set
      {
        if ((this._ShippedDate != value))
        {
          this.OnShippedDateChanging(value);
          this.SendPropertyChanging();
          this._ShippedDate = value;
          this.SendPropertyChanged("ShippedDate");
          this.OnShippedDateChanged();
        }
      }
    }

    [Column(Storage = "_ShipVia", DbType = "Int")]
    public System.Nullable<int> ShipVia
    {
      get
      {
        return this._ShipVia;
      }
      set
      {
        if ((this._ShipVia != value))
        {
          this.OnShipViaChanging(value);
          this.SendPropertyChanging();
          this._ShipVia = value;
          this.SendPropertyChanged("ShipVia");
          this.OnShipViaChanged();
        }
      }
    }

    [Column(Storage = "_Freight", DbType = "Money")]
    public System.Nullable<decimal> Freight
    {
      get
      {
        return this._Freight;
      }
      set
      {
        if ((this._Freight != value))
        {
          this.OnFreightChanging(value);
          this.SendPropertyChanging();
          this._Freight = value;
          this.SendPropertyChanged("Freight");
          this.OnFreightChanged();
        }
      }
    }

    [Field(FieldIndex.Tokenized, FieldStore.Yes, IsDefault=true)]
    [Column(Storage = "_ShipName", DbType = "NVarChar(40)")]
    public string ShipName
    {
      get
      {
        return this._ShipName;
      }
      set
      {
        if ((this._ShipName != value))
        {
          this.OnShipNameChanging(value);
          this.SendPropertyChanging();
          this._ShipName = value;
          this.SendPropertyChanged("ShipName");
          this.OnShipNameChanged();
        }
      }
    }

    [Field(FieldIndex.Tokenized, FieldStore.Yes)]
    [Column(Storage = "_ShipAddress", DbType = "NVarChar(60)")]
    public string ShipAddress
    {
      get
      {
        return this._ShipAddress;
      }
      set
      {
        if ((this._ShipAddress != value))
        {
          this.OnShipAddressChanging(value);
          this.SendPropertyChanging();
          this._ShipAddress = value;
          this.SendPropertyChanged("ShipAddress");
          this.OnShipAddressChanged();
        }
      }
    }

    [Field(FieldIndex.Tokenized, FieldStore.Yes)]
    [Column(Storage = "_ShipCity", DbType = "NVarChar(15)")]
    public string ShipCity
    {
      get
      {
        return this._ShipCity;
      }
      set
      {
        if ((this._ShipCity != value))
        {
          this.OnShipCityChanging(value);
          this.SendPropertyChanging();
          this._ShipCity = value;
          this.SendPropertyChanged("ShipCity");
          this.OnShipCityChanged();
        }
      }
    }

    [Column(Storage = "_ShipRegion", DbType = "NVarChar(15)")]
    public string ShipRegion
    {
      get
      {
        return this._ShipRegion;
      }
      set
      {
        if ((this._ShipRegion != value))
        {
          this.OnShipRegionChanging(value);
          this.SendPropertyChanging();
          this._ShipRegion = value;
          this.SendPropertyChanged("ShipRegion");
          this.OnShipRegionChanged();
        }
      }
    }

    [Column(Storage = "_ShipPostalCode", DbType = "NVarChar(10)")]
    public string ShipPostalCode
    {
      get
      {
        return this._ShipPostalCode;
      }
      set
      {
        if ((this._ShipPostalCode != value))
        {
          this.OnShipPostalCodeChanging(value);
          this.SendPropertyChanging();
          this._ShipPostalCode = value;
          this.SendPropertyChanged("ShipPostalCode");
          this.OnShipPostalCodeChanged();
        }
      }
    }

    [Field(FieldIndex.Tokenized, FieldStore.Yes)]
    [Column(Storage = "_ShipCountry", DbType = "NVarChar(15)")]
    public string ShipCountry
    {
      get
      {
        return this._ShipCountry;
      }
      set
      {
        if ((this._ShipCountry != value))
        {
          this.OnShipCountryChanging(value);
          this.SendPropertyChanging();
          this._ShipCountry = value;
          this.SendPropertyChanged("ShipCountry");
          this.OnShipCountryChanged();
        }
      }
    }

    //[Association(Name = "Customer_Order", Storage = "_Customer", ThisKey = "CustomerID", IsForeignKey = true)]
    //public Customer Customer
    //{
    //  get
    //  {
    //    return this._Customer.Entity;
    //  }
    //  set
    //  {
    //    Customer previousValue = this._Customer.Entity;
    //    if (((previousValue != value)
    //          || (this._Customer.HasLoadedOrAssignedValue == false)))
    //    {
    //      this.SendPropertyChanging();
    //      if ((previousValue != null))
    //      {
    //        this._Customer.Entity = null;
    //        previousValue.Orders.Remove(this);
    //      }
    //      this._Customer.Entity = value;
    //      if ((value != null))
    //      {
    //        value.Orders.Add(this);
    //        this._CustomerID = value.CustomerID;
    //      }
    //      else
    //      {
    //        this._CustomerID = default(string);
    //      }
    //      this.SendPropertyChanged("Customer");
    //    }
    //  }
    //}

    #endregion

    #region Methods

    public event PropertyChangingEventHandler PropertyChanging;

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void SendPropertyChanging()
    {
      if ((this.PropertyChanging != null))
      {
        this.PropertyChanging(this, emptyChangingEventArgs);
      }
    }

    protected virtual void SendPropertyChanged(String propertyName)
    {
      if ((this.PropertyChanged != null))
      {
        this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    #endregion
  }
}
