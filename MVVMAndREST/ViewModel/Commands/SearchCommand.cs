using System.Windows.Input;

namespace MVVMAndREST.ViewModel.Commands;

public class SearchCommand : ICommand
{
    public SearchCommand(WeatherViewModel weatherViewModel)
    {
        _weatherViewModel = weatherViewModel;
    }

    private WeatherViewModel _weatherViewModel;

    public bool CanExecute(object? parameter)
    {
        return _weatherViewModel.Query.Length > 0;
    }

    public void Execute(object? parameter)
    {
        _weatherViewModel.SearchForCities();
    }

    public event EventHandler? CanExecuteChanged;
}