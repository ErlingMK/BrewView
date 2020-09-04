using System;
using System.Collections.Generic;
using System.Text;
using BrewView.Services.Abstracts;

namespace BrewView.Services
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly INavigationService m_navigationService;

        public ExceptionHandler(INavigationService navigationService)
        {
            m_navigationService = navigationService;
        }

        public void Handle(Exception e)
        {
            m_navigationService.ShowError(e);
        }
    }
}
