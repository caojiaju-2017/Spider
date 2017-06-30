using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 数据库管理.Model
{
    public class Sps_user
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 帐号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string EMail { get; set; }
        /// <summary>
        /// 用户中文名
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 用户地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string OrgName { get; set; }
        /// <summary>
        /// 单位经度
        /// </summary>
        public float Lantudite { get; set; }
        /// <summary>
        /// 单位纬度
        /// </summary>
        public float Longdite { get; set; }
       
        public override string ToString()
        {
            return Alias;
        }
    }
}
