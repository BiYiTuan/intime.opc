using System;
using System.Windows.Input;
using Intime.OPC.Infrastructure.Mvvm.View;

namespace Intime.OPC.Infrastructure.Mvvm
{
    public interface IViewModel
    {
        IBaseView View { get; set; }

        Object Model { get; set; }
    }

    public interface ISubView
    {
        ICommand OKCommand { get; set; }

        ICommand CancelCommand { get; set; }
    }
}