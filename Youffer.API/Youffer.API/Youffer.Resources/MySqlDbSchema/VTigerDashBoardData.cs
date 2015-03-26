// ---------------------------------------------------------------------------------------------------
// <copyright file="VTigerDashBoardData.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2015-01-06</date>
// <summary>
//     The VTigerDashBoardData class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.Resources.MySqlDbSchema
{
    using System;
    using System.Linq;
    using Enum;

    /// <summary>
    /// The VTigerDashBoardData class
    /// </summary>
    public class VTigerDashBoardData
    {
        public int id { get; set; }
        public int leadid { get; set; }
        public string interests { get; set; }
        public decimal Rank { get; set; }
        public decimal Distance { get; set; }

        public string[] SubInterest
        {
            get
            {
                if (!string.IsNullOrEmpty(this.interests))
                {
                    return
                        this.interests.Split(new string[] { " |##| " }, StringSplitOptions.None)
                            .Where(x => !string.IsNullOrEmpty(x))
                            .ToArray();
                }

                return null;
            }
        }

        public bool IsAvailable
        {
            get { return this.cf_803 == "1"; }
        }

        public bool IsOnline
        {
            get { return this.cf_891 == "1"; }
        }

        public bool IsActive
        {
            get { return this.cf_767 == "1"; }
        }

        public Availability Availability
        {
            get { return string.IsNullOrWhiteSpace(this.cf_813) ? Availability.Undefined : (Availability)Enum.Parse(typeof(Availability), this.cf_813, true); }
        }

        public OSType OSType
        {
            get { return string.IsNullOrWhiteSpace(this.cf_791) ? OSType.Undefined : (OSType)Enum.Parse(typeof(OSType), this.cf_791); }
        }

        public string[] MainInterest
        {
            get { return new string[] { }; }
        }

        public string description { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Birthday { get; set; }
        public string city { get; set; }
        public string code { get; set; }
        public string state { get; set; }
        public string pobox { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string mobile { get; set; }
        public string fax { get; set; }
        public string lane { get; set; }
        public string leadaddresstype { get; set; }
        public string lead_no { get; set; }
        public string email { get; set; }
        public string interest { get; set; }
        public string firstname { get; set; }
        public string salutation { get; set; }
        public string lastname { get; set; }
        public string company { get; set; }
        public decimal? annualrevenue { get; set; }
        public string campaign { get; set; }
        public string rating { get; set; }
        public string leadstatus { get; set; }
        public string leadsource { get; set; }
        public int? converted { get; set; }
        public string designation { get; set; }
        public string licencekeystatus { get; set; }
        public string space { get; set; }
        public string comments { get; set; }
        public string priority { get; set; }
        public string demorequest { get; set; }
        public string partnercontact { get; set; }
        public string productversion { get; set; }
        public string product { get; set; }
        public DateTime? maildate { get; set; }
        public DateTime? nextstepdate { get; set; }
        public string fundingsituation { get; set; }
        public string purpose { get; set; }
        public string evaluationstatus { get; set; }
        public DateTime? transferdate { get; set; }
        public string revenuetype { get; set; }
        public int? noofemployees { get; set; }
        public string secondaryemail { get; set; }
        public int? assignleadchk { get; set; }
        public string emailoptout { get; set; }
        public TimeSpan? cf_751 { get; set; }
        public string cf_753 { get; set; }
        public string cf_767 { get; set; }
        public string cf_769 { get; set; }
        public string cf_771 { get; set; }
        public string cf_773 { get; set; }
        public DateTime? cf_783 { get; set; }
        public string cf_785 { get; set; }
        public string cf_787 { get; set; }
        public string cf_789 { get; set; }
        public string cf_791 { get; set; }
        public string cf_793 { get; set; }
        public string cf_803 { get; set; }
        public TimeSpan? cf_807 { get; set; }
        public TimeSpan? cf_809 { get; set; }
        public string cf_813 { get; set; }
        public decimal? cf_825 { get; set; }
        public decimal? cf_827 { get; set; }
        public string cf_849 { get; set; }
        public string cf_851 { get; set; }
        public string cf_861 { get; set; }
        public string cf_885 { get; set; }
        public string cf_891 { get; set; }
        public string cf_942 { get; set; }
    }
}
