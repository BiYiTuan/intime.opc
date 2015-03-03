using System.ComponentModel.Composition;
using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure.Mvvm;

namespace Intime.OPC.Modules.Dimension.ViewModels
{
    [Export(typeof(BrandViewModel))]
    public class BrandViewModel : ModalDialogViewModel<Brand>
    {

    }
}
