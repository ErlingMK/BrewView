using System;
using System.ComponentModel;
using System.Windows.Input;
using BrewView.DataViewModels;
using BrewView.Pages.Brew.Details.ViewModels;

namespace BrewView.Pages.Brew.Details.Abstractions
{
    public interface IAddNoteViewModel : INotifyPropertyChanged
    {
        string Note { get; set; }
        DateTime CreatedTime { get; set; }
        int Rating { get; set; }
        ICommand AddCommand { get; }
        ICommand CancelCommand { get; }
        bool Show { get; set; }

        Action<BrewNoteViewModel> OnAddAction { get; set; }
    }
}