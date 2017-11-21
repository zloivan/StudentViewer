
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using StudentViewer.Data;
using StudentViewer.Model;
using StudentViewer.ViewModel;
using StudentViewer.Views;
using System.Windows.Forms;

namespace StudentViewer
{
  
    public partial class MainWindow : Window
    {
        
        
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            
        }
        
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new StudentsViewModel();
            EditBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
        }
        


        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateWindow createWin = new CreateWindow();
            createWin.Closed += CreatWindow_OnClosed;
            createWin.ShowDialog();
        }

        private void CreatWindow_OnClosed(object sender, EventArgs e)
        {
            DataContext = new StudentsViewModel();
            EditBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
           
            if (StudentsDataGrid.SelectedItems != null )
            {
                var student = StudentsDataGrid.SelectedItems;
                if (student.Count > 1)
                {
                    MessageBoxResult result =System.Windows.MessageBox.Show("Do you really want to delete more than one record?",
                                       "Multiple Delete",
                                       MessageBoxButton.OKCancel,
                                       MessageBoxImage.Warning,
                                       MessageBoxResult.Cancel);
                    if (result == MessageBoxResult.OK)
                    {
                        List<int> DeleteId = new List<int>();
                        var studentLocal = XMLContext.StudentsToList(StaticData.Path);
                        foreach (var item in student)
                        {
                            studentLocal.Remove(studentLocal.Find(s => s.Id == (item as Student).Id));
                        }
                        XMLContext.StudentsToXML(studentLocal, StaticData.Path);
                    }

                }
                else
                {
                    Student studentModel = new Student();
                    studentModel = (Student)StudentsDataGrid.SelectedItem;
                    
                    var studentLocal = XMLContext.StudentsToList(StaticData.Path);
                    
                    studentLocal.Remove(studentLocal.Find(s => s.Id == studentModel.Id));
                    
                    XMLContext.StudentsToXML(studentLocal, StaticData.Path);
                }
            }
            DataContext = new StudentsViewModel();
        }

        

        private void StudentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeleteBtn.IsEnabled = true;
            if (StudentsDataGrid.SelectedItems.Count>1)
            {
                
                EditBtn.IsEnabled = false;
                            }
            else
            {
                EditBtn.IsEnabled = true;
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            
            Student student = (Student)StudentsDataGrid.SelectedItem;
            EditWindow editWindow = new EditWindow(student);
            editWindow.Closed += OnEditWindow_Closed;
           
            
            editWindow.ShowDialog();

        }

        private void OnEditWindow_Closed(object sender, EventArgs e)
        {
            DataContext = new StudentsViewModel();
            EditBtn.IsEnabled = false;
            DeleteBtn.IsEnabled = false;
        }

        private void BrowseBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "xml files (*.xml)|*.xml";

            if (fileDialog.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                StaticData.Path= fileDialog.FileName;
            }
            DataContext = new StudentsViewModel();
        }
    }
}
