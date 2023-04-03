﻿using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Ray.BiliTool.Blazor.Models;
using static System.Net.WebRequestMethods;

namespace Ray.BiliTool.Blazor.Services
{
    public interface IAccountService
    {
        Task LoginAsync(LoginParamsType model);
        Task<string> GetCaptchaAsync(string modile);
    }

    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly Random _random = new Random();

        public AccountService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task LoginAsync(LoginParamsType model)
        {
            await _httpClient.PostAsJsonAsync<LoginParamsType>($"api/Auth/Login", model);
        }

        public Task<string> GetCaptchaAsync(string modile)
        {
            var captcha = _random.Next(0, 9999).ToString().PadLeft(4, '0');
            return Task.FromResult(captcha);
        }
    }
}