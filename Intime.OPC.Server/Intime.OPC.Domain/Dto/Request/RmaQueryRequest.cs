using System;
using System.Collections.Generic;
using Intime.OPC.Domain.Enums;

namespace Intime.OPC.Domain.Dto.Request
{
    /// <summary>
    /// RMA qingqiu
    /// </summary>
    public class RmaQueryRequest : DatePageRequest, IStoreDataRoleRequest
    {
        public string OrderNo { get; set; }

        public int? Status { get; set; }

        public string RMANo { get; set; }


        private string _salesOrderNo;

        /// <summary>
        /// ���۵���
        /// </summary>
        [System.Obsolete("����ʹ��SalesOrderNo")]
        public string SaleOrderNo {
            get { return _salesOrderNo; }
            set { _salesOrderNo = value; }
        }

        /// <summary>
        /// ���۵���
        /// </summary>
        public string SalesOrderNo
        {
            get { return _salesOrderNo; }
            set { _salesOrderNo = value; }
        }

        public int? RmaStatus { get; set; }

        /// <summary>
        /// ��״̬ IN ��ϵ
        /// </summary>
        public List<int> Statuses { get; set; }

        public int? StoreId { get; set; }

        public int? SectionId { get; set; }

        /// <summary>
        /// �û�
        /// </summary>
        public int? CurrentUserId { get; set; }

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        public List<int> DataRoleStores { get; set; }

        /// <summary>
        /// �������� orderproducttype = 2 ��������
        /// </summary>
        public int? OrderProductType { get; set; }

        public override void ArrangeParams()
        {
            Status = CheckIsNullOrAndSet(Status);
            StoreId = CheckIsNullOrAndSet(StoreId);
            SectionId = CheckIsNullOrAndSet(SectionId);
            OrderProductType = CheckIsNullOrAndSet(OrderProductType);

            //ʱ��
            if (BuyStartDate != null)
            {
                BuyStartDate = BuyStartDate.Value.Date;
            }

            if (BuyEndDate != null)
            {
                BuyEndDate = BuyEndDate.Value.Date.AddDays(1);
            }

            if (ReturnStartDate != null)
            {
                ReturnStartDate = ReturnStartDate.Value.Date;
            }

            if (ReturnEndDate != null)
            {
                ReturnEndDate = ReturnEndDate.Value.Date.AddDays(1);
            }

            if (ReceiptStartDate.HasValue)
            {
                ReceiptStartDate = ReceiptStartDate.Value.Date;
            }

            if (ReceiptEndDate != null)
            {
                ReceiptEndDate = ReceiptEndDate.Value.Date.AddDays(1);
            }

            base.ArrangeParams();
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime? BuyStartDate { get; set; }

        /// <summary>
        ///  ����ʱ��
        /// </summary>
        public DateTime? BuyEndDate { get; set; }

        /// <summary>
        /// �˻�ʱ��
        /// </summary>
        public DateTime? ReturnStartDate { get; set; }

        /// <summary>
        ///  �˻�ʱ��
        /// </summary>
        public DateTime? ReturnEndDate { get; set; }

        /// <summary>
        /// �ջ�ʱ��
        /// </summary>
        public DateTime? ReceiptStartDate { get; set; }

        /// <summary>
        /// �ջ�ʱ��
        /// </summary>
        public DateTime? ReceiptEndDate { get; set; }

        /// <summary>
        /// �˿͵绰
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// ֧����ʽ,Ŀǰ��֧�� ����� ORDER
        /// </summary>
        public string PayType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EnumReturnGoodsStatus? ReturnGoodsStatus { get; set; }
    }

    /// <summary>
    /// RMA ����
    /// </summary>
    public class RmaSetCashNoRequest : IStoreDataRoleRequest
    {
        public string RmaNo { get; set; }

        public string CashNo { get; set; }
        public void ArrangeParams()
        {

        }

        public int? StoreId { get; set; }
        public List<int> DataRoleStores { get; set; }
        public int? CurrentUserId { get; set; }
    }

    /// <summary>
    /// RMA �ջ�
    /// </summary>
    public class RmaReceiptRequest : IStoreDataRoleRequest
    {
        public string RmaNo { get; set; }

        /// <summary>
        /// ȷ��
        /// </summary>
        public bool? IsConfirm { get; set; }

        public void ArrangeParams()
        {

        }

        public int? StoreId { get; set; }
        public List<int> DataRoleStores { get; set; }
        public int? CurrentUserId { get; set; }
    }

    /// <summary>
    /// RMA �˻�
    /// </summary>
    public class RmaReturnOfGoodsRequest : IStoreDataRoleRequest
    {
        public string RmaNo { get; set; }

        /// <summary>
        /// ȷ��
        /// </summary>
        public bool? IsConfirm { get; set; }

        public void ArrangeParams()
        {

        }

        public int? StoreId { get; set; }
        public List<int> DataRoleStores { get; set; }
        public int? CurrentUserId { get; set; }
    }
}