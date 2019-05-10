using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Model;
using Blog.Web.AuthHelper.OverWrite;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        /// <summary>
        /// 获取指定Id的数据
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public Love Post([FromBody] Love value)
        {
            return value;
        }

        // PUT api/values/5
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        /// <summary>
        /// JWT颁发测试
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [HttpGet("getjwt")]
        public object GetJwtStr(string name,string pass)
        {
            string jwtStr = string.Empty;
            bool suc = false;
            var user =  "Admin";
            if (user!=null)
            {
                TokenModelJwt tokenModel = new TokenModelJwt()
                {
                    Uid = 1,
                    Role = user
                };
                jwtStr = JwtHelper.IssueJwt(tokenModel);
                suc = true;
            }
            else
            {
                jwtStr = "login fail";
            }
            return Ok(new
            {
                success = suc,
                token = jwtStr
            });
        }
    }
}
