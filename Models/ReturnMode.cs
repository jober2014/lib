namespace WxCore.Models
{
    /// <summary>
    /// 反回公用对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReturnModels<T>
    {
        /// <summary>
        /// 返回对象
        /// </summary>
        public T RetrnData { get; set; }

        public bool HasError { get; set; }

        public string Message { get; set; }
  

    }
}
