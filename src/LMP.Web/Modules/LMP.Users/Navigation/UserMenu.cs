using Abp.Application.Navigation;
using Abp.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMP.Users.Navigation
{
    public class UserMenu : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
             .AddItem(
                        new MenuItemDefinition(
                            "Users",
                            new LocalizableString("Users", UserConsts.LocalizationSourceName),
                            url: "#/users",
                            icon: "fa fa-users",
                            order: 1
                        )
                );
        }
    }
}
