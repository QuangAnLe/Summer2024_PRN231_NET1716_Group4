using Azure.Core;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
﻿using MilkTeaBusinessObject.BusinessObject;
using MilkTeaDAO.ZaloPayHelper;
using MilkTeaDAO.ZaloPayHelper.Crypto;
using Newtonsoft.Json;

namespace MilkTeaDAO.DAOs
{
    public class PaymentDAO
    {
        private OrderDAO orderDAO = new OrderDAO();
        private readonly string app_id = "2553";
        private readonly string key1 = "PcY4iZIKFCIdgZvA6ueMcMHHUbRLYjPL";
        private readonly string create_order_url = "https://sb-openapi.zalopay.vn/v2/create";
        private readonly string redirectUrl = "https://localhost:7097/UserPage/MyOrder/OrderDetail?id=";
        public async Task<Dictionary<string, object>> CreateOrder(int id)
        {
            double priceOrder = 0;
            List<string> listNameTea = new List<string>();
            Order order = orderDAO.Get(id);
            List<OrderDetail> list = order.OrderDetails.ToList();
            foreach (var item in list)
            {
                double costsIncurred = ConvertCostsIncurred(item.CostsIncurred);
                priceOrder += item.Quantity * item.Tea.Price + costsIncurred;
                listNameTea.Add(item.Tea.TeaName);
            }
            Random rnd = new Random();
            var embed_data = new { redirecturl=redirectUrl+id };
            var items = new[] { new { } };
            var param = new Dictionary<string, string>();
            var app_trans_id = rnd.Next(1000000); // Generate a random order's ID.

            param.Add("app_id", app_id);
            param.Add("app_user", "user123");
            param.Add("app_time", Utils.GetTimeStamp().ToString());
            param.Add("amount", priceOrder.ToString());
            param.Add("app_trans_id", DateTime.Now.ToString("yyMMdd") + "_" + app_trans_id); // mã giao dich có định dạng yyMMdd_xxxx
            param.Add("embed_data", JsonConvert.SerializeObject(embed_data));
            param.Add("item", JsonConvert.SerializeObject(items));
            param.Add("description", "Thanh toán đơn hàng của " + order.User.FullName);
            param.Add("bank_code", "zalopayapp");
            param.Add("callback_url", "https://f4b0-14-186-77-119.ngrok-free.app/odata/Callback/"+order.OrderID);

            var data = app_id + "|" + param["app_trans_id"] + "|" + param["app_user"] + "|" + param["amount"] + "|"
                + param["app_time"] + "|" + param["embed_data"] + "|" + param["item"];
            param.Add("mac", HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key1, data));

            var result = await HttpHelper.PostFormAsync(create_order_url, param);
            return result;
        }
        private double ConvertCostsIncurred(string costsIncurred)
        {
            if (costsIncurred.EndsWith("d"))
            {
                string numberPart = costsIncurred.Substring(0, costsIncurred.Length - 1);
                if (double.TryParse(numberPart, out double result))
                {
                    return result;
                }
            }
            return 0;
        }
    }
}
