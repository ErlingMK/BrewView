using System;

namespace BrewView.Services.Abstracts
{
    public interface IExceptionHandler
    {
        void Handle(Exception e);
    }
}