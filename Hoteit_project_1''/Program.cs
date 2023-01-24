using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Money : IEquatable<Money>, IComparable<Money>
{
    private bool _sign;
    private int _integerPart;
    private int _fractionalPart;
    private string _currency;

    private static readonly string[] _defaultCurrencies = { "USD", "EUR", "RUB" };
    private static readonly Random _rand = new Random();


    //Access methods
    public bool Sign { get { return _sign; } }
    public int IntegerPart { get { return _integerPart; } }
    public int FractionalPart { get { return _fractionalPart; } }
    public string Currency { get { return _currency; } }

    //Method for displaying whole number in string format
    public string Display()
    {
        return (_sign ? "+" : "-") + _integerPart + "." + _fractionalPart + " " + _currency;
    }

    //Methods for setting new values in class fields
    public void SetSign(bool sign) { _sign = sign; }
    public void SetIntegerPart(int integerPart) { _integerPart = integerPart; }
    public void SetFractionalPart(int fractionalPart) { _fractionalPart = fractionalPart; }
    public void SetCurrency(string currency) { _currency = currency; }
    public void Setwholenumber(string value)
    {
        var parts = value.Split(' ');
        _sign = parts[0] == "+";
        _integerPart = int.Parse(parts[1]);
        _fractionalPart = int.Parse(parts[2]);
        _currency = parts[3];
    }

    //Default constructor
    public Money()
    {
        var rand = new Random();
        _sign = rand.Next(0, 2) == 0;
        _integerPart = rand.Next();
        _fractionalPart = rand.Next(0, 100);
        _currency = _defaultCurrencies[_rand.Next(0, _defaultCurrencies.Length)];
    }

    //Constructor with 4 parameters
    public Money(string currency, bool sign, int integerPart, int fractionalPart)
    {
        _currency = currency;
        _sign = sign;
        _integerPart = integerPart;
        _fractionalPart = fractionalPart;
    }

    //Copy constructor
    public Money(Money other)
    {
        _currency = other._currency;
        _sign = other._sign;
        _integerPart = other._integerPart;
        _fractionalPart = other._fractionalPart;
    }

    //Constructor with string parameter
    public Money(string value)
    {
        Setwholenumber(value);
    }

    //Method for adding value to current money amount
    public void Add(bool sign, int integerPart, int fractionalPart)
    {


        if (_sign == sign)
        {
            _integerPart += integerPart;
            _fractionalPart += fractionalPart;
            if (_fractionalPart >= 100)
            {
                _fractionalPart -= 100;
                _integerPart++;
            }
        }
        if (_sign == !sign)
        {
            _integerPart -= integerPart;
            _fractionalPart -= fractionalPart;

            if (_fractionalPart < 0)
            {
                _fractionalPart += 100;
                _integerPart--;

            }
            if (_integerPart < 0)
            {
                _sign = !_sign;
                _integerPart *= -1;
            }
        }


    }

    public void Add(Money money)
    {
        if (this._currency != money._currency)
        {

            Console.WriteLine("currency mismatch");
        }

        else
        {

            Add(money._sign, money._integerPart, money._fractionalPart);


        }


    }

    //Method for subtraction value from current money amount
    public void Subtract(bool sign, int integerPart, int fractionalPart)

    {
        Add(!sign, integerPart, fractionalPart);
    }

    public void Subtract(Money money)
    {
        if (this._currency != money._currency)

            Console.WriteLine("currency mismatch");
        else

            Subtract(money._sign, money._integerPart, money._fractionalPart);
    }

    //Method for determining the equality of two money objects
    public bool Equals(Money other)
    {
        return _sign == other._sign && _integerPart == other._integerPart && _fractionalPart == other._fractionalPart && _currency == other._currency;
    }

    //Method for comparing two money objects
    public int CompareTo(Money money)
    {
        if (this._currency != money._currency)
        {
            throw new Exception("Currency mismatch");
        }
        if (this._sign != money._sign)
        {
            return this._sign ? 1 : -1;
        }
        if (this._integerPart != money._integerPart)
        {
            return this._integerPart - money._integerPart;
        }
        return this._fractionalPart - money._fractionalPart;
    }

    //Method for calculating sum of two money objects
    public static Money operator +(Money a, Money b)
    {
        var result = new Money(a);
        result.Add(b);
        return result;
    }

    //Method for calculating difference of two money objects
    public static Money operator -(Money a, Money b)
    {
        var result = new Money(a);
        result.Subtract(b);
        return result;
    }

    //Method for converting current money amount object to another currency
    public void ConvertToCurrency(string currency)
    {
        _currency = currency;
    }


    static void Main(string[] args)     //testing all methods in main 
    {

        var money1 = new Money("EUR", false, 5, 50);
        var money2 = new Money("EUR", true, 10, 25); //constructor with 4 parameters 
        var money3 = new Money("EUR", false, 5, 50);

        var money = new Money(); //default constructor random money amount 
        Console.WriteLine("default constructor, random money amount : " + money.Display());

        Console.WriteLine("money 1 : " + money1.Display());
        Console.WriteLine("money 2 : " + money2.Display());
        Console.WriteLine("money 3 : " + money3.Display());

        money1.Add(money2); //MAdd2
        Console.WriteLine("money1 + money2 = " + money1.Display());

        var money4 = new Money("+ 5 33 EUR"); //constructor with string parameter
        Console.WriteLine("money4 : " + money4.Display());

        money4.Subtract(money3); //MSub2
        Console.WriteLine("money4 - money3 : " + money4.Display());

        var money22 = new Money("+ 5 70 EUR");
        Console.WriteLine("money22 : " + money22.Display());


        Console.WriteLine("money1 equal to money2 ? Answer: " + money1.Equals(money2));
        Console.WriteLine("money2 compared to money22 : " + money2.CompareTo(money22));

        var money5 = money2 + money3; //MSUM
        Console.WriteLine("money5 : money2 + money3 : " + money5.Display());

        var money6 = money2 - money3; //MDiff
        Console.WriteLine("money6 : money2 - money3 : " + money6.Display());

        money5.ConvertToCurrency("RUB");
        Console.WriteLine("money5 converted to RUB : " + money5.Display());

        var money7 = new Money("+ 10 10 USD");
        Console.WriteLine("money7 : " + money7.Display());


        var money8 = new Money(money7); //copy constructor
        Console.WriteLine("money8 copy of money7 : " + money8.Display());

        var money10 = new Money("+ 5 50 USD");
        Console.WriteLine("money10 : " + money10.Display());

        money10.Add(true, 2, 50); //MAdd1
        Console.WriteLine("money10 + +2.50 = " + money10.Display());


        var money11 = new Money("+ 1 50 USD");
        Console.WriteLine("money11 : " + money11.Display());

        money11.Subtract(true, 2, 50); //MSub1
        Console.WriteLine("money11 - +2.50 = " + money11.Display());

        var money12 = new Money("+ 7 50 USD");
        Console.WriteLine("money12 : " + money12.Display());

        var money13 = new Money("+ 8 50 EUR");
        Console.WriteLine("money13 : " + money13.Display());

        //CURRENCY MISMATCH
        money12.Add(money13);  //in case of  adding/subtracting different currencies MAdd2 MSub2







    }
}
