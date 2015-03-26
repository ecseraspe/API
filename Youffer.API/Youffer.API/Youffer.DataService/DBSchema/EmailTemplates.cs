// ---------------------------------------------------------------------------------------------------
// <copyright file="EmailTemplates.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2015-01-16</date>
// <summary>
//     The EmailTemplates class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.DataService.DBSchema
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Resources.Enum;

    /// <summary>
    /// THe Email Template class
    /// </summary>
    public class EmailTemplates
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailTemplates"/> class.
        /// </summary>
        public EmailTemplates()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsActive = true; 
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary> 
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets the created on.
        /// </summary>
        public DateTime CreatedOn { get; private set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary> 
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary> 
        public TemplateType TemplateType { get; set; }

        /// <summary>
        /// Gets or sets the Template Name.
        /// </summary> 
        public string TemplateName { get; set; }

        /// <summary>
        /// Gets or sets the Template Html.
        /// </summary> 
        public string TemplateHtml { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deleted.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
