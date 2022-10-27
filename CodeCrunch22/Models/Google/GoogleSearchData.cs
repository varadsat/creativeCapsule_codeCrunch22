namespace CodeCrunch22.Models.Google
{

    public class GoogleSearchData
    {
        public string kind { get; set; }
        public Url url { get; set; }
        public Queries queries { get; set; }
        public Context context { get; set; }
        public Searchinformation searchInformation { get; set; }
        public Item[] items { get; set; }
    }

    public class Url
    {
        public string type { get; set; }
        public string template { get; set; }
    }

    public class Queries
    {
        public Request[] request { get; set; }
        public Nextpage[] nextPage { get; set; }
    }

    public class Request
    {
        public string title { get; set; }
        public string totalResults { get; set; }
        public string searchTerms { get; set; }
        public int count { get; set; }
        public int startIndex { get; set; }
        public string inputEncoding { get; set; }
        public string outputEncoding { get; set; }
        public string safe { get; set; }
        public string cx { get; set; }
    }

    public class Nextpage
    {
        public string title { get; set; }
        public string totalResults { get; set; }
        public string searchTerms { get; set; }
        public int count { get; set; }
        public int startIndex { get; set; }
        public string inputEncoding { get; set; }
        public string outputEncoding { get; set; }
        public string safe { get; set; }
        public string cx { get; set; }
    }

    public class Context
    {
        public string title { get; set; }
    }

    public class Searchinformation
    {
        public float searchTime { get; set; }
        public string formattedSearchTime { get; set; }
        public string totalResults { get; set; }
        public string formattedTotalResults { get; set; }
    }

    public class Item
    {
        public string kind { get; set; }
        public string title { get; set; }
        public string htmlTitle { get; set; }
        public string link { get; set; }
        public string displayLink { get; set; }
        public string snippet { get; set; }
        public string htmlSnippet { get; set; }
        public string cacheId { get; set; }
        public string formattedUrl { get; set; }
        public string htmlFormattedUrl { get; set; }
        public Pagemap pagemap { get; set; }
    }

    public class Pagemap
    {
        public Metatag[] metatags { get; set; }
        public Cse_Thumbnail[] cse_thumbnail { get; set; }
        public Cse_Image[] cse_image { get; set; }
        public Listitem[] listitem { get; set; }
    }

    public class Metatag
    {
        public string msapplicationtilecolor { get; set; }
        public string ogtype { get; set; }
        public string viewport { get; set; }
        public string author { get; set; }
        public string ogtitle { get; set; }
        public string ogurl { get; set; }
        public string msapplicationtileimage { get; set; }
        public string ogdescription { get; set; }
        public string referrer { get; set; }
        public string ogimage { get; set; }
        public string themecolor { get; set; }
        public string ogimagewidth { get; set; }
        public string ogimageheight { get; set; }
        public string formatdetection { get; set; }
        public string twittercard { get; set; }
        public string twittertitle { get; set; }
        public string twitterurl { get; set; }
        public string csrfparam { get; set; }
        public string twitterimage { get; set; }
        public string twittersite { get; set; }
        public string csrftoken { get; set; }
        public string verification { get; set; }
        public string nextheadcount { get; set; }
        public string ogsite_name { get; set; }
        public string image { get; set; }
        public string twittercreator { get; set; }
        public string twitterdescription { get; set; }
        public string ogimagealt { get; set; }
        public string created { get; set; }
        public string title { get; set; }
        public string revised { get; set; }
    }

    public class Cse_Thumbnail
    {
        public string src { get; set; }
        public string width { get; set; }
        public string height { get; set; }
    }

    public class Cse_Image
    {
        public string src { get; set; }
    }

    public class Listitem
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}
