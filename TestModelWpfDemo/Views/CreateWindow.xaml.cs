using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using StudentViewer.Data;
using StudentViewer.Model;

namespace StudentViewer.Views
{
    /// <summary>
    /// Interaction logic for CreateWindow.xaml
    /// </summary>
    public partial class CreateWindow : Window
    {

        

        public CreateWindow()
        {
            InitializeComponent();
            Loaded += OnWindow_Loaded;
            
        }


        private void OnWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GenderComboBox.Items.Add(Model.Gender.Female);
            GenderComboBox.Items.Add(Model.Gender.Male);
            GenderComboBox.SelectedItem = Model.Gender.Male;
        }


        public virtual void Action_Click(object sender, RoutedEventArgs e)
        {

            Model.Student student = new Model.Student();
            student.FirstName = NameTextBox.Text;
            student.LastName = LastNameTextBox.Text;
            if (GenderComboBox.Text == "Male")
                student.Gender = Gender.Male;
            else student.Gender = Gender.Female;

            try
            {
                student.Age = Convert.ToInt32(AgeTextBox.Text);
            }
            catch (Exception)
            {

                NameValidationTextBlock.Text = "Age must contain only digits.";
                return;
            }


            if (ModelValidation.Validate(student).Count() == 0)
            {
                List<Student> localStudent = XMLContext.StudentsToList(StaticData.Path);
                if (localStudent.Count != 0)
                {
                    student.Id = localStudent.OrderBy(s => s.Id).LastOrDefault().Id++;
                }
                else student.Id = 0;
                localStudent.Add(student);
                XMLContext.StudentsToXML(localStudent, StaticData.Path);
                MessageBox.Show($"New student:\nName:{student.FirstName}\nLast Name:{student.LastName}\nAge: {student.Age}\nCreated","Created",MessageBoxButton.OK,MessageBoxImage.Information);

                this.Close();
            }
            else
            {
                StringBuilder warning = new StringBuilder();
                foreach (var item in ModelValidation.Validate(student))
                {
                    warning.AppendLine(item);
                }

                NameValidationTextBlock.Text = warning.ToString();
                return;
            }




        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

    public partial class EditWindow : CreateWindow
    {
        
        Student _student;
        List<Student> _localStudents;

        public EditWindow() : base()
        {
            
            CreateButton.Content = "Edit";
            _localStudents = XMLContext.StudentsToList(StaticData.Path);
        }
        public EditWindow(Student student)
        {
            
            _localStudents = XMLContext.StudentsToList(StaticData.Path);
            CreateButton.Content = "Edit";
            _student = student;


            Title = "Edit";
            Loaded += On_WindowLoaded;
        }

        private void On_WindowLoaded(object sender, RoutedEventArgs e)
        {
            
            NameTextBox.Text = _student.FirstName;
            LastNameTextBox.Text = _student.LastName;
            AgeTextBox.Text = _student.Age.ToString();

            GenderComboBox.SelectedItem = _student.Gender;

        }

        public override void Action_Click(object sender, RoutedEventArgs e)
        {
            
            _student.FirstName = NameTextBox.Text;
            _student.LastName = LastNameTextBox.Text;
            if (GenderComboBox.Text == "Male")
                _student.Gender = Gender.Male;
            else _student.Gender = Gender.Female;

            try
            {
                _student.Age = Convert.ToInt32(AgeTextBox.Text);
            }
            catch (Exception)
            {

                NameValidationTextBlock.Text = "Age must contain only digits.";
                return;
            }


            if (ModelValidation.Validate(_student).Count() == 0)
            {
                _localStudents.Remove(_localStudents.Find(s => s.Id == _student.Id));




                _localStudents.Add(_student);
                XMLContext.StudentsToXML(_localStudents, StaticData.Path);
                MessageBox.Show($"Student databse updated:\nName:{_student.FirstName}\nLast Name:{_student.LastName}\nAge: {_student.Age}", "Edinted", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }
            else
            {
                StringBuilder warning = new StringBuilder();
                foreach (var item in ModelValidation.Validate(_student))
                {
                    warning.AppendLine(item);
                }

                NameValidationTextBlock.Text = warning.ToString();
                return;
            }




        }
    }

}