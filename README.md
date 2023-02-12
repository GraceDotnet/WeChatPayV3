# ZhiFou.WeChatPayV3
[![Nuget](https://img.shields.io/nuget/v/ZhiFou.WeChatPayV3)](https://www.nuget.org/packages/ZhiFou.WeChatPayV3/)
[![Nuget](https://img.shields.io/nuget/dt/ZhiFou.WeChatPayV3)](https://www.nuget.org/packages/ZhiFou.WeChatPayV3/)

微信支付SDK，v3版本


## 调用示例

```
// 接口调用示例
// https://pay.weixin.qq.com/wiki/doc/apiv3/apis/chapter3_1_1.shtml

var client = new WechatPayClient();
var res = client.ExecuteRequest(new TransactionsJsapiRequest
{
    mchid = "161XXXXXXX",
    out_trade_no = "1217752501201407033233368318",
    appid = "wxdace645e0bc2cXXX",
    description = "Image形象店-深圳腾大-QQ公仔",
    notify_url = "https://www.weixin.qq.com/wxpay/pay.php",
    amount = new Amount
    {
        currency = "CNY",
        total = 1
    },
    payer = new Payer { openid = "o4GgauInH_RCEdvrrNGrntXDuXXX" }
}, new WechatOptions("wxdace645e0bc2cXXX", "161XXXXXXX", "cert.p12", "752c744226e5dadad099b406148c9"));
```

```
// 支付回调处理
var notifyBody = """
    {"id":"13d9d2f8-f08c-5870-955c-60e9446b082d","create_time":"2023-02-09T17:37:25+08:00","resource_type":"encrypt-resource","event_type":"TRANSACTION.SUCCESS","summary":"支付成功","resource":{"original_type":"transaction","algorithm":"AEAD_AES_256_GCM","ciphertext":"xKB0bsXAo2YtLZiZ0K09WW21wf6inbV0Ma4TdyCjQ86KY/5drDmSic3vK7ra6s59Y6qNKj6/XD6VkdRrSM8ZPfoblFoPsrmS9HS4ZDyQG0QN/PiV/PU4TFPINeuOVeRgVcRzlt9kti6aubizF+VcLoCZNTXzJrhJptpAcKqwxk/onIr/iH2IyJOhgn886sNSrYLbxak6oTacVuo8XVjnSM5KPF1UzkhbWt0Y0auo2M1rXbpx3PyppkF/MO5lZ8Kp8yDra7HIPKGQ8jM7bnwP6ecfW1ASndNzJfbZsrb3pYQvnWpVSjAIjknjVtpcFwB29cnT1MXs9kB16SskByuLJBD9fKOEtV/acpkX3jfg11pK9Rlv8AnGtTviZcxdVpFi7qh5cRj0cBaNv5f+uu8CwqYiFvqshAqjTiBr1KVP5soh00qSrdB+RhASWWSFPoEb0wA+51KUY6KOZFQBgvdRm81dUSRlKIIXqSPhhbu63GUImtsnz0FMAbQ3dSruR2ZWGukW6OH0RNAYvDjUHPM+UZ6ewQ66lew+sWWwvSlSHC0VBXeqQYIZL7HuxMQuLTyEThuKG2/bSFgdEpYazXKkOg==","associated_data":"transaction","nonce":"3YQJLHk5o9pm"}}
    """;

var payload = JsonConvert.DeserializeObject<WechatNotificationPayload<WeChatPayTransactionsNotify>>(notifyBody);

// 回调解析
client.ExecuteNofity(
    new WechatPayHeader
    {
        // 传如Request中获取的header
    },
    payload,
    new WechatOptions("wxdace645e0bc2cXXX", "161XXXXXXX", "cert.p12", "752c744226e5dadad099b406148c9")
);
```
