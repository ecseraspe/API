// ---------------------------------------------------------------------------------------------------
// <copyright file="SendMessageFromCrmDto.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2015-01-13</date>
// <summary>
//     The SendMessageFromCrmDto class
// </summary>
// ---------------------------------------------------------------------------------------------------
 
namespace Youffer.Resources.MySqlDbSchema
{
    /// <summary>
    /// The SendMessageFromCrmDto class
    /// </summary>
    public class SendMessageFromCrmDto
    {
        public int sendmessageid { get; set; }

        public int sendmessage_tks_sendto { get; set; }

        public string sendmessage_tks_message { get; set; }

        public string setype { get; set; }

        public string GCMId { get; set; }

        public string UDId { get; set; }
    }
}
