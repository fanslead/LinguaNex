namespace LinguaNex.Translates.AI
{
    public class OpenAIHttpClientHandler : HttpClientHandler
    {
        string Url { get; set; }
        public OpenAIHttpClientHandler(string url)
        {
            Url = url;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            UriBuilder uriBuilder;
            switch (request.RequestUri.LocalPath)
            {
                case "/v1/chat/completions":
                    var uri = new UriBuilder(Url);
                    uriBuilder = new UriBuilder(request.RequestUri)
                    {
                        // 这里是你要修改的 URL
                        Scheme = uri.Scheme,
                        Host = uri.Host,
                        Port = uri.Port,
                        Path = "v1/chat/completions",
                    };
                    request.RequestUri = uriBuilder.Uri;
                    break;
            }
            await Console.Out.WriteLineAsync(await request.Content.ReadAsStringAsync());
            ;
            // 接着，调用基类的 SendAsync 方法将你的修改后的请求发出去
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            await Console.Out.WriteLineAsync(await response.Content.ReadAsStringAsync());
            return response;
        }
    }
}
