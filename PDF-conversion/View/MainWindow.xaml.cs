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
            request.Log();
        }
        private void ConvertButton(object sender, RoutedEventArgs e)
        {
            // Invoke Convert()

            MessageBox.Show("Выберите шаблон", "Ошибка");
        }
    }
}
