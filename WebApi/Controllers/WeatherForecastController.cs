using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectStructureDemo.Entities;
using ProjectStructureDemo.Entities.Core;
using ProjectStructureDemo.IServices;
using WebApi.Base;
using WebApi.Extensions;
namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ApiBaseController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUserService _userService;
        public WeatherForecastController(ILogger<WeatherForecastController> logger,IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        private async Task<IList<string>> Add()
        {
            GC.Collect();
            IList<User> list = new List<User>();
            IList<User> list1 = new List<User>();
            IList<User> list2 = new List<User>();
            IList<User> list3 = new List<User>();
            IList<User> list4 = new List<User>();
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (var i = 0;i< 1000000; i++)
            {
                User user = new User();
                user.UserID = Guid.NewGuid().ToSequentialGuid();
                user.UserName = GetNames(1).First<string>();
                user.CellPhone = GetTels(1).First<string>();
                user.CountryCode = "0064";
                user.CreateTime = DateTime.Now;
                if (i < 200001) { list.Add(user); }
                else if(i > 200000 && i <400001)
                {
                    list1.Add(user);
                }
                else if (i > 400000 && i < 600001)
                {
                    list2.Add(user);
                }
                else if (i > 600000 && i < 800001)
                {
                    list3.Add(user);
                }
                else
                {
                    list4.Add(user);
                }
            }
            //—————
            //此处为要计算的运行代码
            //—————
            watch.Stop();
            //获取当前实例测量得出的总运行时间（以毫秒为单位）
            string uuidCreateTime = watch.ElapsedMilliseconds.ToString();
            watch.Start();
            var result = await _userService.InsertBulkAsync(list);
            var result1 = await _userService.InsertBulkAsync(list1);
            await _userService.InsertBulkAsync(list2);
            await _userService.InsertBulkAsync(list3);
            await _userService.InsertBulkAsync(list4);
            watch.Stop();
            //获取当前实例测量得出的总运行时间（以毫秒为单位）
            string dbInsertTime = watch.ElapsedMilliseconds.ToString();
            if (result > 0)
            {
                IList<string> res = new List<string>();
                res.Add(string.Format("createUUIDTime:{0}ms.",uuidCreateTime));
                res.Add(string.Format("insert to db(1000000):{0}ms.", dbInsertTime));
                return res;
            }
            else
            {
                return null;
            }
        }
        [HttpGet]
        [Route("/list")]
        public async Task<Page<User>> List(int pageIndex=1,int pageSize=10)
        {
            Page<User> userList = new Page<User>();
            userList = await _userService.GetPageListAsync(pageIndex, pageSize);
            return userList;
        }
        [HttpGet]
        [Route("/user/name")]
        public async Task<string> UserName(string id)
        {
            var time = DateTime.Now;
            var ns = DateTime.Now.Ticks;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 200; i++)
            {
                sb.Append("\r\n" + DateTime.Now.Ticks/1000);
            }
            return $"time:{time},um:{sb.ToString()}.";
            //return await _userService.GetUserNameAsync(id);
        }
            public static List<string> GetTels(int count)
        {
            List<string> tels = new List<string>() { };
            string[] tel1 = new string[] { "130", "187", "156", "179", "188", "199", "180", "176", "190" };
            Random rad = new Random();//实例化随机数产生器rad；
            for (int i = 0; i < count; i++)
            {
                string tel = tel1[rad.Next(0, tel1.Length - 1)] + rad.Next(1000, 10000) + rad.Next(1000, 10000);
                if (tels.Contains(tel) == false)
                {
                    tels.Add(tel);
                }
                else
                {
                    i--;
                }
            }
            return tels;
        }
        private  List<string> GetNames(int count)
        {
            Random ran = new Random();
            List<string> s = new List<string> { };
            string[] nameS3 = new string[] { "赵", "钱", "孙", "李", "周", "吴", "郑", "王", "冯",
 "陈", "褚", "卫", "蒋", "沈", "韩", "杨", "朱", "秦", "尤", "许", "何", "吕", "施",
 "张", "孔", "曹", "严", "华", "金", "魏", "陶", "姜", "戚", "谢", "邹", "喻", "柏",
 "水", "窦", "章", "云", "苏", "潘", "葛", "奚", "范", "彭", "郎" };
            string[] nameS2 = new string[] {"鲁","韦","昌","马","苗","凤","花","方","俞","任","袁"
 ,"柳","酆","鲍","史","唐","费","廉","岑","薛","雷","贺","倪","汤","滕","殷","罗",
 "毕","郝","邬","安","常","乐","于","时","傅","皮","卞","齐","康","伍","余","元",
 "卜","顾","孟","平","黄"};
            string[] nameS1 = new string[] { "梅", "盛", "林", "刁", "锺", "徐", "邱", "骆", "高",
 "夏", "蔡", "樊", "胡", "凌", "霍", "虞", "万", "支", "柯", "昝", "管", "卢", "莫",
 "经", "房", "裘", "缪", "干", "解", "应", "宗", "丁", "宣", "贲", "邓", "郁", "单",
 "杭", "洪", "包", "诸", "左", "石", "崔", "吉", "钮", "龚", "程", "嵇", "邢", "滑",
 "裴", "陆", "荣", "翁", "荀", "羊", "於", "惠", "甄", "麴", "家", "封", "芮", "羿",
 "储", "靳", "汲", "邴", "糜", "松", "井" };
            for (int i = 0; i < count; i++)
            {
                string s1 = nameS1[ran.Next(0, nameS1.Length - 1)];
                string s2 = nameS2[ran.Next(0, nameS2.Length - 1)];
                string s3 = nameS3[ran.Next(0, nameS3.Length - 1)];
                string name = s1 + s2 + s3;
                if (!s.Contains(name))
                {
                    s.Add(name);
                }
                else
                {
                    i--;
                }
            }
            return s;
        }
    }
}
