using System.ComponentModel.Composition;

namespace Intime.OPC.Modules.CustomerService.ViewModels
{
    [Export(typeof (CustomerReturnSearchRmaViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CustomerReturnSearchRmaViewModel : CustomerReturnSearchViewModel
    {
        public CustomerReturnSearchRmaViewModel()
        {
            IsShowCustomerReViewBtn = false;
            IsShowCustomerAgreeBtn = false;
        }
    }
}