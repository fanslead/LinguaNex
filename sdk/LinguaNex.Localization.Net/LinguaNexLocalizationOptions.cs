﻿namespace LinguaNex.Extensions.Localization.Json
{
    public class LinguaNexLocalizationOptions
    {
        public string LinguaNexApiUrl { get; set; }
        public string Project { get; set; }
        public bool UseWebSocket { get; set; } = false;
    }
}