// ---------------------------------------------------------------------------------------------------
// <copyright file="BaseCrmModel.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-21</date>
// <summary>
//     The BaseCrmModel class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.CRMModel
{
    /// <summary>
    /// The BaseCrmModel class.
    /// </summary>
    public abstract class BaseCrmModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCrmModel" /> class.
        /// </summary>
        public BaseCrmModel()
        {
            this.Assigned_User_Id = "19x1";
        }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>The id.</value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the assigneduserid.
        /// </summary>
        /// <value>The assigneduseridentifier.</value>
        public string Assigned_User_Id { get; set; }
    }
}
