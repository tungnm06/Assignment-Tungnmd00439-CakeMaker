using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewCakeMakerASP.NET.Models
{
    public class GioHang
    {
        DBCakeMakerContext db = new DBCakeMakerContext();
        public int idProduct { get; set; }
        public string sTenSach { get; set; }
        public string sHinhAnh { get; set; }
        public decimal dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public decimal dThanhTien {
            get { return iSoLuong * dDonGia; }
        }
        public GioHang(int IdProduct)
        {
            idProduct = IdProduct;
            Product product = db.Products.SingleOrDefault(n => n.ProductId == IdProduct);
            sTenSach = product.Name;
            sHinhAnh = product.Image;
            dDonGia = decimal.Parse(product.Prince.ToString());
            iSoLuong = 1;
        }
    }
}