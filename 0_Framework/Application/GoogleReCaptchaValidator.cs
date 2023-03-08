using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using _0_Framework.Domain;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace _0_Framework.Application
{
    public class GoogleReCaptchaValidator : ICaptchaValidator
    {

        private readonly HttpClient _httpClient;
        private readonly GoogleReCaptchaConfig _googleReCaptchaConfig;
        private const string RemoteAddress = "https://www.google.com/recaptcha/api/siteverify";
        private string _secretKey;
        private readonly double _minimumScore;

        public GoogleReCaptchaValidator(HttpClient httpClient, IConfiguration configuration,
            GoogleReCaptchaConfig googleReCaptchaConfig)
        {
            _httpClient = httpClient;
            _googleReCaptchaConfig = googleReCaptchaConfig;

            _secretKey = configuration["googleReCaptcha:SecretKey"];
            _minimumScore = double.Parse(configuration["googleReCaptcha:MinimumScore"]);
        }

        public async Task<bool> IsCaptchaPassedAsync(string token)
        {
            dynamic response = await GetCaptchaResultDataAsync(token);
            if (response.success == "true")
            {
                return Convert.ToDouble(response.score) >= _minimumScore;
            }
            return false;
        }

        public async Task<JObject> GetCaptchaResultDataAsync(string token)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("secret", _secretKey),
                new KeyValuePair<string, string>("response", token)
            });
            var res = await _httpClient.PostAsync(RemoteAddress, content);
            if (res.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpRequestException(res.ReasonPhrase);
            }
            var jsonResult = await res.Content.ReadAsStringAsync();
            return JObject.Parse(jsonResult);
        }

        public void UpdateSecretKey(string key)
        {
            _secretKey = key;
        }
    }
}
