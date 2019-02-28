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
using System.Windows.Shapes;

namespace FerreteriaNorte.Views.General
{
    /// <summary>
    /// Lógica de interacción para CustomMessageForm.xaml
    /// </summary>
    public partial class CustomMessageForm : Window, IDisposable
    {
        string title, description;

        public CustomMessageForm()
        {
            InitializeComponent();
        }

        public CustomMessageForm(string title, string description) : this()
        {
            this.title = title;
            this.description = description;
        }

        public void Dispose()
        {
            this.Close();
        }
    }

    /// <summary>
    /// Your custom message box helper.
    /// </summary>
    public static class CustomMessageBox
    {
        public static void Show(string title, string description)
        {
            // using construct ensures the resources are freed when form is closed
            using (var form = new CustomMessageForm(title, description))
            {
                form.ShowDialog();
            }
        }
    }
}
