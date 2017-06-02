using AppChat.Cache;
using AppChat.Model.Online;
using AppChat.Service;
using AppChat.Service._Interface;
using AppChat.Service.User;
using AppChat.SignalRHostSelf.Help;
using AppChat.Utils.Extension;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

using System.Threading.Tasks;

namespace AppChat.SignalRHostSelf.Hubs
{
    [HubName("ChatHub")]
    public class ChatHub : Hub
    {
        private IRedisCache _redisCache;
        private IUserService _userServer;
        private ICreateGroupService _createGroupServer;
        private IMessageHelper _messageHelp;
        public ChatHub(IRedisCache redisCache, IMessageHelper messageHelp,ICreateGroupService createGroupServer,IUserService userServer)
        {
            _redisCache = redisCache;
            _createGroupServer = createGroupServer;
            _messageHelp = messageHelp;
            _userServer = userServer;
        }

        #region Link
        /// <summary>
        /// 当前的connectionId
        /// </summary>
        public string CurrentConnectId
        {
            get
            {
                return Context.ConnectionId;
            }
        }
        /// <summary>
        /// 当前的用户ID
        /// </summary>
        public string CurrentUserId
        {
            get
            {
                var contextBase = Context.Request.GetHttpContext();
                return _redisCache.GetCurrentUserId(contextBase);
            }
        }
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        private OnlineUser CurrentOnlineUser
        {
            get
            {
                return new OnlineUser
                {
                    connectionid = CurrentConnectId,
                    userid = CurrentUserId
                };
            }
        }
        /// <summary>
        /// 建立连接
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            UserOnline();
            return Clients.Caller.receiveMessage("连接成功");
        }
        /// <summary>
        /// 失去连接
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            UserOffline();
            return Clients.Caller.receiveMessage("失去连接");
        }
        /// <summary>
        /// 重新连接
        /// </summary>
        /// <returns></returns>
        public override Task OnReconnected()
        {
            UserOnline();
            return Clients.Caller.receiveMessage("重新连接");
        }
        #endregion

        #region 用户上下线操作
        private void UserOnline()
        {
            //将当前用户添加到redis在线用户缓存中
            _redisCache.OperateOnlineUser(CurrentOnlineUser);
            //发送用户上线消息
            _messageHelp.SendUserOnOffLineMessage(CurrentUserId);
            //由于用户群一般不多，这里直接将用户全部加入群组中
            //var groups = _userServer.GetUserAllGroups(CurrentUserId);
            ////遍历组，该connectionId加入到组中，防止收不到消息
            //foreach (string group in groups)
            //{
            //    var g = _createGroupServer.CreateName(group.ToInt());
            //    Groups.Add(CurrentConnectId, g);
            //}
        }

        private void UserOffline()
        {
            //将当前用户从在线用户列表中剔除
            _redisCache.OperateOnlineUser(CurrentOnlineUser, isDelete: true);
            //发送用户下线消息
            _messageHelp.SendUserOnOffLineMessage(CurrentUserId, online: false);
        }
        #endregion
    }
}
