using System;
using System.ComponentModel;
using System.Windows.Input;
using BrewView.DataViewModels;
using BrewView.Pages.Brew.Details.Abstractions;
using DIPS.Xamarin.UI.Extensions;
using Xamarin.Forms;

namespace BrewView.Pages.Brew.Details.ViewModels
{
    internal class AddNoteViewModel : IAddNoteViewModel
    {
        private DateTime m_createdTime;
        private string m_note;
        private bool m_show;

        public AddNoteViewModel()
        {
            AddCommand = new Command(Add);
            CancelCommand = new Command(Cancel);
        }

        public string Note
        {
            get => m_note;
            set => PropertyChanged.RaiseWhenSet(ref m_note, value);
        }

        public DateTime CreatedTime
        {
            get => m_createdTime;
            set => PropertyChanged.RaiseWhenSet(ref m_createdTime, value);
        }

        public int Rating { get; set; }
        public ICommand AddCommand { get; }
        public ICommand CancelCommand { get; }

        public bool Show
        {
            get => m_show;
            set
            {
                if (value) CreatedTime = DateTime.Now;
                PropertyChanged.RaiseWhenSet(ref m_show, value);
            }
        }

        public Action<BrewNoteViewModel> OnAddAction { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Cancel()
        {
            Show = false;
        }

        private void Add()
        {
            Show = false;
            OnAddAction.Invoke(new BrewNoteViewModel(CreatedTime, Rating, Note));
        }
    }
}