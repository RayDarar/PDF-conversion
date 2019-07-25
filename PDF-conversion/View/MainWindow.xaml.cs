using Microsoft.Win32;
using PDF_conversion.src.interfaces;
using PDF_conversion.src.logic;
using PDF_conversion.src.sources;
using PDF_conversion.src.templates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace PDF_conversion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ITemplate> templates = new List<ITemplate>();
        private List<IConversionSource> conversionSources = new List<IConversionSource>();
        private ObservableCollection<string> files = new ObservableCollection<string>();
        private IRequest request = new Request();

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

            // Loading json settings
            Startup.Load();

            InitializeComponent();
            FilesList.ItemsSource = files;

            // Linking templates
            templates.Add(new Application7());
            templates.Add(new Application8());
            SetTemplates();

            // Linking Conversion sources
            conversionSources.Add(new ZamzarSource());
            SetConversionSources();
        }

        protected override void OnClosed(EventArgs e)
        {
            #region log
            Logger.Log("Time: " + DateTime.Now.ToString());
            Logger.Log("[---------------                                                            ------------------]");
            Logger.Log("[                                                                                             ]");
            Logger.Log("[                                    Application stoped                                       ]");
            Logger.Log("[                                                                                             ]");
            Logger.Log("[---------------------------------------------------------------------------------------------]\n");
            #endregion

            base.OnClosed(e);
        }
        private void SetTemplates()
        {
            foreach (var template in templates)
                Templates.Children.Add(new RadioButton() { Content = template.GetTemplateName(), GroupName = "Template Group", IsChecked = true });
        }
        private void SetConversionSources()
        {
            foreach (var source in conversionSources)
                ConversionSources.Children.Add(new RadioButton() { Content = source.GetName(), GroupName = "Source Group", IsChecked = true });
        }
        private void SaveSettings(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] pairs = Settings.Text.Split(',');

                Dictionary<string, string> temp = new Dictionary<string, string>();

                foreach (string pair in pairs)
                {
                    string data = pair.Trim();
                    string key = data.Split(':')[0], value = data.Split(':')[1];
                    temp[key] = value;
                }

                foreach (var data in temp)
                    DataModule.data[data.Key] = data.Value;

                MessageBox.Show("Настройки сохранены", "Успешно");
                DataModule.Save();
                Settings.Text = "";
            }
            catch (Exception)
            {
                MessageBox.Show("Неверный формат", "Ошибка");
            }

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

        private void Convert(ITemplate template, IConversionSource conversionSource, FileInfo[] files)
        {
            request.SetTemplate(template);
            request.SetConversionSource(conversionSource);
            request.SetFiles(files);
            if (request.Convert())
                MessageBox.Show("Все файлы распологаются на рабочем столе", "Успешно");
            else
                MessageBox.Show("Что-то пошло не так....\nВы можете просмотреть информацию в логах программы", "Ошибка");
        }
        private void ConvertButton(object sender, RoutedEventArgs e)
        {
            // Getting template
            var templateName = Templates.Children.OfType<RadioButton>().First(f => f.IsChecked == true).Content.ToString();
            var template = templates.First(f => f.GetTemplateName() == templateName);

            var sourceName = ConversionSources.Children.OfType<RadioButton>().First(f => f.IsChecked == true).Content.ToString();
            var source = conversionSources.First(f => f.GetName() == sourceName);

            var files = this.files.Select(path => new FileInfo(path)).ToArray();

            Convert(template, source, files);
        }
    }
}
