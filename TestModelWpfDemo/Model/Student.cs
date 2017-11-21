namespace StudentViewer.Model
{
    public enum Gender
    {
        Male,
        Female
    }
    public class Student
    {

        string _firstName;
        string _lastName;
        public int Id { get; set; }
        public int Age { get; set; }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                if (!string.IsNullOrWhiteSpace(_lastName))
                {
                    NameAndLastName = _firstName + " " + _lastName;
                }
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                if (!string.IsNullOrWhiteSpace(_firstName))
                {
                    NameAndLastName = _firstName+" " +_lastName;
                }
            }
        }

        

        public Gender Gender { get; set; }
        
        //Супер костыль так как я не знаю как вывести два свойства в одну ячейку datagrid.
        public string NameAndLastName { get; set; }

    }
}
