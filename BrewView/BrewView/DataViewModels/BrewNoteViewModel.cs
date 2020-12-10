using System;
using BrewView.Pages.Brew.Details.Abstractions;

namespace BrewView.DataViewModels
{
    public class BrewNoteViewModel : IBrewNoteViewModel
    {
        public BrewNoteViewModel(DateTime createdTime, int rating, string note)
        {
            CreatedTime = createdTime;
            Rating = rating;
            Note = note;
        }

        public DateTime CreatedTime { get; }
        public string Note { get; }

        public int Rating { get; }
    }
}