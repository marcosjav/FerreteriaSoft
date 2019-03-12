using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FerreteriaNorte.Resources.Utils
{
    class DBKeys
    {
        /*
         * This class group all database keys
         * to be used by the application
         * */

         public static class Brand
        {
            public const string ID = "id_brand";
            public const string NAME = "name_brand";
        }

        public static class Item
        {
            public const string ID = "id_item";
            public const string NAME = "name_item";
            public const string DESCRIPTION = "description_item";
            static class Subtitle
            {
                public const string ID = "subtitle_id";
            }
            static class Picture
            {
                public const string ID = "picture_id";
            }
        }

        public static class Title
        {
            public const string ID = "id_title";
            public const string NAME = "name_title";
        }

        public static class Subtitle
        {
            public const string ID = "id_subtitle";
            public const string NAME = "name_subtitle";
            public const string TITLE_ID = "title_id";
        }

        public static class Company
        {
            public const string ID = "id_company";
            public const string NAME = "name_company";
            public const string LAST_UPDATE = "last_update_company";
            public const string WEB = "web_company";
            public const string CUIT = "cuit_company";
            public const string PHONE_LIST = "phones";
            public const string ADDRESS_LIST = "addresses";
            public const string EMAIL_LIST = "emails";
            static class Item
            {
                public const string CODE = "code";
                public const string COST = "cost_item";
            }
            public static class Discount
            {
                public static string ID = "id_discount";
                public static string VALUE = "value_discount";
                public static string DESCRIPTION = "description_discount";
            }
        }

        public static class Country
        {
            public const string ID = "id_country";
            public const string NAME = "name_country";
        }

        public static class Province
        {
            public const string ID = "id_province";
            public const string NAME = "name_province";
            public const string COUNTRY = "country";
        }

        public static class City
        {
            public const string ID = "id_city";
            public const string NAME = "name_city";
            public const string PROVINCE = "province";
        }

        public static class Address
        {
            public const string ID = "id_address";
            public const string STREET = "street";
            public const string NUMBER = "number";
            public const string ZIPCODE = "zip_code";
            public const string COORDINATES = "coordinates";
            public const string CITY = "city";
        }

        public static class Shop
        {
            public const string ID = "id_shop";
            public const string NAME = "name_shop";
            public const string ADDRESS = "address_id_address";
        }

        public static class Phone
        {
            public const string ID = "id_phone";
            public const string AREA = "area_phone";
            public const string NUMBER = "number_phone";
            public const string TYPE = "type";
        }

        public static class PhoneType
        {
            public const string ID = "id";
            public const string NAME = "name";
        }

        public static class CompanyHasEmail
        {
            public const string ID = "id_email";
            public const string ADDRESS = "address_email";
            public const string IDCOMPANY = "company_id";
        }

        public static class Unit
        {
            public const string ID = "id_unit";
            public const string NAME = "name_unit";
        }

        public static class Stock
        {
            public const string ITEM_ID = "item_id";
            public const string QUANTITY_STOCK = "quantity_stock";
            public const string MIN_STOCK = "min_stock";
            public const string UNIT_ID = "unit_id";
            public const string SHOP_ID = "shop_id_shop";
        }
    }
}
