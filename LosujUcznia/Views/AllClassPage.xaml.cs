using LosujUcznia.Model;

namespace LosujUcznia.Views;

public partial class AllClassPage : ContentPage
{
	public AllClassPage()
	{
		InitializeComponent();
        BindingContext = new AllClasses();
    }

    protected override void OnAppearing()
    {
        ((AllClasses)BindingContext).LoadClasses();
    }

    private async void AddClass_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is AllClasses context && !string.IsNullOrEmpty(context.NewClassName))
        {
            string fileName = $"{context.NewClassName}_class.txt";
            File.WriteAllText(Path.Combine(FileSystem.AppDataDirectory, fileName), context.NewClassName);
            await Shell.Current.GoToAsync($"{nameof(ClassPage)}?{nameof(ClassPage.ClassName)}={context.NewClassName}");
        }
    }

    private async void ClassButtonClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var context = (SingleClass)button.BindingContext;
        await Shell.Current.GoToAsync($"{nameof(ClassPage)}?{nameof(ClassPage.ClassName)}={context.ClassName}");
    }
}