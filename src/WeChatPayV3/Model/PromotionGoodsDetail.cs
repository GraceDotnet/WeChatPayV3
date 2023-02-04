using Newtonsoft.Json;

namespace WeChatPayV3.Model
{
    /// <summary>
    /// 单品列表信息
    /// </summary>    
    public class PromotionGoodsDetail 
    {
        /// <summary>
        /// 商品编码
        /// 示例值：M1006
        /// </summary>
        [JsonProperty(PropertyName ="goods_id")]
        public string GoodsId { get; set; }

        /// <summary>
        /// 用户购买的数量
        /// 示例值：1
        /// </summary>
        [JsonProperty(PropertyName ="quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// 商品单价，单位为分
        /// 示例值：100
        /// </summary>
        [JsonProperty(PropertyName ="unit_price")]
        public int UnitPrice { get; set; }

        /// <summary>
        /// 商品优惠金额
        /// 示例值：0  
        /// </summary>
        [JsonProperty(PropertyName ="discount_amount")]
        public int DiscountAmount { get; set; }

        /// <summary>
        /// 商品备注信息
        /// 示例值：商品备注信息
        /// </summary>
        [JsonProperty(PropertyName ="goods_remark")]
        public string GoodsRemark { get; set; }
    }
}
