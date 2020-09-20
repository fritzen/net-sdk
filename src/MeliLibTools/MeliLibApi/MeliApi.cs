using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using MeliLibTools.Client;
using MeliLibTools.Model;

namespace MeliLibTools.MeliLibApi
{


    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints (Sync)
    /// </summary>
    public interface IMeliApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Generic Get for Any Meli Resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="requestOptions"></param>
        /// <returns></returns>
        ApiResponse<Object> Get (string resource, RequestOptions requestOptions);
        /// <summary>
        /// Generic Put for Any Meli Resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="requestOptions"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        ApiResponse<Object> Put (string resource, RequestOptions requestOptions, object body = null);
        /// <summary>
        /// Generic Post for Any Meli Resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="requestOptions"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        ApiResponse<Object> Post (string resource, RequestOptions requestOptions, object body = null);
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints (Async)
    /// </summary>
    public interface IMeliApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Generic GetAsync for Any Meli Resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="requestOptions"></param>
        /// <returns></returns>
        Task<ApiResponse<Object>> GetAsync(string resource, RequestOptions requestOptions);
        /// <summary>
        /// Generic PutAsync for Any Meli Resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="requestOptions"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task<ApiResponse<Object>> PutAsync(string resource, RequestOptions requestOptions, object body = null);
        /// <summary>
        /// Generic PostAsync for Any Meli Resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="pararequestOptionsmeters"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task<ApiResponse<Object>> PostAsync(string resource, RequestOptions pararequestOptionsmeters, object body = null);
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IMeliApi : IMeliApiSync, IMeliApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class MeliApi : IMeliApi
    {
        private MeliLibTools.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <returns></returns>
        public MeliApi() : this((string) null)
        {
        }

        /// <summary>
        /// Initializes a new instance
        /// </summary>
        /// <returns></returns>
        public MeliApi(String basePath)
        {
            this.Configuration = MeliLibTools.Client.Configuration.MergeConfigurations(
                MeliLibTools.Client.GlobalConfiguration.Instance,
                new MeliLibTools.Client.Configuration { BasePath = basePath }
            );
            this.Client = new MeliLibTools.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new MeliLibTools.Client.ApiClient(this.Configuration.BasePath);
            this.ExceptionFactory = MeliLibTools.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public MeliApi(MeliLibTools.Client.Configuration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Configuration = MeliLibTools.Client.Configuration.MergeConfigurations(
                MeliLibTools.Client.GlobalConfiguration.Instance,
                configuration
            );
            this.Client = new MeliLibTools.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new MeliLibTools.Client.ApiClient(this.Configuration.BasePath);
            ExceptionFactory = MeliLibTools.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public MeliApi(MeliLibTools.Client.ISynchronousClient client,MeliLibTools.Client.IAsynchronousClient asyncClient, MeliLibTools.Client.IReadableConfiguration configuration)
        {
            if(client == null) throw new ArgumentNullException("client");
            if(asyncClient == null) throw new ArgumentNullException("asyncClient");
            if(configuration == null) throw new ArgumentNullException("configuration");

            this.Client = client;
            this.AsynchronousClient = asyncClient;
            this.Configuration = configuration;
            this.ExceptionFactory = MeliLibTools.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// The client for accessing this underlying API asynchronously.
        /// </summary>
        public MeliLibTools.Client.IAsynchronousClient AsynchronousClient { get; set; }

        /// <summary>
        /// The client for accessing this underlying API synchronously.
        /// </summary>
        public MeliLibTools.Client.ISynchronousClient Client { get; set; }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public String GetBasePath()
        {
            return this.Configuration.BasePath;
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public MeliLibTools.Client.IReadableConfiguration Configuration {get; set;}

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public MeliLibTools.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        /// Generic Get for Any Meli Resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="localVarRequestOptions"></param>
        /// <returns></returns>
        public ApiResponse<object> Get(string resource, RequestOptions localVarRequestOptions = null)
        {
            if (localVarRequestOptions == null)
            {
                localVarRequestOptions = new RequestOptions();
            }

            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "access_token", this.Configuration.AccessToken));

            // authentication (oAuth2AuthCode) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Get<Object>(resource, localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("MeliApiGet", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }
        /// <summary>
        /// Generic GetAsync for Any Meli Resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="localVarRequestOptions"></param>
        /// <returns></returns>
        public async Task<ApiResponse<object>> GetAsync(string resource, RequestOptions localVarRequestOptions = null)
        {
            if (localVarRequestOptions == null)
            {
                localVarRequestOptions = new RequestOptions();
            }

            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "access_token", this.Configuration.AccessToken));

            // authentication (oAuth2AuthCode) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<Object>(resource, localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("MeliApiGet", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }
        /// <summary>
        /// Generic Put for Any Meli Resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="localVarRequestOptions"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public ApiResponse<object> Put(string resource, RequestOptions localVarRequestOptions, object body = null)
        {
            
            localVarRequestOptions.Data = body;
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "access_token", this.Configuration.AccessToken));

            // authentication (oAuth2AuthCode) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Put<Object>(resource, localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("MeliPut", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }
        /// <summary>
        /// Generic PutAsync for Any Meli Resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="localVarRequestOptions"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<ApiResponse<object>> PutAsync(string resource, RequestOptions localVarRequestOptions, object body = null)
        {
            localVarRequestOptions.Data = body;
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "access_token", this.Configuration.AccessToken));

            // authentication (oAuth2AuthCode) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PutAsync<Object>(resource, localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("MeliPut", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }
        /// <summary>
        /// Generic Post for Any Meli Resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="localVarRequestOptions"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public ApiResponse<object> Post(string resource, RequestOptions localVarRequestOptions, object body = null)
        {
            localVarRequestOptions.Data = body;
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "access_token", this.Configuration.AccessToken));

            // authentication (oAuth2AuthCode) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request
            var localVarResponse = this.Client.Post<Object>(resource, localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("MeliPost", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }


        /// <summary>
        /// Generic PostAsync for Any Meli Resource
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="localVarRequestOptions"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<ApiResponse<object>> PostAsync(string resource, RequestOptions localVarRequestOptions, object body = null)
        {

            localVarRequestOptions.Data = body;
            localVarRequestOptions.QueryParameters.Add(ClientUtils.ParameterToMultiMap("", "access_token", this.Configuration.AccessToken));

            // authentication (oAuth2AuthCode) required
            // oauth required
            if (!String.IsNullOrEmpty(this.Configuration.AccessToken))
            {
                localVarRequestOptions.HeaderParameters.Add("Authorization", "Bearer " + this.Configuration.AccessToken);
            }

            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<Object>(resource, localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("MeliPost", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }
    }
}
