using Abp.Application.Navigation;
using Abp.Localization;
using LMP.QuestionSystem;
using LMP.TaskSystem;
using LMP.Users;

namespace LMP.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See .cshtml and .js files under App/Main/views/layout/header to know how to render menu.
    /// </summary>
    public class LMPNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                 .AddItem(
                    new MenuItemDefinition(
                        "TaskList",
                        new LocalizableString("TaskList", TaskSystemConsts.LocalizationSourceName),
                        url: "#/tasks",
                        icon: "fa fa-tasks"
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Questions",
                        new LocalizableString("Questions", QuestionSystemConsts.LocalizationSourceName),
                        url: "#/questions",
                        icon: "fa fa-question"
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Users",
                        new LocalizableString("Users", UserConsts.LocalizationSourceName),
                        url: "#/users",
                        icon: "fa fa-users"
                        )
                 );
                //.AddItem(
                //    new MenuItemDefinition(
                //        "NewTask",
                //        new LocalizableString("NewTask", TaskSystemConsts.LocalizationSourceName),
                //        url: "#/tasks/new",
                //        icon: "fa fa-asterisk"
                //        )
                //);
        }
    }
}
