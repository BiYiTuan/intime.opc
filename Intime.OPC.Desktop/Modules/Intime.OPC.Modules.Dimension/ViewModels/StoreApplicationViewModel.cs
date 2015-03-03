// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StoreApplicationViewModel.cs" company="Intime">
//   Copyright @Intime 2014
// </copyright>
// <summary>
//   The store application view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.ComponentModel.Composition;

namespace Intime.OPC.Modules.Dimension.ViewModels
{
    /// <summary>
    /// The store application view model.
    /// </summary>
    [Export(typeof(StoreApplicationViewModel))]
    public class StoreApplicationViewModel
    {
        /// <summary>
        /// Gets or sets the approval view model.
        /// </summary>
        [Import]
        public ApprovalViewModel ApprovalViewModel { get; set; }

        /// <summary>
        /// Gets or sets the associate view model.
        /// </summary>
        [Import]
        public AssociateViewModel AssociateViewModel { get; set; }
    }
}
