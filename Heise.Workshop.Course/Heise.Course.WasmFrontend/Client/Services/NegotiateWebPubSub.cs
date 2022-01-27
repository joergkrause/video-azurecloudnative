using System.Text.Json.Serialization;

namespace Heise.Course.WasmFrontend.Client.Services {
	public class NegotiateWebPubSub : INegotiateWebPubSub {
		[JsonPropertyName("baseUrl")]
		public string BaseUrl { get; set; }
		[JsonPropertyName("url")]
		public string Url { get; set; }
		[JsonPropertyName("accessToken")]
		public string AccessToken { get; set; }
	}
}
