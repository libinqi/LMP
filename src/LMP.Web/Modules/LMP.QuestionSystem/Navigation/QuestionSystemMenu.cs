using Abp.Application.Navigation;
using Abp.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMP.QuestionSystem.Navigation
{
    public class QuestionSystemMenu : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
             .AddItem(
                    new MenuItemDefinition(
                        "Questions",
                        new LocalizableString("Questions", QuestionSystemConsts.LocalizationSourceName),
                        url: "#/questions",
                        icon: "fa fa-question",
                        order: 2
                        )
                );
        }
    }
}
