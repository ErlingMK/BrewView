using System;

namespace BrewView.Pages.Brew.Details.Abstractions
{
    public interface IBrewNoteViewModel
    {
        DateTime CreatedTime { get; }
        string Note { get; }
        int Rating { get; }
    }
}