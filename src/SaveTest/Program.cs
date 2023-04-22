using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

// 创建一个HttpClient实例
using HttpClient client = new HttpClient();
// 设置请求头部
// 设置Authorization头部，假设你的secret是"my_secret"
// 发送GET请求，获取代理信息
var response = await client.GetAsync("https://raw.githubusercontent.com/Loyalsoldier/clash-rules/release/direct.txt");
// 检查响应状态码
response.EnsureSuccessStatusCode();
// 读取响应内容
var content = await response.Content.ReadAsStringAsync();
// 打印响应内容
Console.WriteLine(content);