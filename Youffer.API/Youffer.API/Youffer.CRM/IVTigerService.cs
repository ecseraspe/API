// ---------------------------------------------------------------------------------------------------
// <copyright file="IVTigerService.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Joginder Yadav</author>
// <date>2014-11-18</date>
// <summary>
//     The IVTigerService class
// </summary>
// ---------------------------------------------------------------------------------------------------

namespace Youffer.CRM
{
    using System;
    using System.Data;

    using Jayrock.Json;

    public interface IVTigerService
    {
        #region Info

        /// <summary>
        /// Retrieve a list of the different entity-types supported by VTiger (for development)
        /// </summary>
        /// <returns></returns>
        VTigerTypeInfo[] Listtypes();

        /// <summary>
        /// Retrieve a list of the different entity-types supported by VTiger (for development)
        /// </summary>
        /// <returns></returns>
        DataTable Listtypes_DataTable();

        /// <summary>
        /// Retrieves detailed information about a VTiger entity-type (for development)
        /// </summary>
        /// <param name="elementType"></param>
        /// <returns></returns>
        VTigerObjectType Describe(VTigerType elementType);

        /// <summary>
        /// Retrieves detailed information about a VTiger entity-type (for development)
        /// </summary>
        /// <param name="elementType"></param>
        /// <returns></returns>
        DataTable Describe_DataTable(VTigerType elementType);

        #endregion

        //====================================================================

        #region Query & Retrieve

        /// <summary>
        /// Retrieve a single element with the specified id
        /// </summary>
        /// <typeparam name="T">Expected result-type (derivate of VTigerEntity)</typeparam>
        /// <param name="id">VTiger-ID</param>
        /// <returns></returns>
        T Retrieve<T>(string id); //where T : JsonObject, VTigerEntity

        /// <summary>
        /// Retrieve a single element with the specified id as a DataTable with a single row
        /// </summary>
        /// <param name="id">VTiger-ID</param>
        /// <returns></returns>
        DataTable Retrieve(string id);

        /// <summary>
        /// Performs a query on the VTiger database
        /// </summary>
        /// <typeparam name="T">Expected type</typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        T VTiger_Query<T>(string query);

        /// <summary>
        /// Performs a query on the VTiger database and converts the result to an array of the desired type
        /// </summary>
        /// <typeparam name="T">Expected entity-type</typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <example>
        /// This query will return the first 10 contacts whose firstname begins with an "M"  
        /// <code>Query&lt;VTigerContact&gt;("SELECT * FROM Contacts WHERE firstname LIKE 'M%' ORDER BY firstname LIMIT 0,10");</code></example>      
        T[] Query<T>(string query) where T : VTigerEntity;

        /// <summary>
        /// Performs a query on the VTiger database and converts the result into a DataTable
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        DataTable Query(string query);

        #endregion

        //====================================================================

        #region Create

        /// <summary>
        /// Creates a new VTiger entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        T Create<T>(T element) where T : VTigerEntity;

        /// <summary>
        /// Creates a new VTiger entity and return the result as a DataTable
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        DataTable Create(VTigerEntity element);

        /// <summary>
        /// Creates a new VTiger entity and return the result as a DataTable
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        DataTable Create(VTigerType elementType, DataRow element);

        /// <summary>
        /// Creates a new empty, locally stored VTiger entity
        /// </summary>
        /// <param name="elementType"></param>
        /// <returns></returns>
        DataTable NewElement(VTigerType elementType);

        # endregion

        //====================================================================

        #region Update

        /// <summary>
        /// Updates an existing entity in the VTiger database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        T Update<T>(T element) where T : VTigerEntity;

        /// <summary>
        /// Updates an existing entity in the VTiger database
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        DataTable Update(DataRow element);

        /// <summary>
        /// Fetches each entry from a DataTable and updates the corrosponding entities in the VTiger database
        /// </summary>
        /// <param name="elements"></param>
        /// <returns></returns>
        DataTable UpdateTable(DataTable elements);

        #endregion

        //====================================================================

        #region Delete & Sync

        /// <summary>
        /// Delete an element from the database
        /// </summary>
        /// <param name="id">VTiger-ID</param>
        void Delete(string id);

        JsonObject Sync(DateTime modifiedTime);

        JsonObject Sync(DateTime modifiedTime, VTigerType elementType);

        #endregion

        //====================================================================

        #region Searches

