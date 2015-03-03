using System;

namespace Intime.OPC.Infrastructure.Job
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class JobHookAttribute : Attribute
    {
        /// <summary>
        /// Matched view name
        /// </summary>
        public string MatchedAuthorizedViewName { get; set; }

        /// <summary>
        /// Run every interval, in minutes.
        /// </summary>
        public int Interval { get; set; }
    }
}
