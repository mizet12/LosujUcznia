using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosujUcznia.Model
{
    internal class AllClasses
    {
        public ObservableCollection<SingleClass> Classes { get; set; } = new ObservableCollection<SingleClass>();

        public string NewClassName { get; set; }

        public AllClasses() => LoadClasses();
        public void LoadClasses()
        {
            Classes.Clear();
            string appDataPath = FileSystem.AppDataDirectory;

            IEnumerable<SingleClass> allClasses = Directory
                .EnumerateFiles(appDataPath, $"*_class.txt")
                .Select(className => new SingleClass()
                {
                    ClassName = File.ReadLines(className).First(),
                });

            foreach (SingleClass singleClass in allClasses)
            {
                Classes.Add(singleClass);
            }
        }
    }
}
