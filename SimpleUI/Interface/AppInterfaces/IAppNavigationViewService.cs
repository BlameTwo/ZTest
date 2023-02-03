using System.Windows.Controls;

namespace SimpleUI.Interface.AppInterfaces
{
    public interface IAppNavigationViewService
    {
        void Navigation<T>(T content, object data);

        /// <summary>
        /// 初始化导航容器
        /// </summary>
        /// <param name="frame">容器</param>
        /// <param name="IsNaved">是否开启导航事件</param>
        void Init(Frame frame,bool IsNaved);
    }
}
