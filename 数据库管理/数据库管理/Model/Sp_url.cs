using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 数据库管理.Model
{
    public class Sp_url
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 索引开始
        /// </summary>
        public int StartIndex { get; set; }
        /// <summary>
        /// 索引结束
        /// </summary>
        public int StopIndex { get; set; }
        /// <summary>
        /// 步长
        /// </summary>
        public int Step { get; set; }
        /// <summary>
        /// 基础URL
        /// </summary>
        public string BaseUrl { get; set; }
        /// <summary>
        /// 动态变化URL
        /// </summary>
        public string ShortUrl { get; set; }
        /// <summary>
        /// 循环模式
        /// </summary>
        public int LoopType { get; set; }
        /// <summary>
        /// 网站名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 网站别名
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// Sheet名称
        /// </summary>
        public string Sheet { get; set; }
        /// <summary>
        /// 归属配置Code
        /// </summary>
        public string ConfigId { get; set; }
        /// <summary>
        /// 记录的唯一索引
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 是否生效
        /// </summary>
        public int Enable { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string Classfic { get; set; }
    }
}
