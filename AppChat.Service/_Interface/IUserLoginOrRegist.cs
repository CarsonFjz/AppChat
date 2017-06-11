using AppChat.Model;
using System.Threading.Tasks;

namespace AppChat.Service._Interface
{
    public interface IUserLoginOrRegist
    {
        #region 用户注册或者登陆
        JsonResultModel UserLogin(string loginName, string loginPwd, out int userid);
        Task<JsonResultModel> UserRegist(string loginNmae, string loginPwd, string nickName, bool sex);
        #endregion
    }
}
