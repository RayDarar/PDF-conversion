using Microsoft.Win32;
using PDF_conversion.src.interfaces;
using PDF_conversion.src.logic;
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

            InitializeComponent();
            FilesList.ItemsSource = files;

            // Linking templates
            templates.Add(new Application7());
            templates.Add(new Application8());
            SetTemplates();
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

        private void Convert(ITemplate template, IConversionSource conversionSource, FileInfo[] files)
        {
            request.SetTemplate(template);
            request.SetConversionSource(conversionSource);
            request.SetFiles(files);
            request.Convert();
            request.Log();
        }
        private void ConvertButton(object sender, RoutedEventArgs e)
        {
            // Invoke Convert()
            MessageBox.Show("Выберите шаблон", "Ошибка");
        }
    }
}
