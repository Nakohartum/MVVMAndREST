using System.Windows.Input;

namespace MVVMAndREST.ViewModel.Commands;

public class GetConditionsCommand : ICommand
{
    private WeatherViewModel _weatherViewModel;

    public GetConditionsCommand(WeatherViewModel weatherViewModel)
    {
        _weatherViewModel = weatherViewModel;
    }
    
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        _weatherViewModel.GetCurrentConditions();
    }

    public event EventHandler? CanExecuteChanged;
}