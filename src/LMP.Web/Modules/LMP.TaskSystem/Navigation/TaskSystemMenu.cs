using Abp.Application.Navigation;
using Abp.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMP.TaskSystem.Navigation
{
    public class TaskSystemMenu : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
             .AddItem(
              new MenuItemDefinition(
                        "TaskList",
                        new LocalizableString("TaskList", TaskSystemConsts.LocalizationSourceName),
                        url: "#/tasks",
                        icon: "fa fa-tasks",
                        order:0
                        )
                );
        }
    }
}
