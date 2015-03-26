// ---------------------------------------------------------------------------------------------------
// <copyright file="MessageTemplates.cs" company="Youffer">
//     Copyright (c) 2015 All Right Reserved
// </copyright>
// <author>Akanksha Vats</author>
// <date>2015-01-27</date>
// <summary>
//     The MessageTemplates class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.DataService.DBSchema
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Youffer.Resources.Enum;

    /// <summary>
    /// Class MessageTemplates
    /// </summary>
    public class MessageTemplates
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageTemplates"/> class.
        /// </summary>
        public MessageTemplates()
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
        /// Gets or sets the type of the template.
        /// </summary>
        /// <value>
        /// The type of the template.
        /// </value>
        public MessageTemplateType TemplateType { get; set; }

        /// <summary>
        /// Gets or sets the Template Name.
        /// </summary> 
        public string TemplateName { get; set; }

        /// <summary>
        /// Gets or sets the template text.
        /// </summary>
        public string TemplateText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
