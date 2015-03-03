namespace Intime.OPC.DataService.Utilities
{
    /// <summary>
    /// The IPaging interface.
    /// </summary>
    public interface IPaging
    {
        /// <summary>
        /// Gets or sets the page index.
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        int PageSize { get; set; }
    }
}
