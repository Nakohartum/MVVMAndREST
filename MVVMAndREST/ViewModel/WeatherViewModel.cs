using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MVVMAndREST.Model;
using MVVMAndREST.ViewModel.Commands;
using MVVMAndREST.ViewModel.Helper;

namespace MVVMAndREST.ViewModel;

public class WeatherViewModel : INotifyPropertyChanged
{
    public ObservableCollection<City> Cities { get; set; }
    private City _selectedCity;
    private CurrentConditions _currentConditions;

    public CurrentConditions CurrentConditions
    {
        get => _currentConditions;
        set
        {
            if (Equals(value, _currentConditions)) return;
            _currentConditions = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(CurrentConditions));
        }
    }

    public City SelectedCity
    {
        get => _selectedCity;
        set
        {
            if (Equals(value, _selectedCity)) return;
            _selectedCity = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(SelectedCity));
            GetCurrentConditions();
        }
    }

    private string _query = String.Empty;

    public string Query
    {
        get => _query;
        set
        {
            if (value == _query) return;
            _query = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged(nameof(Query));
        }
    }

    public SearchCommand SearchCommand { get; set; }

    public WeatherViewModel()
    {
        SearchCommand = new SearchCommand(this);
        Cities = new ObservableCollection<City>();
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public async void SearchForCities()
    {
        Cities.Clear();
        List<City> cities = new();
        cities = await WebHelper.GetAutocomplete(Query);
        cities.ForEach(city => Cities.Add(city));
    }

    public async void GetCurrentConditions()
    {
        if (SelectedCity != null)
        {
            var currentConditions = await WebHelper.GetCurrentConditions(SelectedCity.Key);
            CurrentConditions = currentConditions;
        }
    }
}