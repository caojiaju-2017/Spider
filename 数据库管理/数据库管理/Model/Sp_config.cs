using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 数据库管理.Model
{
    public class Sp_config
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 时间方案
        /// </summary>
        public string TimeType { get; set; }
        /// <summary>
        /// 时间段
        /// </summary>
        public string TimeSep { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string EMail { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 唯一编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 配置名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否生效
        /// </summary>
        public int Enable { get; set; }
        /// <summary>
        /// 工作类名字
        /// </summary>
        public string JobClassName { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
