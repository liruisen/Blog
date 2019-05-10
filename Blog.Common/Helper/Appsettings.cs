using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.IO;

namespace Blog.Common
{
    /// <summary>
    /// appsetting.json操作类
    /// </summary>
    public class Appsettings
    {
        static IConfiguration Configuration { get; set; }
        static Appsettings()
        {
            string Path = "appsettings.json";
            Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).Add(new JsonConfigurationSource { Path = Path, Optional = false, ReloadOnChange = true })//这样的话，可以直接读取目录里面的json文件而不是bin文件夹下的，所以不用修改复制属性
                .Build();
        }
        /// <summary>
        /// 封装要操作的字符
        /// </summary>
        /// <param name="sections"></param>
        /// <returns></returns>
        public static string app(params string[] sections)
        {
            try
            {
                var val = string.Empty;
                for (int i = 0; i < sections.Length; i++)
                {
                    val += sections[i] + ":";
                }
                return Configuration[val.TrimEnd(':')];
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
