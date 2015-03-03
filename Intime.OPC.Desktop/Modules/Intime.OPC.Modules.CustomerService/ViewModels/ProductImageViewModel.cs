using Intime.OPC.Domain.Models;
using Intime.OPC.Infrastructure.Mvvm;

namespace Intime.OPC.Modules.CustomerService.ViewModels
{
    public class ProductImageViewModel : ModalDialogViewModel<OPC_SaleDetail>
    {
        public ProductImageViewModel()
        {
        }

        #region Properties

        public string ImageUrl { get; set; }

        #endregion
    }
}
