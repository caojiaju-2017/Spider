using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 数据库管理.Model
{
    public class Sp_url_attr
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 归属URL的代码
        /// </summary>
        public string UrlCode { get; set; }
        /// <summary>
        /// HTML标签
        /// </summary>
        public string HtmlTag { get; set; }
        /// <summary>
        /// 属性名
        /// </summary>
        public string AttrName { get; set; }
        /// <summary>
        /// 属性别名
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 结果计算方式
        /// </summary>
        public string CalcWay { get; set; }
        /// <summary>
        /// 扩展信息
        /// </summary>
        public string ExternStr { get; set; }
        /// <summary>
        /// 子属性
        /// </summary>
        public string SubAttr { get; set; }
        /// <summary>
        /// 是否关联其他属性，如果是则填关联属性的Code
        /// </summary>
        public string AttachAttr { get; set; }
        /// <summary>
        /// 属性代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 属性对应值是否是链接
        /// </summary>
        public int IsUrl { get; set; }

        public override string ToString()
        {
            return Alias;
        }

    }
}
