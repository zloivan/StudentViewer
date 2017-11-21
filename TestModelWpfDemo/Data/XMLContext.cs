using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using StudentViewer.Model;

namespace StudentViewer.Data
{
    public class XMLContext
    {
        
        public static List<Student> StudentsToList(string path)
        {
            var students = XElement.Load(path).Elements("Student").Select(s => s);

            List<Student> AllStudents = new List<Student>();

            foreach (var item in students)
            {
                AllStudents.Add(new Student
                {
                    Id = int.Parse(item.Attribute("Id").Value),
                    FirstName = item.Element("FirstName").Value,
                    LastName = item.Element("Last").Value,
                    Age = Convert.ToInt32(item.Element("Age").Value),
                    Gender = (Gender)Convert.ToInt32(item.Element("Gender").Value)

                });
            }

            return AllStudents;
        }


        public static void StudentsToXML(List<Student> students, string path)
        {
            
            var xml1 = new XElement("Students", students.OrderBy(s=>s.Id).Select(x => new XElement("Student", new XAttribute("Id", x.Id),
                            new XElement("FirstName", x.FirstName),
                            new XElement("Last", x.LastName),
                            new XElement("Age", x.Age),
                            new XElement("Gender", (int)x.Gender)
                            )));
            FileStream stream = new FileStream(path, FileMode.Create);
            xml1.Save(stream);
            stream.Close();
        }



    }
}
