using System;

namespace LetterboxdSync.Configuration;

public class Account
{
    public string? UserJellyfin { get; set; }

    public string? UserLetterboxd { get; set; }

    public string? PasswordLetterboxd { get; set; }

    public bool Enable { get; set; }

    public bool SendFavorite { get; set; }

    // When enabled, mark all movies as watched on Letterboxd regardless of Jellyfin watched state.
    public bool ForceAllAsWatched { get; set; } = false;
}
