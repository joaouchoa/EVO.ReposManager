using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Application.ValidationMessages
{
    public static class RepoValidationMessages
    {
        public const string NOT_EMPTY_ERROR_MESSAGE = "{PropertyName} cannot be empty.";
        public const string MAX_LENGTH_ERROR_MESSAGE = "{PropertyName} must not reach {MaxLength} characters.";
        public const string MATCHES_ERROR_MESSAGE = "{PropertyName} can only contain lowercase letters, numbers, and hyphens.";
        public const string ID_MATCHES_ERROR_MESSAGE = "{PropertyName} can only contain numbers.";
        public const string START_END_HYPHEN_ERROR_MESSAGE = "{PropertyName} cannot start or end with a hyphen.";
        public const string CONSECUTIVE_HYPHENS_ERROR_MESSAGE = "{PropertyName} cannot contain two consecutive hyphens.";
    }
}
