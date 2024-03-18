using CounterDialog.Models;
using CounterDialog.Services;
using CounterDialog.Views;
using ReactiveUI;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;

namespace CounterDialog.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public CounterItemViewModel CounterItemList { get; set; }

        public MainWindowViewModel() 
        {
            var counterService = new CounterService();

            CounterItemList = new CounterItemViewModel(counterService.GetItems());
        }

        public void DiedButtonPressed()
        {
            var counterService = new CounterService();

        }

        

    }

}
