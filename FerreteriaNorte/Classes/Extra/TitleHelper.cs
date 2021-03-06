﻿using FerreteriaNorte.Resources.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FerreteriaNorte.Classes.Extra
{
    class TitleHelper
    {
        private static List<Title> titles;
        private static List<Subtitle> subtitles;

        /// <summary>
        /// Parse a Title JSONObject to a Title class object
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Returns NULL if JSONObject don't contains id and name of title</returns>
        public static Title parseTitle(string str)
        {
            JObject jsonTitle = JObject.Parse(str);
            string json_id = jsonTitle[DBKeys.Title.ID].ToString();

            int id = 0;
            string name = jsonTitle[DBKeys.Title.NAME].ToString();

            if (int.TryParse(json_id, out id) && name != null)
            {
                return new Title(id, name);
            }

            return null;
        }

        /// <summary>
        /// Parse a subtitle JSONObject to a subtitle class object
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Returns NULL if JSONObject don't contains id and name of subtitle</returns>
        public static Subtitle parseSubtitle(string str)
        {
            JObject jsonSubtitle = JObject.Parse(str);
            string json_id = jsonSubtitle[DBKeys.Subtitle.ID].ToString();
            string json_title_id = jsonSubtitle[DBKeys.Subtitle.TITLE_ID].ToString();

            int id = 0;
            int title_id = 0;
            string name = jsonSubtitle[DBKeys.Subtitle.NAME].ToString();

            if (int.TryParse(json_id, out id) && int.TryParse(json_title_id, out title_id) && name != null)
            {
                return new Subtitle(id, title_id, name);
            }

            return null;
        }

        /// <summary>
        /// Call to DB API REST to get the Title list
        /// </summary>
        /// <returns>A list of Title objects</returns>
        public static List<Title> GetTitles()
        {
            List<Title> titles = new List<Title>();

            string request = Properties.Resources.base_url + "title/titles"; //Properties.Settings.Default.base_url + "/brand/list";

            string response = Functions.readRequest(request);

            JArray jsonArray = JArray.Parse(response);

            foreach (JObject item in jsonArray)
            {
                string str = item.ToString();
                Title newTitle = parseTitle(str);
                if (newTitle != null)
                {
                    titles.Add(newTitle);
                }
            }

            titles.Sort();

            return titles;
        }

        /// <summary>
        /// Call to DB API REST to get the Subtitle list
        /// </summary>
        /// <param name="title_id">The Title that contains the list of Subtitles</param>
        /// <returns>A List of subtitles</returns>
        public static List<Subtitle> GetSubtitles(int title_id = 0)
        {
            List<Subtitle> subtitles = new List<Subtitle>();

            string request = Properties.Resources.base_url + "title/subtitles";
            if (title_id > 0)
                request += "?title_id=" + title_id;

            string response = Functions.readRequest(request);

            JArray jsonArray = JArray.Parse(response);

            foreach (JObject item in jsonArray)
            {
                string str = item.ToString();
                Subtitle newSubtitle = parseSubtitle(str);
                if (newSubtitle != null)
                {
                    subtitles.Add(newSubtitle);
                }
            }

            subtitles.Sort();

            return subtitles;
        }

        public static Title GetTitle(int _id)
        {
            if (TitleHelper.titles == null)
            {
                TitleHelper.titles = GetTitles();
            }
            Title title = TitleHelper.titles.Find(x => x.Equals(_id));

            return title;
        }

        public static Subtitle GetSubtitle(int _id, int _title_id)
        {
            if (TitleHelper.titles == null)
            {
                TitleHelper.titles = GetTitles();
            }
            if (TitleHelper.subtitles == null)
            {
                TitleHelper.subtitles = GetSubtitles(_title_id);
            }
            Subtitle subtitle = TitleHelper.subtitles.Find(x => x.Equals(_id));

            return subtitle;
        }

        public static void setTitleGrid(DataGrid datagrid)
        {
            if (titles == null)
            {
                titles = GetTitles();
            }

            datagrid.ItemsSource = null;
            datagrid.ItemsSource = titles;

            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            datagrid.IsReadOnly = true;

            foreach (DataGridColumn item in datagrid.Columns)
            {
                switch ((string)item.Header)
                {
                    case "Text":
                        item.Header = "Categoría";
                        item.DisplayIndex = 0;
                        break;
                    default:
                        item.Visibility = System.Windows.Visibility.Hidden;
                        break;
                }
            }

        }

        public static void setSubtitleGrid(DataGrid datagrid, int title_id = 0)
        {
            setSubtitleGrid(datagrid, null, title_id);
        }
        public static void setSubtitleGrid(DataGrid datagrid, List<Subtitle> list)
        {
            setSubtitleGrid(datagrid, list, 0);
        }

        public static void setSubtitleGrid(DataGrid datagrid, List<Subtitle> list = null, int title_id = 0)
        {
            if (list == null)
            {
                if (subtitles == null)
                {
                    subtitles = GetSubtitles();
                }
                list = subtitles;
            }

            datagrid.ItemsSource = null;

            if (title_id > 0)
            {
                List<Subtitle> filteredList = list.Where(x => x.title_id == title_id).ToList();

                datagrid.ItemsSource = filteredList;
            } else
            {
                datagrid.ItemsSource = list;
            }

            datagrid.SelectionMode = DataGridSelectionMode.Extended;
            datagrid.IsReadOnly = true;

            foreach (DataGridColumn item in datagrid.Columns)
            {
                switch ((string)item.Header)
                {
                    case "Text":
                        item.Header = "Subcategoría";
                        item.DisplayIndex = 0;
                        break;
                    default:
                        item.Visibility = System.Windows.Visibility.Hidden;
                        break;
                }
            }

        }

        public static int sendSubtitleToDB(Subtitle subtitle)
        {
            string request = Properties.Resources.base_url + "title/add_subtitle";

            request += "?" + DBKeys.Subtitle.NAME + "=" + subtitle.name + "&" + DBKeys.Subtitle.TITLE_ID + "=" + subtitle.title_id;

            string response = Functions.readRequest(request);

            if (response != null)
            {
                Subtitle newSubtitle = new Subtitle(int.Parse(response), subtitle.name);

                subtitles.Add(newSubtitle);

                return newSubtitle.id;
            }

            return 0;
        }

        public static int sendTitleToDB(Title title)
        {
            string request = Properties.Resources.base_url + "title/add_title";

            request += "?" + DBKeys.Title.NAME + "=" + title.name;

            string response = Functions.readRequest(request);

            if (response != null)
            {
                Title newTitle = new Title(int.Parse(response), title.name);

                titles.Add(newTitle);

                return newTitle.id;
            }

            return 0;
        }
    }
}
