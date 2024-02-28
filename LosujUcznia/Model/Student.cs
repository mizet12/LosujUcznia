using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosujUcznia.Model
{
    internal class Student : INotifyPropertyChanged
    {
        private string _name;
        private string _surname;
        public int Number { get; set; }

        private bool isAbsent;
        public bool IsAbsent
        {
            get { return isAbsent; }
            set
            {
                if (value != isAbsent)
                {
                    isAbsent = value;
                    OnPropertyChanged(nameof(IsAbsent));
                }
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                if(_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public string Surname
        {
            get => _surname;
            set
            {
                if(_surname != value)
                {
                    _surname = value;
                    OnPropertyChanged(nameof(Surname));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
