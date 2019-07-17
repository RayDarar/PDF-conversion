using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using PDF_conversion.src.interfaces;
using PDF_conversion.src.logic;
using PDF_conversion.src.templates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace PDF_conversion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<DataTemplateBase> templates = new List<DataTemplateBase>();
        private ObservableCollection<string> files = new ObservableCollection<string>();

        public MainWindow()
        {
            #region log
            Logger.Log("[---------------------------------------------------------------------------------------------]");
            Logger.Log("[                                                                                             ]");
            Logger.Log("[                                    Application started                                      ]");
            Logger.Log("[                                                                                             ]");
            Logger.Log("[---------------                                                            ------------------]");
            Logger.Log("Time: " + DateTime.Now.ToString());
            #endregion

            InitializeComponent();
            FilesList.ItemsSource = files;

            // Linking templates
            templates.Add(new Application8Template());
            templates.Add(new Application7Template());
            SetTemplates();
        }

        protected override void OnClosed(EventArgs e)
        {
            Logger.Log("Time: " + DateTime.Now.ToString());
            Logger.Log("[---------------                                                            ------------------]");
            Logger.Log("[                                                                                             ]");
            Logger.Log("[                                    Application stoped                                       ]");
            Logger.Log("[                                                                                             ]");
            Logger.Log("[---------------------------------------------------------------------------------------------]\n");

            base.OnClosed(e);
        }

        private void SetTemplates()
        {
            foreach (var template in templates)
                Templates.Children.Add(new RadioButton() { Content = template.GetTemplateName(), GroupName = "Template Group" });
        }

        private void AddFileButton(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Выберите файл",
                Filter = "PDF файл (*.pdf) | *.pdf"
            };
            if (dialog.ShowDialog() == true)
                if (!files.Contains(dialog.FileName))
                    files.Add(dialog.FileName);
        }

        private void RemoveFileButton(object sender, RoutedEventArgs e)
        {
            if (FilesList.SelectedItem != null)
                files.Remove(FilesList.SelectedItem as string);
            else
                MessageBox.Show("Выберите файл", "Ошибка");
        }

        private void Convert(string templateName)
        {
            DataTemplateBase parser = null;
            foreach (var template in templates)
                if (template.GetTemplateName() == templateName)
                {
                    parser = template;
                    break;
                }
            DataSource source = new DataSource(files.ToArray());

            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {

                GenerateModel.Generate(dialog.FileName, parser.Parse(source));
            }
            else
                MessageBox.Show("Отмена операции", "Информация");
        }

        private void ConvertButton(object sender, RoutedEventArgs e)
        {
            foreach (var item in Templates.Children)
                if (item is RadioButton && (item as RadioButton).IsChecked == true)
                {
                    Convert((item as RadioButton).Content + "");
                    return;
                }

            MessageBox.Show("Выберите шаблон", "Ошибка");
        }
    }
}
