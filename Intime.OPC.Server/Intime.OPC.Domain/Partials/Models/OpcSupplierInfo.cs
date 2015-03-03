using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intime.OPC.Domain.Models
{
    public partial class OpcSupplierInfo
    {
        [NotMapped]
        public IEnumerable<Brand> Brands { get; set; }

        public static OpcSupplierInfo Convert2Supplier(dynamic obj)
        {
            if (obj == null)
            {
                return null;
            }

            var item = new OpcSupplierInfo
            {
                Id = obj.Id,
                SupplierName = obj.SupplierName,
                SupplierNo = obj.SupplierNo,
                Brands = new List<Brand>(),
                Address = obj.Address,
                Contact = obj.Contact,
                Corporate = obj.Corporate,
                CreatedDate = obj.CreatedDate,
                Contract = obj.Contract,
                CreatedUser = obj.CreatedUser,
                FaxNo = obj.FaxNo,
                Memo = obj.Memo,
                Status = obj.Status,
                StoreId = obj.StoreId,
                TaxNo = obj.TaxNo,
                Telephone = obj.Telephone,
                UpdatedDate = obj.UpdatedDate,
                UpdatedUser = obj.UpdatedUser,


            };

            return item;
        }
    }
}