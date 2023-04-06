using Bing_BOT.Enum;

namespace Bing_BOT;

public static class ChatHelper
{
    public static List<string> GetDefaultOptions(CharStyle style)
    {
        return style switch
        {
            CharStyle.Creative => new List<string>()
            {
                "nlu_direct_response_filter",
                "deepleo",
                "disable_emoji_spoken_text",
                "responsible_ai_policy_235",
                "enablemm",
                "h3imaginative",
                "clgalileo",
                "gencontentv3",
                "cachewriteext",
                "e2ecachewrite",
                "dv3sugg"
            },
            CharStyle.Balanced => new List<string>()
            {
                "nlu_direct_response_filter",
                "deepleo",
                "disable_emoji_spoken_text",
                "responsible_ai_policy_235",
                "enablemm",
                "galileo",
                "cachewriteext",
                "e2ecachewrite",
                "dv3sugg",
                "glpromptv3"
            },
            CharStyle.Precise => new List<string>()
            {
                "nlu_direct_response_filter",
                "deepleo",
                "disable_emoji_spoken_text",
                "responsible_ai_policy_235",
                "enablemm",
                "h3precise",
                "cachewriteext",
                "e2ecachewrite",
                "dv3sugg",
                "clgalileo"
            }
           
        };
    }

    public static List<string> GetDefaultResponseType()
    {
        return new List<string>()
        {
            "Chat",
            "InternalSearchQuery",
            "InternalSearchResult",
            "Disengaged",
            "InternalLoaderMessage",
            "RenderCardRequest",
            "AdsQuery",
            "SemanticSerp",
            "GenerateContentQuery",
            "SearchQuery"
        };
    }

    public static List<string> GetDefaultSlids()
    {
        return new List<string>()
        {
            "anidtestcf",
            "321bic62ups0",
            "sydpayajax",
            "sydperfinput",
            "303hubcancls0",
            "316cache_sss0",
            "323glpromptv3",
            "316e2ecache"
        };
    }
}
