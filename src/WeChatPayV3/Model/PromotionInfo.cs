﻿using System;
using Newtonsoft.Json;

namespace WeChatPayV3.Model
{
    /// <summary>
    /// 优惠信息
    /// </summary>
    public class PromotionInfo
    {
        /// <summary>
        /// 券ID
        /// 示例值：109519
        /// </summary>
        [JsonProperty(PropertyName ="coupon_id")]
        public string CouponId { get; set; }


        /// <summary>
        /// 优惠名称
        /// 示例值：单品惠-6
        /// </summary>
        [JsonProperty(PropertyName ="name")]
        public string Name { get; set; }



        /// <summary>
        /// 优惠范围
        /// GLOBAL：全场代金券
        /// SINGLE：单品优惠
        /// </summary>
        [JsonProperty(PropertyName ="scope")]
        public string Scope
        {
            get { return Scope; }
            set
            {
                value = value?.ToUpper();

                if (value.Equals("GLOBAL") || value.Equals("SINGLE")) Scope = value;

                else if (!string.IsNullOrWhiteSpace(Scope))

                    throw new ArgumentException();
            }
        }
        /// <summary>
        /// 优惠类型
        /// CASH：充值
        /// NOCASH：预充值
        /// </summary>
        [JsonProperty(PropertyName ="type")]
        public string Type
        {
            get { return Type; }
            set
            {
                value = value?.ToUpper();

                if (value.Equals("CASH") || value.Equals("NOCASH")) Scope = value;

                else if (!string.IsNullOrWhiteSpace(value))

                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// 优惠券面额
        /// 示例值：100
        /// </summary>
        [JsonProperty(PropertyName ="amount")]
        public int Amount
        {
            get
            {
                return Amount;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException();
                }

                Amount = value;
            }
        }

        /// <summary>
        /// 活动ID
        /// 示例值：931386
        /// </summary>
        [JsonProperty(PropertyName ="stock_id")]
        public int? StockId { get; set; }


        /// <summary>
        /// 微信出资，单位为分
        /// 示例值：0
        /// </summary>
        [JsonProperty(PropertyName ="wechatpay_contribute")]
        public int? WechatpayContribute { get; set; }


        /// <summary>
        /// 商户出资，单位为分
        /// 示例值：0
        /// </summary>
      [JsonProperty(PropertyName ="merchant_contribute")]
        public int? MerchantContribute { get; set; }


        /// <summary>
        /// 其他出资，单位为分
        ///示例值：0
        /// </summary>
        [JsonProperty(PropertyName ="other_contribute")]
        public int? OtherContribute { get; set; }


        /// <summary>
        /// 优惠币种
        /// </summary>
        [JsonProperty(PropertyName ="currency")]
        public string Currency
        {
            get { return Currency; }
            set { if (value.ToUpper().Equals("CNY")) Currency = value; }
        }
    }
}
