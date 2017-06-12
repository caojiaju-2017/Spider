using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpiderC.Data
{
    public class MultiBatchDatas
    {
        object _dataChangeLock = new object();

        List<object> _currentDatas = new List<object>();
        int _pageSize = 20;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        public List<object> DataSource
        {
            get { return _currentDatas; }
            set { _currentDatas = value; }
        }

        int _currentIndex = 0;

        /// <summary>
        /// 获取翻页信息
        /// </summary>
        public string GetBrowserString
        {
            get { return "0/1页"; }
        }

        public int CurrentIndex
        {
            get { return _currentIndex; }
            set { _currentIndex = value; }
        }

        public void appendNewDatas(List<object> newDatas)
        {
            lock(_dataChangeLock)
            {
                _currentDatas.AddRange(newDatas);
            }
        }

        /// <summary>
        /// 清理结果
        /// </summary>
        public void clearResult()
        {
            lock (_dataChangeLock)
            {
                _currentDatas.Clear();
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="index">当前删除对象在列表中的位置</param>
        /// <param name="rmObj">删除对象句柄</param>
        /// <returns>返回listview刷新的部分</returns>
        public List<object> removeObject(int index,object rmObj)
        {
            lock (_dataChangeLock)
            {
                _currentDatas.Remove(rmObj);
            }

            return null;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="index"></param>
        /// <param name="rmObj"></param>
        public void updateObject(int index,object rmObj)
        {
            lock (_dataChangeLock)
            {
            }
        }

        /// <summary>
        /// 获取当前显示的数据
        /// </summary>
        public void getCurrentObjects()
        {
            lock (_dataChangeLock)
            {

            }
        }

        /// <summary>
        /// 是否为第一页
        /// </summary>
        /// <returns></returns>
        public bool isFirstPage()
        {
            return true;
        }

        /// <summary>
        /// 是否为最后一页
        /// </summary>
        /// <returns></returns>
        public bool isEndPage()
        {
            return true;
        }
    }
}
