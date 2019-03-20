using FerreteriaNorte.Classes.Extra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FerreteriaNorte.Views.Titles
{
    /// <summary>
    /// Lógica de interacción para Titles.xaml
    /// </summary>
    public partial class Titles : Page
    {
        public Titles()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTitles();
            LoadSubtitles();
        }

        private void LoadTitles()
        {
            TitleHelper.setTitleGrid(dataGridTitles);
        }

        private void LoadSubtitles()
        {
            int titleId = 0;
            if (dataGridTitles.SelectedItem != null)
            {
                titleId = ((Title)dataGridTitles.SelectedItem).id;
            }

            TitleHelper.setSubtitleGrid(dataGridSubtitles, titleId);
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            switch (((Button)sender).Name)
            {
                case "btnAddTitle":
                    addTitle();
                    break;
                case "btnAddSubtitle":
                    addSubtitle();
                    break;
                default:
                    break;
            }
        }

        private void addTitle()
        {
            if (tbTitle.Text.Length > 2)
            {
                Title title = new Title(tbTitle.Text);
                
                int titleId = TitleHelper.sendTitleToDB(title);
                if (titleId > 0)
                {
                    MessageBox.Show("Se guardó con el código: " + titleId.ToString());
                    tbTitle.Text = "";
                }
                else
                {
                    MessageBox.Show("Ocurrió un error y no pudo guardarse, intente más tarde");
                }
                LoadTitles();
            }
            else
            {
                MessageBox.Show("Nombre muy corto");
            }
        }

        private void addSubtitle()
        {
            if (tbSubtitle.Text.Length > 2 && dataGridTitles.SelectedItem != null)
            {
                int titleId = ((Title)dataGridTitles.SelectedItem).id;

                Subtitle subtitle = new Subtitle(titleId, tbSubtitle.Text);

                int subtitleId = TitleHelper.sendSubtitleToDB(subtitle);

                if (subtitleId > 0)
                {
                    MessageBox.Show("Se guardó con el código: " + subtitleId.ToString());
                    tbSubtitle.Text = "";
                }
                else
                {
                    MessageBox.Show("Ocurrió un error y no pudo guardarse, intente más tarde");
                }

                LoadSubtitles();
            }
            else
            {
                MessageBox.Show("Nombre muy corto");
            }
        }

        private void dataGridTitles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadSubtitles();
        }
    }
}
