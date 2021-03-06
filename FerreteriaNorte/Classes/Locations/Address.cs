﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Classes.Locations
{
    class Address : IComparable
    {
        public int id { get; set; }
        public string street { get; set; }
        public string number { get; set; }
        public string zipCode { get; set; }
        public string coordinates { get; set; }
        public int city { get; set; }

        // This properties are used to combobox
        public string Text { get; set; }
        public object Value { get; set; }

        public Address()
        {
        }

        public Address(string street, string number, string zipCode, string coordinates, int city)
        {
            this.street = street;
            this.number = number;
            this.zipCode = zipCode;
            this.coordinates = coordinates;
            this.city = city;
            this.Text = this.ToString();
            this.Value = this.id == 0 ? "" : this.id.ToString();
        }

        public Address(int id, string street, string number, string zipCode, string coordinates, int city)
        {
            this.id = id;
            this.street = street;
            this.number = number;
            this.zipCode = zipCode;
            this.coordinates = coordinates;
            this.city = city;
            this.Text = this.ToString();
            this.Value = this.id==0? "" : this.id.ToString();
        }

        public override string ToString()
        {
            City city = AddressHelper.GetCity(this.city);
            Province province = AddressHelper.GetProvince(city.province);
            Country country = AddressHelper.GetCountry(province.country);

            return street + " " + number + " - " + city.name + " - " + province.name + " - " + country.name;
        }

        public int CompareTo(object obj)
        {
            int nameResult = this.street.CompareTo(((Address)obj).street);

            if (nameResult == 0)
            {
                return this.number.CompareTo(((Address)obj).number);
            }

            return nameResult;
        }
    }

    class City : IComparable, IEquatable<int>
    {
        public int id { get; set; }
        public string name { get; set; }
        public int province { get; set; }

        // This properties are used to combobox
        public string Text { get; set; }
        public object Value { get; set; }

        private void setValues()
        {
            this.Text = this.name;
            this.Value = this.id;
        }

        public City()
        {

        }

        public City(string name, int province)
        {
            this.name = name;
            this.province = province;
            setValues();
        }

        public City(int id, string name, int province)
        {
            this.id = id;
            this.name = name;
            this.province = province;
            setValues();
        }

        public override string ToString()
        {
            return this.name;
        }

        public int CompareTo(object obj)
        {
            return this.name.CompareTo(((City)obj).name);
        }

        public bool Equals(int other)
        {
            return id == other;
        }
    }

    class Province : IComparable, IEquatable<int>
    {
        public int id { get; set; }
        public string name { get; set; }
        public int country { get; set; }

        // This properties are used to combobox
        public string Text { get; set; }
        public object Value { get; set; }

        private void setValues()
        {
            this.Text = this.name;
            this.Value = this.id;
        }

        public Province()
        {

        }

        public Province(string name, int country)
        {
            this.name = name;
            this.country = country;
            setValues();
        }

        public Province(int id, string name, int country)
        {
            this.id = id;
            this.name = name;
            this.country = country;
            setValues();
        }

        public override string ToString()
        {
            return this.name;
        }

        public int CompareTo(object obj)
        {
            return this.name.CompareTo(((Province)obj).name);
        }

        public bool Equals(int other)
        {
            return id == other;
        }
    }

    class Country : IComparable, IEquatable<int>
    {
        public int id { get; set; }
        public string name { get; set; }

        // This properties are used to combobox
        public string Text { get; set; }
        public object Value { get; set; }

        private void setValues()
        {
            this.Text = this.name;
            this.Value = this.id;
        }

        public Country()
        {

        }

        public Country(int id, string name)
        {
            this.id = id;
            this.name = name;
            setValues();
        }

        public Country(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return this.name;
        }

        public int CompareTo(object obj)
        {
            return this.name.CompareTo(((Country)obj).name);
        }

        public bool Equals(int other)
        {
            return id == other;
        }
    }
}
