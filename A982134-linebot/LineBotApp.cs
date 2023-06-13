﻿using System.Net.Mime;
using Line.Messaging;
using Line.Messaging.Webhooks;

namespace A982134_linebot;

public class LineBotApp : WebhookApplication
{
    private readonly LineMessagingClient _messagingClient;

    private static Dictionary<string, string> _pool = new Dictionary<string, string>();
    static LineBotApp()
    {
        _pool.Add("","");
    }
    public LineBotApp(LineMessagingClient lineMessagingClient)
    {
        _messagingClient = lineMessagingClient;
    }

    protected override async Task OnMessageAsync(MessageEvent ev)
    {
        var result = null as List<ISendMessage>;

        switch (ev.Message)
        {
            //文字訊息
            case TextEventMessage textMessage:
            {
                //頻道Id
                var channelId = ev.Source.Id;
                //使用者Id
                var userId = ev.Source.UserId;
                //使用者輸入的文字
                var text = ((TextEventMessage)ev.Message).Text;
                
                /*
                //機器人不會回覆 喵
                if (PoolHasMsg(text))
                {
                    // 從記憶體池查詢資料
                    string response = GetResponse(text);
                    result = new List<ISendMessage>
                    {
                        new TextMessage(response)
                    };
                }
                else
                {
                    if (CheckFormat(text))
                    {
                        //將資料寫入記憶體池
                        TeachDog(text);
                    }
                }
                */
                
                var outputText = text;
                
                if (text.Contains("pocky"))
                {
                    outputText = "嗨!阿PO";
                }
                
                if (text.Contains("小羊"))
                {
                    outputText = "嗨!小羊";
                }
                
                if (text.Contains("嗨寶包"))
                {
                    outputText = "嗨!寶包";
                }

                //回傳 hellow
                result = new List<ISendMessage>
                {
                    new TextMessage(outputText),
                };
            }
                break;
        }

        if (result != null)
            await _messagingClient.ReplyMessageAsync(ev.ReplyToken, result);
    }
    
    /*
    /// <summary>
    /// 確認是否已經學習過這個對話
    /// </summary>
    /// <param name="inputMsg"></param>
    /// <returns></returns>
    private bool PoolHasMsg(string inputMsg)
    {
        return _pool.ContainsKey(inputMsg);
    }

    /// <summary>
    /// 用於 已經學習過的對話
    /// </summary>
    /// <param name="inputMsg"></param>
    /// <returns></returns>
    private string GetResponse(string inputMsg)
    {
        return _pool[inputMsg];
    }

    private bool CheckFormat(string inputMsg)
    {
        bool result = false;
        try
        {
            string[] subs = inputMsg.Split(';');
            //檢查
            if (subs.Length == 3)
            {
                if (subs[0] == "草莓蛋糕")
                {
                    result = true;
                }
            }
        }
        catch (Exception exp)
        {
            Console.WriteLine(exp);
            throw;
        }
        return result;
    }

    private void TeachDog(string inputMsg)
    {
        try
        {
            string[] subs = inputMsg.Split(';');
            //檢查
            if (subs.Length == 3)
            {
                if (subs[0] == "草莓蛋糕")
                {
                    _pool.Add(subs[1], subs[2]);
                }
            }
        }
        catch (Exception exp)
        {
            Console.WriteLine(exp);
            throw;
        }
    }
    */
}