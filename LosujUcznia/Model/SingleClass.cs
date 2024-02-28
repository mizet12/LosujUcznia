using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosujUcznia.Model
{
    internal class SingleClass
    {
        public string ClassName { get; set; }
        public string NewStudentName { get; set; }
        public string NewStudentSurname { get; set; }
        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();

    }
}
