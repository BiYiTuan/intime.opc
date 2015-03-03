namespace Intime.OPC.Infrastructure.Auth
{
    /// <summary>
    ///     用户接口
    /// </summary>
    public interface IUser
    {
        string UserID { get; }

        /// <summary>
        ///     用户名
        /// </summary>
        /// <value>The name.<alue>
        string Name { get; }

        /// <summary>
        ///     昵称
        /// </summary>
        /// <value>The name of the pet.<alue>
        string ShowName { get; }
    }

    /// <summary>
    ///     角色接口
    /// </summary>
    public interface IRole
    {
        /// <summary>
        ///     角色名称
        /// </summary>
        /// <value>The name.<alue>
        string Name { get; set; }
    }
}