        /// <summary>
        /// Creates a new VTigerQueryWriter and initializes it with default search-parameters.
        /// Empty parameters are excluded from the search.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="PrimaryCol">Primary search-column-name</param>
        /// <param name="OptionalCols">Optional search-column-names</param>
        /// <param name="DateCol">Column for date-search</param>
        /// <param name="PrimaryText"></param>
        /// <param name="OptionalText"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <returns>Returns the initialized VTigerQueryWriter</returns>       
        VTigerQueryWriter DefaultSearchQuery(VTigerType table,
           string PrimaryCol, string PrimaryText,
           string[] OptionalCols, string OptionalText,
           string DateCol, DateTime FromDate, DateTime ToDate);

        /// <summary>
        /// Searches for an entity which matches the specified condition and retrives it's ID
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="field">The field of the entity which should match the specified value</param>
        /// <param name="value"></param>
        /// <returns></returns>
        string FindEntityID(VTigerType elementType, string field, string value);

        /// <summary>
        /// Searches for an entity which matches the specified condition and retrives it's data
        /// </summary>
        /// <param name="elementType"></param>
        /// <param name="field">The field of the entity which should match the specified value</param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// public T Create<T>(T element) where T : VTigerEntity
        T FindEntity<T>(string field, string value) where T : VTigerEntity, new();

        #region Default Searches

        /// <summary>
        /// Returns a default search-query
        /// </summary>
        /// <remarks>
        /// Default search-attributes:
        /// Primary-Column: invoice_no
        /// Optional-Columns: subject, hdnGrandTotal, hdnSubTotal, hdnDiscountAmount, txtAdjustment, terms_conditions
        /// Date-Column: invoicedate
        /// </remarks>
        /// <param name="invoice_no"></param>
        /// <param name="searchText"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        /// <seealso cref="VTigerApi.VTiger.DefaultSearchQuery"/>
        VTigerQueryWriter DefaultSearchInvoices(string invoice_no, string searchText, DateTime fromDate, DateTime toDate);

        #endregion

        #region Default GetID-functions

        string GetUserID(string name);

        string GetAccountID(string name);

        string GetProductID(string name);

        string GetCampaignID(string name);

        string GetServiceID(string name);

        string GetAssetID(string name);

        string GetProjectTaskID(string name);

        string GetProjectID(string name);

        string GetGroupID(string name);

        string GetCurrencyID(string name);

        #endregion

        #endregion

        //====================================================================

        #region Default Add-functions

        VTigerUserReviews AddUserReview(VTigerUserReviews review);

        VTigerCompanyNotes AddCompanyNotes(string note, string assigned_user_id, string contact_id, string related_to);

        VTigerCalendar AddCalendar(string user_id, string subject, DateTime date_start, DateTime due_date, TaskStatus taskStatus);

        VTigerLead AddLead(string lastname, string company, string assigned_user_id);

        VTigerAccount AddAccount(string accountname, string assigned_user_id);

        VTigerContact AddContact(string firstname, string lastname, string user_id);

        VTigerPotential AddPotential(string potentialname, string related_to, string closingdate, Sales_stage sales_stage, string assigned_user_id);

        VTigerProduct AddProduct(string productname);

        VTigerDocument AddDocument(string notes_title, string assigned_user_id);

        VTigerEmail AddEmail(string subject, DateTime date_start, string from_email, string[] saved_toid, string assigned_user_id);

        VTigerHelpDesk AddHelpDesk(string assigned_user_id, Ticketstatus ticketstatus, string ticket_title);

        VTigerFaq AddFaq(Faqstatus faqstatus, string question, string faq_answer);

        VTigerVendor AddVendor(string vendorname);

        VTigerPriceBook AddPriceBook(string bookname, string currency_id);

        VTigerQuote AddQuote(string subject, Quotestage quotestage, string bill_street, string ship_street, string account_id, string assigned_user_id);

        VTigerPurchaseOrder AddPurchaseOrder(string subject, string vendor_id, PoStatus postatus, string bill_street, string ship_street, string assigned_user_id);

        VTigerSalesOrder AddSalesOrder(string subject, SoStatus sostatus, string bill_street, string ship_street, Invoicestatus invoicestatus, string account_id, string assigned_user_id);

        VTigerInvoice AddInvoice(string subject, string bill_street, string ship_street, string account_id, string assigned_user_id);

        VTigerCampaign AddCampaign(string campaignname, DateTime closingdate, string assigned_user_id);

        VTigerEvent AddEvent(string subject, string date_start, string time_start, string due_date, string time_end, int duration_hours, Eventstatus eventstatus, Activitytype activitytype, string assigned_user_id);

        VTigerPBXManager AddPBXManager(string callfrom, string callto);

        VTigerServiceContract AddServiceContract(string subject, string assigned_user_id);

        VTigerService AddService(string servicename);

        VTigerAsset AddAsset(string product, string serialnumber, string datesold, string dateinservice, Assetstatus assetstatus, string assetname, string account, string assigned_user_id);

        #endregion 
    }
}
