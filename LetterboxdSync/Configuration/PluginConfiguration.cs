using System.Collections.Generic;
using MediaBrowser.Model.Plugins;

namespace LetterboxdSync.Configuration;

public class PluginConfiguration : BasePluginConfiguration
{
    public List<Account> Accounts { get; set; } = new List<Account>();

    // Optional global rate limit in requests per minute. 0 disables throttling.
    public int RequestsPerMinute { get; set; } = 0;
}
