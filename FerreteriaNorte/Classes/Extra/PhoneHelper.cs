using FerreteriaNorte.Resources.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FerreteriaNorte.Classes.Extra
{
    class PhoneHelper
    {
        private static List<Phone> phones;
        private static List<PhoneType> phoneTypes;

        public static PhoneType parsePhoneType(string str)
        {
            JObject jsonCountry = JObject.Parse(str);

            int id = 0;
            string name = (jsonCountry[DBKeys.PhoneType.NAME]??"").ToString();
            string json_id = (jsonCountry[DBKeys.PhoneType.ID] ?? "").ToString();

            if (int.TryParse(json_id, out id) && name != "")
            {
                return new PhoneType(id, name);
            }

            return null;
        }

        public static Phone parsePhone(string str)
        {
            JObject jsonCountry = JObject.Parse(str);

            int id = 0, type = 0;

            string number = (jsonCountry[DBKeys.Phone.NUMBER] ?? "").ToString();
            string json_id = (jsonCountry[DBKeys.Phone.ID] ?? "").ToString();
            string json_type = (jsonCountry[DBKeys.Phone.TYPE] ?? "").ToString();
            string area = (jsonCountry[DBKeys.Phone.AREA] ?? "").ToString();

            if (int.TryParse(json_id, out id) && number != "" && int.TryParse(json_type, out type))
            {
                return new Phone(id, number, area, type);
            }

            return null;
        }


        public static List<PhoneType> GetPhoneTypes(int _id = 0)
        {
            if (phoneTypes == null)
            {
                List<PhoneType> types = new List<PhoneType>();

                //Dictionary<string, string> parms = new Dictionary<string, string>();
                //if (id > 0)
                //   parms.Add(DBKeys.PhoneType.ID, id.ToString());

                string response = Functions.readRequest( Properties.Resources.base_url + "phone/types");

                JArray jsonArray = JArray.Parse(response);

                foreach (JObject item in jsonArray)
                {
                    string str = item.ToString();
                    PhoneType newPhoneType = parsePhoneType(str);
                    if (newPhoneType != null)
                    {
                        types.Add(newPhoneType);
                    }
                }

                types.Sort();

                phoneTypes = types;
            }
            
            return phoneTypes;
        }

        public static int sendToDB(Phone phone)
        {
            Dictionary<string, string> parms = new Dictionary<string, string>();

            parms.Add(DBKeys.Phone.AREA, phone.area);
            parms.Add(DBKeys.Phone.NUMBER, phone.number);
            parms.Add(DBKeys.Phone.TYPE, phone.type.ToString());

            string response = Functions.createRequest("phone/add", parms);

            if (response != null)
            {
                return int.Parse(response);
            }

            return 0;
        }

        public static void setPhoneGrid(DataGrid datagrid, List<Phone> list)
        {
            datagrid.ItemsSource = null;
            datagrid.ItemsSource = list;

            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            datagrid.IsReadOnly = true;

            foreach (DataGridColumn item in datagrid.Columns)
            {
                switch ((string)item.Header)
                {
                    case "Value":
                        item.Header = "#";
                        item.DisplayIndex = 0;
                        break;
                    case "Text":
                        item.Header = "Teléfono";
                        item.DisplayIndex = 1;
                        break;
                    default:
                        item.Visibility = System.Windows.Visibility.Hidden;
                        break;
                }
            }

        }

        public static PhoneType GetPhoneType(int _id)
        {
            PhoneType pt = new PhoneType();

            if (phoneTypes == null)
            {
                GetPhoneTypes();
            }

            if (_id > 0)
            {    
                pt = phoneTypes.Find(x => x.id.Equals(_id));
            }

            return pt;
        }

    }
}
