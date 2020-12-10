using Microcharts;

namespace BrewView.Pages.Brew.Details.ViewModels
{
    public class ChartWithLabel
    {
        public float Value { get; set; }
        public Chart Chart { get; set; }
        public string Description { get; set; }
    }
}