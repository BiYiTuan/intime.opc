using Microsoft.Practices.Prism.Mvvm;
namespace Intime.OPC.Domain.Models
{
    public class OPC_AuthMenu : BindableBase
    {
        private bool _isSelected;

        public int Id { get; set; }
        public string MenuName { get; set; }
        public int PraentMenuId { get; set; }
        public int Sort { get; set; }
        public string Url { get; set; }
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }
    }
}