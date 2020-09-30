using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace WebStore.Domain.DTO.Identity
{
    public abstract class ClaimDTO : UserDTO // claim - утверждения, доп.инфо, которая м.б. указана для пользователя или роли
    {
        public IEnumerable<Claim> Claims { get; set; }
    }

    public class AddClaimDTO : ClaimDTO {}

    public class RemoveClaimDTO : ClaimDTO { }

    public class ReplaceClaimDTO : UserDTO
    { 
        public Claim Claim { get; set; } // старое утверждение

        public Claim NewClaim { get; set; } // новое утверждение
    }

}
