using CounterDialog.Models;
using CounterDialog.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CounterDialog.ViewModels
{
    public class CounterItemViewModel : ViewModelBase
    {
        public ObservableCollection<CounterItem> ListItems { get; set; }

        private string? _activeName;
        private int? _activeCount;

        public string? ActiveCounterItem_Name { 
            get { return _activeName; }
            set {
                if (_activeName != value)
                {
                    _activeName = value;
                    this.RaisePropertyChanged(nameof(ActiveCounterItem_Name));
                }
            }
        }
        public int? ActiveCounterItem_Count
        {
            get { return _activeCount; }
            set
            {
                if (_activeCount != value)
                {
                    _activeCount = value;
                    this.RaisePropertyChanged(nameof(ActiveCounterItem_Count));
                }
            }
        }

        public ReactiveCommand<string, Unit> DiedButton_Command { get; }

        private CounterService CounterService = new CounterService();

        public CounterItemViewModel(IEnumerable<CounterItem> items)
        {
            ListItems = new ObservableCollection<CounterItem>(items);

            DiedButton_Command = ReactiveCommand.Create<string>(IncrementDeathCounter);
        }

        public void UpdateActiveCounter(CounterItem item)
        {
            ActiveCounterItem_Name = item.Name;
            ActiveCounterItem_Count = item.Count;
        }
        
        private void IncrementDeathCounter(string bossName)
        {
            CounterService.IncrementCounter(bossName);
            ActiveCounterItem_Count++;
        }
    }
}
