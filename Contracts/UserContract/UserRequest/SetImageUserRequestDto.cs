using System;

namespace Contracts.UserContract.UserRequest
{
    public class SetImageUserRequestDto
    {
        public Guid UserId { get; set; }
        public string ImageBaseString { get; set; }
    }
}
