using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using TestModelWpfDemo.Model;
namespace TestModelWpfDemo.ViewModel
{
    class StudentsViewModel:DependencyObject
    {


        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(StudentsViewModel), new PropertyMetadata("", FilterText_TextChanged));

        private static void FilterText_TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var current = d as StudentsViewModel;
            if (current!=null)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.FilterStudent;
            }
        }

        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for C.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(StudentsViewModel), new PropertyMetadata(null));

        public StudentsViewModel()
        {
            Items = CollectionViewSource.GetDefaultView(Data.XMLContext.StudentsToList("Students.xml"));
            
            Items.Filter = FilterStudent;
        }

        private bool FilterStudent(object obj)
        {
            bool result = true;
            Student current = obj as Student;
            if (!string.IsNullOrWhiteSpace(FilterText)&& current != null && !current.FirstName.Contains(FilterText) && !current.LastName.Contains(FilterText) )
            {
                result = false;
            }
            return result;
        }
    }
}
