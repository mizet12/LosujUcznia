using LosujUcznia.Model;
using System.Collections.ObjectModel;

namespace LosujUcznia.Views;

[QueryProperty(nameof(ClassName), nameof(ClassName))]
public partial class ClassPage : ContentPage
{
	public ClassPage()
	{
		InitializeComponent();
        SingleClass classObject = new SingleClass();
        BindingContext = classObject;
	}
    private List<Student> lastThreeSelectedStudents = new List<Student>();

    public int LastNumber;

    private string className;
    public string ClassName
    {
        get
        {
            return className;
        }
        set
        {
            if (value != null)
            {
                className = value;
                LoadClass(value);
            }
        }
    }

    private void LoadClass(string className)
    {
        string studentsFile = File.ReadAllText(Path.Combine(FileSystem.AppDataDirectory, $"{className}_class.txt"));
        string[] studentsPerLine = studentsFile.Split("\r\n");
        studentsPerLine = studentsPerLine.Skip(1).ToArray();

        for (int i = 0; i < studentsPerLine.Length; i++)
        {
            string[] data = studentsPerLine[i].Split(';');

            Student student = new Student()
            {
                Number = i + 1,
                Name = data[0],
                Surname = data[1]
            };

            ((SingleClass)BindingContext).Students.Add(student);
        }

        LastNumber = studentsPerLine.Length;
    }

    private void AddStudent_Clicked(object sender, EventArgs e)
    {
        var context = ((SingleClass) BindingContext);
        if (!string.IsNullOrEmpty(context.NewStudentName) && !string.IsNullOrEmpty(context.NewStudentSurname))
        {
            string studentLine = $"\r\n{context.NewStudentName};{context.NewStudentSurname}";

            File.AppendAllText(Path.Combine(FileSystem.AppDataDirectory, $"{this.ClassName}_class.txt"), studentLine);

            Student student = new Student()
            {
                Number = LastNumber + 1,
                Name = context.NewStudentName,
                Surname = context.NewStudentSurname,
            };
            LastNumber = student.Number;
            ((SingleClass)BindingContext).Students.Add(student);
        }
    }

    private void DrawStudent_Clicked(object sender, EventArgs e)
    {
        
        var presentStudents = ((SingleClass)BindingContext).Students.Where(student => student.IsAbsent).ToList();

        if (presentStudents.Count > 0)
        {
            Random random = new Random();

            var availableStudents = presentStudents.Except(lastThreeSelectedStudents).ToList();

            if (availableStudents.Count == 0)
            {
                Shell.Current.DisplayAlert("Uwaga", "Wszyscy obecni uczniowie zostali ju¿ odpytani.", "ok");
                return;
            }

            int randomIndex = random.Next(0, availableStudents.Count);

            string randomPerson = $"{availableStudents[randomIndex].Name} {availableStudents[randomIndex].Surname}";

            Shell.Current.DisplayAlert("Wylosowany uczen", randomPerson, "ok");

            lastThreeSelectedStudents.Insert(0, availableStudents[randomIndex]);

            if (lastThreeSelectedStudents.Count > 3)
            {
                lastThreeSelectedStudents.RemoveAt(3);
            }
        }
        else
        {
            Shell.Current.DisplayAlert("Uwaga", "Brak obecnych uczniów.", "ok");
        }
    }

    private async void DeleteClass_Clicked(object sender, EventArgs e)
    {
        File.Delete(FileSystem.AppDataDirectory + $"/{this.className}_class.txt");
        await Shell.Current.GoToAsync("..");
    }

    private void DeleteStudent_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var uczen = (Student)button.BindingContext;

        string personsFromFile = File.ReadAllText(Path.Combine(FileSystem.AppDataDirectory, $"{this.className}_class.txt"));
        string[] separatedPersons = personsFromFile.Split("\r\n");
        separatedPersons = separatedPersons.Skip(1).ToArray();


        string newWholeClass = $"{this.ClassName}";
        foreach (string PersonLine in separatedPersons)
        {
            string[] things = PersonLine.Split(';');

            if (things[0] != uczen.Name || things[1] != uczen.Surname)
            {
                newWholeClass += $"\r\n{things[0]};{things[1]}";
            }
            else continue;
        }

        File.Delete(Path.Combine(FileSystem.AppDataDirectory + $"{this.ClassName}_class.txt"));
        File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, $"{this.ClassName}_class.txt"), newWholeClass);
        ((SingleClass)BindingContext).Students.Clear();
        LoadClass(this.className);
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var checkBox = (CheckBox)sender;
        var student = (Student)checkBox.BindingContext;

        student.IsAbsent = e.Value;
    }
}


