using AppChat.Model.Core;
using AppChat.Model.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppChat.Model.Convert
{
    public static class MessageConvert
    {
        public static List<ApplyMessage> Convert(this List<v_layim_apply> enitiyList,int userid)
        {
            var list = new List<ApplyMessage>();

            if (enitiyList.Count > 0)
            {
                var model = new ApplyMessage();

                foreach (var item in enitiyList)
                {
                    #region 1.基础信息
                    model.applyid = item.applyid;
                    model.userid = item.userid;
                    model.applyname = item.applyname;
                    model.applytype = item.applytype;
                    model.applyavatar = item.applyavatar;
                    model.isself = item.userid == userid;
                    model.addtime = item.applytime.ToString("yyyy/MM/dd HH:mm");
                    #endregion

                    #region 2.申请加我为好友的
                    if (item.applytype == 0)
                    {
                        model.msg = string.Format("请求加你为好友,附加信息:{0}", item.other);
                    }
                    #endregion

                    #region 3.申请退群的
                    if (true)//TODO:还有条件没写
                    {
                        model.msg = string.Format("请求加入群({0}),附加信息:{1}", item.targetname, item.other);
                    }
                    #endregion

                    #region 4.我的加好友信息已经审批的
                    if (true)//TODO:还有条件没写
                    {
                        StringBuilder str = new StringBuilder();

                        str.Append("已经");

                        if (item.result == 1)
                        {
                            str.Append("通过");
                        }
                        if (item.result == 2)
                        {
                            str.Append("拒绝");
                        }

                        str.Append("了你的好友请求");

                        if (item.result == 2)
                        {
                            //TODO:拒绝原因
                            //str.AppendFormat(",原因:{0}",);
                        }
                    }
                    #endregion

                    #region 5.我的加群信息
                    if (true)//TODO:还有条件没写
                    {
                        StringBuilder str = new StringBuilder();

                        str.Append("已经");

                        if (item.result == 1)
                        {
                            str.Append("通过");
                        }
                        if (item.result == 2)
                        {
                            str.Append("拒绝");
                        }

                        str.Append("了你的加群请求");

                        if (item.result == 2)
                        {
                            //TODO:拒绝原因
                            //str.AppendFormat(",原因:{0}",);
                        }
                    }
                    #endregion
                }
            }

            return list;
        }
    }
}
