using System;
using System.Collections.Generic;
using System.Net;
using HtmlAgilityPack;
/// <summary>
/// A result encapsulating the Url and the HtmlDocument
/// </summary>

public abstract class WebPage
{
    public Uri Url { get; set; }

    /// <summary>
    /// Get every WebPage.Internal on a web site (or part of a web site) visiting all internal links just once
    /// plus every external page (or other Url) linked to the web site as a WebPage.External
    /// </summary>
    /// <remarks>
    /// Use .OfType WebPage.Internal to get just the internal ones if that's what you want
    /// </remarks>
    public static IEnumerable<WebPage> GetAllPagesUnder(Uri urlRoot)
    {
        var queue = new Queue<Uri>();
        var allSiteUrls = new HashSet<Uri>();

        queue.Enqueue(urlRoot);
        allSiteUrls.Add(urlRoot);

        while (queue.Count > 0)
        {
            Uri url = queue.Dequeue();

            HttpWebRequest oReq = (HttpWebRequest)WebRequest.Create(url);
            oReq.UserAgent = @"Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.5) Gecko/20091102 Firefox/3.5.5";

            HttpWebResponse resp = (HttpWebResponse)oReq.GetResponse();

            WebPage result;

            if (resp.ContentType.StartsWith("text/html", StringComparison.InvariantCultureIgnoreCase))
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                try
                {
                    var resultStream = resp.GetResponseStream();
                    doc.Load(resultStream); // The HtmlAgilityPack
                    result = new Internal() { Url = url, HtmlDocument = doc };
                }
                catch (System.Net.WebException ex)
                {
                    result = new WebPage.Error() { Url = url, Exception = ex };
                }
                catch (Exception ex)
                {
                    ex.Data.Add("Url", url);    // Annotate the exception with the Url
                    throw;
                }

                // Success, hand off the page
                yield return new WebPage.Internal() { Url = url, HtmlDocument = doc };

                // And and now queue up all the links on this page
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes(@"//a[@href]"))
                {
                    HtmlAttribute att = link.Attributes["href"];
                    if (att == null) continue;
                    string href = att.Value;
                    if (href.StartsWith("javascript", StringComparison.InvariantCultureIgnoreCase)) continue;      // ignore javascript on buttons using a tags

                    Uri urlNext = new Uri(href, UriKind.RelativeOrAbsolute);

                    // Make it absolute if it's relative
                    if (!urlNext.IsAbsoluteUri)
                    {
                        urlNext = new Uri(urlRoot, urlNext);
                    }

                    if (!allSiteUrls.Contains(urlNext))
                    {
                        allSiteUrls.Add(urlNext);               // keep track of every page we've handed off

                        if (urlRoot.IsBaseOf(urlNext))
                        {
                            queue.Enqueue(urlNext);
                        }
                        else
                        {
                            yield return new WebPage.External() { Url = urlNext };
                        }
                    }
                }
            }
        }
    }

    ///// <summary>
    ///// In the future might provide all the images too??
    ///// </summary>
    //public class Image : WebPage
    //{
    //}

    /// <summary>
    /// Error loading page
    /// </summary>
    public class Error : WebPage
    {
        public int HttpResult { get; set; }
        public Exception Exception { get; set; }
    }

    /// <summary>
    /// External page - not followed
    /// </summary>
    /// <remarks>
    /// No body - go load it yourself
    /// </remarks>
    public class External : WebPage
    {
    }

    /// <summary>
    /// Internal page
    /// </summary>
    public class Internal : WebPage
    {
        /// <summary>
        /// For internal pages we load the document for you
        /// </summary>
        public virtual HtmlAgilityPack.HtmlDocument HtmlDocument { get; internal set; }
    }
}
