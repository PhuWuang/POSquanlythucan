using QLBanDoAnNhanh.DAL;
using QLBanDoAnNhanh.DAL.Models;
using QLBanDoAnNhanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLBanDoAnNhanh.BLL
{
    public class OrderService
    {
        private OrderDAL _orderDAL;
        private ProductDAL _productDAL; // Cần ProductDAL để lấy giá sản phẩm

        public OrderService()
        {
            _orderDAL = new OrderDAL();
            _productDAL = new ProductDAL();
        }

        // Hàm này nhận ID nhân viên và một Dictionary chứa <ID sản phẩm, Số lượng>
        public bool CreateOrder(int employeeId, Dictionary<int, int> items)
        {
            // 1. Tạo đối tượng Order chính
            var order = new Order
            {
                IdEmployee = employeeId,
                CreateDate = DateTime.Now,
                OrderDetails = new List<OrderDetail>()
            };

            decimal totalPrice = 0;

            // 2. Lặp qua các món hàng để tạo OrderDetail và tính tổng tiền
            foreach (var item in items)
            {
                var productId = item.Key;
                var quantity = item.Value;

                // Lấy thông tin sản phẩm từ CSDL để có giá chính xác
                // Điều này đảm bảo giá luôn đúng, dù giá trên giao diện có thể cũ
                var product = _productDAL.GetById(productId); // CHÚNG TA SẼ THÊM HÀM NÀY SAU
                if (product == null)
                {
                    // Nếu một sản phẩm không tồn tại, hủy toàn bộ giao dịch
                    return false;
                }

                var orderDetail = new OrderDetail
                {
                    IdProduct = productId,
                    quantity = quantity
                    // IdOrder sẽ được EF tự động gán khi lưu
                };
                order.OrderDetails.Add(orderDetail);

                // Cộng dồn vào tổng tiền hóa đơn
                totalPrice += (product.PriceProduct.Value * quantity);
            }

            // 3. Tính thuế VAT 5% và gán tổng tiền cuối cùng cho Order
            order.Total = totalPrice * 1.05m;

            // 4. Gửi đối tượng Order hoàn chỉnh xuống DAL để lưu
            return _orderDAL.CreateOrder(order);
        }
    }
}