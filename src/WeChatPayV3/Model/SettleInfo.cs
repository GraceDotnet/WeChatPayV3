using Newtonsoft.Json;

namespace ZhiFou.WeChatPayV3.Model
{
    /// <summary>
    /// 结算信息
    /// </summary>
    public class SettleInfo
    {
        /// <summary>
        /// 是否指定分账
        /// </summary>
        [JsonProperty(PropertyName = "profit_sharing")]
        public bool ProfitSharing { get; set; }

        /// <summary>
        /// 补差金额
        /// SettleInfo.profit_sharing为true时，该金额才生效。
        /// 注意：单笔订单最高补差金额为5000元
        /// </summary>
        [JsonProperty(PropertyName = "subsidy_amount")]
        public int SubsidyAmount { get; set; }
    }
}
