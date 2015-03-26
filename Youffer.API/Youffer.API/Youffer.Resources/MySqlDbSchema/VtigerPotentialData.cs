// ---------------------------------------------------------------------------------------------------
// <copyright file="VtigerPotentialData.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2015-01-12</date>
// <summary>
//     The VtigerPotentialData class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.MySqlDbSchema
{
    using System;
    using Enum;

    public class VtigerPotentialData
    {
        public bool MarkPurchased
        {
            get { return this.cf_857 == "1"; }
        }

        public bool IsAvailable
        {
            get { return this.cf_795 == "1"; }
        }

        public bool IsOnline
        {
            get { return this.cf_889 == "1"; }
        }

        public bool IsActive
        {
            get { return this.cf_835 == "1"; }
        }

        public Availability Availability
        {
            get { return string.IsNullOrWhiteSpace(this.cf_815) ? Availability.Undefined : (Availability)Enum.Parse(typeof(Availability), this.cf_815, true); }
        }

        public OSType OSType
        {
            get { return string.IsNullOrWhiteSpace(this.cf_763) ? OSType.Undefined : (OSType)Enum.Parse(typeof(OSType), this.cf_763); }
        }

        public bool CanCall
        {
            get { return this.cf_843 == "1"; }
        }

        public string[] MainInterest
        {
            get { return new string[] { }; }
        }

        public DateTime CreatedOn { get; set; }
        public string description { get; set; }
        public decimal Rank { get; set; }
        public string Id { get; set; }
        public string mailingcountry { get; set; }
        public int contactid { get; set; }
        public string contact_no { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string title { get; set; }
        public string secondaryemail { get; set; }
        public string donotcall { get; set; }

        public string cf_757 { get; set; }
        public string cf_759 { get; set; }
        public string cf_761 { get; set; }
        public string cf_763 { get; set; }
        public string cf_765 { get; set; }
        public string cf_795 { get; set; }
        public TimeSpan? cf_797 { get; set; }
        public TimeSpan? cf_799 { get; set; }
        public string cf_815 { get; set; }
        public decimal? cf_821 { get; set; }
        public decimal? cf_823 { get; set; }
        public string cf_835 { get; set; }
        public string cf_845 { get; set; }
        public string cf_847 { get; set; }
        public string cf_859 { get; set; }
        public string cf_889 { get; set; }
        public string Birthday { get; set; }
        public int potentialid { get; set; }

        public int? related_to { get; set; }
        public string potentialname { get; set; }
        public decimal? amount { get; set; }
        public string currency { get; set; }

        public string cf_841 { get; set; }
        public string cf_843 { get; set; }
        public string cf_853 { get; set; }
        public string cf_855 { get; set; }
        public string cf_857 { get; set; }
        public string cf_883 { get; set; }
    }
}
