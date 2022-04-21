using isRock.LineBot;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using provider.InfraStructure.Log;
using provider.Model.LineBot;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace provider.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LineBotController : ControllerBase
    {
        private readonly LineBotConfig _lineBotConfig;
        private readonly ILogger<LineBotController> _logger;
        public LineBotController(LineBotConfig lineBotConfig, ILogger<LineBotController> ilogger) 
        {
            _lineBotConfig = lineBotConfig;
            _logger = ilogger;
        }
        [HttpPost]
        public async Task<IActionResult> LineBotCallBack(dynamic request)
        {

            try
            {
                string rawData = Convert.ToString(request);
                //剖析JSON
                var ReceivedMessage = isRock.LineBot.Utility.Parsing(rawData);
                var item = ReceivedMessage.events.FirstOrDefault();
                string Message = "";

                switch (item.type)
                {
                    case "join":
                        Message = $"有人把我加入{item.source.type}中了，大家好啊~";

                        //回覆用戶
                        isRock.LineBot.Utility.ReplyMessage(ReceivedMessage.events[0].replyToken, Message, _lineBotConfig.ChannelAccessToken);
                        break;
                    case "message":
                        if (item.message.text == "bye")
                        {
                            //回覆用戶
                            isRock.LineBot.Utility.ReplyMessage(item.replyToken, "bye-bye", _lineBotConfig.ChannelAccessToken);
                            //離開
                            if (item.source.type.ToLower() == "room")
                                isRock.LineBot.Utility.LeaveRoom(item.source.roomId, _lineBotConfig.ChannelAccessToken);
                            if (item.source.type.ToLower() == "group")
                                isRock.LineBot.Utility.LeaveGroup(item.source.roomId, _lineBotConfig.ChannelAccessToken);

                            break;
                        }
                        Message = "你說了:" + ReceivedMessage.events[0].message.text;
                        //取得用戶名稱 
                        LineUserInfo UserInfo = null;
                        if (item.source.type.ToLower() == "room")
                            UserInfo = isRock.LineBot.Utility.GetRoomMemberProfile(
                                item.source.roomId, item.source.userId, _lineBotConfig.ChannelAccessToken);
                        if (item.source.type.ToLower() == "group")
                            UserInfo = isRock.LineBot.Utility.GetGroupMemberProfile(
                                item.source.groupId, item.source.userId, _lineBotConfig.ChannelAccessToken);
                        //顯示用戶名稱
                        if (item.source.type.ToLower() != "user")
                            Message += "\n你是:" + UserInfo.displayName;

                        //回覆用戶
                        isRock.LineBot.Utility.ReplyMessage(item.replyToken, Message, _lineBotConfig.ChannelAccessToken);
                        break;
                    default:
                        break;
                }
                //回覆API OK
                return Ok();
            }
            catch (Exception ex)
            {
                //todo:請自行處理exception
                return Ok();
            }
        }

        [HttpGet]
        public IActionResult LineBotTest()
        {
            LogExtensions.ServerLog(_logger, "Config", _lineBotConfig.ChannelAccessToken,"","");
            return Ok("Testing");
        }
    }
}
