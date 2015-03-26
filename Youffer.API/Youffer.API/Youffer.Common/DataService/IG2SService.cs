// ---------------------------------------------------------------------------------------------------
// <copyright file="IG2SService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gaurav Barar</author>
// <date>2014-12-28</date>
// <summary>
//     The IG2SService interface
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Common.DataService
{
    /// <summary>
    /// Interface ICommonService
    /// </summary>
    public interface IG2SService
    {
        /// <summary>
        /// Sets the G2S payment transaction data.
        /// </summary>
        /// <returns>Set was successful or not.</returns>
        bool SaveTransaction();
    }
}
