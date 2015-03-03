using OPCApp.BasicsManage.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OPCApp.BasicsManage.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EditCompanyEntryModel : BaseViewModel
    {
        private ExpressCompany _editCompanyEntry;
        public ExpressCompany editCompanyEntry
        {
            get { return _editCompanyEntry; }
            set
            {
                _editCompanyEntry = value;
                OnPropertyChanged("editCompanyEntry");
            }
        }

        private List<enumExpressType> _expressTypeList;
        public List<enumExpressType> expressTypeList
        {
            get
            {
                return _expressTypeList = new List<enumExpressType>()
                {
                    new enumExpressType(){ ID=0, Info="顺丰"}, 
                    new enumExpressType(){ ID=1, Info="韵达"}, 
                    new enumExpressType(){ ID=2, Info="圆通"}, 
                    new enumExpressType(){ ID=3, Info="中通"}, 
                    new enumExpressType(){ ID=4, Info="申通"}, 
                    new enumExpressType(){ ID=5, Info="EMS"}
                };
            }
            set
            {
                _expressTypeList = value;
                OnPropertyChanged("expressTypeList");
            }
        }

        ///
        /// 当快递公司类型选中项更改时发生
        ///
        private int _selectedExpressType;
        public int selectedExpressType
        {
            get
            {
                return this._selectedExpressType;
            }
            set
            {
                this._selectedExpressType = value;
                OnPropertyChanged("selectedExpressType");
            }
        }

        private List<enumDeliveryType> _deliveryTypeList;
        public List<enumDeliveryType> deliveryTypeList
        {
            get
            {
                return _deliveryTypeList = new List<enumDeliveryType>()
                {
                    new enumDeliveryType(){ ID=0, Info="快递"}, 
                    new enumDeliveryType(){ ID=1, Info="平邮"}
                };
            }
            set
            {
                _deliveryTypeList = value;
                OnPropertyChanged("deliveryTypeList");
            }
        }

        ///
        /// 当邮寄类型选中项更改时发生
        ///
        private int _selectedDeliveryType;
        public int selectedDeliveryType
        {
            get
            {
                return this._selectedDeliveryType;
            }
            set
            {
                this._selectedDeliveryType = value;
                OnPropertyChanged("selectedDeliveryType");
            }
        }

        ///
        /// 当是否启用选中项更改时发生
        ///
        private bool _isEnabled;
        public bool isEnabled
        {
            get
            {
                return this._isEnabled;
            }
            set
            {
                this._isEnabled = value;
                OnPropertyChanged("isEnabled");
            }
        }

        private readonly RelayCommand _excuteCommand;
        private readonly RelayCommand _quitCommand;

        /// <summary>
        /// 添加Window属性
        /// </summary>
        public Window window { get; set; }
        /// <summary>
        /// 关闭窗口
        /// </summary>
        public void WindowClose()
        {
            this.window.Close();
        }

        /// <summary>
        /// 执行修改
        /// </summary>
        public ICommand ExcuteCommand
        {
            get { return _excuteCommand; }
        }

        /// <summary>
        /// 取消修改
        /// </summary>
        public ICommand QuitCommand
        {
            get { return _quitCommand; }
        }

        public EditCompanyEntryModel(ExpressCompany companyEntry)
        {
            this.editCompanyEntry = companyEntry;
            this.selectedExpressType = enumExpressType.getExpressTypeIndex(companyEntry.ExpressType);
            this.selectedDeliveryType = enumDeliveryType.getDeliveryTypeIndex(companyEntry.DeliveryType);
            this.isEnabled = companyEntry.IsEnabled;
            this._excuteCommand = new RelayCommand(this.ExcuteUpdateCommand, this.CheckExcuteCommandCanExecute);
            this._quitCommand = new RelayCommand(this.QuitUpdateCommand);
        }

        public EditCompanyEntryModel()
        {
            this.editCompanyEntry = new ExpressCompany();
            this.selectedExpressType = 0;
            this.selectedDeliveryType = 0;
            this.isEnabled = true;
            this._excuteCommand = new RelayCommand(this.ExcuteUpdateCommand, this.CheckExcuteCommandCanExecute);
            this._quitCommand = new RelayCommand(this.QuitUpdateCommand);
        }

        private void ExcuteUpdateCommand()
        {
            ExpressCompany newCompanyModel = new ExpressCompany();
            newCompanyModel.ExpressName = editCompanyEntry.ExpressName;
            newCompanyModel.Address = editCompanyEntry.Address;
            newCompanyModel.ContractName = editCompanyEntry.ContractName;
            newCompanyModel.ContractNumber = editCompanyEntry.ContractNumber;
            newCompanyModel.ContractPhone = editCompanyEntry.ContractPhone;
            newCompanyModel.DeliveryType = enumDeliveryType.getDeliveryTypeInfo(this.selectedDeliveryType);
            newCompanyModel.ExpressType = enumExpressType.getExpressTypeInfo(this.selectedExpressType); 
            newCompanyModel.PrintTemplate = editCompanyEntry.PrintTemplate;
            newCompanyModel.IsEnabled = this.isEnabled;
            editCompanyEntry = newCompanyModel;
            this.WindowClose();
        }

        private void QuitUpdateCommand()
        {
            this.WindowClose();
        }

        private bool CheckExcuteCommandCanExecute()
        {
            return editCompanyEntry != null;
        }
    }
}
