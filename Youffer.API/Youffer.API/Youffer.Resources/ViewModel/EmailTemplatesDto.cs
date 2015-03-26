// ---------------------------------------------------------------------------------------------------
// <copyright file="EmailTemplatesDto.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2015-01-16</date>
// <summary>
//     The EmailTemplatesDto class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.ViewModel
{
    using System;
    using Enum;

    /// <summary>
    /// THe Email Template class
    /// </summary>
    public class EmailTemplatesDto
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailTemplatesDto"/> class.
        /// </summary>
        public EmailTemplatesDto()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsActive = true;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>  
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
