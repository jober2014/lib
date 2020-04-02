using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WxCore.Command
{
    /// <summary>
    /// 自定义缓存类
    /// </summary>
    public sealed class OwnCache
    {
        /// <summary>
        /// 缓存数据集
        /// </summary>
        private static List<DataMode> OcData = new List<DataMode>();
        private static object ob = new object();
        private static Thread th = null;
        static OwnCache()
        {
            if (th == null)
            {
                th = new Thread(RunTask);
                th.IsBackground = true;
            }
            if (th.ThreadState != ThreadState.Running)
            {
                th.Start();
            }
        }
        private static void RunTask()
        {
            //缓存数据监控
            while (true)
            {
                var data = OcData.Where(r => r.Time < DateTime.Now && r.Time.Year > 1900);
                if (data.Any())
                {
                    OcData.RemoveAll(r => r.Time < DateTime.Now && r.Time.Year > 1900);
                }
                else
                {
                    //降低CPU资源消耗
                    Thread.Sleep(1000);
                }
            }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetData(String key)
        {
            var rdata = OcData.Where(w => w.Key == key);
            return rdata != null ? rdata.FirstOrDefault() : null;
        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="Value"></param>
        public static void Add(String key, object Value)
        {

            var rdata = OcData.Where(w => w.Key == key);
            lock (ob)
            {
                if (rdata != null && rdata.Any())
                {
                    rdata.ToList().ForEach(f => f.Value = Value);
                }
                else
                {
                    OcData.Add(new DataMode { Key = key, Value = Value });
                }
            }

        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="Value">值</param>
        /// <param name="ts">过期时间间隔</param>
        public static void Add(String key, object Value, TimeSpan ts)
        {

            var rdata = OcData.Where(w => w.Key == key);
            lock (ob)
            {
                if (rdata != null && rdata.Any())
                {
                    rdata.ToList().ForEach(f =>
                    {
                        f.Value = Value;
                        f.Time = DateTime.Now.Add(ts);
                    });
                }
                else
                {
                    OcData.Add(new DataMode { Key = key, Value = Value, Time = DateTime.Now.Add(ts) });
                }
            }

        }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="Value">值</param>
        /// <param name="ts">指定过期时间（该时间必需大于当前时间）</param>
        public static void Add(String key, object Value, DateTime dt)
        {

            var rdata = OcData.Where(w => w.Key == key);
            lock (ob)
            {
                if (rdata != null && rdata.Any())
                {
                    rdata.ToList().ForEach(f =>
                    {
                        f.Value = Value;
                        f.Time = dt;
                    });
                }
                else
                {
                    OcData.Add(new DataMode { Key = key, Value = Value, Time = dt });
                }
            }

        }
        /// <summary>
        /// 缓存清理
        /// </summary>
        public static void Clear()
        {
            OcData.Clear();
        }
        public static void Clear(String key)
        {
            OcData.RemoveAll(r => r.Key == key);
        }
        /// <summary>
        /// 数据模型
        /// </summary>
        private class DataMode
        {
            /// <summary>
            /// 键
            /// </summary>
            public String Key { get; set; }
            /// <summary>
            /// 值
            /// </summary>
            public object Value { get; set; }
            /// <summary>
            /// 时间
            /// </summary>
            public DateTime Time { get; set; }
        }

    }
}
