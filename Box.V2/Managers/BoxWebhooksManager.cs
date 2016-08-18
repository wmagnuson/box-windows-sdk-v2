﻿using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Converter;
using Box.V2.Models;
using Box.V2.Models.Request;
using Box.V2.Services;
using Box.V2.Extensions;
using System.Threading.Tasks;

namespace Box.V2.Managers
{
    public class BoxWebhooksManager : BoxResourceManager
    {
        public BoxWebhooksManager(IBoxConfig config, IBoxService service, IBoxConverter converter, IAuthRepository auth, string asUser = null, bool? suppressNotifications = null)
            : base(config, service, converter, auth, asUser, suppressNotifications) { }

        public async Task<BoxWebhook> CreateWebhookAsync(BoxWebhookRequest webhookRequest)
        {
            BoxRequest request = new BoxRequest(_config.WebhooksUri)
                .Method(RequestMethod.Post)
                .Payload(_converter.Serialize<BoxWebhookRequest>(webhookRequest));

            IBoxResponse<BoxWebhook> response = await ToResponseAsync<BoxWebhook>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        public async Task<BoxWebhook> GetWebhookAsync(string id)
        {
            id.ThrowIfNullOrWhiteSpace("id");

            BoxRequest request = new BoxRequest(_config.WebhooksUri, id);

            IBoxResponse<BoxWebhook> response = await ToResponseAsync<BoxWebhook>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }

        public async Task<BoxWebhook> UpdateWebhookAsync(BoxWebhookRequest webhookRequest)
        {
            webhookRequest.ThrowIfNull("webhookRequest")
                .Id.ThrowIfNullOrWhiteSpace("webhookRequest.Id");

            BoxRequest request = new BoxRequest(_config.WebhooksUri, webhookRequest.Id)
                .Method(RequestMethod.Put)
                .Payload(_converter.Serialize<BoxWebhookRequest>(webhookRequest));

            IBoxResponse<BoxWebhook> response = await ToResponseAsync<BoxWebhook>(request).ConfigureAwait(false);

            return response.ResponseObject;
        }
    }
}
