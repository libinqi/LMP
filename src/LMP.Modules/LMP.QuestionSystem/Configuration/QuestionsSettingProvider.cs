using System.Collections.Generic;
using Abp.Configuration;

namespace LMP.QuestionSystem.Configuration
{
    public class QuestionsSettingProvider : SettingProvider
    {
        public const string QuestionsDefaultPageSize = "QuestionsDefaultPageSize";

        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
                   {
                       new SettingDefinition(QuestionsDefaultPageSize, "10")
                   };
        }
    }
}
