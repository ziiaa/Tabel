using System.Collections.ObjectModel;
using System.ComponentModel;
using System;

internal class Customer
{
    //fields  
    int _id, _countryID;
    string _first, _last;
    double _weight;
    bool _active;

    //data generators  
    static Random _rnd = new Random();
    static string[] _firstNames = "Andy|Ben|Charlie|Dan|Ed|Fred|Gil|Herb|Jim|Elena|Stefan|Alaric|Gina".Split('|');
    static string[] _lastNames = "Ambers|Bishop|Cole|Danson|Evers|Frommer|Salvatore|Spencer|Saltzman|Rodriguez".Split('|');
    static string[] _countries = "China|India|United States|Japan|Myanmar".Split('|');

    public Customer ()
        : this(_rnd.Next())
    {
    }
    public Customer ( int id )
    {
        ID = id;
        Active = false;
        FirstName = GetString(_firstNames);
        LastName = GetString(_lastNames);
        CountryID = _rnd.Next() % _countries.Length;
        Weight = 50 + _rnd.NextDouble() * 50;
    }
    //Object model         
    public int ID
    {
        get { return _id; }
        set
        {
            if (value != _id)
            {
                _id = value;
                RaisePropertyChanged("ID");
            }
        }
    }
    public bool Active
    {
        get { return _active; }
        set
        {
            _active = value;
            RaisePropertyChanged("Active");
        }
    }
    public string Name
    {
        get { return string.Format("{0} {1}",FirstName,LastName); }
    }
    public string Country
    {
        get { return _countries[_countryID]; }
    }
    public int CountryID
    {
        get { return _countryID; }
        set
        {
            if (value != _countryID && value > -1 && value < _countries.Length)
            {
                _countryID = value;
                RaisePropertyChanged(null);
            }
        }
    }
    public string FirstName
    {
        get { return _first; }
        set
        {
            if (value != _first)
            {
                _first = value;
                RaisePropertyChanged(null);
            }
        }
    }
    public string LastName
    {
        get { return _last; }
        set
        {
            if (value != _last)
            {
                _last = value;
                RaisePropertyChanged(null);
            }
        }
    }
    public double Weight
    {
        get { return _weight; }
        set
        {
            if (value != _weight)
            {
                _weight = value;
                RaisePropertyChanged("Weight");
            }
        }
    }
    //  utilities  
    static string GetString ( string[] arr )
    {
        return arr[_rnd.Next(arr.Length)];
    }
    static string GetName ()
    {
        return string.Format("{0} {1}",GetString(_firstNames),GetString(_lastNames));
    }
    //  static list provider  
    public static ObservableCollection<Customer> GetCustomerList ( int count )
    {
        var list = new ObservableCollection<Customer>();
        for (int i = 0; i < count; i++)
        {
            list.Add(new Customer(i));
        }
        return list;
    }

    // this interface allows bounds controls to react to changes in the data objects.  
    void RaisePropertyChanged ( string propertyName )
    {
        OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged ( PropertyChangedEventArgs e )
    {
        if (PropertyChanged != null)
            PropertyChanged(this,e);
    }

}

