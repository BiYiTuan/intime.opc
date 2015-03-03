using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Intime.OPC.Modules.CustomerService.ViewModels;
using Intime.OPC.Domain.Models;

namespace OPCApp.TransManage.Views
{
    /// <summary>
    ///     PrintInvoiceViewModel.xaml 的交互逻辑
    /// </summary>
    [Export("CustomerReturnGoods", typeof (UserControl))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class CustomerReturnGoods
    {
        public CustomerReturnGoods()
        {
            InitializeComponent();
        }

        [Import("CustomerReturnGoodsViewModel")]
        public CustomerReturnGoodsViewModel ViewModel
        {
            set { DataContext = value; }
            get { return DataContext as CustomerReturnGoodsViewModel; }
        }

        private void cbxList_DropDownOpened(object sender, EventArgs e)
        {
            if (ViewModel.OrderItem != null)
            {
                var orderItem = ViewModel.OrderItem;
                int count = orderItem.Quantity - orderItem.ReturnCount;
                var list = new List<int>();
                for (int i = 1; i < count + 1; i++)
                {
                    list.Add(i);
                }
                ViewModel.ReturnCountList = list;
            }
        }
    }
}