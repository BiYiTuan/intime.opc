using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using OPCAPP.Domain.Models;
using Intime.OPC.Infrastructure;

namespace Intime.OPC.Modules.Authority.ViewModels
{
    [Export(typeof(UserUpdatePwdViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UserUpdatePwdViewModel : BindableBase
    {
        private UpdatePwdModel _modelConfig;
        public DelegateCommand OkCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public UpdatePwdModel Model
        {
            get { return _modelConfig; }
            set { SetProperty(ref _modelConfig, value); }
        }

        public UserUpdatePwdViewModel()
        {
           Model = new UpdatePwdModel();
        }

     

    }

 
}
