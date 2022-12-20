using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Docs;
using Models.Users;
using Models.Utils.I18N;
using System.ComponentModel.DataAnnotations;

namespace Services.Docs.Requests
{
    public class PutRequest
    {

        public long DocTypeId { get; init; }

        public long CreatedUserId { get; init; }

        public long ReceiverUserId { get; init; }
    }
}