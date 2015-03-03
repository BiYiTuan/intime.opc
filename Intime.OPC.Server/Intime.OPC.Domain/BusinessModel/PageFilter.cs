using Intime.OPC.Domain.Base;

namespace Intime.OPC.Domain.BusinessModel
{
    public class PageFilter : BaseFilter
    {
        public int? Page { get; set; }

        public int? PageSize { get; set; }
    }

    public abstract class BaseFilter
    {
        private static readonly int? DefNullableInt = null;

        /// <summary>
        /// 检查是否为 NULL OR -1 返回 NULL
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        protected int? CheckIsNullOrAndSet(int? val)
        {
            return CheckIsNullOrAndSet(val, DefNullableInt);
        }

        /// <summary>
        /// 检查是否为 NULL OR -1 返回 defalueVal
        /// </summary>
        /// <param name="val"></param>
        /// <param name="defalueVal">默认值</param>
        /// <returns></returns>
        protected int? CheckIsNullOrAndSet(int? val, int? defalueVal)
        {
            if (val == null || val == -1)
            {
                return defalueVal;
            }

            return val;
        }
    }
}