using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 数据库管理.Model
{
    public class Sps_service
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 帐号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户中文名
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// 用户地址
        /// </summary>
        public string Info { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
